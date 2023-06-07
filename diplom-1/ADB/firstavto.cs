using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADB
{
    public partial class firstavto : Form
    {
        public firstavto()
        {
            InitializeComponent();
            
        }
        int x = 0;
        private void loaddata()
        {
            DB db = new DB();
            db.openConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("Select image,a_name,a_price,a_info from avto", db.getConnection());
            adapter.SelectCommand = command;
            DataTable table = new DataTable();
            table.Clear();
            adapter.Fill(table);
            if(table.Rows.Count > 0)
            {
                DataRow row = table.Rows[x];
                label1.Text = row[1].ToString();
                label2.Text = row[2].ToString();
                label3.Text = row[3].ToString();
                byte[] img = (byte[])row[0];
                if(img == null)
                {
                    pictureBox1.Image = null;
                }
                else 
                { 
                    MemoryStream ms = new MemoryStream(img);
                    pictureBox1.Image = Image.FromStream(ms); 

                }

            }
            db.closeConnection();
        }
        private int count()
        {
            int count = 0;
            string y = "";
            DB db = new DB();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("Select COUNT(*) from avto", db.getConnection());
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                y = reader[0].ToString();
                count = int.Parse(y);
            }
            db.closeConnection();
            return count;
            
        }
        //LOAD
        private void firstavto_Load(object sender, EventArgs e)
        {
            loaddata();

        }

        private void close_form_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
       "Вы уверены что хотите закрыть приложение?",
       "Warning",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        //vpered
        private void button4_Click(object sender, EventArgs e)
        {

            if (x < count()-1)
            {
                x++;
                loaddata();
            }
            else { MessageBox.Show("Конец"); }
        }
        //nazad
        private void button2_Click_1(object sender, EventArgs e)
        {

            if (x!=0)
            {
                x--;
                loaddata();
            }
            else { MessageBox.Show("Конец"); }
        }
        //pereitu
        private void button1_Click_1(object sender, EventArgs e)
        {
            id.n = label1.Text;

            Secondavto secondavto = new Secondavto();
            secondavto.Show();
            this.Hide();
        }
    }
}
