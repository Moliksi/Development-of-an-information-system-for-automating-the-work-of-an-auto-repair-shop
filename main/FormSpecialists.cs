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
    public partial class FormSpecialists : Form
    {
        public FormSpecialists()
        {
            InitializeComponent();
        }

        private void FormSpecialists_Load(object sender, EventArgs e)
        {
            this.специалистыTableAdapter.Fill(this.course_paperDataSet13.Специалисты);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.специалистыBindingSource.EndEdit();
            this.специалистыTableAdapter.Update(this.course_paperDataSet13.Специалисты);
        }
    }
}
