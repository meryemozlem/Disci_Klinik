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

namespace DisKlinik
{
    public partial class Patient : Form
    {
        public Patient()
        {
            InitializeComponent();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO HastaTBL (HAd, HTel, HAdres, HDTarih, HCinsiyet, HAlerji) " +
                           "VALUES (@adSoyad, @tel, @adres, @dogTar, @cinsiyet, @alerji)";

            SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\mozay\\Documents\\DentalDb.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand(query, baglanti);

            cmd.Parameters.AddWithValue("@adSoyad", HAdSoyadtb.Text);
            cmd.Parameters.AddWithValue("@tel", HastaTeltb.Text);
            cmd.Parameters.AddWithValue("@adres", HAdres.Text);
            cmd.Parameters.AddWithValue("@dogTar", HDogTar.Text);
            cmd.Parameters.AddWithValue("@cinsiyet", HCinsiyetCB.SelectedItem?.ToString() ?? "");
            cmd.Parameters.AddWithValue("@alerji", HAlerji.Text);

            try
            {
                baglanti.Open();
                cmd.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Hasta kaydı eklendi.");
                Uyeler();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
            }
        }



        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void Uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from HastaTBL";
            DataSet ds = Hs.ShowHasta(query);
            HastaDGWiev.DataSource = ds.Tables[0];
        }
        void Filter() 
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from HastaTBL where HAd like '%" + guna2TextBox6.Text + "%'";
            DataSet ds = Hs.ShowHasta(query);
            HastaDGWiev.DataSource = ds.Tables[0];
        }
        void Reset() 
        {
            HTCtb.Text = "";
            HAdSoyadtb.Text = "";
            HastaTeltb.Text = "";
            HAdres.Text = "";
            HDogTar.Text = "";
            HCinsiyetCB.SelectedItem = "";
            //null
            HAlerji.Text = "";
     
        }

        private void Patient_Load(object sender, EventArgs e)
        {
            Uyeler();
            Reset();
        }

        int key=0;
        private void HastaDGWiev_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HTCtb.Text = HastaDGWiev.SelectedRows[0].Cells[1].Value.ToString();
            HAdSoyadtb.Text = HastaDGWiev.SelectedRows[0].Cells[2].Value.ToString();
            HastaTeltb.Text = HastaDGWiev.SelectedRows[0].Cells[3].Value.ToString();
            HAdres.Text = HastaDGWiev.SelectedRows[0].Cells[4].Value.ToString();
            HDogTar.Text = HastaDGWiev.SelectedRows[0].Cells[5].Value.ToString();
            HCinsiyetCB.SelectedItem = HastaDGWiev.SelectedRows[0].Cells[6].Value.ToString();
            HAlerji.Text = HastaDGWiev.SelectedRows[0].Cells[7].Value.ToString();

            if (HAdSoyadtb.Text == "") 
            {
                key = 0;
            }
            else
            {
                //dönüşüm
                key = Convert.ToInt32(HastaDGWiev.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Hastayı Seçiniz");
            }
            else 
            {
                try
                {
                    string query = "Delete from HastaTBL where HId=" + key + "";
                    Hs.HastaSil(query);
                    MessageBox.Show("Hasta Kaydı Silindi");
                    Uyeler();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
              
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Güncellenecek Hastayı Seçiniz");
            }
            else
            {
                try
                {
                    string query = "Update HastaTBL set HAd= '"+HAdSoyadtb.Text+"', HTel='"+HastaTeltb.Text+"', HAdres='"+HAdres.Text+"', HDTarih='"+HDogTar.Text+"',HCinsiyet='"+HCinsiyetCB.SelectedItem.ToString()+"', HAlerji= '"+HAlerji.Text+"' where HId=" + key + ";";
                    Hs.HastaSil(query);
                    MessageBox.Show("Hasta Kaydı Güncellendi!");
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

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            Randevu rnd = new Randevu();
            rnd.Show();
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
