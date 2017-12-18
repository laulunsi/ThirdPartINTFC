using LogUtility;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ZIT.ThirdPartINTFC.BLL;
using ZIT.ThirdPartINTFC.Model;
using ZIT.ThirdPartINTFC.Utils;

namespace ZIT.ThirdPartINTFC.UI
{
    public partial class MainUI : Form
    {
        /// <summary>
        /// 调用初始化网络委托
        /// </summary>
        public delegate void InvokeInitNetwork();

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainUI()
        {
            LogUtility.DataLog.WriteLog(LogLevel.Info, "软件开始启动", new RunningPlace("MainUI", "Start"), "OP");
            InitializeComponent();
            this.Text = SysParameters.SoftName;
        }

        /// <summary>
        /// 窗体加载函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainUI_Load(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();
                MethodInvoker init = new MethodInvoker(InitProgram);
                init.BeginInvoke(null, null);
            }
            catch (Exception ex)
            {
                LogUtility.DataLog.WriteLog(LogUtility.LogLevel.Info, ex.Message, new LogUtility.RunningPlace("MainUI", "MainUI_Load"), "UIErr");
            }
        }

        /// <summary>
        /// 初始化应用程序
        /// </summary>
        private void InitProgram()
        {
            try
            {
                Core control = Core.GetInstance();
                control.StatusChangedEvent += Server_StatusChangedEvent;
                control.Start();
                LogUtility.DataLog.WriteLog(LogLevel.Info, "软件启动完毕", new RunningPlace("MainUI", "Start"), "OP");
            }
            catch (Exception ex)
            {
                LogUtility.DataLog.WriteLog(LogUtility.LogLevel.Info, ex.Message, new LogUtility.RunningPlace("MainUI", "InitProgram"), "UIErr");
            }
        }

        /// <summary>
        /// 业务服务器 GPS业务服务器 数据库连接的状态变化
        /// </summary>
        /// <param name="status"></param>
        private void Server_StatusChangedEvent(StatusChanged status)
        {
            try
            {
                object o = null;
                switch (status.Module)
                {
                    case FunModule.Bs:
                        o = lblBssConnectStatus;
                        break;

                    case FunModule.Gs:
                        o = lblGpsConnectStatus;
                        break;

                    case FunModule.Db:
                        o = lblDbConnectStatus;
                        break;
                }
                (o as Label)?.BeginInvoke(new MethodInvoker(() =>
                {
                    switch (status.Status)
                    {
                        case ConStatus.DisConnected:
                            (o as Label).Text = "断开";
                            (o as Label).ForeColor = Color.Red;
                            break;

                        case ConStatus.Connected:
                            (o as Label).Text = "已连接";
                            (o as Label).ForeColor = Color.Green;
                            break;

                        case ConStatus.Login:
                            (o as Label).Text = "已登录";
                            (o as Label).ForeColor = Color.Green;
                            break;

                        default:
                            break;
                    }
                }));
            }
            catch (Exception ex)
            {
                LogUtility.DataLog.WriteLog(LogUtility.LogLevel.Info, ex.Message, new LogUtility.RunningPlace("MainUI", "Server_StatusChangedEvent"), "UIErr");
            }
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "确定要退出么？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Core.GetInstance().Stop();
                    Thread.Sleep(1000);
                    LogUtility.DataLog.WriteLog(LogLevel.Info, "软件退出", new RunningPlace("MainUI", "Start"), "OP");
                    System.Environment.Exit(System.Environment.ExitCode);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (System.Exception ex)
            {
                LogUtility.DataLog.WriteLog(LogUtility.LogLevel.Info, ex.Message, new LogUtility.RunningPlace("MainUI", "MainUI_FormClosing"), "UIErr");
            }
        }

        private void MenuItemViewLog_Click(object sender, EventArgs e)
        {
            try
            {
                string strLogPath;
                strLogPath = Directory.GetCurrentDirectory();
                strLogPath += "\\LogDF\\";
                if (Directory.Exists(strLogPath))
                {
                    Process.Start("explorer.exe", strLogPath);
                }
                else
                {
                    MessageBox.Show("未检测到日志文件目录！");
                }
            }
            catch { }
        }

        private void MenuItemExitSystem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "确定要退出么？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Core.GetInstance().Stop();
                    Thread.Sleep(1000);
                    LogUtility.DataLog.WriteLog(LogLevel.Info, "软件退出", new RunningPlace("MainUI", "Start"), "OP");
                    System.Environment.Exit(System.Environment.ExitCode);
                }
            }
            catch (System.Exception ex)
            {
                LogUtility.DataLog.WriteLog(LogUtility.LogLevel.Info, ex.Message, new LogUtility.RunningPlace("MainUI", "menuItemExitSystem_Click"), "UIErr");
            }
        }

        private void MenuItemAbout_Click(object sender, EventArgs e)
        {
            FrmAbout about = new FrmAbout();
            about.ShowDialog();
        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
        }
    }
}