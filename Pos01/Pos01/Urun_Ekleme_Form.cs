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
    public partial class Urun_Ekleme_Form : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter dataAdapter; 
        FilterInfoCollection Cihazlar;
        VideoCaptureDevice kameram;
        Form1 Form1 = new Form1();
        Ayarlar ayarlar = new Ayarlar();
        public Urun_Ekleme_Form()
        {
            InitializeComponent();
        }
        void StokYonetimineUrunGotur()
        {
            try
            {
                string kayitekle = "INSERT INTO Stok_Urunler(Urun_ID, Urun_Kategori, Urun_Marka, Urun_Adi, Urun_Adet, Urun_Fiyat, Urun_Barkod) SELECT Ekli_Urunler.Urun_ID,Ekli_Urunler.Urun_Kategori,Ekli_Urunler.Urun_Marka,Ekli_Urunler.Urun_Adi,Ekli_Urunler.Urun_Adet,Ekli_Urunler.Urun_Fiyat,Ekli_Urunler.Urun_Barkod FROM Ekli_Urunler LEFT JOIN Stok_Urunler ON Ekli_Urunler.Urun_ID = Stok_Urunler.Urun_ID WHERE Stok_Urunler.Urun_ID IS NULL;";
                komut = new SqlCommand(kayitekle, baglanti);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                DatabaseVeriGetir();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lütfen Ayarlar Bölümünden ConnectionStringin doğru olup olmadığına bakın."+ "\n Hata : " + e);
            }
        }     
        void DatabaseVeriGetir()
        {
            try
            {
                baglanti = new SqlConnection(Form1.ConnectionStrings);
                baglanti.Open();
                dataAdapter = new SqlDataAdapter("SELECT * FROM Ekli_Urunler", baglanti);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                Urun_Ekleme_DataGridW.DataSource = table;
                baglanti.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Ayarlar Bölümünden ConnectionStringin doğru olup olmadığına bakın.");
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            baglanti.Close();
        }
        private void Urun_Ekleme_Form_Load(object sender, EventArgs e)
        {
            DatabaseVeriGetir();
            Cihazlar = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo cihaz in Cihazlar)
            {
                Kamera_Seçim.Items.Add(cihaz.Name);
            }
            Kamera_Seçim.SelectedIndex = 0;
        }

        private void Urun_Ekle_Sayfa_Cıkıs_Click(object sender, EventArgs e)
        {
            if (kameram != null)
            {
                if (kameram.IsRunning)
                {
                    kameram.Stop();
                }
            }
            baglanti.Close();
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
        private void Urun_Ekleme_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (kameram != null)
            {
                if (kameram.IsRunning)
                {
                    kameram.Stop();
                }
            }
            Application.Exit();
        }

        private void Urun_Kayit_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Urun_Kategori_TxtBx.Text) || string.IsNullOrEmpty(Urun_Marka_TxtBx.Text) || string.IsNullOrEmpty(Urun_Adi_TxtBx.Text) || string.IsNullOrEmpty(Urun_Fiyat_TxtBx.Text) || string.IsNullOrEmpty(Urun_Barkod_TxtBx.Text))
                {

                    MessageBox.Show("Boş veri gönderilemez");
                }
                else
                {
                    string kayitekle = "INSERT INTO Ekli_Urunler(Urun_Kategori,Urun_Marka,Urun_Adi,Urun_Adet,Urun_Fiyat,Urun_Barkod) VALUES (@Urun_Kategori,@Urun_Marka,@Urun_Adi,@Urun_Adet,@Urun_Fiyat,@Urun_Barkod)";
                    komut = new SqlCommand(kayitekle, baglanti);
                    komut.Parameters.AddWithValue("@Urun_Kategori", Urun_Kategori_TxtBx.Text);
                    komut.Parameters.AddWithValue("@Urun_Marka", Urun_Marka_TxtBx.Text);
                    komut.Parameters.AddWithValue("@Urun_Adi", Urun_Adi_TxtBx.Text);
                    komut.Parameters.AddWithValue("@Urun_Adet", 1);
                    komut.Parameters.AddWithValue("@Urun_Fiyat", Urun_Fiyat_TxtBx.Text);
                    komut.Parameters.AddWithValue("@Urun_Barkod", Urun_Barkod_TxtBx.Text);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    StokYonetimineUrunGotur();
                    DatabaseVeriGetir();
                    baglanti.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void Urun_Ekleme_DataGridW_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Urun_Ekleme_DataGridW_CellClick(object sender, DataGridViewCellEventArgs e)
        { 
            Urun_ID_TxtBx.Text = Urun_Ekleme_DataGridW.CurrentRow.Cells[0].Value.ToString();
            Urun_Kategori_TxtBx.Text = Urun_Ekleme_DataGridW.CurrentRow.Cells[1].Value.ToString();
            Urun_Marka_TxtBx.Text = Urun_Ekleme_DataGridW.CurrentRow.Cells[2].Value.ToString();
            Urun_Adi_TxtBx.Text = Urun_Ekleme_DataGridW.CurrentRow.Cells[3].Value.ToString();
            Urun_Adet_TxtBx.Text = Urun_Ekleme_DataGridW.CurrentRow.Cells[4].Value.ToString();
            Urun_Fiyat_TxtBx.Text = Urun_Ekleme_DataGridW.CurrentRow.Cells[5].Value.ToString();
            Urun_Barkod_TxtBx.Text = Urun_Ekleme_DataGridW.CurrentRow.Cells[6].Value.ToString();
        }

        private void Barkod_Okuyucu_Baslat_Btn_Click(object sender, EventArgs e)
        {
            if (kameram != null && kameram.IsRunning)
            {
                kameram.SignalToStop();
                kameram.WaitForStop();
                kameram = null;
                Barkod_Okuyucu_Kamera.BackColor = Color.Black;
            }
            else
            {
                kameram = new VideoCaptureDevice(Cihazlar[Kamera_Seçim.SelectedIndex].MonikerString);
                kameram.NewFrame += VideoCaptureDevice_NewFrame;
                kameram.Start();            
            }         
        } 


        public void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap GoruntulenenBarkod = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader okuyucu = new BarcodeReader();
            var sonuc = okuyucu.Decode(GoruntulenenBarkod);

            if (sonuc != null)
            {
                Urun_Barkod_TxtBx.Invoke(new MethodInvoker(delegate ()
                {
                    Urun_Barkod_TxtBx.Text = sonuc.ToString();
                }
                ));
            }
            Barkod_Okuyucu_Kamera.Image = GoruntulenenBarkod;
        }

        private void Urun_Guncelle_Btn_Click(object sender, EventArgs e)
        {//Urun_Kategori,Urun_Marka,Urun_Adi,Urun_Adet,Urun_Fiyat,Urun_Barkod

            try
            {

                baglanti.Open();
                SqlCommand VeriGuncelleEkli = new SqlCommand("update Ekli_Urunler set Urun_Kategori=@Urun_Kategori,Urun_Marka=@Urun_Marka,Urun_Adi=@Urun_Adi,Urun_Adet=@Urun_Adet,Urun_Fiyat=@Urun_Fiyat,Urun_Barkod=@Urun_Barkod where Urun_ID=@Urun_ID", baglanti);
                VeriGuncelleEkli.Parameters.AddWithValue("@Urun_ID", Urun_ID_TxtBx.Text);
                VeriGuncelleEkli.Parameters.AddWithValue("@Urun_Kategori", Urun_Kategori_TxtBx.Text);
                VeriGuncelleEkli.Parameters.AddWithValue("@Urun_Marka", Urun_Marka_TxtBx.Text);
                VeriGuncelleEkli.Parameters.AddWithValue("@Urun_Adi", Urun_Adi_TxtBx.Text);
                VeriGuncelleEkli.Parameters.AddWithValue("@Urun_Adet", Urun_Adet_TxtBx.Text);
                VeriGuncelleEkli.Parameters.AddWithValue("@Urun_Fiyat", Urun_Fiyat_TxtBx.Text);
                VeriGuncelleEkli.Parameters.AddWithValue("@Urun_Barkod", Urun_Barkod_TxtBx.Text);
                VeriGuncelleEkli.ExecuteNonQuery();
                SqlCommand VeriGuncelleStok = new SqlCommand("update Stok_Urunler set Urun_Kategori=@Urun_Kategori,Urun_Marka=@Urun_Marka,Urun_Adi=@Urun_Adi,Urun_Adet=@Urun_Adet,Urun_Fiyat=@Urun_Fiyat,Urun_Barkod=@Urun_Barkod where Urun_ID=@Urun_ID", baglanti);
                VeriGuncelleStok.Parameters.AddWithValue("@Urun_ID", Urun_ID_TxtBx.Text);
                VeriGuncelleStok.Parameters.AddWithValue("@Urun_Kategori", Urun_Kategori_TxtBx.Text);
                VeriGuncelleStok.Parameters.AddWithValue("@Urun_Marka", Urun_Marka_TxtBx.Text);
                VeriGuncelleStok.Parameters.AddWithValue("@Urun_Adi", Urun_Adi_TxtBx.Text);
                VeriGuncelleStok.Parameters.AddWithValue("@Urun_Adet", Urun_Adet_TxtBx.Text);
                VeriGuncelleStok.Parameters.AddWithValue("@Urun_Fiyat", Urun_Fiyat_TxtBx.Text);
                VeriGuncelleStok.Parameters.AddWithValue("@Urun_Barkod", Urun_Barkod_TxtBx.Text);
                VeriGuncelleStok.ExecuteNonQuery();
                baglanti.Close();
                DatabaseVeriGetir();
                MessageBox.Show(Urun_ID_TxtBx.Text + " Nolu Ürün Güncellendi");
             }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Urun_Sil_Btn_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand VeriSil = new SqlCommand("Delete from Ekli_Urunler where Urun_ID=@Urun_ID", baglanti);
            VeriSil.Parameters.AddWithValue("@Urun_ID", Urun_ID_TxtBx.Text);
            VeriSil.ExecuteNonQuery();
            baglanti.Close();
            DatabaseVeriGetir();
            MessageBox.Show("Veri silindi");
            texboxtemizle();
            baglanti.Close();
        }

        public void texboxtemizle()
        {
            Urun_ID_TxtBx.Text = "";
            Urun_Kategori_TxtBx.Text = "";
            Urun_Marka_TxtBx.Text = "";
            Urun_Adi_TxtBx.Text = "";
            Urun_Adet_TxtBx.Text = "";
            Urun_Fiyat_TxtBx.Text = "";
            Urun_Barkod_TxtBx.Text = "";
        }

       
    }
}
