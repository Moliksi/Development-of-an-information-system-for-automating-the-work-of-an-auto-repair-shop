using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Course_page
{
    public partial class FormContract : Form
    {
        public FormContract()
        {
            InitializeComponent();
        }
        private string connectionString = @"Data Source=DESKTOP-O74EA9U\SQLEXPRESS;Initial Catalog=Course paper;Integrated Security=True";
        private void FormContract_Load(object sender, EventArgs e)
        {
            this.автомобильTableAdapter.Fill(this.course_paperDataSet91.Автомобиль);
            this.специалистыTableAdapter.Fill(this.course_paperDataSet81.Специалисты);
            this.договорTableAdapter.Fill(this.course_paperDataSet11.Договор);           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.договорBindingSource.EndEdit();
            try
            {
                this.договорTableAdapter.Update(this.course_paperDataSet11.Договор);
            }
            catch
            {
                MessageBox.Show("Введенная дата больше, чем дата на сегодняшний день");
            }
        }

        private void combobox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {                                 
                    DataRowView dataRowView= (DataRowView)comboBox1.SelectedItem;                
                    string selectedSurname = dataRowView[1].ToString();               
                    DataClasses1DataContext data = new DataClasses1DataContext(connectionString);
                    var client = from Специалист in data.Специалисты
                                 where Специалист.Фамилия == selectedSurname
                                 select Специалист.Имя;
                comboBox3.DataSource = client;
            }
        }
        private void combobox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                DataRowView dataRowView = (DataRowView)comboBox1.SelectedItem;
                string selectedSurname = dataRowView[1].ToString();
                string selectedSurname3 = comboBox3.SelectedItem.ToString();
                DataClasses1DataContext data = new DataClasses1DataContext(connectionString);
                var client = from Специалист in data.Специалисты
                             where Специалист.Фамилия == selectedSurname && Специалист.Имя==selectedSurname3
                             select Специалист.Отчество;
                comboBox4.DataSource = client;
            }
        }
        private void combobox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem != null)
            {
                DataRowView dataRowView = (DataRowView)comboBox1.SelectedItem;
                string selectedSurname = dataRowView[1].ToString();
                string selectedSurname3 = comboBox3.SelectedItem.ToString();
                string selectedSurname4 = comboBox4.SelectedItem.ToString();
                DataClasses1DataContext data = new DataClasses1DataContext(connectionString);
                var client = from Специалист in data.Специалисты
                             where Специалист.Фамилия == selectedSurname && Специалист.Имя == selectedSurname3 && Специалист.Отчество==selectedSurname4
                             select new { Специалист.КодСпециалиста, Специалист.Квалификация };
                var kod = from Специалист in client                       
                          select  Специалист.КодСпециалиста;
                var kval= from Специалист in client
                          select Специалист.Квалификация;
                comboBox2.DataSource = kod;
                comboBox5.DataSource = kval;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           if(textBox1.Text!="")
            {
                DataClasses1DataContext data = new DataClasses1DataContext(connectionString);

                var client = from Израсход in data.ИзрасходованныеЗапчасти
                             where Израсход.КодДоговора== long.Parse(textBox1.Text)
                             select Израсход;
                if (client.Any())
                {

                    var Sum1 = from Рем in data.Ремонт
                              where Рем.КодДоговора == long.Parse(textBox1.Text)
                              select Рем.ПредварительнаяСтоимость;
                    var Sum2 = from Израсход in data.ИзрасходованныеЗапчасти
                                 where Израсход.КодДоговора == long.Parse(textBox1.Text)
                                 select Израсход.Стоимость;
                    textBox2.Text = (Sum1.Sum()+Sum2.Sum()).ToString();
                }
                else
                {
                    var Sum = from Рем in data.Ремонт
                              where Рем.КодДоговора == long.Parse(textBox1.Text)
                              select Рем.ПредварительнаяСтоимость;
                    textBox2.Text=Sum.Sum().ToString();
                    
                }
            }
        }
    }
}
