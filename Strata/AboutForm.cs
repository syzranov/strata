using System;
using System.Windows.Forms;

namespace Strata
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            labelName.Text = EnvironmentVariables.GetProjectName();
            button1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void AboutForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
        private void AboutForm_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            var form = new HelpForm();
            form.Show();

            hlpevent.Handled = true;
        }
    }
}