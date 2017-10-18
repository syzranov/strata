using System.Drawing;
using System.Windows.Forms;

namespace Strata
{
    public partial class ToolWindowControl : UserControl
    {
        public ToolWindowControl()
        {
            InitializeComponent();
        }
        public string Header { get { return labelHeader.Text; } set { labelHeader.Text = value; } }
    }
}
