using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        public static MySqlConnection baglan;
        public MySqlConnection baglanac()
        {
            baglan = new MySqlConnection();
            baglan.ConnectionString = "server=localhost;user=root;password='';database=hastane";
            baglan.Open();
            return baglan;
        }
        public MySqlConnection baglankapa()
        {
            baglan.Close();
            return baglan;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanac();
            MySqlCommand kaydet = new MySqlCommand();
            kaydet.CommandText = "insert into doktor values(@1,@2,@3,@4,@5)";
            kaydet.Connection = baglan;
            kaydet.Parameters.Add("@1",textBox1.Text);
            kaydet.Parameters.Add("@2", textBox2.Text);
            kaydet.Parameters.Add("@3", comboBox1.Text);
            kaydet.Parameters.Add("@4", comboBox2.Text);
            kaydet.Parameters.Add("@5", textBox3.Text);
            if (kaydet.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Kaydetme başarılı.");

            }
            else
            {
                MessageBox.Show("Başarısız.");
            }

            baglankapa();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if(tabControl1.SelectedIndex == 1) {
                baglanac();
                MySqlCommand listele= new MySqlCommand();
                listele.CommandText = "select * from doktor";
                listele.Connection = baglan;
                DataSet ds = new DataSet();
                MySqlDataAdapter adp = new MySqlDataAdapter(listele);
                adp.Fill(ds, "doktor");
                dataGridView1.DataSource = ds.Tables["doktor"];

                baglankapa();

            }
            else if (tabControl1.SelectedIndex == 2)
            {
                baglanac();
                MySqlCommand listele = new MySqlCommand();
                listele.CommandText = "select * from randevu";
                listele.Connection = baglan;
                DataSet ds = new DataSet();
                MySqlDataAdapter adp = new MySqlDataAdapter(listele);
                adp.Fill(ds, "randevu");
                dataGridView2.DataSource = ds.Tables["randevu"];

                baglankapa();
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                Application.Exit();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilensatir = dataGridView1.CurrentRow.Index;
            textBox4.Text = dataGridView1.Rows[secilensatir].Cells["tckimlik"].Value.ToString();
            label11.Text = textBox4.Text;
            textBox5.Text = dataGridView1.Rows[secilensatir].Cells["adsoyad"].Value.ToString();
            comboBox4.Text = dataGridView1.Rows[secilensatir].Cells["cinsiyet"].Value.ToString();
            comboBox3.Text = dataGridView1.Rows[secilensatir].Cells["bolum"].Value.ToString();
            textBox8.Text = dataGridView1.Rows[secilensatir].Cells["mezuniyet"].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanac();
            MySqlCommand guncelle = new MySqlCommand();
            guncelle.CommandText = "update doktor set tckimlik=@1, adsoyad=@2, cinsiyet=@3," +
                "bolum=@4, cinsiyet=@5 where tckimlik=@6";
            guncelle.Connection = baglan;
            guncelle.Parameters.Add("@1", textBox4.Text);
            guncelle.Parameters.Add("@2", textBox5.Text);
            guncelle.Parameters.Add("@3", comboBox4.Text);
            guncelle.Parameters.Add("@4", comboBox3.Text);
            guncelle.Parameters.Add("@5", textBox8.Text);
            guncelle.Parameters.Add("@6", label11.Text);
            if (guncelle.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Güncelleme başarılı.");
            }
            else
            {
                MessageBox.Show("Güncelleme başarısız.");
            }


            MySqlCommand listele = new MySqlCommand();
            listele.CommandText = "select * from doktor";
            listele.Connection = baglan;
            
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter(listele);
            adp.Fill(ds, "doktor");
            dataGridView1.DataSource = ds.Tables["doktor"];
            
            baglankapa();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanac();
            MySqlCommand sil = new MySqlCommand();
            sil.CommandText = "delete from doktor where tckimlik=@1";
            sil.Connection = baglan;
            sil.Parameters.Add("@1", label11.Text);
            if (sil.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Silme işlemi başarılı.");
            }
            else
            {
                MessageBox.Show("Silme işlemi başarısız.");
            }
            MySqlCommand listele = new MySqlCommand();
            listele.CommandText = "select * from doktor";
            listele.Connection = baglan;

            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter(listele);
            adp.Fill(ds, "doktor");
            dataGridView1.DataSource = ds.Tables["doktor"];

            baglankapa();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilensatir = dataGridView2.CurrentRow.Index;
            textBox9.Text = dataGridView2.Rows[secilensatir].Cells["tckimlik"].Value.ToString();
            label19.Text = textBox9.Text;
            textBox7.Text = dataGridView2.Rows[secilensatir].Cells["ad"].Value.ToString();
            textBox10.Text = dataGridView2.Rows[secilensatir].Cells["bolum"].Value.ToString();
            textBox6.Text = dataGridView2.Rows[secilensatir].Cells["doktoradi"].Value.ToString();
            dataGridView1.Text = dataGridView2.Rows[secilensatir].Cells["tarih"].Value.ToString();
            textBox11.Text = dataGridView2.Rows[secilensatir].Cells["saat"].Value.ToString();
            textBox12.Text= dataGridView2.Rows[secilensatir].Cells["soyad"].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanac();
            MySqlCommand sil = new MySqlCommand();
            sil.CommandText = "delete from randevu where tckimlik=@1";
            sil.Connection = baglan;
            sil.Parameters.Add("@1", label19.Text);
            if (sil.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("İptal etme işlemi başarılı.");
            }
            else
            {
                MessageBox.Show("İptal etme işlemi başarısız.");
            }
            MySqlCommand listele = new MySqlCommand();
            listele.CommandText = "select * from randevu";
            listele.Connection = baglan;

            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter(listele);
            adp.Fill(ds, "randevu");
            dataGridView2.DataSource = ds.Tables["randevu"];

            baglankapa();
        }
    }
}
