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
    public partial class FormRepair : Form
    {
        public FormRepair()
        {
            InitializeComponent();
        }

        private void FormRepair_Load(object sender, EventArgs e)
        {
            this.ремонтTableAdapter.Fill(this.course_paperDataSet12.Ремонт);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ремонтBindingSource.EndEdit();
            this.ремонтTableAdapter.Update(this.course_paperDataSet12.Ремонт);
        }
    }
}
