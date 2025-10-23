using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_page
{
    public partial class FormInputAdministrator : Form
    {
        public FormInputAdministrator()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = "Putin";
            string password = "123";
            if (textBox1.Text == login && textBox2.Text == password)
            {
                FormAdministrator myForm10 = new FormAdministrator();
                Hide();
                myForm10.Show();                
            }
            else MessageBox.Show("Неправильный логин или пароль");
        }
    }
}
