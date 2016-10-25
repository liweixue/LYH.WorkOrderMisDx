using System;
using System.Data;
using System.Windows.Forms;
using LYH.Framework.Commons;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmPwd : Form
    {
        public FrmPwd()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            textBox1.Text = SqlHelper.UserId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sql = $"SELECT TOP 1 * FROM IDPASS WHERE ID='{textBox1.Text.Trim()}'";
            var s5Dr5 = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            if (s5Dr5.HasRows)
            {
                s5Dr5.Read();
                var adss = s5Dr5["pass"].ToString().Trim();
                if (textBox2.Text.Trim() == adss)
                {
                    if (textBox3.Text.Trim() == textBox4.Text.Trim())
                    {
                        sql = "";
                        sql = $"UPDATE IDPASS set pass='{textBox3.Text.Trim()}' WHERE ID='{textBox1.Text.Trim()}'";
                        SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("新密码输入不一致，请重新输入！", "提示");
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox3.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("当前密码与登录的密码不一致，请重新输入！", "提示");
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox2.Focus();
                }
            }
        }
    }
}
