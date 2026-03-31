using System;
using System.Linq;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Diagnostics; // Debug çıktıları için gerekli

namespace Speech_To_Text_Text_To_Speech
{
    public partial class Form1 : Form
    {
        private SpeechSynthesizer synth = new SpeechSynthesizer();

        public Form1()
        {
            InitializeComponent();

            // Debug: Form başlatıldı
            Debug.WriteLine("Uygulama başlatıldı: Constructor çalışıyor.");

            cmbIslemSecimi.Items.Clear();
            cmbIslemSecimi.Items.Add("Yazıdan Sese");
            cmbIslemSecimi.Items.Add("Sesten Yazıya");
            cmbIslemSecimi.SelectedIndex = 0;
        }

        private void btnBaslat_Click(object sender, EventArgs e)
        {
            // Debug 1: Butona basıldı mı?
            Debug.WriteLine("Butona basıldı! işlem başlıyor");

            if (cmbIslemSecimi.SelectedItem == null)
            {
                Debug.WriteLine("Hata: ComboBox seçili değil!");
                return;
            }

            string secilenIslem = cmbIslemSecimi.SelectedItem.ToString();
            Debug.WriteLine("Seçilen İşlem: " + secilenIslem);

            if (secilenIslem == "Yazıdan Sese")
            {
                YaziOkumaIslemi(txtMetin.Text);
            }
            else
            {
                MessageBox.Show("Sesten yazıya henüz aktif değil.");
            }
        }

        private void YaziOkumaIslemi(string metin)
        {
            // Debug 2: Metin kutusu boş mu?
            if (string.IsNullOrWhiteSpace(metin))
            {
                Debug.WriteLine("Uyarı: Metin kutusu boş.");
                MessageBox.Show("Metin kutusu boş, lütfen bir şeyler yazın!");
                return;
            }

            try
            {
                synth.SetOutputToDefaultAudioDevice();

                var voices = synth.GetInstalledVoices();
                Debug.WriteLine("Yüklü ses sayısı: " + voices.Count);

                var turkishVoice = voices.FirstOrDefault(v => v.VoiceInfo.Culture.Name == "tr-TR");

                if (turkishVoice != null)
                {
                    Debug.WriteLine("Türkçe ses bulundu: " + turkishVoice.VoiceInfo.Name);
                    synth.SelectVoice(turkishVoice.VoiceInfo.Name);
                }
                else
                {
                    Debug.WriteLine("Uyarı: Türkçe ses bulunamadı!");
                    MessageBox.Show("Sistemde Türkçe ses paketi bulunamadı.");
                }

                Debug.WriteLine("Okuma başlatılıyor: " + metin);
                synth.SpeakAsync(metin);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("HATA OLUŞTU: " + ex.Message);
                MessageBox.Show("Hata detayı: " + ex.Message);
            }
        }
    }
}
