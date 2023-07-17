using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos01
{
    public partial class klavye : Form
    {
        Stok_Yönetim Stok_Yönetim = new Stok_Yönetim();
        public klavye()
        {
            InitializeComponent();
        }

        private void buttonDesign19_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (Stok_Yönetim.ActiveControl is TextBox)
            {
                Stok_Yönetim.ActiveControl.Text += btn.Text;
            }
        }
    }
}
