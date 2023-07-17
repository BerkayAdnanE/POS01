
namespace Pos01
{
    partial class Satislar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Satislar));
            this.Satislar_DataGridW = new System.Windows.Forms.DataGridView();
            this.Satis_Ekle_Cikis_Btn = new WindowsFormsAppDeneme.Desgin.ButtonDesign();
            ((System.ComponentModel.ISupportInitialize)(this.Satislar_DataGridW)).BeginInit();
            this.SuspendLayout();
            // 
            // Satislar_DataGridW
            // 
            this.Satislar_DataGridW.AllowUserToAddRows = false;
            this.Satislar_DataGridW.AllowUserToDeleteRows = false;
            this.Satislar_DataGridW.AllowUserToResizeColumns = false;
            this.Satislar_DataGridW.AllowUserToResizeRows = false;
            this.Satislar_DataGridW.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Satislar_DataGridW.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.Satislar_DataGridW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Satislar_DataGridW.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.Satislar_DataGridW.Location = new System.Drawing.Point(12, 12);
            this.Satislar_DataGridW.Name = "Satislar_DataGridW";
            this.Satislar_DataGridW.RowHeadersWidth = 51;
            this.Satislar_DataGridW.RowTemplate.Height = 24;
            this.Satislar_DataGridW.Size = new System.Drawing.Size(1511, 527);
            this.Satislar_DataGridW.TabIndex = 58;
            // 
            // Satis_Ekle_Cikis_Btn
            // 
            this.Satis_Ekle_Cikis_Btn.BackColor = System.Drawing.Color.Red;
            this.Satis_Ekle_Cikis_Btn.FlatAppearance.BorderSize = 0;
            this.Satis_Ekle_Cikis_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Satis_Ekle_Cikis_Btn.ForeColor = System.Drawing.Color.White;
            this.Satis_Ekle_Cikis_Btn.Location = new System.Drawing.Point(1292, 662);
            this.Satis_Ekle_Cikis_Btn.Name = "Satis_Ekle_Cikis_Btn";
            this.Satis_Ekle_Cikis_Btn.Size = new System.Drawing.Size(214, 105);
            this.Satis_Ekle_Cikis_Btn.TabIndex = 54;
            this.Satis_Ekle_Cikis_Btn.Text = "Çıkış";
            this.Satis_Ekle_Cikis_Btn.UseVisualStyleBackColor = false;
            this.Satis_Ekle_Cikis_Btn.Click += new System.EventHandler(this.Satis_Ekle_Cikis_Btn_Click);
            // 
            // Satislar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1535, 792);
            this.Controls.Add(this.Satislar_DataGridW);
            this.Controls.Add(this.Satis_Ekle_Cikis_Btn);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Satislar";
            this.Text = "Satislar";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Satislar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Satislar_DataGridW)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WindowsFormsAppDeneme.Desgin.ButtonDesign Satis_Ekle_Cikis_Btn;
        private System.Windows.Forms.DataGridView Satislar_DataGridW;
    }
}