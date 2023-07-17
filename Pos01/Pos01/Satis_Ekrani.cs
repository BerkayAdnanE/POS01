using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using ZXing;

namespace Pos01
{
    public partial class Satis_Ekrani : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter dataAdapter;
        FilterInfoCollection Cihazlar;
        VideoCaptureDevice kameram;
        public string Urun_Barkod;
        public string Urun_ID;
        Form1 Form1 = new Form1();

        public Satis_Ekrani()
        {
            InitializeComponent();
        }
        void ToplamFiyat()
        {
            string sqlQuery = "SELECT SUM(Urun_Fiyat) AS toplam_fiyat FROM Satistaki_Urunler";


            decimal toplamFiyat = 0;

            using (SqlConnection connection = new SqlConnection(Form1.ConnectionStrings))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        toplamFiyat = Convert.ToDecimal(result);
                    }
                }
            }

            Console.WriteLine("Toplam Fiyat: " + toplamFiyat);
            //Toplam_Fiyat_label.Text = toplamFiyat.ToString();
            Toplam_Fiyat_Txbx.Text = toplamFiyat.ToString();
        }
        void DatabaseVeriGetir()
        {
            try
            {
                baglanti = new SqlConnection(Form1.ConnectionStrings);
                baglanti.Open();
                dataAdapter = new SqlDataAdapter("SELECT * FROM Satistaki_Urunler", baglanti);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                Satis_Urunler_DataGridW.DataSource = table;
                baglanti.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lütfen Ayarlar Bölümünden ConnectionStringin doğru olup olmadığına bakın." + "\n Hata : " + e);
            }
        }
        void TriggerKapat()
        {
            baglanti.Open();
            SqlCommand TriggerKapat = new SqlCommand("ALTER TABLE Satistaki_Urunler DISABLE TRIGGER ALL", baglanti);
            TriggerKapat.ExecuteNonQuery();
            baglanti.Close();
        }
        void TriggerAc()
        {
            baglanti.Open();
            SqlCommand TriggerAc = new SqlCommand("ALTER TABLE Satistaki_Urunler ENABLE TRIGGER ALL", baglanti);
            TriggerAc.ExecuteNonQuery();
            baglanti.Close();
        }

        void TumDBsil()
        {
            baglanti.Open();
            SqlCommand dbsil = new SqlCommand("Delete From Satistaki_Urunler", baglanti);
            dbsil.ExecuteNonQuery();
            MessageBox.Show("Satış Tamamlandı");
            baglanti.Close();
        }


        void OkunanUrunuGonder()
        {
           
            if (Urun_Barkod_TxtBx!=null)
            {
                try
                {
                    baglanti.Open();
                    string kayitekle = "INSERT INTO Satistaki_Urunler (Urun_ID,Urun_Kategori,Urun_Marka,Urun_Adi,Urun_Adet,Urun_Fiyat,Urun_Barkod) SELECT Urun_ID,Urun_Kategori,Urun_Marka,Urun_Adi,Urun_Adet,Urun_Fiyat,Urun_Barkod FROM Ekli_Urunler WHERE @Urun_Barkod=Urun_Barkod";
                    komut = new SqlCommand(kayitekle, baglanti);
                    komut.Parameters.AddWithValue("@Urun_Barkod", Urun_Barkod_TxtBx.Text);                 
                    komut.ExecuteNonQuery();
                    DatabaseVeriGetir();
                    baglanti.Close();
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Stok Yetersiz");
                    baglanti.Close();
                }
                
            }
            else
            {
                MessageBox.Show("Önce Barkodu Okutunuz.");
            }
        }

        private void Satis_Ekrani_Load(object sender, EventArgs e)
        {
            Cihazlar = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo cihaz in Cihazlar)
            {
                Kamera_Seçim.Items.Add(cihaz.Name);
            }
            Kamera_Seçim.SelectedIndex = 0;
            DatabaseVeriGetir();
            TriggerAc();
            ToplamFiyat();

        }

        private void Urun_Ekleme_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();

            if (kameram != null)
            {
                if (kameram.IsRunning)
                {
                    kameram.Stop();
                }
            }
        }

        private void Barkod_Okuyucu_Baslat_Btn_Click(object sender, EventArgs e)
        {

            if (kameram != null && kameram.IsRunning)
            {
                kameram.SignalToStop();
                kameram.WaitForStop();
                kameram = null;
                Barkod_Okuyucu_Baslat_Btn.Text = "Borkod Okuyucu Başlat";
            }
            else
            {
                kameram = new VideoCaptureDevice(Cihazlar[Kamera_Seçim.SelectedIndex].MonikerString);
                kameram.NewFrame += VideoCaptureDevice_NewFrame;
                kameram.Start();
                Barkod_Okuyucu_Baslat_Btn.Text = "Borkod Okuyucu Durdur";
            }
        }

        private void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap GoruntulenenBarkod = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader okuyucu = new BarcodeReader();
            var sonuc = okuyucu.Decode(GoruntulenenBarkod);

            if (sonuc != null)
            {
                Urun_Barkod_TxtBx.Invoke(new MethodInvoker(delegate ()
                {
                    Urun_Barkod_TxtBx.Text = sonuc.ToString();
                    DatabaseVeriGetir();
                }
                ));
            }
            Barkod_Okuyucu_Kamera.Image = GoruntulenenBarkod;
        }

        private int lastGeneratedID = 100;

        private int GenerateUniqueID()
        {
            lastGeneratedID++;
            return lastGeneratedID;
        }

        private void Satis_Tamamla_Btn_Click(object sender, EventArgs e)
        {
            TriggerKapat();
            try
            {
                string kayitekle = "INSERT INTO Satislar (Genel_Sepet_Numara,Sepet_Numara,Urun_ID,Urun_Kategori,Urun_Marka,Urun_Adi,Urun_Adet,Urun_Fiyat,Urun_Barkod) SELECT @Genel_Sepet_Numara,Sepet_Numara,Urun_ID,Urun_Kategori,Urun_Marka,Urun_Adi,Urun_Adet,Urun_Fiyat,Urun_Barkod FROM Satistaki_Urunler";
                komut = new SqlCommand(kayitekle, baglanti);
                komut.Parameters.AddWithValue("@Genel_Sepet_Numara", GenerateUniqueID());
                baglanti.Open();
                komut.ExecuteNonQuery();
                DatabaseVeriGetir();
                baglanti.Close();
            }
            catch (System.Data.SqlClient.SqlException)
            {

                MessageBox.Show("Stok Yetersiz");
            }
            TumDBsil();
            DatabaseVeriGetir();
        }

        private void Satis_Ekle_Cikis_Btn_Click(object sender, EventArgs e)
        {
            if (kameram != null)
            {
                if (kameram.IsRunning)
                {
                    kameram.Stop();
                }
            }
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Urun_Ekle_Bttn_Click(object sender, EventArgs e)
        {
            OkunanUrunuGonder();
            DatabaseVeriGetir();
            ToplamFiyat();
            DatabaseVeriGetir();
        }

        private void Satis_İptal_Bttn_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand VeriSil = new SqlCommand("Delete from Satistaki_Urunler where Sepet_Numara=@Sepet_Nuamra", baglanti);
            VeriSil.Parameters.AddWithValue("@Sepet_Nuamra", Siparis_Numara_TxtB.Text);
            VeriSil.ExecuteNonQuery();
            baglanti.Close();
            DatabaseVeriGetir();
            MessageBox.Show("Veri silindi");
        }

        private void Satis_Urunler_DataGridW_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Siparis_Numara_TxtB.Text = Satis_Urunler_DataGridW.CurrentRow.Cells[1].Value.ToString();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            const int maxrows = 4;
            const int maxcols = 3;

            var chars = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '0', '-' };

            var row = (e.Y * maxrows) / this.NumPad.Height;
            var col = (e.X * maxcols) / this.NumPad.Width;

            var scancode = row * maxcols + col;

            if (this.ActiveControl is TextBox)
            {
                this.ActiveControl.Text += chars[scancode];
            }
        }

        private void Toplam_fiyata_ekle_Click(object sender, EventArgs e)
        {
            Double sonuc = 0;
            int Fiyat_Ekle = Convert.ToInt32(Toplama_Cıkarma_Tbx.Text);
            sonuc = Convert.ToDouble(Toplam_Fiyat_Txbx.Text);
            sonuc = sonuc + Fiyat_Ekle;
            Toplam_Fiyat_Txbx.Text = sonuc.ToString();
        }

        private void Toplam_Fiyat_Cikar_Click(object sender, EventArgs e)
        {
            Double sonuc = 0;
            int Fiyat_Ekle = Convert.ToInt32(Toplama_Cıkarma_Tbx.Text);
            sonuc = Convert.ToDouble(Toplam_Fiyat_Txbx.Text);
            sonuc = sonuc - Fiyat_Ekle;
            Toplam_Fiyat_Txbx.Text = sonuc.ToString();
           
        }
        private void Sil_Txbx_Click(object sender, EventArgs e)
        {
            if (Toplama_Cıkarma_Tbx.Text.Length > 0)
            {
                Toplama_Cıkarma_Tbx.Text = Toplama_Cıkarma_Tbx.Text.Substring(0, Toplama_Cıkarma_Tbx.Text.Length - 1);
            }
        }

        private void Tumunu_Sil_Click(object sender, EventArgs e)
        {
            Toplama_Cıkarma_Tbx.Text = "";
        }
       
    }
}