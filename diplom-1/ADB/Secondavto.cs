using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace ADB
{
    public partial class Secondavto : Form
    {
        public Secondavto()
        {
            InitializeComponent();
        }
        int f = 0;
        int x = 0;
        int vin = 0;
        string query = "";
        // load
        private void loaddata()
        {
            label6.Text = "Показать Vin";
            DB db = new DB();
            query = "Select av_name, av_dvig, av_complectaction, av_c_opic, av_color, av_trans, av_price, av_foto,av_vin FROM avtomobiles WHERE av_name = @id.n and av_status = 'В наличии'";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(query, db.getConnection());
            command.Parameters.Add("@id.n", MySqlDbType.String).Value = id.n;
            adapter.SelectCommand = command;
            db.openConnection();
            DataTable datatable = new DataTable();
            datatable.Clear();
            adapter.Fill(datatable);
            if (datatable.Rows.Count > 0)
            {
                DataRow row = datatable.Rows[x];
                label1.Text = row[0].ToString();
                label2.Text = row[1].ToString();
                label3.Text = row[2].ToString();
                label7.Text = row[3].ToString();
                label4.Text = row[4].ToString();
                label9.Text = row[5].ToString();
                label5.Text = row[6].ToString();
                vin = int.Parse(row[8].ToString());
                byte[] img = (byte[])(row[7]);
                if (img == null)
                {
                    pictureBox1.Image = null;
                }
                else
                {

                    MemoryStream ms = new MemoryStream(img);
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else { MessageBox.Show("Ни один элемент не соответствует вашему запросу"); }
            db.closeConnection();

        }
        private void Secondavto_Load(object sender, EventArgs e)
        {
            loaddata();

        }
        //nazad
        private void button1_Click(object sender, EventArgs e)
        {
            label6.Text = "Показать Vin";
            if (f == 0)
            {
                if (x != 0 )
                {
                    x--;
                    loaddata();
                }
                else MessageBox.Show("Конец");
            }
            if (f == 1)
            {
                if (x != 0)
                {
                    x--;
                    fil();
                }
                else MessageBox.Show("Конец");
            }
            if (f == 2)
            {
                if (x != 0 )
                {
                    x--;
                    filt();
                }
                else MessageBox.Show("Конец");
            }
            if (f == 3)
            {
                if (x != 0)
                {
                    x--;
                    filter();
                }
                else MessageBox.Show("Конец");
            }
            if (f == 4)
            {
                if (x != 0)
                {
                    x--;
                    vozr();
                }
                else MessageBox.Show("Конец");
            }

        }
        //vpered
        private void button2_Click(object sender, EventArgs e)
        {
            label6.Text = "Показать Vin";
            if (f == 0)
            {
                int count = coun();
                if (x < count - 1)
                {
                    x++;
                    loaddata();
                }
                else MessageBox.Show("Конец");
            }
            if(f == 1)
            {   
                int count = fil1();
                if (x < count - 1)
                {
                    x++;
                    fil();
                }
                else MessageBox.Show("Конец");
                    
            }
            if( f == 2)
            {
                int count = filt2();
                if (x < count - 1)
                {
                    x++;
                    filt();
                }
                else MessageBox.Show("Конец");

            }
            if( f == 3)
            {
                int count = filter3();
                if (x < count - 1)
                {
                    x++;
                    filter();
                }
                else MessageBox.Show("Конец");
            }
            if (f == 4)
            {
                int count = coun();
                if (x < count - 1)
                {
                    x++;
                    vozr();
                }
                else MessageBox.Show("Конец");
            }
        }
        //фильтр
        private void button3_Click(object sender, EventArgs e)
        {
            label6.Text = "Показать Vin";
            x = 0;
            if (comboBox1.Text != "" && comboBox2.Text != "")
            {
                f = 1;
                fil();
            }
            else if (comboBox1.Text != "")
            {
                f = 2;
                filt();
            }
            else if (comboBox2.Text != "")
            {
                f = 3;
                filter();
            }
            else { MessageBox.Show("Вы ничего не выбрали"); }
        }
        // очистить
        private void button5_Click(object sender, EventArgs e)
        {
            label6.Text = "Показать Vin";
            x = 0;
            f = 0;
            comboBox1.Text = "";
            comboBox2.Text = "";
            loaddata();

        }
        //заполнение
        private void comboBox1_Enter(object sender, EventArgs e)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("select av_color from avtomobiles group by av_color", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            comboBox1.Items.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                comboBox1.Items.Add(table.Rows[i]["av_color"]);
            }
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("select av_complectaction from avtomobiles group by av_complectaction", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            comboBox2.Items.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                comboBox2.Items.Add(table.Rows[i]["av_complectaction"]);
            }
        }
        //фильтры
        private void fil()
        {
            label6.Text = "Показать Vin";
            string cvet = comboBox1.Text;
            string comp = comboBox2.Text;
            DB db = new DB();
            db.openConnection();
            DataTable datatable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("Select av_name, av_dvig, av_complectaction, av_c_opic, av_color, av_trans, av_price, av_foto,av_vin FROM avtomobiles WHERE av_name = @id.n and av_color = @cvet and av_complectaction = @comp and av_status = 'В наличии'", db.getConnection());
            command.Parameters.Add("@cvet", MySqlDbType.VarChar).Value = cvet;
            command.Parameters.Add("@comp", MySqlDbType.VarChar).Value = comp;
            command.Parameters.Add("@id.n", MySqlDbType.String).Value = id.n;
            adapter.SelectCommand = command;
            adapter.Fill(datatable);
            if (datatable.Rows.Count > 0)
            {

                DataRow row = datatable.Rows[x];
                label1.Text = row[0].ToString();
                label2.Text = row[1].ToString();
                label3.Text = row[2].ToString();
                label7.Text = row[3].ToString();
                label4.Text = row[4].ToString();
                label9.Text = row[5].ToString();
                label5.Text = row[6].ToString();
                vin = int.Parse(row[8].ToString());
                byte[] img = (byte[])(row[7]);
                if (img == null)
                {
                    pictureBox1.Image = null;
                }
                else
                {

                    MemoryStream ms = new MemoryStream(img);
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else { MessageBox.Show("Ни один элемент не соответствует вашему запросу"); }
            db.closeConnection();
        }
        private void filt()
        {
            label6.Text = "Показать Vin";
            string cvet = comboBox1.Text;

            DB db = new DB();
            db.openConnection();
            DataTable datatable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("Select av_name, av_dvig, av_complectaction, av_c_opic, av_color, av_trans, av_price, av_foto,av_vin FROM avtomobiles WHERE av_name = @id.n and av_color = @cvet and av_status = 'В наличии'", db.getConnection());
            command.Parameters.Add("@cvet", MySqlDbType.VarChar).Value = cvet;
            command.Parameters.Add("@id.n", MySqlDbType.String).Value = id.n;
            adapter.SelectCommand = command;
            adapter.Fill(datatable);
            if (datatable.Rows.Count > 0)
            {
                DataRow row = datatable.Rows[x];
                label1.Text = row[0].ToString();
                label2.Text = row[1].ToString();
                label3.Text = row[2].ToString();
                label7.Text = row[3].ToString();
                label4.Text = row[4].ToString();
                label9.Text = row[5].ToString();
                label5.Text = row[6].ToString();
                vin = int.Parse(row[8].ToString());
                byte[] img = (byte[])(row[7]);
                if (img == null)
                {
                    pictureBox1.Image = null;
                }
                else
                {

                    MemoryStream ms = new MemoryStream(img);
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else { MessageBox.Show("Ни один элемент не соответствует вашему запросу"); }
            db.closeConnection();
        }
        private void filter()
        {
            label6.Text = "Показать Vin";
            string comp = comboBox2.Text;
            DB db = new DB();
            db.openConnection();
            DataTable datatable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("Select av_name, av_dvig, av_complectaction, av_c_opic, av_color, av_trans, av_price, av_foto,av_vin FROM avtomobiles WHERE av_name = @id.n and av_complectaction = @comp and av_status = 'В наличии'", db.getConnection());
            command.Parameters.Add("@comp", MySqlDbType.VarChar).Value = comp;
            command.Parameters.Add("@id.n", MySqlDbType.String).Value = id.n;
            adapter.SelectCommand = command;
            adapter.Fill(datatable);
            if (datatable.Rows.Count > 0)
            {
                DataRow row = datatable.Rows[x];
                label1.Text = row[0].ToString();
                label2.Text = row[1].ToString();
                label3.Text = row[2].ToString();
                label7.Text = row[3].ToString();
                label4.Text = row[4].ToString();
                label9.Text = row[5].ToString();
                label5.Text = row[6].ToString();
                vin = int.Parse(row[8].ToString());
                byte[] img = (byte[])(row[7]);
                if (img == null)
                {
                    pictureBox1.Image = null;
                }
                else
                {

                    MemoryStream ms = new MemoryStream(img);
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else { MessageBox.Show("Ни один элемент не соответствует вашему запросу"); }
            db.closeConnection();
        }
        //count
        private int fil1()
        {
            string cvet = comboBox1.Text;
            string comp = comboBox2.Text;
            string y = "";
            int count = 0;
            DB d = new DB();
            d.openConnection();
            MySqlCommand comman = new MySqlCommand("Select count(*) FROM avtomobiles WHERE av_name = @id.n and av_color = @cvet and av_complectaction = @comp and av_status = 'В наличии'", d.getConnection());
            comman.Parameters.Add("@cvet", MySqlDbType.VarChar).Value = cvet;
            comman.Parameters.Add("@comp", MySqlDbType.VarChar).Value = comp;
            comman.Parameters.Add("@id.n", MySqlDbType.String).Value = id.n;
            MySqlDataReader reade = comman.ExecuteReader();
            reade.Read();
            if (reade.HasRows)
            {

                y = reade[0].ToString();
                count = Int32.Parse(y);
            }
            d.closeConnection();
            return count;
        }
        private int filt2()
        {
            string cvet = comboBox1.Text;
            string comp = comboBox2.Text;
            string y = "";
            int count = 0;
            DB d = new DB();
            d.openConnection();
            MySqlCommand comman = new MySqlCommand("Select count(*) FROM avtomobiles WHERE av_name = @id.n and av_color = @cvet  and av_status = 'В наличии'", d.getConnection());
            comman.Parameters.Add("@cvet", MySqlDbType.VarChar).Value = cvet;
            
            comman.Parameters.Add("@id.n", MySqlDbType.String).Value = id.n;
            MySqlDataReader reade = comman.ExecuteReader();
            reade.Read();
            if (reade.HasRows)
            {

                y = reade[0].ToString();
                count = Int32.Parse(y);
            }
            d.closeConnection();
            return count;
        }
        private int filter3()
        {
            string cvet = comboBox1.Text;
            string comp = comboBox2.Text;
            string y = "";
            int count = 0;
            DB d = new DB();
            d.openConnection();
            MySqlCommand comman = new MySqlCommand("Select count(*) FROM avtomobiles WHERE av_name = @id.n and av_complectaction = @comp and av_status = 'В наличии'", d.getConnection());
            comman.Parameters.Add("@comp", MySqlDbType.VarChar).Value = comp;
            comman.Parameters.Add("@id.n", MySqlDbType.String).Value = id.n;
            MySqlDataReader reade = comman.ExecuteReader();
            reade.Read();
            if (reade.HasRows)
            {

                y = reade[0].ToString();
                count = Int32.Parse(y);
            }
            d.closeConnection();
            return count;
        }
        //copt no vozr
        private void vozr()
        {
            
            DB db = new DB();
            db.openConnection();
            DataTable datatable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("Select av_name, av_dvig, av_complectaction, av_c_opic, av_color, av_trans, av_price, av_foto,av_vin FROM avtomobiles WHERE av_name = @id.n and av_status = 'В наличии' order by av_price ASC ", db.getConnection());
            command.Parameters.Add("@id.n", MySqlDbType.String).Value = id.n;
            adapter.SelectCommand = command;
            adapter.Fill(datatable);
            if (datatable.Rows.Count > 0)
            {

                DataRow row = datatable.Rows[x];
                label1.Text = row[0].ToString();
                label2.Text = row[1].ToString();
                label3.Text = row[2].ToString();
                label7.Text = row[3].ToString();
                label4.Text = row[4].ToString();
                label9.Text = row[5].ToString();
                label5.Text = row[6].ToString();
                vin = int.Parse(row[8].ToString());
                byte[] img = (byte[])(row[7]);
                if (img == null)
                {
                    pictureBox1.Image = null;
                }
                else
                {

                    MemoryStream ms = new MemoryStream(img);
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else { MessageBox.Show("Ни один элемент не соответствует вашему запросу"); }
            db.closeConnection();
        }
        private int coun()
        {
            string y = "";
            int count = 0;
            DB d = new DB();
            d.openConnection();
            MySqlCommand comman = new MySqlCommand("SELECT COUNT(*) FROM avtomobiles where av_name = @id.n and av_status = 'В наличии'", d.getConnection());
            comman.Parameters.Add("@id.n", MySqlDbType.String).Value = id.n;
            MySqlDataReader reade = comman.ExecuteReader();
            reade.Read();
            if (reade.HasRows)
            {

                y = reade[0].ToString();
                count = Int32.Parse(y);
            }
            d.closeConnection();
            return count;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            f = 4;
        }
        //bron
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            Byuers byuers = new Byuers();
            byuers.Show();
        }
        //vin
        private void label6_Click(object sender, EventArgs e)
        {
            label6.Text = vin.ToString();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
       "Вы уверены что хотите закрыть вкладку?",
       "Warning",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                this.Close();
                firstavto firstavto = new firstavto();
                firstavto.Show();
            }
            
        }
    }
}
