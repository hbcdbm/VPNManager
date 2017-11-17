using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using DotRas;
using System.Text.RegularExpressions;
using System.Threading;

namespace VPNManager
{
    class VPNHelper
    {
        // 系统路径 C:\windows\system32\
        private static string WinDir = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\";
        // rasdial.exe
        private static string RasDialFileName = "rasdial.exe";
        // VPN路径 C:\windows\system32\rasdial.exe
        private static string VPNPROCESS = WinDir + RasDialFileName;
        // VPN地址
        public string IPToPing { get; set; }
        // VPN名称
        public string VPNName { get; set; }
        // VPN用户名
        public string UserName { get; set; }
        // VPN密码
        public string PassWord { get; set; }

        public VPNHelper()
        {
        }
        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="_vpnIP"></param>
        /// <param name="_vpnName"></param>
        /// <param name="_userName"></param>
        /// <param name="_passWord"></param>
        public VPNHelper(string _vpnIP, string _vpnName, string _userName, string _passWord)
        {
            this.IPToPing = _vpnIP;
            this.VPNName = _vpnName;
            this.UserName = _userName;
            this.PassWord = _passWord;
        }
        /// <summary>
        /// 尝试连接VPN(默认VPN)
        /// </summary>
        /// <returns></returns>
        public void TryConnectVPN()
        {
            this.TryConnectVPN(this.VPNName,this.UserName,this.PassWord);
        }
        /// <summary>
        /// 尝试断开连接(默认VPN)
        /// </summary>
        /// <returns></returns>
        public void TryDisConnectVPN()
        {
            this.TryDisConnectVPN(this.VPNName);
        }
        /// <summary>
        /// 创建或更新一个默认的VPN连接
        /// </summary>
        public void CreateOrUpdateVPN()
        {
            this.CreateOrUpdateVPN(this.VPNName, this.IPToPing);
        }
        /// <summary>
        /// 尝试删除连接(默认VPN)
        /// </summary>
        /// <returns></returns>
        public void TryDeleteVPN()
        {
            this.TryDeleteVPN(this.VPNName);
        }
        /// <summary>
        /// 尝试连接VPN(指定VPN名称，用户名，密码)
        /// </summary>
        /// <returns></returns>
        //public void TryConnectVPN(string connVpnName,string connUserName,string connPassWord)
        //{
        //    try
        //    {
        //        string args = string.Format("{0} {1} {2}", connVpnName, connUserName, connPassWord);
        //        ProcessStartInfo myProcess = new ProcessStartInfo(VPNPROCESS, args);
        //        myProcess.CreateNoWindow = true;
        //        myProcess.UseShellExecute = false;
        //        Process.Start(myProcess);
        //    }
        //    catch (Exception Ex)
        //    {
        //        Debug.Assert(false, Ex.ToString());
        //    }
        //}

        public bool TryConnectVPN(string connVpnName, string connUserName, string connPassWord)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            string args = string.Format("{0} {1} {2}", connVpnName, connUserName, connPassWord);
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
            p.Start();
            p.StandardInput.WriteLine(VPNPROCESS + " " + args);
            p.StandardInput.WriteLine("exit");
            string strRst = p.StandardOutput.ReadToEnd();
            p.Close();
            if (strRst.Contains("已连接"))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 尝试断开VPN(指定VPN名称)
        /// </summary>
        /// <returns></returns>
        public void TryDisConnectVPN(string disConnVpnName)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            string args = string.Format(@"{0} /d", disConnVpnName);
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
            p.Start();
            p.StandardInput.WriteLine(VPNPROCESS + " " + args);
            p.StandardInput.WriteLine("exit");
            string strRst = p.StandardOutput.ReadToEnd();
            p.Close();
        }
        /// <summary>
        /// 创建或更新一个VPN连接(指定VPN名称，及IP)
        /// </summary>
        public void CreateOrUpdateVPN(string updateVPNname,string updateVPNip)
        {
            RasDialer dialer = new RasDialer();
            RasPhoneBook allUsersPhoneBook = new RasPhoneBook();
            allUsersPhoneBook.Open(true);
            // 如果已经该名称的VPN已经存在，则更新这个VPN服务器地址
            if (allUsersPhoneBook.Entries.Contains(updateVPNname))
            {
                allUsersPhoneBook.Entries[updateVPNname].PhoneNumber = updateVPNip;
                // 不管当前VPN是否连接，服务器地址的更新总能成功，如果正在连接，则需要VPN重启后才能起作用
                allUsersPhoneBook.Entries[updateVPNname].Update();               
            }
            // 创建一个新VPN
            else
            {
                RasEntry entry = RasEntry.CreateVpnEntry(updateVPNname, updateVPNip, RasVpnStrategy.PptpFirst, RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn));
                allUsersPhoneBook.Entries.Add(entry);
                dialer.EntryName = updateVPNname;
                dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);               
            }
        }
        /// <summary>
        /// 删除指定名称的VPN
        /// 如果VPN正在运行，一样会在电话本里删除，但是不会断开连接，所以，最好是先断开连接，再进行删除操作
        /// </summary>
        /// <param name="delVpnName"></param>
        public void TryDeleteVPN(string delVpnName)
        {
            RasDialer dialer = new RasDialer();
            RasPhoneBook allUsersPhoneBook = new RasPhoneBook();
            allUsersPhoneBook.Open();
            if (allUsersPhoneBook.Entries.Contains(delVpnName))
            {
                allUsersPhoneBook.Entries.Remove(delVpnName);
            }
        }
        /// <summary>
        /// 获取当前正在连接中的VPN名称
        /// </summary>
        public string GetCurrentConnectingVPNNames()
        {
            string ConnectingVPNip = "";
            Process proIP = new Process();
            proIP.StartInfo.FileName = "cmd.exe ";
            proIP.StartInfo.UseShellExecute = false;
            proIP.StartInfo.RedirectStandardInput = true;
            proIP.StartInfo.RedirectStandardOutput = true;
            proIP.StartInfo.RedirectStandardError = true;
            proIP.StartInfo.CreateNoWindow = true;//不显示cmd窗口
            proIP.Start();
            proIP.StandardInput.WriteLine(RasDialFileName);
            proIP.StandardInput.WriteLine("exit");
            // 命令行运行结果
            string strResult = proIP.StandardOutput.ReadToEnd();
            proIP.Close();
            // 用正则表达式匹配命令行结果，只限于简体中文系统哦^_^
            Regex regger = new Regex("已连接(?<vpn>[\\s\\S]*)命令已完成。", RegexOptions.Multiline);
            
            // 如果匹配，则说有正在连接的VPN
            if (regger.IsMatch(strResult))
            {
                ConnectingVPNip = regger.Match(strResult).Groups["vpn"].Value;
                ConnectingVPNip = ConnectingVPNip.Replace("\n", "");
                ConnectingVPNip = ConnectingVPNip.Replace("\r", "");
            }
            // 没有正在连接的VPN，则直接返回一个空List<string>
            return ConnectingVPNip;
        }
    }
}
