namespace Speech_To_Text_Text_To_Speech
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            cmbIslemSecimi = new ComboBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            txtMetin = new TextBox();
            btnBaslat = new Button();
            SuspendLayout();
            // 
            // cmbIslemSecimi
            // 
            cmbIslemSecimi.FormattingEnabled = true;
            cmbIslemSecimi.Location = new Point(321, 72);
            cmbIslemSecimi.Name = "cmbIslemSecimi";
            cmbIslemSecimi.Size = new Size(440, 28);
            cmbIslemSecimi.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // txtMetin
            // 
            txtMetin.Location = new Point(152, 124);
            txtMetin.Multiline = true;
            txtMetin.Name = "txtMetin";
            txtMetin.Size = new Size(751, 318);
            txtMetin.TabIndex = 2;
            // 
            // btnBaslat
            // 
            btnBaslat.Location = new Point(149, 486);
            btnBaslat.Name = "btnBaslat";
            btnBaslat.Size = new Size(240, 29);
            btnBaslat.TabIndex = 3;
            btnBaslat.Text = "Çalıştır";
            btnBaslat.UseVisualStyleBackColor = true;
            btnBaslat.Click += btnBaslat_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1051, 562);
            Controls.Add(btnBaslat);
            Controls.Add(txtMetin);
            Controls.Add(cmbIslemSecimi);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbIslemSecimi;
        private ContextMenuStrip contextMenuStrip1;
        private TextBox txtMetin;
        private Button btnBaslat;
    }
}
