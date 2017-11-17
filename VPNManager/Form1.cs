using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using System.Diagnostics;
using System.Net;

namespace VPNManager
{
    public partial class Form1 : Form
    {
        string strAccount = "";
        string strPsw = "";
        int nCurrent;
        string[] IPLIST;
        private static string strVPNname = "vpn1";
        VPNHelper vpnh;
        int nSemiHour = 0;
        public delegate void SetCtrlWindowTextdelegate(TextBox tb, string strText);

        public void SetCtrlWindowText(TextBox tb, string strText)
        {
            if (tb.InvokeRequired)
            {
                SetCtrlWindowTextdelegate handler = new SetCtrlWindowTextdelegate(SetCtrlWindowText);
                tb.Invoke(handler, new object[] { tb, strText });
            }
            else
            {
                tb.Text = strText;
            }
        }

        public Form1()
        {
            InitializeComponent();
            vpnh  = new VPNHelper();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(Thread_job);
            t.Start();
        }

        private string ChooseCurrentVPNip()
        {
            string strVPNip = IPLIST[nCurrent++];
            if (nCurrent == IPLIST.Length)
                nCurrent = 0;
            return strVPNip;
        }

        private void SaveCurrentVPNip(string strCurrentVPNip)
        {
            File.WriteAllText("now", strCurrentVPNip);
        }

        private void Log(string strText)
        {
            string strTmp = DateTime.Now + " " + strText + "\r\n";
            File.AppendAllText("Log.txt", strTmp);
        }

        private static bool CmdPing(string strIp) 
        {     
            Process p = new Process(); 
            p.StartInfo.FileName = "cmd.exe";
            //设定程序名   
            p.StartInfo.UseShellExecute = false; 
            //关闭Shell的使用   
            p.StartInfo.RedirectStandardInput = true;
            //重定向标准输入   
            p.StartInfo.RedirectStandardOutput = true;
            //重定向标准输出   
            p.StartInfo.RedirectStandardError = true;
            //重定向错误输出   
            p.StartInfo.CreateNoWindow = true;
            //设置不显示窗口   
            string pingrst = "";
            p.Start(); 
            p.StandardInput.WriteLine("ping " + strIp);
            p.StandardInput.WriteLine("exit");
            string strRst = p.StandardOutput.ReadToEnd();
            if (strRst.IndexOf("% 丢失") != -1 || strRst.IndexOf("% loss") != -1)
            {
                if (strRst.IndexOf("100% 丢失") != -1 || strRst.IndexOf("100% loss") != -1)
                    pingrst = "超时";
                else
                    pingrst = "连接";
            }
            p.Close();
            if (pingrst == "连接")
                return true;
            else
                return false;
        } 


        private bool PING(string strhost)
        {
            Ping pingSender = new Ping();
            int timeout = 250;
            PingReply reply = pingSender.Send(strhost, timeout);
            if (reply.Status == IPStatus.Success)
                return true;
            else
                return false;
        }

        private void Thread_job()
        {
            try
            {
                //读取VPN地址列表
                IPLIST = File.ReadAllLines("vpnlist.txt");
                if (File.Exists("now"))
                {
                    string strNowIp = File.ReadAllText("now");
                    for (int i = 0; i < IPLIST.Length; i++)
                    {
                        if (strNowIp == IPLIST[i])
                        {
                            nCurrent = i;
                            break;
                        }
                    }
                    nCurrent++;
                }
                else
                {
                    nCurrent = 0;
                }
                if (nCurrent == IPLIST.Length)
                    nCurrent = 0;
                string strVPNip = "";
                try
                {
                    do
                    {
                        vpnh.TryDisConnectVPN(strVPNname);
                        Thread.Sleep(2000);
                        strVPNip = ChooseCurrentVPNip();
                        vpnh.CreateOrUpdateVPN(strVPNname, strVPNip);
                        SetCtrlWindowText(tb_VPNip, strVPNip);
                        SetCtrlWindowText(tb_VPNstate, "正在连接");
                        Thread.Sleep(1000);
                        if (!vpnh.TryConnectVPN(strVPNname, strAccount, strPsw))
                            continue;
                        SetCtrlWindowText(tb_VPNstate, "已连接，正在测试连通");
                        Thread.Sleep(4000);
                    }
                    while (!CmdPing("www.google.com"));
                    SetCtrlWindowText(tb_VPNstate, "已连接且连通");
                    SetCtrlWindowText(tb_nexttime, DateTime.Now.AddHours(6).ToString());
                    SaveCurrentVPNip(strVPNip);
                    Log("已更换使用" + strVPNip);
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetCtrlWindowText(tb_VPNname, strVPNname);
            DateTime dt = DateTime.Now;
            SetCtrlWindowText(tb_starttime, dt.ToString());
            SetCtrlWindowText(tb_nexttime, dt.AddHours(6).ToString());
            Thread t = new Thread(Thread_job);
            t.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            System.Environment.Exit(0); 
        }

        private void checkvpntimer_Tick(object sender, EventArgs e)
        {
            //nSemiHour++;
            //每半小时检测VPN连接状态
            string strVPNip = "";
            while (!CmdPing("www.google.com"))
            {
                SetCtrlWindowText(tb_VPNstate, "未连通");
                vpnh.TryDisConnectVPN(strVPNname);
                Thread.Sleep(2000);
                strVPNip = ChooseCurrentVPNip();
                vpnh.CreateOrUpdateVPN(strVPNname, strVPNip);
                SetCtrlWindowText(tb_VPNip, strVPNip);
                SetCtrlWindowText(tb_VPNstate, "正在连接");
                Thread.Sleep(1000);
                if (!vpnh.TryConnectVPN(strVPNname, strAccount, strPsw))
                {
                    continue;
                }
                SetCtrlWindowText(tb_VPNstate, "已连接，正在测试连通");
                Thread.Sleep(4000);
            }
            SetCtrlWindowText(tb_VPNstate, "已连接且连通");
            if (strVPNip != "")
            {
                SaveCurrentVPNip(strVPNip);
                Log("已更换使用" + strVPNip);
            }
            //if (nSemiHour == 24)
            //{
            //    nSemiHour = 0;
            //    //每12小时更换VPN地址
            //    Thread t = new Thread(Thread_job);
            //    t.Start();
            //}
        }
    }
}
