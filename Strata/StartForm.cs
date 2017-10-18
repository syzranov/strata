using System;
using System.Windows.Forms;

namespace Strata
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            labelName.Text = EnvironmentVariables.GetProjectName();
        }

        public StartFormDialogResultEnum StartFormDialogResult { get; set; }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            StartFormDialogResult = StartFormDialogResultEnum.None;
            Close();
        }

        private void linkLabelHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartFormDialogResult = StartFormDialogResultEnum.ShowHelp;
            Close();
        }

        private void linkLabelAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartFormDialogResult = StartFormDialogResultEnum.ShowAbout;
            Close();
        }

        private void buttonCreateProject_Click(object sender, EventArgs e)
        {
            StartFormDialogResult = StartFormDialogResultEnum.CreateProject;
            Close();
        }

        private void buttonOpenProject_Click(object sender, EventArgs e)
        {
            StartFormDialogResult = StartFormDialogResultEnum.OpenProject;
            Close();
        }

        private void StartForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                StartFormDialogResult = StartFormDialogResultEnum.None;
                Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                buttonCreateProject_Click(this, null);
            }
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            buttonCreateProject.Focus();
        }
    }
}