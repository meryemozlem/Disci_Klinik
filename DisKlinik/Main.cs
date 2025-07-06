using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisKlinik
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            //page connection
            Randevu rnd = new Randevu() ;
            rnd.Show();
            this.Hide();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Patient hs= new Patient() ;
            hs.Show();
            this.Hide();

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Receteler rct = new Receteler() ;
            rct.Show();
            this.Hide();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Tedavi td = new Tedavi();
            td.Show();
            this.Hide();

        }
    }
}
