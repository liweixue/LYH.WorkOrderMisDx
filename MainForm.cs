using System;
using System.Threading;
using System.Windows.Forms;
using LYH.Framework.BaseUI;
using LYH.Framework.BaseUI.SplashScreen;
using LYH.Framework.Commons;
using LYH.WorkOrder.Properties;
using LYH.WorkOrder.share;
using Timer = System.Timers.Timer;

namespace LYH.WorkOrder
{
    public partial class MainForm : BaseForm
    {
        //private int childFormNumber = 0;
        private readonly Timer _timer = new Timer();

        public MainForm()
        {
            InitializeComponent();
            Splasher.Status = "正在展示相关的内容...";
            Thread.Sleep(100);
            InitUserRelated();
            Splasher.Status = "初始化完毕...";
            Thread.Sleep(50);
            Splasher.Close();

            SetTimerParam();
        }

        private void SetTimerParam()
        {
            _timer.Elapsed += CheckUpdate;
            _timer.Interval = 10 * 60 * 1000;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void 工单录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SqlHelper.UserType == Resources.UT_Input || SqlHelper.UserType == Resources.UT_Check ||
                SqlHelper.UserType == Resources.UT_Admin)
                ChildWinManagement.LoadMdiForm(this, typeof(FrmConstructionInput));
        }

        private void 序价管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (SqlHelper.UserType == Resources.UT_Check || SqlHelper.UserType == Resources.UT_Audit ||
            //    SqlHelper.UserType == Resources.UT_Admin)
            ChildWinManagement.LoadMdiForm(this, typeof(FrmWorkOrderProcUPrice));
        }

        private void 录入审核ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SqlHelper.UserType == Resources.UT_Audit || SqlHelper.UserType == Resources.UT_Admin)
                ChildWinManagement.LoadMdiForm(this, typeof(FrmConstructionAduit));
        }

        //private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    Application.Exit();/*
        //    if (MessageBox.Show("是否确认退出系统", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
        //    {

        //        //e.Cancel = false;
        //        //this.Close();
        //        Application.Exit();
        //    }
        //    else
        //    {
        //        e.Cancel=true;
        //        //Hide();
        //        //MDIParent1 student = new MDIParent1();
        //        //student.Show();
        //    }*/
        //}

        private void MainForm_Load(object sender, EventArgs e)
        {
            _timer.Enabled = true;
        }

        private void InitUserRelated()
        {
            tssl.Text = $"   状态：当前登录用户==>{SqlHelper.UserName}   登录类型==>{SqlHelper.UserType}";
        }

        private void 手工工单制作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SqlHelper.UserType == Resources.UT_Input || SqlHelper.UserType == Resources.UT_Admin ||
                SqlHelper.UserType == Resources.UT_Check)
                ChildWinManagement.LoadMdiForm(this, typeof(FrmContructionHandwriting));
        }

        private void 报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildWinManagement.LoadMdiForm(this, typeof(FrmConstructionRpt));
        }

        private void 密码修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new FrmPwd();
            aboutForm.ShowDialog();
        }

        private void p15工单更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SqlHelper.UserType == Resources.UT_Check || SqlHelper.UserType == Resources.UT_Admin)
                ChildWinManagement.LoadMdiForm(this, typeof(FrmDataImport));
        }

        private void 跨月工单录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SqlHelper.UserType == Resources.UT_Check || SqlHelper.UserType == Resources.UT_Admin)
                ChildWinManagement.LoadMdiForm(this, typeof(FrmBimonthly));
        }

        private void 工单查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildWinManagement.LoadMdiForm(this, typeof(FrmConstructionGather));
        }

        private void 工单接收ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildWinManagement.LoadMdiForm(this, typeof(FrmWorkOrderReceive));
        }

        private void bOM管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildWinManagement.LoadMdiForm(this, typeof(FrmProcCardBom));
        }

        private void 工艺卡管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildWinManagement.LoadMdiForm(this, typeof(FrmProcCard));
        }

        private void 工艺卡录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildWinManagement.LoadMdiForm(this, typeof(FrmProcCardInput));
        }

        private void cNC报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildWinManagement.LoadMdiForm(this, typeof(FrmProcCardRpt));
        }

        private void 班组员工管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildWinManagement.LoadMdiForm(this, typeof(FrmProcCardTeam));
        }

        private void G关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ab = new AboutBox();
            ab.ShowDialog();
        }

        private void 重新登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageDxUtil.ShowYesNoAndWarning("您确定需要重新登录吗？") != DialogResult.Yes) return;
            Hide();
            var logon = new Logon();
            if (DialogResult.OK == logon.ShowDialog())
            {
                CloseAllDocuments();
                InitUserRelated();
            }

            logon.Dispose();
            Show();
        }

        public void CloseAllDocuments()
        {
            foreach (var form in MdiChildren)
            {
                form.Close();
                if (!form.IsDisposed)
                    form.Dispose();
            }
        }

        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            if (MdiChildren.Length <= 1) return;
            foreach (var mdiChild in MdiChildren)
                mdiChild.Width = Width / MdiChildren.Length;
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void CheckUpdate(object sender, EventArgs e)
        {
            UpdaterExtend.CheckUpdate();
        }
    }
}