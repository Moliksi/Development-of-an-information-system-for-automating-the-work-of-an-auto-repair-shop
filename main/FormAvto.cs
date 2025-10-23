using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Course_page
{
    public partial class FormAvto : Form
    {
        public FormAvto()
        {
            InitializeComponent();
        }

        private void FormAvto_Load(object sender, EventArgs e)
        {
            this.клиентыTableAdapter.Fill(this.course_paperDataSet15.Клиенты);
            this.автомобильTableAdapter.Fill(this.course_paperDataSet9.Автомобиль);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.автомобильBindingSource.EndEdit();
            try
            {
                this.автомобильTableAdapter.Update(this.course_paperDataSet9.Автомобиль);
            }
            catch
            {
                MessageBox.Show("Vin_Код должен иметь 17 символов");
            }
        }

        
    }
}
