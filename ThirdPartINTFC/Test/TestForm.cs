using LogUtility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ZIT.ThirdPartINTFC.Model;
using ZIT.ThirdPartINTFC.Utils;

namespace Test
{
    public partial class TestForm : Form
    {
        #region 变量

        private Dictionary<int, string> cphDictionary = new Dictionary<int, string>();

        internal Client Client;

        public static bool blnDo;

        public delegate void DealMessageHandler(string message);

        public delegate void SendPosMsgHandler(double x, double y);

        #endregion 变量

        #region 构造函数

        public TestForm()
        {
            InitializeComponent();
            init();
        }

        #endregion 构造函数

        #region 方法

        private void init()
        {
            blnDo = false;
            cphDictionary.Add(100, "苏A12100");
            cphDictionary.Add(101, "苏A12101");
            cphDictionary.Add(102, "苏A12102");
            cphDictionary.Add(103, "苏A12103");
            StartListen();
        }

        private void StartListen()
        {
            Client = new Client();
            Client.LocalPort = 1003;
            Client.ReceiveEvent += Client_ReceiveEvent;
            Client.SendEvent += Client_SendEvent;
            Client.Start();
        }

        private void Handle5213Message(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new DealMessageHandler(Handle5213Message), new object[] { message });
            }
            else
            {
                JhChargebackresult obj = GetModelFromMsg<JhChargebackresult>(message);
                if (!string.IsNullOrEmpty(obj.Zldbh) && obj.Zldbh == txtZLDBH.Text)
                {
                    txtTDFK.Text = obj.Tdjg + "-" + obj.Jjtdly;
                }
            }
        }

        private void Handle5210Message(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new DealMessageHandler(Handle5210Message), new object[] { message });
            }
            else
            {
                InitControl();
                blnDo = false;
                JhWorkorder obj = GetModelFromMsg<JhWorkorder>(message);
                if (!string.IsNullOrEmpty(obj.Zldbh))
                {
                    this.txtZLDBH.Text = obj.Zldbh;
                    this.txtBJR.Text = obj.Bjr;
                    this.txtBJNR.Text = obj.Bajnr;
                    this.txtBJSJ.Text = obj.Bjsj;
                    this.txtXB.Text = obj.Xb;
                    this.txtLXDH.Text = obj.Lxdh;
                    this.txtSFDZ.Text = obj.Sfdz;
                    btnQS.Enabled = true;
                    btnTD.Enabled = true;
                    txtTDYY.ReadOnly = false;
                }
            }
        }

        private void InitControl()
        {
            foreach (var control in this.Controls)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.TextBox")
                {
                    (control as TextBox).Text = "";
                }
                else if (control.GetType().ToString() == "System.Windows.Forms.Button")
                {
                    (control as Button).Enabled = false;
                }
            }
        }

        private string GetMessage(string message)
        {
            int nBegin = message.IndexOf('[');
            int nEnd = message.IndexOf(']');
            if (nEnd > nBegin + 1 && nBegin >= 0)
            {
                return message.Substring(nBegin, nEnd - nBegin + 1);
            }
            else
            {
                return string.Empty;
            }
        }

        private T GetModelFromMsg<T>(string message)
        {
            T obj = default(T);
            try
            {
                Type type = typeof(T);
                obj = (T)Activator.CreateInstance(type);
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (var property in propertyInfos)
                {
                    property.SetValue(obj, Convert.ToString(GetValueByKey(message, property.Name.ToUpper())));
                }
            }
            catch (Exception e)
            {
                //LogUtility.DataLog.WriteLog(LogLevel.Info, e.Message, new RunningPlace("HandleMessage", "GetModelFromMsg"), "ComErr");
            }
            return obj;
        }

        private string GetValueByKey(string message, string key)
        {
            string strReturn = "";

            Regex reg = new Regex(key + ":(.*?)\\*#");
            if (reg.IsMatch(message))
            {
                Match match = reg.Match(message);
                strReturn = match.Groups[1].Value.Trim();
            }
            return strReturn;
        }

        #endregion 方法

        #region 事件

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                JhSigninfo jh = new JhSigninfo()
                {
                    Zldbh = txtZLDBH.Text,
                    Qsdw = "南京急救指挥中心",
                    Qsr = "张三",
                    Qssj = DateTime.Now.ToString(),
                    Ext1 = "0"
                };
                Client.SendMsg(StringHelper.CombinMsg<JhSigninfo>("5211", jh), true);
                btnQS.Enabled = false;
                btnTD.Enabled = false;
                btnFK.Enabled = true;
                MessageBox.Show("签收成功");
            }
            catch (Exception ex)
            {
            }
        }

        private void btnTD_Click(object sender, EventArgs e)
        {
            try
            {
                JhChargeback jh = new JhChargeback()
                {
                    Zldbh = txtZLDBH.Text,
                    Tddw = "南京急救指挥中心",
                    Tdr = "张三",
                    Tdsj = DateTime.Now.ToString(),
                    Tdyy = txtTDYY.Text,
                    Tdbh = "120" + txtZLDBH.Text,
                    Ext1 = "0"
                };
                Client.SendMsg(StringHelper.CombinMsg<JhChargeback>("5212", jh), true);

                btnQS.Enabled = false;
                btnTD.Enabled = false;
                MessageBox.Show("退单成功");
            }
            catch (Exception ex)
            {
            }
        }

        private void btnPC_Click(object sender, EventArgs e)
        {
            try
            {
                JhAmbulanceinfo jh = new JhAmbulanceinfo()
                {
                    Zldbh = txtZLDBH.Text,
                    Jhccph = cphDictionary[int.Parse(cmbCL.Text)],
                    Ssjg = "南京急救指挥中心",
                    Lxdh = "13333333333",
                    Jsyxm = "赵四",
                    Ysxm = "徐某",
                    Ysdh = "13344444444",
                    Gpsstatus = "1",
                    Ext1 = "0"
                };
                Client.SendMsg(StringHelper.CombinMsg<JhAmbulanceinfo>("5215", jh), true);
                btnPC.Enabled = false;
                btnCC.Enabled = true;
                MessageBox.Show("派车成功");
            }
            catch (Exception ex)
            {
            }
        }

        private void btnFK_Click(object sender, EventArgs e)
        {
            try
            {
                JhFeedback jh = new JhFeedback()
                {
                    Zldbh = txtZLDBH.Text,
                    Fkdbh = "120" + txtZLDBH.Text,
                    Fkdw = "南京急救指挥中心",
                    Fkr = "张三",
                    Fksj = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    Fknr = txtFK.Text,
                    Fkjqlb = "过程反馈",
                    Ext1 = "0"
                };
                Client.SendMsg(StringHelper.CombinMsg<JhFeedback>("5214", jh), true);
                btnFK.Enabled = false;
                btnPC.Enabled = true;
                cmbCL.SelectedIndex = 0;
                MessageBox.Show("反馈成功");
            }
            catch (Exception ex)
            {
            }
        }

        private void Client_SendEvent(string message, System.Net.IPEndPoint ipep)
        {
            LogUtility.DataLog.WriteLog(LogLevel.Info, string.Format("向地址：{0}发送消息:{1}", Convert.ToString(ipep), message), new RunningPlace("BSSClient", "Client_SendEvent"), "ToBssServer");
        }

        private void Client_ReceiveEvent(string message, System.Net.IPEndPoint ipep)
        {
            message = GetMessage(message);
            string strMessageId = message.Substring(1, 4);
            switch (strMessageId)
            {
                case "5210":
                    Handle5210Message(message);
                    break;

                case "5213":
                    Handle5213Message(message);
                    break;

                case "3000":
                    Client.SendMsg(message, false);
                    break;

                default:
                    break;
            }
        }

        #endregion 事件

        private void btnCC_Click(object sender, EventArgs e)
        {
            try
            {
                blnDo = true;
                ThreadPool.QueueUserWorkItem(SendJWDInfo);
                SendStatusInfo(btnCC.Text);

                btnCC.Enabled = false;
                btnRWZZ.Enabled = true;
                btnDDXC.Enabled = true;
                MessageBox.Show("出车成功");
            }
            catch (Exception ex)
            {
            }
        }

        private void SendJWDInfo(object state)
        {
            double x = 114.38060;
            double y = 22.74856;
            while (blnDo)
            {
                x = x + 0.1;
                y = y + 0.05;

                SendPosMsg(x, y);
                Thread.Sleep(1000 * 10);
            }
        }

        private void SendPosMsg(double x, double y)
        {
            if (this.cmbCL.InvokeRequired)
            {
                this.BeginInvoke(new SendPosMsgHandler(SendPosMsg), new object[] { x, y });
            }
            else
            {
                JhAmbulanceposition jh = new JhAmbulanceposition()
                {
                    Zldbh = txtZLDBH.Text,
                    Jhccph = cphDictionary[int.Parse(cmbCL.Text)],
                    Xzb = x.ToString(),
                    Yzb = y.ToString(),
                    Ext1 = "0",
                    Time = DateTime.Now.ToString(),
                };
                Client.SendMsg(StringHelper.CombinMsg<JhAmbulanceposition>("5216", jh), true);
            }
        }

        private void btnDDXC_Click(object sender, EventArgs e)
        {
            try
            {
                SendStatusInfo(btnDDXC.Text);
                btnDDXC.Enabled = false;
                btnBRSC.Enabled = true;
                MessageBox.Show("到达现场成功");
            }
            catch (Exception ex)
            {
            }
        }

        private void btnBRSC_Click(object sender, EventArgs e)
        {
            try
            {
                SendStatusInfo(btnBRSC.Text);
                btnBRSC.Enabled = false;
                btnSDYY.Enabled = true;
                MessageBox.Show("病人上车成功");
            }
            catch (Exception ex)
            {
            }
        }

        private void btnSDYY_Click(object sender, EventArgs e)
        {
            try
            {
                SendStatusInfo(btnSDYY.Text);
                btnSDYY.Enabled = false;
                btnRWWC.Enabled = true;
                MessageBox.Show("送达医院成功");
            }
            catch (Exception ex)
            {
            }
        }

        private void btnRWWC_Click(object sender, EventArgs e)
        {
            try
            {
                blnDo = false;
                SendStatusInfo(btnRWWC.Text);
                btnRWWC.Enabled = false;
                btnRWZZ.Enabled = false;
                MessageBox.Show("任务完成成功");
            }
            catch (Exception ex)
            {
            }
        }

        private void btnRWZZ_Click(object sender, EventArgs e)
        {
            try
            {
                blnDo = false;
                SendStatusInfo(btnRWZZ.Text);
                btnRWZZ.Enabled = false;
                MessageBox.Show("任务中止成功");
            }
            catch (Exception ex)
            {
            }
        }

        private void SendStatusInfo(string text)
        {
            JhAmbulancestatus jhAmbulancestatus = new JhAmbulancestatus()
            {
                Zldbh = txtZLDBH.Text,
                Jhccph = cphDictionary[int.Parse(cmbCL.Text)],
                Status = text,
                Checi = "01",
                Time = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Ext1 = "0"
            };
            Client.SendMsg(StringHelper.CombinMsg<JhAmbulancestatus>("5217", jhAmbulancestatus), true);
        }
    }
}