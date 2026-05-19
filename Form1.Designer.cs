using System.Drawing;
using System.Windows.Forms;

namespace Speech_To_Text_Text_To_Speech
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            cmbIslemSecimi = new ComboBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            txtMetin = new TextBox();
            btnBaslat = new Button();
            lblBaslik = new Label();
            lblDurum = new Label();
            SuspendLayout();

            // 
            // cmbIslemSecimi
            // 
            cmbIslemSecimi.Anchor = AnchorStyles.Top;
            cmbIslemSecimi.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbIslemSecimi.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cmbIslemSecimi.FormattingEnabled = true;
            cmbIslemSecimi.Items.AddRange(new object[] {
            "Yazıdan Sese (Text to Speech)",
            "Sesten Yazıya (Speech to Text)"});
            cmbIslemSecimi.Location = new Point(250, 75);
            cmbIslemSecimi.Name = "cmbIslemSecimi";
            cmbIslemSecimi.Size = new Size(300, 36);
            cmbIslemSecimi.TabIndex = 0;
            cmbIslemSecimi.Cursor = Cursors.Hand;

            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);

            // 
            // txtMetin
            // 
            txtMetin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtMetin.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            txtMetin.Location = new Point(50, 150);
            txtMetin.Multiline = true;
            txtMetin.Name = "txtMetin";
            txtMetin.ScrollBars = ScrollBars.Vertical;
            txtMetin.Size = new Size(700, 300);
            txtMetin.TabIndex = 2;

            // 
            // btnBaslat
            // 
            btnBaslat.Anchor = AnchorStyles.Bottom;
            btnBaslat.BackColor = Color.FromArgb(0, 120, 215);
            btnBaslat.Cursor = Cursors.Hand;
            btnBaslat.FlatAppearance.BorderSize = 0;
            btnBaslat.FlatStyle = FlatStyle.Flat;
            btnBaslat.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnBaslat.ForeColor = Color.White;
            btnBaslat.Location = new Point(300, 475);
            btnBaslat.Name = "btnBaslat";
            btnBaslat.Size = new Size(200, 50);
            btnBaslat.TabIndex = 3;
            btnBaslat.Text = "Çalıştır";
            btnBaslat.UseVisualStyleBackColor = false;
            btnBaslat.Click += btnBaslat_Click;

            // 
            // lblBaslik
            // 
            lblBaslik.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblBaslik.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            lblBaslik.ForeColor = Color.FromArgb(45, 45, 48);
            lblBaslik.Location = new Point(0, 15);
            lblBaslik.Name = "lblBaslik";
            lblBaslik.Size = new Size(800, 45);
            lblBaslik.TabIndex = 4;
            lblBaslik.Text = "Ses & Metin Dönüştürücü";
            lblBaslik.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblDurum
            // 
            lblDurum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblDurum.Font = new Font("Segoe UI", 11F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            lblDurum.ForeColor = Color.Crimson;
            lblDurum.Location = new Point(0, 115);
            lblDurum.Name = "lblDurum";
            lblDurum.Size = new Size(800, 30);
            lblDurum.TabIndex = 5;
            lblDurum.Text = "🔴 Dinleniyor... Lütfen konuşun.";
            lblDurum.TextAlign = ContentAlignment.MiddleCenter;
            lblDurum.Visible = false;

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 244, 248);
            ClientSize = new Size(800, 560);
            Controls.Add(lblDurum);
            Controls.Add(lblBaslik);
            Controls.Add(btnBaslat);
            Controls.Add(txtMetin);
            Controls.Add(cmbIslemSecimi);
            MinimumSize = new Size(600, 450);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ses & Metin Dönüştürücü";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbIslemSecimi;
        private ContextMenuStrip contextMenuStrip1;
        private TextBox txtMetin;
        private Button btnBaslat;
        private Label lblBaslik;
        private Label lblDurum;
    }
}