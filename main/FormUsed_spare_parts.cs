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
    public partial class FormUsed_spare_parts : Form
    {
        public FormUsed_spare_parts()
        {
            InitializeComponent();
        }

        private void FormUsed_spare_parts_Load(object sender, EventArgs e)
        {
          
            this.израсходованныеЗапчастиTableAdapter.Fill(this.course_paperDataSet141.ИзрасходованныеЗапчасти);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.израсходованныеЗапчастиBindingSource.EndEdit();
            this.израсходованныеЗапчастиTableAdapter.Update(this.course_paperDataSet141.ИзрасходованныеЗапчасти);
        }
    }
}
