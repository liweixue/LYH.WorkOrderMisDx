using System;
using System.Data;
using System.Windows.Forms;
using LYH.WorkOrder.Properties;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class Logon : Form
    {
        public bool BLogin;

        public Logon()
        {
            KeyDown += FrmWin_KeyDown;
            InitializeComponent();
        }

        private void FrmWin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    DialogResult = DialogResult.Cancel;
                    break;
            }
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            if (txtUserId.Text == "")
            {
                MessageBox.Show(@"请输入登录帐号!!", Resources.T提示);
                txtUserId.Focus();
            }
            else
            {
                var sql = $"SELECT TOP 1 * FROM IDPASS WHERE ID='{txtUserId.Text.Trim()}'";
                try
                {
                    var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
                    if (dr.Read())
                    {
                        var pass = dr["pass"].ToString().Trim();
                        if (txtPassword.Text.Trim() == pass)
                        {
                            SqlHelper.UserName = dr["name"].ToString().Trim();
                            SqlHelper.UserType = dr["leiq"].ToString().Trim();
                            SqlHelper.DeptId= dr["DeptId"].ToString().Trim();
                            SqlHelper.UserId = txtUserId.Text.Trim();
                            Hide();
                            BLogin = true;
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show(@"密码错误，请重新输入！", Resources.T提示);
                            txtPassword.Text = "";
                            txtPassword.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"此帐号< {txtUserId.Text.Trim()} >不存在，请重新输入!!", Resources.T提示);
                        txtUserId.Text = "";
                        txtPassword.Text = "";
                        txtUserId.Focus();
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"数据库连接错误!" + ex.Message);
                }
            }
        }

        private void txtUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void btnLogoff_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}