using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace ADB
{
    public partial class avto : Form
    {
        /* SELECT * FROM cars*/
        public avto()
        {
            InitializeComponent();
                     
        }
        private void loaddata()
        {

            DB db = new DB();
            string query = "";
            query = "SELECT av_id as id , av_name as модель, av_dvig as двигатель,av_complectaction as комплектация,av_c_opic as описание,av_color as цвет,av_trans as коробка,av_status as статус,av_foto as фото,av_price as цена,av_vin as вин FROM avtomobiles";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection());
            db.openConnection();
            DataTable datatable = new DataTable();
            datatable.Clear();
            adapter.Fill(datatable);
            dataGridView1.DataSource = datatable;
            db.closeConnection();
            //dataGridView1.RowHeadersWidth=100;
            //dataGridView1.ColumnHeadersHeight = 500;
        }
            
        private void avto_Load(object sender, EventArgs e)
        {
            loaddata();
        }

        /**/
        /* закрытие окна */
        private void label1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Вы уверены что хотите закрыть окно ?",
        "Warning",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                this.Close();
                glavnaya glavnaya = new glavnaya();
                glavnaya.Show();
                //ошибка
            }
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.White;
        }
        
        //кнопка добавления
        private void insbutton_Click(object sender, EventArgs e)
        {


            DB db = new DB();
            db.openConnection();
            for (int i = dataGridView1.Rows.Count-1; i < dataGridView1.Rows.Count; i++)
            {
                
                try {
                    
                    MySqlCommand command = new MySqlCommand("Insert into avtomobiles (av_name,av_dvig,av_complectaction,av_c_opic,av_color,av_trans,av_status,av_price,av_vin,av_foto) VALUES (@avn,@avd,@avc,@avco,@avcol,@avt,@avs,@avp,@avv,@avf) ", db.getConnection());
                    command.Parameters.Add("@avn", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Модель"].Value.ToString();
                    command.Parameters.Add("@avd", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Двигатель"].Value.ToString();
                    command.Parameters.Add("@avc", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Комплектация"].Value.ToString();
                    command.Parameters.Add("@avco", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Описание"].Value.ToString();
                    command.Parameters.Add("@avcol", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Цвет"].Value.ToString();
                    command.Parameters.Add("@avt", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Коробка"].Value.ToString();
                    command.Parameters.Add("@avs", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Статус"].Value.ToString();
                    command.Parameters.Add("@avp", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Цена"].Value.ToString();
                    command.Parameters.Add("@avv", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Вин"].Value.ToString();
                    
                    
                        var image = new Bitmap(pictureBox2.Image);
                        using (var memoryStream = new MemoryStream())
                        {
                            image.Save(memoryStream, ImageFormat.Jpeg);
                            memoryStream.Position = 0;
                            var sqlParameter = new MySqlParameter("avf", MySqlDbType.VarBinary, (int)memoryStream.Length)
                            {
                                Value = memoryStream.ToArray()
                            };
                            command.Parameters.Add(sqlParameter);
                        
                    }


                        command.ExecuteNonQuery();
                    


                }
                catch(System.NullReferenceException) { MessageBox.Show("Проверьте верно ли вы ввели данные и нет ли пустых строк"); }
                

            }

            db.closeConnection();
            loaddata();
        }
        //
        //кнопка удаления
        private void delbtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены что вы хотите удалить строку?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                try
                {
                    DB db = new DB();
                    db.openConnection();
                    MySqlCommand command = new MySqlCommand("DELETE FROM avtomobiles where av_id = @id", db.getConnection());

                    command.Parameters.Add("@id", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["id"].Value.ToString();//сравнение айди со строкой
                    command.ExecuteNonQuery();
                    loaddata();
                    db.closeConnection();
                    MessageBox.Show("Строка удалена !");
                }
                catch { MessageBox.Show("Строка не выбрана!"); }
            }
        }
        //search
        private void button1_Click(object sender, EventArgs e)
        {
            string search = textBox3.Text;
            DB db = new DB();
            DataTable datatable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT av_id as id , av_name as модель, av_dvig as двигатель,av_complectaction as комлпектация,av_c_opic as описание,av_color as цвет,av_trans as коробка,av_status as статус,av_foto as фото,av_price as цена,av_vin as вин FROM avtomobiles WHERE av_name = @search", db.getConnection());
            command.Parameters.Add("@search", MySqlDbType.VarChar).Value = search;
            datatable.Clear();
            adapter.SelectCommand = command;
            adapter.Fill(datatable);

            if (datatable.Rows.Count > 0)
            {
                dataGridView1.DataSource = datatable;
                textBox3.Text = "";
            }
            else
            {
                textBox3.Text = "";
                MessageBox.Show("Вашему запросу не соответствует ни одна строка, проверьте корректность данных!", "Warning!");
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            loaddata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox2.Image = Image.FromFile(openFileDialog.FileName);
                    }
                }
            }
        }
        //update
        private void button3_Click_1(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("Update avtomobiles set av_name = @avn,av_dvig = @avd,av_complectaction = @avc,av_c_opic = @avco,av_color=@avcol,av_trans = @avt,av_price =@avp,av_vin = @avv,av_status = @avs where av_id = @id", db.getConnection());
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
            command.Parameters.Add("@avn", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Модель"].Value.ToString();
            command.Parameters.Add("@avd", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Двигатель"].Value.ToString();
            command.Parameters.Add("@avc", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Комплектация"].Value.ToString();
            command.Parameters.Add("@avco", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Описание"].Value.ToString();
            command.Parameters.Add("@avcol", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Цвет"].Value.ToString();
            command.Parameters.Add("@avt", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Коробка"].Value.ToString();
            command.Parameters.Add("@avs", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Статус"].Value.ToString();
            command.Parameters.Add("@avp", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Цена"].Value.ToString();
            command.Parameters.Add("@avv", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["Вин"].Value.ToString();
            command.ExecuteNonQuery();
            loaddata();
            db.closeConnection();
        }
        //
    }

       
}

       
    

