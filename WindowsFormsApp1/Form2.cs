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
    public partial class Form2 : Form
    {
        public Form2()
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
            kaydet.CommandText = "insert into hasta values(@1,@2,@3,@4,@5,@6)";
            kaydet.Connection = baglan;
            kaydet.Parameters.Add("@1",textBox2.Text);
            kaydet.Parameters.Add("@2", textBox3.Text);
            kaydet.Parameters.Add("@3", textBox1.Text);
            kaydet.Parameters.Add("@4", comboBox1.Text);
            kaydet.Parameters.Add("@5", Convert.ToDateTime(dateTimePicker1.Text));
            kaydet.Parameters.Add("@6", textBox4.Text);
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
    }
}
