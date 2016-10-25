using System;
using System.Data;
using System.Windows.Forms;
using LYH.Framework.Commons;
using LYH.WorkOrder.Properties;
using LYH.WorkOrder.share;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmProcCardUprice : Form
    {
        public FrmProcCardUprice()
        {
            InitializeComponent();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(textBox2.Text) && !String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("输入数据类型错误，请重新输入！", Resources.T提示);
                textBox2.Text = "";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(textBox3.Text) && !String.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("输入数据类型错误，请重新输入！", Resources.T提示);
                textBox3.Text = "";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(textBox4.Text) && !String.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("输入数据类型错误，请重新输入！", Resources.T提示);
                textBox4.Text = "";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var uMi = dataGridView1.SelectedCells[0].Value.ToString().Trim();
            var qMi = dataGridView1.SelectedCells[1].Value.ToString().Trim();
            var wMi = dataGridView1.SelectedCells[2].Value.ToString().Trim();
            var eMi = dataGridView1.SelectedCells[3].Value.ToString().Trim();
            var rMi = dataGridView1.SelectedCells[4].Value.ToString().Trim();
            var tMi = dataGridView1.SelectedCells[5].Value.ToString().Trim();
            var fMi = dataGridView1.SelectedCells[6].Value.ToString().Trim();
            cbxCraft.Text = qMi;
            textBox7.Text = wMi;
            textBox2.Text = eMi;
            textBox3.Text = rMi;
            textBox4.Text = tMi;
            textBox6.Text = uMi;
            textBox5.Text = fMi;
        }

        private void Form20_Load(object sender, EventArgs e)
        {
            BindData();
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox7.ReadOnly = true;
        }
        private void BindData()
        {
            var sql = $"SELECT Craft FROM PD_ProcCard_UPrice WHERE DeptId='{SqlHelper.DeptId}' ORDER BY ID";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString("dzdj"), CommandType.Text, sql);
            cbxCraft.DataSource = ds.Tables[0];
            cbxCraft.DisplayMember = "Craft";
            cbxCraft.ValueMember = "Craft";
            sql = "SELECT IDO '流水',iname '工序名称',qtime '时间起',ztime '时间止',"+
                  "jilu '单价基数',jisu '计算折率',beizu '备注' FROM udodt  ORDER BY iname , ztime ";
            ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].Width = 80;
            dataGridView1.Columns[6].Width = 200;
            dataGridView1.Sort(dataGridView1.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == Resources.X保存)
            {
                if (cbxCraft.Text != "")
                {
                    if (textBox7.Text != "")
                    {
                        if (textBox2.Text != "")
                        {
                            if (textBox3.Text != "")
                            {
                                if (textBox4.Text != "")
                                {
                                    var lMi = dataGridView1.SelectedCells[0].Value.ToString().Trim();
                                    var sql =
                                        $"UPDATE udodt set iname='{cbxCraft.Text.Trim().ToUpper()}',qtime='{decimal.Parse(textBox7.Text.Trim())}',ztime='{decimal.Parse(textBox2.Text.Trim())}',jilu='{decimal.Parse(textBox3.Text.Trim())}',jisu='{decimal.Parse(textBox4.Text.Trim())}'," +
                                        $"beizu='{textBox5.Text.Trim()}' WHERE IDO ='{lMi}'";
                                    SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                                    BindData();
                                    button6.Text = Resources.X修改;
                                    button1.Visible = true;
                                    button3.Visible = true;
                                    textBox2.ReadOnly = true;
                                    textBox3.ReadOnly = true;
                                    textBox4.ReadOnly = true;
                                    textBox5.ReadOnly = true;
                                    textBox7.ReadOnly = true;
                                    cbxCraft.Text = "";
                                    textBox2.Text = "";
                                    textBox3.Text = "";
                                    textBox4.Text = "";
                                    textBox5.Text = "";
                                    textBox6.Text = "";
                                    textBox7.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("计算折率不能为空", Resources.T提示);
                                    textBox4.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("单价基数不能为空", Resources.T提示);
                                textBox3.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("时间止不能为空", Resources.T提示);
                            textBox2.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("时间起不能为空", Resources.T提示);
                        textBox7.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("请先选择所要修改的项目行", Resources.T提示);
                    cbxCraft.Focus();
                }
            }
            else
            {
                button6.Text = Resources.X保存;
                button1.Visible = false;
                button3.Visible = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox7.ReadOnly = false;
                cbxCraft.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == Resources.A保存)
            {
                if (cbxCraft.Text != "")
                {
                    if (textBox7.Text != "")
                    {
                        if (textBox2.Text != "")
                        {
                            if (textBox3.Text != "")
                            {
                                if (textBox4.Text != "")
                                {
                                    var sql =
                                        $"insert into udodt(iname,qtime,ztime,jilu,jisu,beizu) values('{cbxCraft.Text.Trim().ToUpper()}','{decimal.Parse(textBox7.Text.Trim())}','{decimal.Parse(textBox2.Text.Trim())}'," +
                                        $"'{decimal.Parse(textBox3.Text.Trim())}','{decimal.Parse(textBox4.Text.Trim())}','{textBox5.Text.Trim()}')";
                                    SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                                    button1.Text = Resources.A新增;
                                    button6.Visible = true;
                                    button3.Visible = true;
                                    textBox2.ReadOnly = true;
                                    textBox3.ReadOnly = true;
                                    textBox4.ReadOnly = true;
                                    textBox5.ReadOnly = true;
                                    textBox7.ReadOnly = true;
                                    cbxCraft.Text = "";
                                    textBox2.Text = "";
                                    textBox3.Text = "";
                                    textBox4.Text = "";
                                    textBox5.Text = "";
                                    textBox6.Text = "";
                                    textBox7.Text = "";
                                    BindData();
                                }
                                else
                                {
                                    MessageBox.Show("计算折率不能为空", Resources.T提示);
                                    textBox4.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("单价基数不能为空", Resources.T提示);
                                textBox3.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("时间止不能为空", Resources.T提示);
                            textBox2.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("时间起不能为空", Resources.T提示);
                        textBox7.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("工序名称不能为空", Resources.T提示);
                    cbxCraft.Focus();
                }
            }
            else
            {
                button1.Text = Resources.A保存;
                button6.Visible = false;
                button3.Visible = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox7.ReadOnly = false;
                cbxCraft.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Text = Resources.A新增;
            button6.Text = Resources.X修改;
            button1.Visible = true;
            button6.Visible = true;
            button3.Visible = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox7.ReadOnly = true;
            cbxCraft.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            BindData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var kMMi = dataGridView1.SelectedCells[0].Value.ToString().Trim();
            if (MessageBox.Show("是否要删除所选择的行", Resources.J警告, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                var sql = $"delete FROM udodt WHERE IDO='{kMMi}'";
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                BindData();
            }
            else
            {
                BindData();
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(textBox7.Text) && !String.IsNullOrEmpty(textBox7.Text))
            {
                MessageBox.Show("输入数据类型错误，请重新输入！", Resources.T提示);
                textBox7.Text = "";
            }
        }
    }
}
