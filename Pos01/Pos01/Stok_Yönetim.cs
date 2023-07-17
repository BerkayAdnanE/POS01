using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Pos01
{
    public partial class Stok_Yönetim : Form
    {
        SqlConnection baglanti;
        SqlDataAdapter dataAdapter;
        Form1 Form1 = new Form1();
        public Stok_Yönetim()
        {       
            InitializeComponent();
        }
        void AraVeriGetir()
        {
            try
            {
                DataTable table = new DataTable();
                baglanti.Open();
                SqlDataAdapter komut = new SqlDataAdapter("Select * From Stok_Urunler where Urun_Adi like '%" + Arama_TxtBx.Text + "%' or Urun_Kategori like '%" + Arama_TxtBx.Text + "%' or Urun_Marka like '%" + Arama_TxtBx.Text + "%'", baglanti);
                komut.Fill(table);
                Stok_Yönetimi_DataGridW.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            baglanti.Close();
        }
        void DatabaseVeriGetir()
        {
            try
            {
                baglanti = new SqlConnection(Form1.ConnectionStrings);
                baglanti.Open();
                dataAdapter = new SqlDataAdapter("SELECT * FROM Stok_Urunler", baglanti);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                Stok_Yönetimi_DataGridW.DataSource = table;
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
        private void Stok_Yönetim_Load(object sender, EventArgs e)
        {
            DatabaseVeriGetir();
        }
        void TümDatabaseSil()
        {
            baglanti.Open();
            SqlCommand VeriSil = new SqlCommand("Delete from Stok_Urunler", baglanti);
            VeriSil.ExecuteNonQuery();
            baglanti.Close();
            DatabaseVeriGetir();
            MessageBox.Show("Tüm Veriler silindi");
        }
        private void buttonDesign1_Click(object sender, EventArgs e)
        {
            TümDatabaseSil();
        }

        private void Urun_Guncelle_Btn_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand VeriGuncelle = new SqlCommand("update Stok_Urunler set Urun_Kategori=@Urun_Kategori,Urun_Marka=@Urun_Marka,Urun_Adi=@Urun_Adi,Urun_Adet=@Urun_Adet,Urun_Fiyat=@Urun_Fiyat,Urun_Barkod=@Urun_Barkod where Urun_ID=@Urun_ID", baglanti);
            VeriGuncelle.Parameters.AddWithValue("@Urun_ID", Urun_ID_TxtBx.Text);
            VeriGuncelle.Parameters.AddWithValue("@Urun_Kategori", Urun_Kategori_TxtBx.Text);
            VeriGuncelle.Parameters.AddWithValue("@Urun_Marka", Urun_Marka_TxtBx.Text);
            VeriGuncelle.Parameters.AddWithValue("@Urun_Adi", Urun_Adi_TxtBx.Text);
            VeriGuncelle.Parameters.AddWithValue("@Urun_Adet", Urun_Adet_TxtBx.Text);
            VeriGuncelle.Parameters.AddWithValue("@Urun_Fiyat", Urun_Fiyat_TxtBx.Text);
            VeriGuncelle.Parameters.AddWithValue("@Urun_Barkod", Urun_Barkod_TxtBx.Text);
            VeriGuncelle.ExecuteNonQuery();
            baglanti.Close();
            DatabaseVeriGetir();
            MessageBox.Show(Urun_ID_TxtBx.Text + " Nolu Ürün Güncellendi");
        }

        private void Urun_Sil_Btn_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand VeriSil = new SqlCommand("Delete from Stok_Urunler where Urun_ID=@Urun_ID", baglanti);
            VeriSil.Parameters.AddWithValue("@Urun_ID", Urun_ID_TxtBx.Text);
            VeriSil.ExecuteNonQuery();
            baglanti.Close();
            DatabaseVeriGetir();
            MessageBox.Show("Veri silindi");
            texboxtemizle();
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

        private void Satis_Ekle_Cikis_Btn_Click(object sender, EventArgs e)
        {
            baglanti.Close();
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Arama_Btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Şuan bu özellik yok");
        }

        private void Stok_Yönetimi_DataGridW_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Urun_ID_TxtBx.Text = Stok_Yönetimi_DataGridW.CurrentRow.Cells[0].Value.ToString();
            Urun_Kategori_TxtBx.Text = Stok_Yönetimi_DataGridW.CurrentRow.Cells[1].Value.ToString();
            Urun_Marka_TxtBx.Text = Stok_Yönetimi_DataGridW.CurrentRow.Cells[2].Value.ToString();
            Urun_Adi_TxtBx.Text = Stok_Yönetimi_DataGridW.CurrentRow.Cells[3].Value.ToString();
            Urun_Adet_TxtBx.Text = Stok_Yönetimi_DataGridW.CurrentRow.Cells[4].Value.ToString();
            Urun_Fiyat_TxtBx.Text = Stok_Yönetimi_DataGridW.CurrentRow.Cells[5].Value.ToString();
            Urun_Barkod_TxtBx.Text = Stok_Yönetimi_DataGridW.CurrentRow.Cells[6].Value.ToString();
        }

        private void Arama_TxtBx_TextChanged(object sender, EventArgs e)
        {
            if (Arama_TxtBx == null)
            {
                DatabaseVeriGetir();
            }
            else
            {
                AraVeriGetir();
            }
        }
        private void buttonDesign2_Click(object sender, EventArgs e)
        {
            klavye klavye = new klavye();
            klavye.Show();
        }
    }
}
