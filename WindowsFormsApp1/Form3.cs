using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            kaydet.CommandText = "insert into randevu values(@1,@2,@3,@4,@5,@6,@7)";
            kaydet.Connection = baglan;
            kaydet.Parameters.Add("@1", comboBox3.Text);
            kaydet.Parameters.Add("@2", textBox5.Text);
            kaydet.Parameters.Add("@3", textBox6.Text);
            kaydet.Parameters.Add("@4", comboBox1.Text);
            kaydet.Parameters.Add("@5", comboBox2.Text);
            kaydet.Parameters.Add("@6", Convert.ToDateTime(dateTimePicker1.Text));
            kaydet.Parameters.Add("@7", textBox7.Text);
            kaydet.Parameters.Add("@8", label13.Text);

            if (kaydet.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Kaydedildi.");
            }
            else
            {
                MessageBox.Show("Başarısız.");
            }
            baglankapa();
        }
        private void BolumleriYukle()
        {
            try
            {
                baglanac();
                MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT bolum FROM doktor", baglan);
                MySqlDataReader oku = cmd.ExecuteReader();

                comboBox1.Items.Clear();
                while (oku.Read())
                {
                    comboBox1.Items.Add(oku["bolum"].ToString());
                }

                oku.Close();
                baglankapa();
            }
            catch
            {
                MessageBox.Show("Bölümler yüklenirken hata oluştu");
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            label13.Text = Form1.tckimlik;
            BolumleriYukle();
            baglanac();

            MySqlCommand getir = new MySqlCommand();
            getir.CommandText = "select tckimlik from hasta";
            getir.Connection = baglan;

            MySqlDataReader oku = getir.ExecuteReader();

            if (oku.HasRows)
            {
                while (oku.Read())
                {
                    comboBox3.Items.Add(oku["tckimlik"].ToString());
                }
            }
            else
            {
                MessageBox.Show("Veri bulunamadı.");
            }
            oku.Close();
            
            baglankapa();
            
        }
        
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                baglanac();

                string secilentc = comboBox3.SelectedItem.ToString();
                string sorgu = "select hastaad, hastasoyad from hasta where tckimlik = @1";

                MySqlCommand getir = new MySqlCommand(sorgu, baglan);
                getir.Parameters.Add("@1", secilentc);

                MySqlDataReader oku = getir.ExecuteReader();

                if (oku.Read())
                {
                    textBox5.Text = oku["hastaad"].ToString();
                    textBox6.Text = oku["hastasoyad"].ToString();
                }

                oku.Close();
                baglankapa();
            }
            catch
            {
                MessageBox.Show("Hata");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string secilenBolum = comboBox1.SelectedItem.ToString();
            DoktorlariYukle(secilenBolum);
        }
        private void DoktorlariYukle(string bolum)
        {
            try
            {
                baglanac();
                MySqlCommand cmd = new MySqlCommand("SELECT adsoyad FROM doktor WHERE bolum = @bolum", baglan);
                cmd.Parameters.Add("@bolum", bolum);

                MySqlDataReader oku = cmd.ExecuteReader();

                comboBox2.Items.Clear();
                while (oku.Read())
                {
                    comboBox2.Items.Add(oku["adsoyad"].ToString());
                }

                oku.Close();
                baglankapa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doktorlar yüklenirken hata oluştu.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox4.Text = row.Cells["tckimlik"].Value.ToString();
                textBox1.Text = row.Cells["adsoyad"].Value.ToString();
                textBox2.Text = row.Cells["bolum"].Value.ToString();
                textBox3.Text = row.Cells["cinsiyet"].Value.ToString();
            }
        }
        private int oylama()
        {
            if (radioButton1.Checked) return 1;
            if (radioButton2.Checked) return 2;
            if (radioButton3.Checked) return 3;
            if (radioButton4.Checked) return 4;
            if (radioButton5.Checked) return 5;
            return 0;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
    {
        MessageBox.Show("Lütfen bir doktor seçin.");
        return;
    }

    int doktorID = Convert.ToInt32(textBox4.Text);
    int oy = oylama();

    if (oy == 0)
    {
        MessageBox.Show("Lütfen bir puan seçin.");
        return;
    }

    int kullaniciID = 1; // Şimdilik sabit kullanıcı (kendi sistemine göre değiştirirsin)

    
    {
        baglan.Open();

        string komut = "INSERT INTO Oylar (DoktorID, KullaniciID, Oy) VALUES (@dID, @kID, @oy)";
        MySqlCommand cmd = new MySqlCommand(komut, baglan);
        cmd.Parameters.AddWithValue("@dID", doktorID);
        cmd.Parameters.AddWithValue("@kID", kullaniciID);
        cmd.Parameters.AddWithValue("@oy", oy);
        cmd.ExecuteNonQuery();

        MessageBox.Show("Oy başarıyla kaydedildi.");
    }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanac();
            MySqlCommand listele = new MySqlCommand();
            listele.CommandText = "select * from doktor";
            listele.Connection = baglan;
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter(listele);
            adp.Fill(ds, "doktor");
            dataGridView1.DataSource = ds.Tables["doktor"];

            baglankapa();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            baglanac();

            MySqlCommand listele = new MySqlCommand();
            listele.CommandText = "select DoktorID,Oy from oylar";
            listele.Connection = baglan;
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter(listele);
            adp.Fill(ds, "oylar");
            dataGridView1.DataSource = ds.Tables["oylar"];
            baglankapa();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            baglanac();
            MySqlCommand getir = new MySqlCommand();
            getir.CommandText = "select * from hasta where tckimlik=@1";
            getir.Connection = baglan;
            getir.Parameters.Add("@1",label13.Text);
            MySqlDataReader oku= getir.ExecuteReader();
            if (oku.HasRows)
            {
                while(oku.Read())
                {
                    textBox8.Text = oku[0].ToString();
                    textBox9.Text = oku[1].ToString();
                    textBox10.Text = oku[2].ToString();
                    textBox11.Text = oku[3].ToString();
                    dateTimePicker2.Text= oku[4].ToString();
                    textBox12.Text = oku[5].ToString();
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı bulunamadı.");
            }

            baglankapa();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanac();
            MySqlCommand guncelle = new MySqlCommand();
            guncelle.CommandText = "update hasta set tckimlik=@1, hastaad=@2, hastasoyad=@3," +
                "cinsiyet=@4, dogumtarihi=@5,sifre=@6 where tckimlik=@7";
            guncelle.Connection = baglan;
            guncelle.Parameters.Add("@1", textBox8.Text);
            guncelle.Parameters.Add("@2", textBox9.Text);
            guncelle.Parameters.Add("@3", textBox10.Text);
            guncelle.Parameters.Add("@4", textBox11.Text);
            guncelle.Parameters.Add("@5", dateTimePicker2.Text);
            guncelle.Parameters.Add("@6", textBox12.Text);
            guncelle.Parameters.Add("@7", label13.Text);
            if (guncelle.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Güncelleme başarılı.");
            }
            else
            {
                MessageBox.Show("Güncelleme başarısız.");
            }

            baglankapa();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            baglanac();
            MySqlCommand listele = new MySqlCommand();
            listele.CommandText = "select * from randevu where tckimlik=@1";
            listele.Connection = baglan;
            listele.Parameters.Add("@1", label13.Text);
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter(listele);
            adp.Fill(ds,"randevu");
            dataGridView2.DataSource = ds.Tables["randevu"];

            baglankapa();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult onay;
            onay= MessageBox.Show("Uygulamadan çıkmak istiyor musunuz?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (onay == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            dataGridView2.BackgroundColor = colorDialog1.Color;
            button5.BackColor=colorDialog1.Color;
            button6.BackColor=colorDialog1.Color;
        }
    }
}
