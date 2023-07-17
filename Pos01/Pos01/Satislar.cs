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

namespace Pos01
{
    public partial class Satislar : Form
    {
        SqlConnection baglanti;
        SqlDataAdapter dataAdapter;
        public Satislar()
        {
            InitializeComponent();
        }
        void DatabaseVeriGetir()
        {
            try
            {
                baglanti = new SqlConnection(Form1.ConnectionStrings);
                baglanti.Open();
                dataAdapter = new SqlDataAdapter("SELECT * FROM Satislar", baglanti);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                Satislar_DataGridW.DataSource = table;
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
        private void Satis_Ekle_Cikis_Btn_Click(object sender, EventArgs e)
        {
            baglanti.Close();
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Satislar_Load(object sender, EventArgs e)
        {
            DatabaseVeriGetir();
        }
    }
}
