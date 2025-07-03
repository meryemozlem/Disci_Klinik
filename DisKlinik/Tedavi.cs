using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace DisKlinik
{
    public partial class Tedavi : Form
    {
        public Tedavi()
        {
            InitializeComponent();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO TedaviTBL VALUES ('"+TedaviAdTB.Text+"', '"+ tutarTB.Text + "', '"+aciklamaTB.Text+"')";
            Hastalar hs = new Hastalar();

            try
            {
                hs.HastaEkle(query);
                MessageBox.Show("Tedavi kaydı eklendi.");
                Uyeler();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        int key = 0;
        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Güncellenecek Tedaviyi Seçiniz");
            }
            else
            {
                try
                {
                    string query = "Update TedaviTBL set TedAd= '" + TedaviAdTB.Text + "', TedUcret='" + tutarTB.Text + "', TedAciklama='" + aciklamaTB.Text + "' where TedId=" + key + ";";
                    Hs.HastaSil(query);
                    MessageBox.Show("Tedavi Kaydı Güncellendi!");
                    Uyeler();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Tedaviyi Seçiniz");
            }
            else
            {
                try
                {
                    string query = "Delete from TedaviTBL where TedId=" + key + "";
                    Hs.HastaSil(query);
                    MessageBox.Show("Tedavi Kaydı Silindi");
                    Uyeler();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        void Uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from TedaviTBL";
            DataSet ds = Hs.ShowHasta(query);
            TedaviDGWiev.DataSource = ds.Tables[0];
        }
        void Reset()
        {
            TedaviAdTB.Text = "";
            tutarTB.Text = "";
            aciklamaTB.Text = null; 
        }

        private void Tedavi_Load(object sender, EventArgs e)
        {
            Uyeler();
            Reset();
        }

        private void TedaviDGWiev_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TedaviAdTB.Text = TedaviDGWiev.SelectedRows[0].Cells[1].Value.ToString();
            tutarTB.Text = TedaviDGWiev.SelectedRows[0].Cells[2].Value.ToString();
            aciklamaTB.Text = TedaviDGWiev.SelectedRows[0].Cells[3].Value.ToString();
           
            if (TedaviAdTB.Text == "")
            {
                key = 0;
            }
            else
            {
                //dönüşüm
                key = Convert.ToInt32(TedaviDGWiev.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
