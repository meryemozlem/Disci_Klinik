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
        }


        void Uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from RandevuTBL";
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

            string query = "INSERT INTO RandevuTBL VALUES ('" + RdvTCtb.Text + "', '" + RdvAdCb.SelectedValue.ToString() + "', '" + RdTelTB.Text + "', '" + RdTedTurCB.SelectedValue.ToString() + "', '" + RdTarih.Value.Date + "','" + RdSaatCB.Text + "')";
            Hastalar hs = new Hastalar();

            try
            {
                hs.HastaEkle(query);
                MessageBox.Show("Randevu oluşturuldu");
                Uyeler();
                //Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
