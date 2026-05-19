using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Diagnostics;
using NAudio.Wave;
using Vosk;
using Newtonsoft.Json.Linq;

namespace Speech_To_Text_Text_To_Speech
{
    public partial class Form1 : Form
    {
        // ── Yazıdan Sese ──────────────────────────────────────────────
        private SpeechSynthesizer synth = new SpeechSynthesizer();

        // ── Sesten Yazıya (Vosk) ──────────────────────────────────────
        private static readonly string MODEL_PATH =
            @"C:\Users\omrkv\source\repos\Speech-To-Text-Text-To-Speech\Speech-To-Text-Text-To-Speech\vosk-model-small-tr-0.3";

        private Model? voskModel = null;
        private VoskRecognizer? recognizer = null;
        private WaveInEvent? waveIn = null;
        private bool dinliyorMu = false;

        public Form1()
        {
            InitializeComponent();

            // Yüklü sesleri sadece Debug'a yaz — UI'da hata gösterme
            try
            {
                foreach (var voice in synth.GetInstalledVoices())
                    Debug.WriteLine($"Yüklü ses: {voice.VoiceInfo.Name} - {voice.VoiceInfo.Culture.Name}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Ses listesi alınamadı: " + ex.Message);
            }

            cmbIslemSecimi.Items.Clear();
            cmbIslemSecimi.Items.Add("Yazıdan Sese");
            cmbIslemSecimi.Items.Add("Sesten Yazıya");
            cmbIslemSecimi.SelectedIndex = 0;

            cmbIslemSecimi.SelectedIndexChanged += cmbIslemSecimi_SelectedIndexChanged;

            Vosk.Vosk.SetLogLevel(-1);
            Task.Run(() => ModelYukle());
        }

        private void cmbIslemSecimi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIslemSecimi.SelectedItem?.ToString() == "Sesten Yazıya")
                btnBaslat.Text = "Dinlemeyi Başlat";
            else
            {
                btnBaslat.Text = "Başlat";
                if (dinliyorMu) DinlemeyiDurdur();
            }
        }

        private void btnBaslat_Click(object sender, EventArgs e)
        {
            string secilen = cmbIslemSecimi.SelectedItem?.ToString() ?? "";

            if (secilen == "Yazıdan Sese")
                YaziOkumaIslemi(txtMetin.Text);
            else if (secilen == "Sesten Yazıya")
            {
                if (!dinliyorMu) DinlemeyiBaslat();
                else DinlemeyiDurdur();
            }
        }

        // ═══════════════════════════════════════
        // YAZIDAN SESE
        // ═══════════════════════════════════════
        private void YaziOkumaIslemi(string metin)
        {
            if (string.IsNullOrWhiteSpace(metin))
            {
                MessageBox.Show("Metin kutusu boş, lütfen bir şeyler yazın!");
                return;
            }

            try
            {
                synth.SetOutputToDefaultAudioDevice();

                // Türkçe ses varsa seç, yoksa varsayılanla devam et — hata verme
                var turkishVoice = synth.GetInstalledVoices()
                    .FirstOrDefault(v => v.VoiceInfo.Culture.Name.StartsWith("tr"));

                if (turkishVoice != null)
                    synth.SelectVoice(turkishVoice.VoiceInfo.Name);
                // Türkçe ses yoksa sessizce varsayılan ses kullanılır

                synth.SpeakAsync(metin);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yazıdan sese hata: " + ex.Message);
            }
        }

        // ═══════════════════════════════════════
        // SESTEN YAZIYA — VOSK
        // ═══════════════════════════════════════
        private void ModelYukle()
        {
            try
            {
                if (!Directory.Exists(MODEL_PATH))
                {
                    MesajGoster($"Model klasörü bulunamadı:\n{MODEL_PATH}");
                    return;
                }

                voskModel = new Model(MODEL_PATH);
                Debug.WriteLine("Vosk modeli başarıyla yüklendi.");
            }
            catch (Exception ex)
            {
                MesajGoster("Model yüklenemedi: " + ex.Message);
            }
        }

        private void DinlemeyiBaslat()
        {
            if (voskModel == null)
            {
                MessageBox.Show("Model henüz yüklenmedi veya bulunamadı.", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                recognizer = new VoskRecognizer(voskModel, 16000f);
                recognizer.SetMaxAlternatives(0);
                recognizer.SetWords(true);

                waveIn = new WaveInEvent
                {
                    WaveFormat = new WaveFormat(16000, 1),
                    BufferMilliseconds = 100
                };

                waveIn.DataAvailable += WaveIn_DataAvailable;
                waveIn.RecordingStopped += WaveIn_RecordingStopped;
                waveIn.StartRecording();

                dinliyorMu = true;
                btnBaslat.Text = "Dinlemeyi Durdur";
                Debug.WriteLine("Dinleme başladı.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dinleme başlatılamadı:\n" + ex.Message, "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DinlemeyiDurdur();
            }
        }

        private void DinlemeyiDurdur()
        {
            try
            {
                if (waveIn != null)
                {
                    waveIn.StopRecording();
                    waveIn.DataAvailable -= WaveIn_DataAvailable;
                    waveIn.RecordingStopped -= WaveIn_RecordingStopped;
                }

                if (recognizer != null)
                {
                    string sonMetin = JsonMetniCikar(recognizer.FinalResult());
                    MetniEkle(sonMetin);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Durdurma hatası: " + ex.Message);
            }
            finally
            {
                waveIn?.Dispose();
                recognizer?.Dispose();
                waveIn = null;
                recognizer = null;
                dinliyorMu = false;
                ButonGuncelle("Dinlemeyi Başlat");
                Debug.WriteLine("Dinleme durduruldu.");
            }
        }

        private void WaveIn_DataAvailable(object? sender, WaveInEventArgs e)
        {
            if (recognizer == null || e.BytesRecorded == 0) return;

            try
            {
                byte[] buffer = new byte[e.BytesRecorded];
                Array.Copy(e.Buffer, buffer, e.BytesRecorded);

                if (recognizer.AcceptWaveform(buffer, buffer.Length))
                {
                    string metin = JsonMetniCikar(recognizer.Result());
                    if (!string.IsNullOrWhiteSpace(metin))
                        MetniEkle(metin);
                }
                else
                {
                    string kismi = JsonKismiCikar(recognizer.PartialResult());
                    if (!string.IsNullOrWhiteSpace(kismi))
                        Debug.WriteLine("Kısmi: " + kismi);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Vosk işleme hatası: " + ex.Message);
            }
        }

        private void WaveIn_RecordingStopped(object? sender, StoppedEventArgs e)
        {
            if (e.Exception != null)
                MesajGoster("Mikrofon kayıt hatası: " + e.Exception.Message);
        }

        // ── JSON yardımcıları ──────────────────
        private static string JsonMetniCikar(string json)
        {
            try { return JObject.Parse(json)["text"]?.ToString()?.Trim() ?? ""; }
            catch { return ""; }
        }

        private static string JsonKismiCikar(string json)
        {
            try { return JObject.Parse(json)["partial"]?.ToString()?.Trim() ?? ""; }
            catch { return ""; }
        }

        // ── Thread-safe UI güncellemeleri ──────
        private void MetniEkle(string metin)
        {
            if (string.IsNullOrWhiteSpace(metin)) return;
            Action ekle = () =>
            {
                txtMetin.Text += (string.IsNullOrEmpty(txtMetin.Text) ? "" : " ") + metin;
                txtMetin.SelectionStart = txtMetin.Text.Length;
                txtMetin.ScrollToCaret();
            };
            if (txtMetin.InvokeRequired) txtMetin.Invoke(ekle);
            else ekle();
        }

        private void ButonGuncelle(string yazi)
        {
            Action guncelle = () => btnBaslat.Text = yazi;
            if (btnBaslat.InvokeRequired) btnBaslat.Invoke(guncelle);
            else guncelle();
        }

        private void MesajGoster(string mesaj)
        {
            Action goster = () => MessageBox.Show(mesaj);
            if (InvokeRequired) Invoke(goster);
            else goster();
        }

        // ── Form kapanırken temizlik ───────────
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (dinliyorMu) DinlemeyiDurdur();
            synth?.Dispose();
            voskModel?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
