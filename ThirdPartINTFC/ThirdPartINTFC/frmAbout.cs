using System.Windows.Forms;
using ZIT.ThirdPartINTFC.Utils;

namespace ZIT.ThirdPartINTFC.UI
{
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();
            this.Text = "关于-" + SysParameters.SoftName;
            this.label3.Text = string.Format("中兴120急救指挥调度系统-{0}", SysParameters.SoftName);
        }
    }
}