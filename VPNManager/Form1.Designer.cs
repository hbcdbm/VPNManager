namespace VPNManager
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_VPNname = new System.Windows.Forms.TextBox();
            this.tb_VPNip = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_VPNstate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tooltipmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkvpntimer = new System.Windows.Forms.Timer(this.components);
            this.tb_starttime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_nexttime = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tooltipmenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(299, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "功能：每半小时检测修复VPN连接，每6小时更换VPN地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "现用VPN名称：";
            // 
            // tb_VPNname
            // 
            this.tb_VPNname.Location = new System.Drawing.Point(119, 48);
            this.tb_VPNname.Name = "tb_VPNname";
            this.tb_VPNname.ReadOnly = true;
            this.tb_VPNname.Size = new System.Drawing.Size(170, 21);
            this.tb_VPNname.TabIndex = 2;
            // 
            // tb_VPNip
            // 
            this.tb_VPNip.Location = new System.Drawing.Point(119, 76);
            this.tb_VPNip.Name = "tb_VPNip";
            this.tb_VPNip.ReadOnly = true;
            this.tb_VPNip.Size = new System.Drawing.Size(170, 21);
            this.tb_VPNip.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "现用VPN地址：";
            // 
            // tb_VPNstate
            // 
            this.tb_VPNstate.Location = new System.Drawing.Point(119, 105);
            this.tb_VPNstate.Name = "tb_VPNstate";
            this.tb_VPNstate.ReadOnly = true;
            this.tb_VPNstate.Size = new System.Drawing.Size(170, 21);
            this.tb_VPNstate.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "VPN连接状态：";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.tooltipmenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "VPN管理(无忧府)";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // tooltipmenu
            // 
            this.tooltipmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出ToolStripMenuItem});
            this.tooltipmenu.Name = "tooltipmenu";
            this.tooltipmenu.Size = new System.Drawing.Size(101, 26);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // checkvpntimer
            // 
            this.checkvpntimer.Enabled = true;
            this.checkvpntimer.Interval = 1800000;
            this.checkvpntimer.Tick += new System.EventHandler(this.checkvpntimer_Tick);
            // 
            // tb_starttime
            // 
            this.tb_starttime.Location = new System.Drawing.Point(119, 132);
            this.tb_starttime.Name = "tb_starttime";
            this.tb_starttime.ReadOnly = true;
            this.tb_starttime.Size = new System.Drawing.Size(170, 21);
            this.tb_starttime.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "启动时间：";
            // 
            // tb_nexttime
            // 
            this.tb_nexttime.Location = new System.Drawing.Point(119, 159);
            this.tb_nexttime.Name = "tb_nexttime";
            this.tb_nexttime.ReadOnly = true;
            this.tb_nexttime.Size = new System.Drawing.Size(170, 21);
            this.tb_nexttime.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "下一次更换时间：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 190);
            this.Controls.Add(this.tb_nexttime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_starttime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_VPNstate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_VPNip);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_VPNname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VPN管理(无忧府)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tooltipmenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_VPNname;
        private System.Windows.Forms.TextBox tb_VPNip;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_VPNstate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip tooltipmenu;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.Timer checkvpntimer;
        private System.Windows.Forms.TextBox tb_starttime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_nexttime;
        private System.Windows.Forms.Label label6;

    }
}

