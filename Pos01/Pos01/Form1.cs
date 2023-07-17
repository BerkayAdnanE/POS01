using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos01
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti;
        public static string ConnectionStrings = ConfigurationManager.ConnectionStrings["sqlim"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                baglanti = new SqlConnection(ConnectionStrings);
                baglanti.Open();
                baglanti.Close();
            }catch (Exception ex)
            {
                MessageBox.Show("Lütfen Ayarlar Bölümünden ConnectionStringin doğru olup olmadığına bakın." + "\n Hata : " + ex);
            }
        }

        private void Urun_Ekle_Sayfa_Gecis_Btn_Click(object sender, EventArgs e)
        {
            Urun_Ekleme_Form Urun_Ekleme_Form = new Urun_Ekleme_Form();
            Urun_Ekleme_Form.Show();
            this.Hide();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Urun_Satis_Sayfa_Gecis_Btn_Click(object sender, EventArgs e)
        {
            Satis_Ekrani Satis_Ekrani = new Satis_Ekrani();
            Satis_Ekrani.Show();
            this.Hide();
        }

        private void Ayarlar_Sayfa_Gecis_Btn_Click(object sender, EventArgs e)
        {
            Ayarlar Ayarlar = new Ayarlar();
            Ayarlar.Show();
            this.Hide();
        }

        private void Stok_Yonetimi_Form_Gecis_Btn_Click(object sender, EventArgs e)
        {
            Stok_Yönetim Stok_Yönetim = new Stok_Yönetim();
            Stok_Yönetim.Show();
            this.Hide();
        }

        private void Satislar_Click(object sender, EventArgs e)
        {
            Satislar Satislar = new Satislar();
            Satislar.Show();
            this.Hide();
        }
    }
}
