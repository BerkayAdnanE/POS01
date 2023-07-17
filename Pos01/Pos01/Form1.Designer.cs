
namespace Pos01
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Urun_Ekle_Sayfa_Gecis_Btn = new WindowsFormsAppDeneme.Desgin.ButtonDesign();
            this.Ayarlar_Sayfa_Gecis_Btn = new WindowsFormsAppDeneme.Desgin.ButtonDesign();
            this.Urun_Satis_Sayfa_Gecis_Btn = new WindowsFormsAppDeneme.Desgin.ButtonDesign();
            this.Stok_Yonetimi_Form_Gecis_Btn = new WindowsFormsAppDeneme.Desgin.ButtonDesign();
            this.Satislar = new WindowsFormsAppDeneme.Desgin.ButtonDesign();
            this.SuspendLayout();
            // 
            // Urun_Ekle_Sayfa_Gecis_Btn
            // 
            this.Urun_Ekle_Sayfa_Gecis_Btn.BackColor = System.Drawing.Color.Blue;
            this.Urun_Ekle_Sayfa_Gecis_Btn.FlatAppearance.BorderSize = 0;
            this.Urun_Ekle_Sayfa_Gecis_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Urun_Ekle_Sayfa_Gecis_Btn.Font = new System.Drawing.Font("Noto Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Urun_Ekle_Sayfa_Gecis_Btn.ForeColor = System.Drawing.Color.White;
            this.Urun_Ekle_Sayfa_Gecis_Btn.Location = new System.Drawing.Point(13, 497);
            this.Urun_Ekle_Sayfa_Gecis_Btn.Margin = new System.Windows.Forms.Padding(4);
            this.Urun_Ekle_Sayfa_Gecis_Btn.Name = "Urun_Ekle_Sayfa_Gecis_Btn";
            this.Urun_Ekle_Sayfa_Gecis_Btn.Size = new System.Drawing.Size(220, 120);
            this.Urun_Ekle_Sayfa_Gecis_Btn.TabIndex = 0;
            this.Urun_Ekle_Sayfa_Gecis_Btn.Text = "Ürün Ekle";
            this.Urun_Ekle_Sayfa_Gecis_Btn.UseVisualStyleBackColor = false;
            this.Urun_Ekle_Sayfa_Gecis_Btn.Click += new System.EventHandler(this.Urun_Ekle_Sayfa_Gecis_Btn_Click);
            // 
            // Ayarlar_Sayfa_Gecis_Btn
            // 
            this.Ayarlar_Sayfa_Gecis_Btn.BackColor = System.Drawing.Color.DodgerBlue;
            this.Ayarlar_Sayfa_Gecis_Btn.FlatAppearance.BorderSize = 0;
            this.Ayarlar_Sayfa_Gecis_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Ayarlar_Sayfa_Gecis_Btn.Font = new System.Drawing.Font("Noto Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Ayarlar_Sayfa_Gecis_Btn.ForeColor = System.Drawing.Color.White;
            this.Ayarlar_Sayfa_Gecis_Btn.Location = new System.Drawing.Point(13, 638);
            this.Ayarlar_Sayfa_Gecis_Btn.Margin = new System.Windows.Forms.Padding(4);
            this.Ayarlar_Sayfa_Gecis_Btn.Name = "Ayarlar_Sayfa_Gecis_Btn";
            this.Ayarlar_Sayfa_Gecis_Btn.Size = new System.Drawing.Size(220, 120);
            this.Ayarlar_Sayfa_Gecis_Btn.TabIndex = 3;
            this.Ayarlar_Sayfa_Gecis_Btn.Text = "Ayarlar";
            this.Ayarlar_Sayfa_Gecis_Btn.UseVisualStyleBackColor = false;
            this.Ayarlar_Sayfa_Gecis_Btn.Click += new System.EventHandler(this.Ayarlar_Sayfa_Gecis_Btn_Click);
            // 
            // Urun_Satis_Sayfa_Gecis_Btn
            // 
            this.Urun_Satis_Sayfa_Gecis_Btn.BackColor = System.Drawing.Color.Blue;
            this.Urun_Satis_Sayfa_Gecis_Btn.FlatAppearance.BorderSize = 0;
            this.Urun_Satis_Sayfa_Gecis_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Urun_Satis_Sayfa_Gecis_Btn.Font = new System.Drawing.Font("Noto Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Urun_Satis_Sayfa_Gecis_Btn.ForeColor = System.Drawing.Color.White;
            this.Urun_Satis_Sayfa_Gecis_Btn.Location = new System.Drawing.Point(13, 214);
            this.Urun_Satis_Sayfa_Gecis_Btn.Margin = new System.Windows.Forms.Padding(4);
            this.Urun_Satis_Sayfa_Gecis_Btn.Name = "Urun_Satis_Sayfa_Gecis_Btn";
            this.Urun_Satis_Sayfa_Gecis_Btn.Size = new System.Drawing.Size(220, 120);
            this.Urun_Satis_Sayfa_Gecis_Btn.TabIndex = 4;
            this.Urun_Satis_Sayfa_Gecis_Btn.Text = "Satış Ekranı";
            this.Urun_Satis_Sayfa_Gecis_Btn.UseVisualStyleBackColor = false;
            this.Urun_Satis_Sayfa_Gecis_Btn.Click += new System.EventHandler(this.Urun_Satis_Sayfa_Gecis_Btn_Click);
            // 
            // Stok_Yonetimi_Form_Gecis_Btn
            // 
            this.Stok_Yonetimi_Form_Gecis_Btn.BackColor = System.Drawing.Color.DodgerBlue;
            this.Stok_Yonetimi_Form_Gecis_Btn.FlatAppearance.BorderSize = 0;
            this.Stok_Yonetimi_Form_Gecis_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Stok_Yonetimi_Form_Gecis_Btn.Font = new System.Drawing.Font("Noto Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Stok_Yonetimi_Form_Gecis_Btn.ForeColor = System.Drawing.Color.White;
            this.Stok_Yonetimi_Form_Gecis_Btn.Location = new System.Drawing.Point(13, 356);
            this.Stok_Yonetimi_Form_Gecis_Btn.Margin = new System.Windows.Forms.Padding(4);
            this.Stok_Yonetimi_Form_Gecis_Btn.Name = "Stok_Yonetimi_Form_Gecis_Btn";
            this.Stok_Yonetimi_Form_Gecis_Btn.Size = new System.Drawing.Size(220, 120);
            this.Stok_Yonetimi_Form_Gecis_Btn.TabIndex = 5;
            this.Stok_Yonetimi_Form_Gecis_Btn.Text = "Stok Yönetimi";
            this.Stok_Yonetimi_Form_Gecis_Btn.UseVisualStyleBackColor = false;
            this.Stok_Yonetimi_Form_Gecis_Btn.Click += new System.EventHandler(this.Stok_Yonetimi_Form_Gecis_Btn_Click);
            // 
            // Satislar
            // 
            this.Satislar.BackColor = System.Drawing.Color.Blue;
            this.Satislar.FlatAppearance.BorderSize = 0;
            this.Satislar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Satislar.Font = new System.Drawing.Font("Noto Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Satislar.ForeColor = System.Drawing.Color.White;
            this.Satislar.Location = new System.Drawing.Point(13, 67);
            this.Satislar.Margin = new System.Windows.Forms.Padding(4);
            this.Satislar.Name = "Satislar";
            this.Satislar.Size = new System.Drawing.Size(220, 120);
            this.Satislar.TabIndex = 6;
            this.Satislar.Text = "Satışlar";
            this.Satislar.UseVisualStyleBackColor = false;
            this.Satislar.Click += new System.EventHandler(this.Satislar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1653, 774);
            this.Controls.Add(this.Satislar);
            this.Controls.Add(this.Stok_Yonetimi_Form_Gecis_Btn);
            this.Controls.Add(this.Urun_Satis_Sayfa_Gecis_Btn);
            this.Controls.Add(this.Ayarlar_Sayfa_Gecis_Btn);
            this.Controls.Add(this.Urun_Ekle_Sayfa_Gecis_Btn);
            this.Font = new System.Drawing.Font("Noto Mono", 9F, System.Drawing.FontStyle.Bold);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private WindowsFormsAppDeneme.Desgin.ButtonDesign Urun_Ekle_Sayfa_Gecis_Btn;
        private WindowsFormsAppDeneme.Desgin.ButtonDesign Ayarlar_Sayfa_Gecis_Btn;
        private WindowsFormsAppDeneme.Desgin.ButtonDesign Urun_Satis_Sayfa_Gecis_Btn;
        private WindowsFormsAppDeneme.Desgin.ButtonDesign Stok_Yonetimi_Form_Gecis_Btn;
        private WindowsFormsAppDeneme.Desgin.ButtonDesign Satislar;
    }
}

