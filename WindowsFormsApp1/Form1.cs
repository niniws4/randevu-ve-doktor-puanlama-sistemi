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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
        public static string tckimlik;
        string dogruKod = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == dogruKod)
            {
                MessageBox.Show("Giriş başarılı!");
                MySqlConnection baglan = new MySqlConnection();
                baglan.ConnectionString = "server=localhost;user=root;password='';database=hastane";
                baglan.Open();
                MySqlCommand getir = new MySqlCommand();
                getir.CommandText = "select tckimlik,sifre from hasta where tckimlik=@1 and sifre=@2";
                getir.Connection = baglan;
                getir.Parameters.Add("@1", textBox1.Text);
                tckimlik = textBox1.Text;
                getir.Parameters.Add("@2", textBox2.Text);

                MySqlDataReader oku = getir.ExecuteReader();
                if (oku.HasRows)
                {
                    while (oku.Read())
                    {
                        timer1.Start();
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı girişi başarısız.");
                }
                baglan.Close();
            }
            else
            {
                MessageBox.Show("Kod yanlış! Lütfen tekrar deneyin.");
                RastgeleKodUret();
            }
        }
        private void RastgeleKodUret()
        {
            Random rnd = new Random();
            dogruKod = "";
            Label[] labelDizisi = new Label[] { label6, label7, label8, label9 };

            for (int i = 0; i < 4; i++)
            {
                int sayi = rnd.Next(0, 10);
                dogruKod += sayi.ToString();
                labelDizisi[i].Text = sayi.ToString();
            }

            textBox3.Clear();
            textBox3.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RastgeleKodUret();
        }
        bool sifreGosteriliyor = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            if (progressBar1.Value < 100)
            {
                progressBar1.Value = progressBar1.Value + 10;
            }
            else
            {
                Form3 f3 = new Form3();
                f3.Show();
                this.Hide();
                timer1.Stop();
            }
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                groupBox1.Visible = true;
                groupBox3.Visible = false;
            }
            else if (radioButton2.Checked)
            {
                groupBox3.Visible = true;
                
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox5.Text=="admin" && textBox4.Text == "123")
            {
                Form4 f4 = new Form4();
                f4.ShowDialog();
            }
            else
            {
                MessageBox.Show("Hatalı giriş.");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (sifreGosteriliyor)
            {
                textBox2.PasswordChar = '*';
                sifreGosteriliyor = false;
            }
            else
            {
                textBox2.PasswordChar = '\0';
                sifreGosteriliyor = true;
            }
        }
        bool sifreGosteriliyor2 = false;
        private void pictureBox3_Click(object sender, EventArgs e)
        {

            if (sifreGosteriliyor2)
            {
                textBox4.PasswordChar = '*';
                sifreGosteriliyor2 = false;
            }
            else
            {
                textBox4.PasswordChar = '\0';
                sifreGosteriliyor2 = true;
            }
        }
    }
}
