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
using System.Data.Common;

namespace proje_otopark
{
    public partial class frmAraçOtoparkYerleri : Form
    {
        public frmAraçOtoparkYerleri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-ELH1N6VM;Initial Catalog=araç_otopark;Integrated Security=True");

        private void DoluParkYerleri()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                SqlCommand komut = new SqlCommand("SELECT * FROM araçdurumu", baglanti);
                SqlDataReader read = komut.ExecuteReader();

                while (read.Read())
                {
                    string parkYeri = read["parkyeri"].ToString();
                    string durumu = read["durumu"].ToString();

                    foreach (Control item in this.Controls)
                    {
                        if (item is Button && item.Name == parkYeri)
                        {
                            if (durumu == "DOLU")
                            {
                                item.BackColor = Color.Pink; // Set color for occupied parking spaces
                                item.Text = read["plaka"].ToString(); // Display license plate information
                            }
                            else
                            {
                                // Set a default color for empty parking spaces
                                item.BackColor = Color.Green; // You can choose a different color
                                item.Text = "Boş";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Bağlantıyı sadece kapat, eğer zaten kapalı değilse
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }



        
        private void frmAraçOtoparkYerleri_Load(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                SqlCommand komut = new SqlCommand("SELECT * FROM araçdurumu", baglanti);
                SqlDataReader read = komut.ExecuteReader();

                while (read.Read())
                {
                    string ParkYeri = read["parkyeri"].ToString();
                    string durumu = read["durumu"].ToString();

                    foreach (Control item in this.Controls)
                    {
                        if (item is Button && item.Name == ParkYeri)
                        {
                            Console.WriteLine($"Park Yeri: {ParkYeri}, Durumu: {durumu}");

                            if (durumu == "DOLU")
                            {
                                item.BackColor = Color.Pink; 
                                item.Text = read["plaka"].ToString(); 
                            }
                            else
                            {
                                
                                item.BackColor = Color.Green; 
                                item.Text = "Boş";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }



        private void BoşParkYerleri()
        {
            int sayac = 1;

            foreach (Control item in Controls)
            {
                if (item is Button)
                {
                    item.Text = "p-"+sayac;
                    item.Name = "p-"+sayac;
                    sayac++;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
