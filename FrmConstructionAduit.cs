using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using LYH.Framework.Commons;
using LYH.WorkOrder.share;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmConstructionAduit : Form
    {
        public FrmConstructionAduit()
        {
            KeyDown += FrmWin_KeyDown;
            InitializeComponent();
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            label3.Visible = false;
            panel1.Visible = false;
            panel2.Visible = true;
            label14.Visible = false;
        }
        private string _ak7948;
        private string _ak798;
        private string _ak984;
        private string _ak985;
        private string _ak986;
        private void Form5_Load(object sender, EventArgs e)
        {
        }

        private void FrmWin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    button5_Click(sender, e);
                    Close();
                    DialogResult = DialogResult.Cancel;
                    break;
            }
        }

        private void Goiss()
        {
            if (textBox15.Text.Trim() != "")
            {
                string sql = "SELECT xuhao as '工序号',gongxumingcheng as '工序名称',jihuariqi as '计划完成日期'," +
                             "shengchanyuan as '生产班组',wanshengsuliang as'完成数量',jgsu as '加工数',xujah as'序价号'," +
                             "xujia as '序价',buzu as '补助',gongsi as '公式',hezhi as '件资合计',lururiqi as '录入日期'," +
                             "luruyan as '录入人',qiugairiqi as '修改日期',qgren as '修改人' FROM tf_sgdantwo " +
                             $"WHERE shigongdanhao='{textBox15.Text.Trim()}'";
                DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 60;
                dataGridView1.Columns[1].Width = 75;
                dataGridView1.Columns[2].Width = 90;
                dataGridView1.Columns[3].Width = 70;
                dataGridView1.Columns[4].Width = 60;
                dataGridView1.Columns[5].Width = 60;
                dataGridView1.Columns[6].Width = 60;
                dataGridView1.Columns[7].Width = 60;
                dataGridView1.Columns[8].Width = 50;
                dataGridView1.Columns[9].Width = 50;
                dataGridView1.Columns[10].Width = 100;
                dataGridView1.Columns[11].Width = 100;
                dataGridView1.Columns[12].Width = 60;
                dataGridView1.Columns[13].Width = 100;
                dataGridView1.Columns[14].Width = 60;
                dataGridView1.Sort(dataGridView1.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
            }
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            if (textBox15.Text.Length == 7)
            {
                string sql = $"SELECT TOP 1 * FROM mf_sgdan WHERE shigongdanhao='{textBox15.Text.Trim()}'";
                SqlDataReader sdra5 = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (sdra5.HasRows)
                {
                    sdra5.Read();
                    if (!String.IsNullOrEmpty(sdra5["suhu"].ToString().Trim()))
                    {
                        textBox1.Text = sdra5["kehu"].ToString().Trim();
                        textBox2.Text = sdra5["dingdanhao"].ToString().Trim();
                        textBox4.Text = sdra5["chanpintuhao"].ToString().Trim();
                        textBox6.Text = sdra5["chanpinmingcheng"].ToString().Trim();
                        textBox14.Text = sdra5["dingdansuliang"].ToString().Trim();
                        textBox3.Text = sdra5["jiaohuoqi"].ToString().Trim();
                        textBox5.Text = sdra5["tuzhiyema"].ToString().Trim();
                        sdra5.Close();
                        button2.Visible = true;
                        button3.Visible = true;
                        textBox15.ReadOnly = true;
                        button2.Focus();
                        Goiss();
                        label3.Visible = true;
                        MessageBox.Show($"此工单号{textBox15.Text.Trim()}已审核!!", "提示");
                    }
                    else
                    {
                        textBox1.Text = sdra5["kehu"].ToString().Trim();
                        textBox2.Text = sdra5["dingdanhao"].ToString().Trim();
                        textBox4.Text = sdra5["chanpintuhao"].ToString().Trim();
                        textBox6.Text = sdra5["chanpinmingcheng"].ToString().Trim();
                        textBox14.Text = sdra5["dingdansuliang"].ToString().Trim();
                        textBox3.Text = sdra5["jiaohuoqi"].ToString().Trim();
                        textBox5.Text = sdra5["tuzhiyema"].ToString().Trim();
                        sdra5.Close();
                        button1.Visible = true;
                        button2.Visible = true;
                        button4.Visible = true;
                        textBox15.ReadOnly = true;
                        button1.Focus();
                        Goiss();
                        label3.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show($"此工单号{textBox15.Text.Trim()}不存在，请重新输入!!", "提示");
                    textBox15.Text = "";
                    sdra5.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)//审核
        {
            string sql = $"UPDATE mf_sgdan set suhu='审核' WHERE shigongdanhao ='{textBox15.Text.Trim()}'";
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
            sql = "";
            sql =
                $"UPDATE tf_sgdantwo set suhu='审核' ,suren='{SqlHelper.UserName}'WHERE shigongdanhao ='{textBox15.Text.Trim()}'";
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
            label3.Visible = true;
            button1.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = false;
            button5.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)//下一单
        {
            textBox15.Text = "";
            Goiss();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            textBox14.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            label3.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            panel1.Visible = false;
            panel2.Visible = true;
            textBox15.ReadOnly = false;
            textBox15.Focus();
        }

        private void button3_Click(object sender, EventArgs e)//弃审
        {
            string sql = $"UPDATE mf_sgdan set suhu='' WHERE shigongdanhao ='{textBox15.Text.Trim()}'";
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
            sql = "";
            sql = $"UPDATE tf_sgdantwo set suhu='',suren='' WHERE shigongdanhao ='{textBox15.Text.Trim()}'";
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = false;
            button4.Visible = true;
            label3.Visible = false;
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(textBox19.Text) && textBox19.Text != "")
            {
                MessageBox.Show("只允许输入数字，请重新输入！", "提示");
                textBox19.Text = "";
            }
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(textBox20.Text) && !String.IsNullOrEmpty(textBox20.Text))
            {
                MessageBox.Show("输入数据类型错误，请重新输入！", "提示");
                textBox20.Text = "";
            }
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(textBox22.Text) && !String.IsNullOrEmpty(textBox22.Text))
            {
                        MessageBox.Show("输入数据类型错误，请重新输入！", "提示");
                        textBox22.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (panel1.Visible == false)
            {
                button4.Text = "&X保存";
                label14.Visible = true;
                panel1.Visible = true;
                panel2.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                button5.Visible = true;
                textBox22.ReadOnly = true;
            }
            else
            {
                if (textBox16.Text != "" & textBox17.Text != "")
                {
                    if (textBox18.Text != "")
                    {
                        if (textBox19.Text != "")
                        {
                            if (textBox20.Text != "")
                            {
                                if (textBox22.Text != "")
                                {
                                    if (textBox21.Text != "")
                                    {
                                        string sql = $"SELECT TOP 1 * FROM xujia WHERE xujhao='{textBox21.Text.Trim()}'";
                                        SqlDataReader a3C = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                                        if (a3C.HasRows)
                                        {
                                            a3C.Read();
                                            _ak798 = a3C["xujia"].ToString().Trim();
                                            textBox24.Text = a3C["gongsi"].ToString().Trim();
                                            _ak7948 = a3C["buzu"].ToString().Trim();
                                            a3C.Close();
                                            string sKl = textBox21.Text.Trim().ToUpper();
                                            if (sKl.Contains("C15") & textBox22.Text.Trim() != _ak798 & textBox22.ReadOnly == false)
                                            {
                                            }
                                            else
                                            {
                                                textBox22.Text = _ak798;
                                            }
                                            sql = "";
                                            sql =
                                                $"SELECT count(*) icount FROM tf_sgdantwo WHERE gongxumingcheng='{textBox17.Text.Trim()}' AND shigongdanhao='{textBox15.Text.Trim()}' " +
                                                $"AND xujah='{textBox21.Text.Trim()} 'AND buzu>0 ";
                                            SqlDataReader g2 = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                                            if (g2.HasRows)
                                            {
                                                g2.Read();
                                                if ((int)g2["icount"] > 0)
                                                {
                                                    if (_ak986 == _ak7948)
                                                    {
                                                        textBox23.Text = _ak986;
                                                        g2.Close();
                                                    }
                                                    else
                                                    {
                                                        textBox23.Text = "0";
                                                        g2.Close();
                                                    }
                                                }
                                                else
                                                {
                                                    textBox23.Text = _ak7948;
                                                    g2.Close();
                                                }
                                            }
                                            decimal fIko = decimal.Parse(textBox19.Text.Trim());
                                            decimal fIkp = decimal.Parse(textBox20.Text.Trim());
                                            decimal fIks = decimal.Parse(textBox22.Text.Trim());
                                            decimal fIkt = decimal.Parse(textBox23.Text.Trim());
                                            decimal fIkf;
                                            textBox22.ReadOnly = true;
                                            if (textBox24.Text.Trim() == "1")
                                            {
                                                fIkf = fIko * fIkp * fIks + fIkt;
                                            }
                                            else if (textBox24.Text.Trim() == "2")
                                            {
                                                fIkf = fIko * fIks + fIkt;
                                            }
                                            else
                                            {
                                                fIkf = fIkp * fIks + fIkt;
                                            }
                                            sql = "";
                                            sql =
                                                $"UPDATE tf_sgdantwo set shengchanyuan='{textBox18.Text.Trim().ToUpper()}',wanshengsuliang='{textBox19.Text.Trim()}',wyqg='{SqlHelper.UserName}',gongsi='{textBox24.Text.Trim()}'," +
                                                $"jgsu='{textBox20.Text.Trim()}',hezhi='{fIkf}',wyrq='{DateTime.Now}',xujah='{textBox21.Text.Trim().ToUpper()}',xujia='{textBox22.Text.Trim()}',buzu='{textBox23.Text.Trim()}' WHERE shigongdanhao ='{textBox15.Text.Trim()}' AND " +
                                                $"xuhao='{textBox16.Text.Trim()}'";
                                            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                                            button1.Visible = true;
                                            button2.Visible = true;
                                            button3.Visible = false;
                                            button4.Visible = true;
                                            button5.Visible = false;
                                            panel1.Visible = false;
                                            panel2.Visible = true;
                                            label14.Visible = false;
                                            button4.Text = "&X修改";
                                            textBox16.Text = "";
                                            textBox17.Text = "";
                                            textBox18.Text = "";
                                            textBox19.Text = "";
                                            textBox20.Text = "";
                                            textBox21.Text = "";
                                            textBox22.Text = "";
                                            textBox23.Text = "";
                                            textBox24.Text = "";
                                            Goiss();
                                        }
                                        else
                                        {
                                            MessageBox.Show($"序价代码{textBox21.Text.Trim()}不存在，请重新输入!!", "提示");
                                            textBox21.Text = "";
                                            textBox22.Text = "";
                                            textBox23.Text = "";
                                            textBox24.Text = "";
                                            textBox21.Focus();
                                            a3C.Close();
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("序价代码不能为空！", "提示");
                                        textBox21.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("序价不能为空！", "提示");
                                    textBox22.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("加工数不能为空！", "提示");
                                textBox20.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("完成数量不能为空！", "提示");
                            textBox19.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("生产班组不能为空！", "提示");
                        textBox18.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("请先选择行数据", "提示");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = false;
            button4.Visible = true;
            button5.Visible = false;
            label14.Visible = false;
            button4.Text = "&X修改";
            panel1.Visible = false;
            panel2.Visible = true;
            textBox22.ReadOnly = true;
            textBox16.Text = "";
            textBox17.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";
            textBox20.Text = "";
            textBox21.Text = "";
            textBox22.Text = "";
            textBox23.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.SelectedRows.Count != 0)    //当选中了行的时候才显示数据
            {
                //将被点击的行的第一列的数据显示到第一个TextBox中
                textBox16.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                //将被点击的行的第二列数据显示到第二个TextBox中，后面以此类推
                textBox17.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Trim();
                textBox18.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString().Trim();
                textBox19.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString().Trim();
                textBox20.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString().Trim();
                textBox21.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString().Trim();
                textBox22.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString().Trim();
                textBox23.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString().Trim();
                textBox24.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString().Trim();
                string sql =
                    $"SELECT TOP 1 * FROM tf_sgdantwo WHERE shigongdanhao='{textBox15.Text.Trim()}'AND xuhao='{textBox16.Text.Trim()} '";
                SqlDataReader a5C = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (a5C.HasRows)
                {
                    a5C.Read();
                    _ak984 = a5C["xujah"].ToString().Trim();
                    _ak985 = a5C["xujia"].ToString().Trim();
                    _ak986 = a5C["buzu"].ToString().Trim();
                    a5C.Close();
                }
            }
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

            decimal count = 0; //根据需要，设置你想要求合时的数据类型。当用int时处理decimal时产生错误然后由contunue语句跳过，继续累加int型的
            for (int i = 0; i < dataGridView1.SelectedCells.Count; i++) //用SelectedCells得到选中单元格的集合，循环来遍历集合当中的所有值
            {
                if (dataGridView1.SelectedCells[i].Value != null)
                {
                    try
                    {
                        count = count + decimal.Parse(dataGridView1.SelectedCells[i].Value.ToString());
                    }
                    catch (Exception)
                    {
                        continue;// 非数字时,会报错,跳过
                    }
                }
            }
            if (count == 0)
            {
                label1.Text = "";
            }
            else
            {
                label1.Text = count.ToString();
            }

        }

        private void textBox21_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox16.Text != "" & textBox17.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string sql = $"SELECT TOP 1 * FROM xujia WHERE xujhao='{textBox21.Text.Trim()}'";
                    SqlDataReader ac = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                    if (ac.HasRows)
                    {
                        ac.Read();
                        textBox22.Text = ac["xujia"].ToString().Trim();
                        textBox24.Text = ac["gongsi"].ToString().Trim();
                        _ak7948 = ac["buzu"].ToString().Trim();
                        ac.Close();
                        //MessageBox.Show(ak97, "提示");
                        sql = "";
                        sql =
                            $"SELECT count(*) icount FROM tf_sgdantwo WHERE gongxumingcheng='{textBox17.Text.Trim()}' AND shigongdanhao='{textBox15.Text.Trim()}' " +
                            $"AND xujah='{textBox21.Text.Trim()}' AND buzu>0 ";
                        SqlDataReader g2 = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                        if (g2.HasRows)
                        {
                            g2.Read();
                            if ((int)g2["icount"] > 0)
                            {
                                if (_ak986 == _ak7948)
                                {
                                    g2.Close();
                                    textBox23.Text = _ak986;
                                    textBox20.Focus();
                                }
                                else
                                {
                                    textBox23.Text = "0";
                                    //MessageBox.Show(ak798, "提示");
                                    g2.Close();
                                    textBox20.Focus();
                                }
                            }
                            else
                            {
                                textBox23.Text = _ak7948;
                                g2.Close();
                                textBox20.Focus();
                            }
                        }
                        string sKl = textBox21.Text.Trim().ToUpper();
                        if (sKl.Contains("C15"))
                        {
                            textBox22.ReadOnly = false;
                        }
                        else
                        {
                            textBox22.ReadOnly = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"序价代码{textBox21.Text.Trim()}不存在，请重新输入!!", "提示");
                        textBox21.Text = "";
                        textBox21.Focus();
                        ac.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选择行数据", "提示");
            }
        }

        private void textBox18_LostFocus(object sender, KeyEventArgs e)
        {
            if (textBox18.Text == "")
            {
                MessageBox.Show("生产班组不能为空，请填写！", "提示");
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    textBox19.Focus();
                }
            }
        }

        private void textBox19_LostFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox21.Focus();
            }
        }

        private void textBox19_Leave(object sender, EventArgs e)
        {
            if (textBox19.Text == "")
            {
                MessageBox.Show("完成数量不能为空，请填写！", "提示");
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows != null)    //当选中了行的时候才显示数据
            {
                //将被点击的行的第一列的数据显示到第一个TextBox中
                textBox16.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                //将被点击的行的第二列数据显示到第二个TextBox中，后面以此类推
                textBox17.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Trim();
                textBox18.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString().Trim();
                textBox19.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString().Trim();
                textBox20.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString().Trim();
                textBox21.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString().Trim();
                textBox22.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString().Trim();
                textBox23.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString().Trim();
                textBox24.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString().Trim();
                string sql =
                    $"SELECT TOP 1 * FROM tf_sgdantwo WHERE shigongdanhao='{textBox15.Text.Trim()}'AND xuhao='{textBox16.Text.Trim()} '";
                SqlDataReader a5C = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (a5C.HasRows)
                {
                    a5C.Read();
                    _ak984 = a5C["xujah"].ToString().Trim();
                    _ak985 = a5C["xujia"].ToString().Trim();
                    _ak986 = a5C["buzu"].ToString().Trim();
                    a5C.Close();
                }
            }
        }

    }
}
