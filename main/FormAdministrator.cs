using Course_page.Course_paperDataSet11TableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Course_page
{
    public partial class FormAdministrator : Form
    {
        private string connectionString = @"Data Source=DESKTOP-O74EA9U\SQLEXPRESS;Initial Catalog=Course paper;Integrated Security=True";
        public FormAdministrator()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.автомобильTableAdapter1.Fill(this.course_paperDataSet9.Автомобиль);
            this.специалистыTableAdapter.Fill(this.course_paperDataSet8.Специалисты);
            this.ремонтTableAdapter.Fill(this.course_paperDataSet7.Ремонт);
            this.клиентыTableAdapter.Fill(this.course_paperDataSet6.Клиенты);
            this.израсходованныеЗапчастиTableAdapter.Fill(this.course_paperDataSet5.ИзрасходованныеЗапчасти);
            this.договорTableAdapter.Fill(this.course_paperDataSet4.Договор);
            this.автомобильTableAdapter.Fill(this.course_paperDataSet3.Автомобиль);

        }

        private void автомобилиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAvto myForm2 = new FormAvto();
            myForm2.Show();
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClient myForm3 = new FormClient();
            myForm3.Show();
        }

        private void договорыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormContract myForm4 = new FormContract();
            myForm4.Show();
        }

        private void специалистыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSpecialists myForm5 = new FormSpecialists();
            myForm5.Show();
        }

        private void ремонтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRepair myForm6 = new FormRepair();
            myForm6.Show();
        }

        private void израсходованныеЗапчастиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUsed_spare_parts myForm7 = new FormUsed_spare_parts();
            myForm7.Show();
        }

        private void добавитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddUser myForm8 = new FormAddUser();
            myForm8.Show();
        }

        private void доходОтСотрудниковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Specialist_Profit", connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }

                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Невозможно выполнить команду!");
            }
        }

        

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext data = new DataClasses1DataContext(connectionString);
            var client = from Автомобил in data.Автомобиль
                         join Сделка in data.Договор on Автомобил.КодАвтомобиля equals Сделка.КодАвтомобиля
                         join Специалист in data.Специалисты on Сделка.КодСпециалиста equals Специалист.КодСпециалиста
                         join Клиент in data.Клиенты on Автомобил.КодКлиента equals Клиент.КодКлиента
                         where Клиент.Телефон == toolStripTextBox3.Text
                         select new {Клиент.Фамилия, Клиент.Имя, Клиент.Отчество,  Сделка.КодДоговора, Сделка.СтоимостьРемонта, Сделка.ДатаРемонта, Сделка.ВидНеисправности, Фамилия_Специалиста= Специалист.Фамилия, Имя_Специалиста = Специалист.Имя, Отчество_Специалиста = Специалист.Отчество, Автомобил.Марка, Автомобил.Модель, Автомобил.НомернойЗнак };
            dataGridView1.DataSource = client;
        }

       

        private void расчётToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext data = new DataClasses1DataContext(connectionString);

            var profit = from Сотруд in data.Специалисты
                         join Дог in data.Договор on Сотруд.КодСпециалиста equals Дог.КодСпециалиста
                         where Сотруд.КодСпециалиста==int.Parse(toolStripTextBox4.Text) && DateTime.ParseExact(toolStripTextBox5.Text, "dd.MM.yyyy", null) <= Дог.ДатаРемонта && Дог.ДатаРемонта <= DateTime.ParseExact(toolStripTextBox6.Text, "dd.MM.yyyy", null)
                         select new {Сотруд.Фамилия, Сотруд.Имя, Сотруд.Отчество, Сотруд.Квалификация,Дог.ДатаРемонта, Дог.СтоимостьРемонта};
            SqlMoney money = 0;
            foreach (var prof in profit)
            {
                money += prof.СтоимостьРемонта;
            }
            MessageBox.Show("Доход за указанный промежуток времени специалистом = " + money.ToString());
            dataGridView1.DataSource = profit;
        }

        private void самыйЛучшийСотрудникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext data = new DataClasses1DataContext(connectionString);

            var profit = from договор in data.Договор
                         group договор by договор.КодСпециалиста into группа
                         let общийДоход = группа.Sum(д => д.СтоимостьРемонта)
                         orderby общийДоход descending
                         select new { Специалист = группа.Key, Доход = общийДоход};
            var topSpecialist = from spec in profit
                                join specis in data.Специалисты on spec.Специалист equals specis.КодСпециалиста
                                select new {Код_Специалиста=spec.Специалист, specis.Фамилия, specis.Имя, specis.Отчество, specis.Квалификация, spec.Доход};
            var topSpecialist1 = new List<dynamic> { topSpecialist.FirstOrDefault() };
            dataGridView1.DataSource = topSpecialist1;
        }

        private void самыйЛучшийСотрудникПоВыполненнымРаботамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext data = new DataClasses1DataContext(connectionString);
            var specialistMostWork = from special in data.Специалисты
                                     join Dog in data.Договор on special.КодСпециалиста equals Dog.КодСпециалиста
                                     group special by special.КодСпециалиста into группа
                                     let Выполнено_работ = группа.Count()
                                     orderby Выполнено_работ descending
                                     select new { Код_Специалиста = группа.Key,  Выполнено_работ };
            var specialistWithMostWorks=from spec in specialistMostWork
                                        join specis in data.Специалисты on spec.Код_Специалиста equals specis.КодСпециалиста
                                        select new {spec.Код_Специалиста, specis.Фамилия, specis.Имя, specis.Отчество, specis.Квалификация, spec.Выполнено_работ };
            var topSpecialist1 = new List<dynamic> { specialistWithMostWorks.FirstOrDefault() };          
            dataGridView1.DataSource = topSpecialist1;
        }


        private void поискToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext data = new DataClasses1DataContext(connectionString);
            var client = from Автомобил in data.Автомобиль
                         join Клиент in data.Клиенты on Автомобил.КодКлиента equals Клиент.КодКлиента
                         where Клиент.Телефон == toolStripTextBox8.Text
                         select new {Клиент.КодКлиента, Клиент.Фамилия, Клиент.Имя, Клиент.Отчество, Автомобил.Марка, Автомобил.Модель, Автомобил.НомернойЗнак };
            dataGridView1.DataSource = client;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext data = new DataClasses1DataContext(connectionString);

            var profit = from Дата in data.Договор
                         where DateTime.ParseExact(toolStripTextBox7.Text, "dd.MM.yyyy", null) <= Дата.ДатаРемонта && Дата.ДатаРемонта <= DateTime.ParseExact(toolStripTextBox9.Text, "dd.MM.yyyy", null)
                         select new { Дата.СтоимостьРемонта, Дата.ДатаРемонта };
            SqlMoney money = 0;
            foreach (var prof in profit)
            {
                money += prof.СтоимостьРемонта;
            }
            MessageBox.Show("Доход за указанный промежуток времени = " + money.ToString());
            dataGridView1.DataSource = profit;
        }

      

        private void поискToolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GetCarDog", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NumberPlate", toolStripTextBox2.Text);
                    DataTable dataTable = new DataTable();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                    dataGridView1.DataSource = dataTable;
                }
            }

                    
        }



        private void поискToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext data = new DataClasses1DataContext(connectionString);
            var client = from Автомобил in data.Автомобиль
                         join Сделка in data.Договор on Автомобил.КодАвтомобиля equals Сделка.КодАвтомобиля
                         join Специалист in data.Специалисты on Сделка.КодСпециалиста equals Специалист.КодСпециалиста
                         join Рем in data.Ремонт on Сделка.КодДоговора equals Рем.КодДоговора                    
                         where Автомобил.НомернойЗнак == toolStripTextBox12.Text && DateTime.ParseExact(toolStripTextBox11.Text, "dd.MM.yyyy", null)==Сделка.ДатаРемонта
                         select new { Автомобил.КодАвтомобиля, Автомобил.Марка, Автомобил.Модель, Автомобил.НомернойЗнак, Сделка.КодДоговора, Сделка.ДатаРемонта, Сделка.ВидНеисправности,Рем.Название,Рем.ПредварительнаяСтоимость};
            dataGridView1.DataSource = client;
        }

        private void поискToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext data = new DataClasses1DataContext(connectionString);
            var client = from Автомобил in data.Автомобиль
                         join Сделка in data.Договор on Автомобил.КодАвтомобиля equals Сделка.КодАвтомобиля
                         join Специалист in data.Специалисты on Сделка.КодСпециалиста equals Специалист.КодСпециалиста
                         join Израсход in data.ИзрасходованныеЗапчасти on Сделка.КодДоговора equals Израсход.КодДоговора
                         where Автомобил.НомернойЗнак == toolStripTextBox1.Text && DateTime.ParseExact(toolStripTextBox10.Text, "dd.MM.yyyy", null) == Сделка.ДатаРемонта
                         select new { Автомобил.КодАвтомобиля, Автомобил.Марка, Автомобил.Модель, Автомобил.НомернойЗнак, Сделка.КодДоговора, Сделка.ДатаРемонта, Сделка.ВидНеисправности, Израсход.Детали, Израсход.Стоимость };
            dataGridView1.DataSource = client;
        }

        
    }
}
