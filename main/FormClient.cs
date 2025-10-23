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
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            this.клиентыTableAdapter.Fill(this.course_paperDataSet10.Клиенты);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.клиентыBindingSource.EndEdit();
            try
            {
                this.клиентыTableAdapter.Update(this.course_paperDataSet10.Клиенты);
            }
            catch
            {
                MessageBox.Show("Номер телефона должен иметь 10 символов");
            }
        }
    }
}
