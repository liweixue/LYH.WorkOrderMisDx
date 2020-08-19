using System;
using System.Data;
using System.Windows.Forms;
using LYH.WorkOrder.Properties;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmUser : Form
    {
        public FrmUser()
        {
            InitializeComponent();
            GetUserInfo();
        }
        private string _mMid;

        private void GetUserInfo()
        {
            panel1.Visible = false;
            panel2.Visible = false;
            const string sql = "SELECT lu as '序号',ID as '帐号',pass as '密码',name as '用户姓名',leiq as '用户类型' FROM IDPASS";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 46;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Sort(dataGridView1.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
        }

        private void button2_Click(object sender, EventArgs e)//修改
        {
            _mMid = dataGridView1.SelectedCells[0].Value.ToString().Trim();
            //MMid = Convert.ToInt32(dataGridView1.SelectedCells[0].Value);
            panel2.Visible = true;
            var sql = $"SELECT * FROM IDPASS WHERE lu='{_mMid}'";
            var sdr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            sdr.Read();
            textBox8.Text = sdr["lu"].ToString().Trim();
            textBox7.Text = sdr["ID"].ToString().Trim();
            textBox6.Text = sdr["pass"].ToString().Trim();
            textBox5.Text = sdr["name"].ToString().Trim();
            comboBox2.Text = sdr["leiq"].ToString().Trim();
            sdr.Close();
        }

        private void button1_Click(object sender, EventArgs e)//新建
        {
            panel1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)//删除
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("提示：请选择要删除的用户信息！", Resources.J警告);
            }
            else
            {
                var mMid = dataGridView1.SelectedCells[0].Value.ToString().Trim();
                if (MessageBox.Show($"是否要删除< {mMid} >序号用户", Resources.J警告, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    var sql = $"delete FROM IDPASS WHERE lu='{mMid}'";
                    SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text,sql);
                    GetUserInfo();
                }
                else
                {
                    GetUserInfo();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)//退出
        {
            Close();
        }

        private void button8_Click(object sender, EventArgs e)//修改保存
        {
            if (textBox7.Text != "")
            {
                if (textBox6.Text != "")
                {
                    if (textBox5.Text != "")
                    {

                        if (comboBox2.Text != "")
                        {
                            if (SqlHelper.UserName == "管理" && comboBox1.Text.Trim() == Resources.UT_Admin)
                            {
                                MessageBox.Show("此帐号不可以修改成<admin>帐号，请重新输入!!", Resources.T提示);
                            }
                            else
                            {
                                var sql =
                                    $"UPDATE IDPASS set ID='{textBox7.Text.Trim()}',pass='{textBox6.Text.Trim()}',name='{textBox5.Text.Trim()}',leiq='{comboBox2.Text.Trim()}' WHERE lu='{_mMid}'";
                                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text,sql);
                                GetUserInfo();
                            }
                        }
                        else
                        {
                            MessageBox.Show("帐号类型不能为空！", Resources.T提示);
                        }
                    }
                    else
                    {
                        MessageBox.Show("姓名不能为空！", Resources.T提示);
                        textBox5.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("密码不能为空！", Resources.T提示);
                    textBox6.Focus();
                }
            }
            else
            {
                MessageBox.Show("帐号不能为空！", Resources.T提示);
                textBox7.Focus();
            }
        }

        private void button7_Click(object sender, EventArgs e)//修改取消
        {
            GetUserInfo();
            textBox8.Text = "";
            textBox7.Text = "";
            textBox6.Text = "";
            textBox5.Text = "";
            comboBox2.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)//新建保存
        {
            if (textBox2.Text != "")
            {
                if (textBox4.Text != "")
                {

                    if (comboBox1.Text != "")
                    {
                        if (SqlHelper.UserName == "管理"&& comboBox1.Text .Trim ()==Resources.UT_Admin)
                        {
                            MessageBox.Show("此帐号不可以创建<admin>帐号，请重新输入!!", Resources.T提示);
                        }
                        else
                        {
                            var sql = $"SELECT ID FROM IDPASS WHERE ID='{textBox2.Text.Trim()}'";
                            var sdddr1Q = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                            if (sdddr1Q.HasRows)
                            {
                                MessageBox.Show($"此帐号< {textBox2.Text.Trim()} >已存在，请重新输入!!", Resources.T提示);
                                textBox2.Text = "";
                                sdddr1Q.Close();
                            }
                            else
                            {
                                sdddr1Q.Close();
                                sql =
                                    $"insert into IDPASS(ID,pass,name,leiq) values('{textBox2.Text.Trim()}','{textBox3.Text.Trim()}','{textBox4.Text.Trim()}','{comboBox1.Text.Trim()}')";
                                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text,sql);
                                textBox2.Text = "";
                                textBox3.Text = "";
                                textBox4.Text = "";
                                comboBox1.Text = "";
                                GetUserInfo();
                            }
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("帐号类型不能为空！", Resources.T提示);
                    }
                }
                else
                {
                    MessageBox.Show("姓名不能为空！", Resources.T提示);
                    textBox4.Focus();
                }
                
            }
            else
            {
                MessageBox.Show("帐号不能为空！", Resources.T提示);
                textBox2.Focus();
            }
        }

        private void button6_Click(object sender, EventArgs e)//新建取消
        {
            GetUserInfo();
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
        }
    }
}
