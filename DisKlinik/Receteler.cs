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
using DisKlinik;
using static System.Net.WebRequestMethods;

namespace DisKlinik
{
    public partial class Receteler : Form
    {
        public Receteler()
        {
            InitializeComponent();
        }

        //using sql ve data
        ConnectionString MyConnection = new ConnectionString();
        
        //otomatik olarak hasta ve tedavi türleri receteler sayfasına gelecek.
        private void FillHasta()
        {
            SqlConnection baglanti = MyConnection.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT HAd FROM HastaTBL", baglanti);
            SqlDataReader rdr;
            rdr = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("HAd", typeof(string));
            dt.Load(rdr);
            HasAdCB.ValueMember = "HAd";
            HasAdCB.DataSource = dt;
            baglanti.Close();

        }

        //ad soyada göre tedavisi görülen hastaların tedavi türleri receteler sayfasına gelecek. randebu sayfasından alacağız bilgileri.

        private void FillTedavi()
        {
            SqlConnection baglanti = MyConnection.GetCon();
            baglanti.Open();
            //where koşullarını bağlarken virgül değil AND veya OR kullanılır.

            string query = "SELECT * FROM RandevuTBL WHERE Hasta = '" + HasAdCB.SelectedValue.ToString() + "' AND Tedavi = '" + TedaviTB.Text + "'";
            SqlCommand komut = new SqlCommand(query, baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            sda.Fill(dt);

            foreach (DataRow dr in dt.Rows) 
            {
                //randevutbl de ki  tedavi
                TedaviTB.Text = dr["Tedavi"].ToString();
            }
            baglanti.Close();
        }
        //Ödenecek Tutar
        private void FillPrice()
        {
            SqlConnection baglanti = MyConnection.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TedaviTBL WHERE TedAd = '"+TedaviTB.Text+"' ", baglanti);
            
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                //randevutbl de ki  tedavi
                tutarTB.Text = dr["TedUcret"].ToString();
            }
            baglanti.Close();
        }

        void Uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from ReceteTBL";
            DataSet ds = Hs.ShowHasta(query);
            ReceteDGV.DataSource = ds.Tables[0];
        }
        void Filter()
        {
            //araTB isimlendi. filtre komutu uygun veri tabanına göre yazıldı.
            //eventdan çift tık, fonksioynu çağır.
            Hastalar Hs = new Hastalar();
            string query = "select * from ReceteTBL where HstAd like '%"+AraTB.Text+"%'";
            DataSet ds = Hs.ShowHasta(query);
            ReceteDGV.DataSource = ds.Tables[0];
        }
        void Reset()
        {
            HasAdCB.SelectedIndex = -1;
            TedaviTB.Text = null;
            tutarTB.Text = null;
            ilacTB.Text = null;
            miktarTB.Text = "";

        }

        private void Receteler_Load(object sender, EventArgs e)
        {
            FillHasta();
            Uyeler();
            Reset();
        }

        //ad soyad kombobox seçilir event den.
        //SelectionChanged: her seçim değiştiğinde çalışır
        // SelectionChangeCommitted:kullanıcı gerçekten bir seçim yaptığında çalışır

        private void HasAdCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillTedavi();
        }


        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            Main mn = new Main();
            mn.Show();
            this.Hide();
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            Patient hs = new Patient();
            hs.Show();
            this.Hide();
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            Randevu rnd = new Randevu();
            rnd.Show();
            this.Hide();
        }

        private void guna2GradientButton10_Click(object sender, EventArgs e)
        {
            Tedavi td = new Tedavi();
            td.Show();
            this.Hide();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TedaviTB_TextChanged(object sender, EventArgs e)
        {
            FillPrice();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO ReceteTBL VALUES ('" + HasAdCB.SelectedValue.ToString() + "', '" + TedaviTB.Text + "', '" + tutarTB.Text + "' , '"+ilacTB.Text+"','"+miktarTB.Text+"')";
            Hastalar hs = new Hastalar();

            try
            {
                hs.HastaEkle(query);
                MessageBox.Show("Reçete Eklendi.");
                Uyeler();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void ReceteDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        int key = 0;
        private void ReceteDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HasAdCB.Text = ReceteDGV.SelectedRows[0].Cells[1].Value.ToString();
            TedaviTB.Text = ReceteDGV.SelectedRows[0].Cells[2].Value.ToString();
            tutarTB.Text = ReceteDGV.SelectedRows[0].Cells[3].Value.ToString();
            ilacTB.Text = ReceteDGV.SelectedRows[0].Cells[4].Value.ToString();
            miktarTB.Text = ReceteDGV.SelectedRows[0].Cells[5].Value.ToString();


            if (TedaviTB.Text == "")
            {
                key = 0;
            }
            else
            {
                //dönüşüm
                key = Convert.ToInt32(ReceteDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Reçeteyi Seçiniz");
            }
            else
            {
                try
                {
                    string query = "Delete from ReceteTBL where RecId=" + key + "";
                    Hs.HastaSil(query);
                    MessageBox.Show("Reçete Silindi");
                    Uyeler();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void AraTB_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }
        Bitmap bitmap;
        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            int height = ReceteDGV.Height;
            ReceteDGV.Height = ReceteDGV.RowCount * ReceteDGV.RowTemplate.Height * 2;
            bitmap = new Bitmap(ReceteDGV.Width, ReceteDGV.Height);
            ReceteDGV.DrawToBitmap(bitmap, new Rectangle(0, 10, ReceteDGV.Width, ReceteDGV.Height));
            ReceteDGV.Height = height;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
    }
}
