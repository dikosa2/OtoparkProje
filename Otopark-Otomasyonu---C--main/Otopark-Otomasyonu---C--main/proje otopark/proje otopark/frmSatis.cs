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
    public partial class frmSatis : Form
    {
        public frmSatis()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-ELH1N6VM;Initial Catalog=araç_otopark;Integrated Security=True");
        DataSet daset = new DataSet();
        private void frmSatis_Load(object sender, EventArgs e)
        {
            satislariListele();
            Hesapla();
        }

        private void Hesapla()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(tutar) from satis", baglanti);
            label1.Text = "TOPLAM TUTAR = "+komut.ExecuteScalar()+"TL";
            baglanti.Close();
        }

        private void satislariListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from satis ", baglanti);
            adtr.Fill(daset, "satis");
            dataGridView1.DataSource = daset.Tables["satis"];
            baglanti.Close();
        }
    }
}
