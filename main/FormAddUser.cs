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

namespace Course_page
{
    public partial class FormAddUser : Form
    {
        public FormAddUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sqlCommand1.Parameters.AddWithValue("@login_", textBox1.Text);
                sqlCommand1.Parameters.AddWithValue("@password1", textBox2.Text);
                sqlConnection1.Open();
                sqlCommand1.ExecuteNonQuery();
                sqlConnection1.Close();
                MessageBox.Show("Пользователь добавлен!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Невозможно добавить пользователя!");
            }
        }
    }
}
