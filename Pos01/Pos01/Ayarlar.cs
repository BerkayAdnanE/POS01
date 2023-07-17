using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using AForge.Video.DirectShow;

namespace Pos01
{
    public partial class Ayarlar : Form
    {
        SqlConnection baglanti;

        public Ayarlar()
        {
            InitializeComponent();
        }
        

        private void Ayarlar_Sayfa_Cıkıs_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Ayarlar_Load(object sender, EventArgs e)
        {

        }

        private void ConnectionStrings_TxtBx_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void Bağlantıyı_Test_Et_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti = new SqlConnection(Form1.ConnectionStrings);
                baglanti.Open();
                MessageBox.Show("Bağlantı Başarılı");
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lütfen Ayarlar Bölümünden ConnectionStringin doğru olup olmadığına bakın." + "\n Hata : " + ex);
            }           
        }
    }
}
