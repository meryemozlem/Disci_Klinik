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
using Guna.UI2.WinForms;

namespace DisKlinik
{
    public partial class Randevu : Form
    {
        public Randevu()
        {
            InitializeComponent();
        }

        ConnectionString MyConnection = new ConnectionString();
        //Hastaları comconx metinleri olarak getireceğiz.
        private void FillHasta()
        { 
            SqlConnection baglanti = MyConnection.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT HAd FROM HastaTBL", baglanti);
            SqlDataReader rdr;
            rdr= komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("HAd", typeof(string));
            dt.Load(rdr);
            RdvAdCb.ValueMember = "HAd";
            RdvAdCb.DataSource = dt;
            baglanti.Close();

        }

        private void FillTedavi()
        {
            SqlConnection baglanti = MyConnection.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT TedAd FROM TedaviTBL", baglanti);
            SqlDataReader rdr;
            rdr = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TedAd", typeof(string));
            dt.Load(rdr);
            RdTedTurCB.ValueMember = "TedAd";
            RdTedTurCB.DataSource = dt;
            baglanti.Close();

        }

        private void Randevu_Load(object sender, EventArgs e)
        {
            FillHasta();
            FillTedavi();
            Uyeler();
            Reset();
        }


        void Uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from RandevuTBL";
            DataSet ds = Hs.ShowHasta(query);
            RandevuDGV.DataSource = ds.Tables[0];
        }

        void Reset()
        {
            RdvTCtb.Text = "";
            RdvAdCb.SelectedIndex = -1;
            RdTarih.Value = DateTime.Now;
            RdSaatCB.SelectedIndex = -1;
            RdTedTurCB.SelectedIndex = -1;
           
        }

        void Filter()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from RandevuTBL where Hasta like '%" + araTB.Text + "%'";
            DataSet ds = Hs.ShowHasta(query);
            RandevuDGV.DataSource = ds.Tables[0];
        }


        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            //combobox = selectedValue  selectedIndex
            //textbox = text
            //RdTarih.Value.Date 
            /*
            SelectedIndex Seçilen öğenin listedeki sırası (int)
            SelectedValue Seçilen öğeye karşılık gelen değeri(object/ id)
            SelectedItem  Seçilen öğenin kendisi(genellikle metin)
            */

            string query = "INSERT INTO RandevuTBL (HstTC, Hasta, RTarih, RSaat, Tedavi)" + 
                "VALUES ('" + RdvTCtb.Text + "', '" + RdvAdCb.SelectedValue.ToString() + "', '" + RdTarih.Text + "', '" + RdSaatCB.Text + "', '" + RdTedTurCB.SelectedValue.ToString() + "')";
            Hastalar hs = new Hastalar();

            try
            {
                hs.HastaEkle(query);
                MessageBox.Show("Randevu oluşturuldu");
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
                MessageBox.Show("Güncellenecek Randevuyu Seçiniz");
            }
            else
            {
                try
                {
                    //TextBox= ad.text
                    //combobox= ad.selectedValue.ToString()
                    //RdSaatCB.Text doğrudan ComboBox'ta görünen metni alır, null olmaz. NULL hatası fırladığında.
                    string query = "Update RandevuTBL set HstTC= '" + RdvTCtb.Text + "', Hasta='" + RdvAdCb.SelectedValue.ToString() + "', RTarih='" + RdTarih.Text + "', RSaat='" + RdSaatCB.Text+ "', Tedavi='" + RdTedTurCB.SelectedValue.ToString() + "' where RanId=" + key + ";";
                    Hs.HastaSil(query);
                    MessageBox.Show("Randevu Kaydı Güncellendi!");
                    Uyeler();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void RandevuDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RdvTCtb.Text = RandevuDGV.SelectedRows[0].Cells[1].Value.ToString();
            RdvAdCb.SelectedValue = RandevuDGV.SelectedRows[0].Cells[2].Value.ToString();
            RdTarih.Text = RandevuDGV.SelectedRows[0].Cells[3].Value.ToString();
            RdSaatCB.Text = RandevuDGV.SelectedRows[0].Cells[4].Value.ToString();
            RdTedTurCB.SelectedValue = RandevuDGV.SelectedRows[0].Cells[5].Value.ToString();

            if (RdvAdCb.SelectedIndex == -1)
            {
                key = 0;
            }
            else
            {
                //dönüşüm
                key = Convert.ToInt32(RandevuDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Randevuyu Seçiniz");
            }
            else
            {
                try
                {
                    string query = "Delete from RandevuTBL where RanId=" + key + "";
                    Hs.HastaSil(query);
                    MessageBox.Show("Randevu Kaydı Silindi");
                    Uyeler();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            Main mn = new Main();
            mn.Show();
            this.Hide();


        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            Patient hs = new Patient();
            hs.Show();
            this.Hide();
        }

        private void guna2GradientButton9_Click(object sender, EventArgs e)
        {
            Tedavi td = new Tedavi();
            td.Show();
            this.Hide();
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            Receteler rct = new Receteler();
            rct.Show();
            this.Hide();
        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }
    }
}
