namespace LYH.WorkOrder
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private new System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.录入管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工单录入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.手工工单制作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.p15工单更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.跨月工单录入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.序价管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.录入审核ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工单查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cNCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.班组员工管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工单接收ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bOM管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工艺卡管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工艺卡录入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cNC报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.密码修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.h帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.G关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.录入管理ToolStripMenuItem,
            this.序价管理ToolStripMenuItem,
            this.录入审核ToolStripMenuItem,
            this.报表ToolStripMenuItem,
            this.工单查询ToolStripMenuItem,
            this.cNCToolStripMenuItem,
            this.密码修改ToolStripMenuItem,
            this.h帮助ToolStripMenuItem,
            this.重新登录ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 录入管理ToolStripMenuItem
            // 
            this.录入管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.工单录入ToolStripMenuItem,
            this.手工工单制作ToolStripMenuItem,
            this.p15工单更新ToolStripMenuItem,
            this.跨月工单录入ToolStripMenuItem});
            this.录入管理ToolStripMenuItem.Name = "录入管理ToolStripMenuItem";
            this.录入管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.录入管理ToolStripMenuItem.Text = "录入管理";
            // 
            // 工单录入ToolStripMenuItem
            // 
            this.工单录入ToolStripMenuItem.Name = "工单录入ToolStripMenuItem";
            this.工单录入ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.工单录入ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.工单录入ToolStripMenuItem.Text = "工单录入";
            this.工单录入ToolStripMenuItem.Click += new System.EventHandler(this.工单录入ToolStripMenuItem_Click);
            // 
            // 手工工单制作ToolStripMenuItem
            // 
            this.手工工单制作ToolStripMenuItem.Name = "手工工单制作ToolStripMenuItem";
            this.手工工单制作ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.手工工单制作ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.手工工单制作ToolStripMenuItem.Text = "手写工单录入";
            this.手工工单制作ToolStripMenuItem.Click += new System.EventHandler(this.手工工单制作ToolStripMenuItem_Click);
            // 
            // p15工单更新ToolStripMenuItem
            // 
            this.p15工单更新ToolStripMenuItem.Name = "p15工单更新ToolStripMenuItem";
            this.p15工单更新ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.p15工单更新ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.p15工单更新ToolStripMenuItem.Text = "C15工单更新";
            this.p15工单更新ToolStripMenuItem.Click += new System.EventHandler(this.p15工单更新ToolStripMenuItem_Click);
            // 
            // 跨月工单录入ToolStripMenuItem
            // 
            this.跨月工单录入ToolStripMenuItem.Name = "跨月工单录入ToolStripMenuItem";
            this.跨月工单录入ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.跨月工单录入ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.跨月工单录入ToolStripMenuItem.Text = "跨月工单录入";
            this.跨月工单录入ToolStripMenuItem.Click += new System.EventHandler(this.跨月工单录入ToolStripMenuItem_Click);
            // 
            // 序价管理ToolStripMenuItem
            // 
            this.序价管理ToolStripMenuItem.Name = "序价管理ToolStripMenuItem";
            this.序价管理ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.序价管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.序价管理ToolStripMenuItem.Text = "序价管理";
            this.序价管理ToolStripMenuItem.Click += new System.EventHandler(this.序价管理ToolStripMenuItem_Click);
            // 
            // 录入审核ToolStripMenuItem
            // 
            this.录入审核ToolStripMenuItem.Name = "录入审核ToolStripMenuItem";
            this.录入审核ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.录入审核ToolStripMenuItem.Size = new System.Drawing.Size(89, 21);
            this.录入审核ToolStripMenuItem.Text = "录入审核(F8)";
            this.录入审核ToolStripMenuItem.Click += new System.EventHandler(this.录入审核ToolStripMenuItem_Click);
            // 
            // 报表ToolStripMenuItem
            // 
            this.报表ToolStripMenuItem.Name = "报表ToolStripMenuItem";
            this.报表ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.报表ToolStripMenuItem.Size = new System.Drawing.Size(89, 21);
            this.报表ToolStripMenuItem.Text = "工单报表(F4)";
            this.报表ToolStripMenuItem.Click += new System.EventHandler(this.报表ToolStripMenuItem_Click);
            // 
            // 工单查询ToolStripMenuItem
            // 
            this.工单查询ToolStripMenuItem.Name = "工单查询ToolStripMenuItem";
            this.工单查询ToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.工单查询ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.工单查询ToolStripMenuItem.Size = new System.Drawing.Size(89, 21);
            this.工单查询ToolStripMenuItem.Text = "工单查询(F3)";
            this.工单查询ToolStripMenuItem.Click += new System.EventHandler(this.工单查询ToolStripMenuItem_Click);
            // 
            // cNCToolStripMenuItem
            // 
            this.cNCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.班组员工管理ToolStripMenuItem,
            this.工单接收ToolStripMenuItem,
            this.bOM管理ToolStripMenuItem,
            this.工艺卡管理ToolStripMenuItem,
            this.工艺卡录入ToolStripMenuItem,
            this.cNC报表ToolStripMenuItem});
            this.cNCToolStripMenuItem.Name = "cNCToolStripMenuItem";
            this.cNCToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.cNCToolStripMenuItem.Size = new System.Drawing.Size(87, 21);
            this.cNCToolStripMenuItem.Text = "&Z工艺卡管理";
            // 
            // 班组员工管理ToolStripMenuItem
            // 
            this.班组员工管理ToolStripMenuItem.Name = "班组员工管理ToolStripMenuItem";
            this.班组员工管理ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.班组员工管理ToolStripMenuItem.Text = "(&1)员工管理";
            this.班组员工管理ToolStripMenuItem.Click += new System.EventHandler(this.班组员工管理ToolStripMenuItem_Click);
            // 
            // 工单接收ToolStripMenuItem
            // 
            this.工单接收ToolStripMenuItem.Name = "工单接收ToolStripMenuItem";
            this.工单接收ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.工单接收ToolStripMenuItem.Text = "(&2)工单接收";
            this.工单接收ToolStripMenuItem.Click += new System.EventHandler(this.工单接收ToolStripMenuItem_Click);
            // 
            // bOM管理ToolStripMenuItem
            // 
            this.bOM管理ToolStripMenuItem.Name = "bOM管理ToolStripMenuItem";
            this.bOM管理ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.bOM管理ToolStripMenuItem.Text = "(&3)分解工艺";
            this.bOM管理ToolStripMenuItem.Click += new System.EventHandler(this.bOM管理ToolStripMenuItem_Click);
            // 
            // 工艺卡管理ToolStripMenuItem
            // 
            this.工艺卡管理ToolStripMenuItem.Name = "工艺卡管理ToolStripMenuItem";
            this.工艺卡管理ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.工艺卡管理ToolStripMenuItem.Text = "(&4)生成及打印";
            this.工艺卡管理ToolStripMenuItem.Click += new System.EventHandler(this.工艺卡管理ToolStripMenuItem_Click);
            // 
            // 工艺卡录入ToolStripMenuItem
            // 
            this.工艺卡录入ToolStripMenuItem.Name = "工艺卡录入ToolStripMenuItem";
            this.工艺卡录入ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.工艺卡录入ToolStripMenuItem.Text = "(&5)工艺卡录入";
            this.工艺卡录入ToolStripMenuItem.Click += new System.EventHandler(this.工艺卡录入ToolStripMenuItem_Click);
            // 
            // cNC报表ToolStripMenuItem
            // 
            this.cNC报表ToolStripMenuItem.Name = "cNC报表ToolStripMenuItem";
            this.cNC报表ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.cNC报表ToolStripMenuItem.Text = "(&6)报表";
            this.cNC报表ToolStripMenuItem.Click += new System.EventHandler(this.cNC报表ToolStripMenuItem_Click);
            // 
            // 密码修改ToolStripMenuItem
            // 
            this.密码修改ToolStripMenuItem.Name = "密码修改ToolStripMenuItem";
            this.密码修改ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.密码修改ToolStripMenuItem.Text = "密码修改";
            this.密码修改ToolStripMenuItem.Click += new System.EventHandler(this.密码修改ToolStripMenuItem_Click);
            // 
            // h帮助ToolStripMenuItem
            // 
            this.h帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.G关于ToolStripMenuItem});
            this.h帮助ToolStripMenuItem.Name = "h帮助ToolStripMenuItem";
            this.h帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.h帮助ToolStripMenuItem.Text = "帮助";
            // 
            // G关于ToolStripMenuItem
            // 
            this.G关于ToolStripMenuItem.Name = "G关于ToolStripMenuItem";
            this.G关于ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.G关于ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.G关于ToolStripMenuItem.Text = "关于本软件";
            this.G关于ToolStripMenuItem.Click += new System.EventHandler(this.G关于ToolStripMenuItem_Click);
            // 
            // 重新登录ToolStripMenuItem
            // 
            this.重新登录ToolStripMenuItem.Name = "重新登录ToolStripMenuItem";
            this.重新登录ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.重新登录ToolStripMenuItem.Size = new System.Drawing.Size(96, 21);
            this.重新登录ToolStripMenuItem.Text = "重新登录(F11)";
            this.重新登录ToolStripMenuItem.Click += new System.EventHandler(this.重新登录ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 707);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl
            // 
            this.tssl.Name = "tssl";
            this.tssl.Size = new System.Drawing.Size(44, 17);
            this.tssl.Text = "状态栏";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // MainForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "工单管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MdiChildActivate += new System.EventHandler(this.MainForm_MdiChildActivate);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 录入管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工单录入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 序价管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 录入审核ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 手工工单制作ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 密码修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel tssl;
        private System.Windows.Forms.ToolStripMenuItem p15工单更新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 跨月工单录入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工单查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cNCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工单接收ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bOM管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工艺卡管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工艺卡录入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cNC报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 班组员工管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem h帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem G关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重新登录ToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}



