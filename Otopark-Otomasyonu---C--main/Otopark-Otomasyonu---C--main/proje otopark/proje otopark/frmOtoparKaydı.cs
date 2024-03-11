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

namespace proje_otopark
{
    public partial class frmOtoparKaydı : Form
    {
        public frmOtoparKaydı()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-ELH1N6VM;Initial Catalog=araç_otopark;Integrated Security=True");
        private void frmOtoparKaydı_Load(object sender, EventArgs e)
        {
            BoşAraçlar();
            MARKA();
           
        }

        private void MARKA()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka from markabilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboMarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void BoşAraçlar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from araçdurumu WHERE durumu='BOŞ'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboParkYeri.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into araç_otopark_kaydı(tc,ad,soyad,telefon,email,plaka,marka,seri,renk,parkyeri,tarih) values(@tc,@ad,@soyad,@telefon,@email,@plaka,@marka,@seri,@renk,@parkyeri,@tarih)", baglanti);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@email", txtEmail.Text);
            komut.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut.Parameters.AddWithValue("@marka", comboMarka.Text);
            komut.Parameters.AddWithValue("@seri", comboSeri.Text);
            komut.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut.Parameters.AddWithValue("@parkyeri", comboParkYeri.Text);
            komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
            
        
            komut.ExecuteNonQuery();


            SqlCommand komut2 = new SqlCommand("UPDATE araçdurumu SET durumu = 'DOLU' WHERE parkyeri = @parkyeri", baglanti);
            komut2.Parameters.AddWithValue("@parkyeri", comboParkYeri.SelectedItem.ToString());
            komut2.ExecuteNonQuery();
            baglanti.Close() ;
            MessageBox.Show("Araç kaydı oluşturuldu","kayıt");
            
            BoşAraçlar();
            
            MARKA();
            comboSeri.Items.Clear();
            comboParkYeri.Items.Clear();
            comboMarka.Items.Clear();



            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Text = "";
                }
            }

            
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Text = "";
                }
            }

            foreach (Control item in groupBox1.Controls)
            {
                if (item is ComboBox)
                {
                    ((ComboBox)item).SelectedIndex = -1;
                }
            }


        }

        private void comboParkYeri_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtAd_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboSeri.Items.Clear() ;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka,seri from seribilgileri WHERE marka='"+comboMarka.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboSeri.Items.Add(read["seri"].ToString());
            }
            baglanti.Close();

        }

        private void txtPlaka_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmMarka marka = new frmMarka();
            marka.ShowDialog();

        }

        private void btnSeri_Click(object sender, EventArgs e)
        {
            frmSeri seri = new frmSeri();
            seri.ShowDialog();
        }
    }
}
