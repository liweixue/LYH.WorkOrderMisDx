using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using LYH.Framework.Commons;
using LYH.WorkOrder.share;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmBimonthly : Form
    {
        string _jihuadate;

        public FrmBimonthly()
        {
            KeyDown += FrmWin_KeyDown;
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
        }

        private void FrmWin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    DialogResult = DialogResult.Cancel;
                    break;

                //case Keys.F2:
                //    this.button1_Click(RuntimeHelpers.GetObjectValue(sender), e);
                //    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {
                    if (textBox3.Text != "")
                    {
                        if (textBox4.Text != "")
                        {
                            string gime;
                            const string fileType1 = "数冲";
                            const string fileType2 = "激光";
                            const string fileType3 = "折弯";
                            const string fileType4 = "焊接";
                            const string fileType5 = "装配";
                            const string fileType6 = "入仓";
                            string[] type = new[] { textBox3.Text.Trim() };
                            if (type.Contains(fileType1))   //判断Type 数组是否包含了fileType字符串
                            {
                                gime = fileType1 + textBox4.Text.Trim();
                                gime = gime.ToUpper();
                            }
                            else if (type.Contains(fileType2))
                            {
                                gime = fileType2 + textBox4.Text.Trim();
                                gime = gime.ToUpper();
                            }
                            else if (type.Contains(fileType3))
                            {
                                gime = fileType3 + textBox4.Text.Trim();
                                gime = gime.ToUpper();
                            }
                            else if (type.Contains(fileType4))
                            {
                                gime = fileType4 + textBox4.Text.Trim();
                                gime = gime.ToUpper();
                            }
                            else if (type.Contains(fileType5))
                            {
                                gime = fileType5 + textBox4.Text.Trim();
                                gime = gime.ToUpper();
                            }
                            else if (type.Contains(fileType6))
                            {
                                gime = fileType5 + textBox4.Text.Trim();
                                gime = gime.ToUpper();
                            }
                            else
                            {
                                gime = textBox4.Text.Trim();
                                gime = gime.ToUpper();
                            }
                            if (textBox5.Text != "")
                            {
                                if (textBox6.Text != "")
                                {
                                    if (textBox7.Text != "")
                                    {
                                        string sql = $"SELECT TOP 1 * FROM xujia WHERE xujhao='{textBox7.Text.Trim()}'";
                                        SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                                        if (dr.HasRows)
                                        {
                                            dr.Read();
                                            textBox15.Text = dr["xujia"].ToString().Trim();
                                            textBox16.Text = dr["buzu"].ToString().Trim();
                                            string formula = dr["gongsi"].ToString().Trim();
                                            dr.Close();
                                            sql = "";
                                            sql =
                                                $"SELECT count(*) icount FROM tf_sgdantwo WHERE gongxumingcheng='{textBox3.Text.Trim()}' AND shigongdanhao='{textBox1.Text.Trim()} 'AND xujah='{textBox7.Text.Trim()} 'AND buzu>0 ";
                                            dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                                            if (dr.HasRows)
                                            {
                                                dr.Read();
                                                if ((int)dr["icount"] > 0)
                                                {
                                                    textBox16.Text = "0";
                                                }
                                            }
                                            dr.Close();
                                            if (textBox7.Text.Contains("C15") || textBox7.Text.Contains("H1") || textBox7.Text.Contains("Z1"))
                                            {
                                                sql = "";
                                                sql =
                                                    $"SELECT * FROM dzdj.dbo.bom WHERE customer='{textBox8.Text.Trim()}' AND  partno='{textBox10.Text.Trim()}'";
                                                dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                                                if (dr.HasRows)
                                                {
                                                    dr.Read();
                                                    if (textBox7.Text.Contains("C15"))
                                                    {
                                                        textBox15.Text = dr["machuprice"].ToString().Trim();
                                                    }
                                                    //else if (textBox14.Text.Contains("H1"))
                                                    //{
                                                    //    textBox15.Text = dr["welduprice"].ToString().Trim();
                                                    //}
                                                    //else if (textBox14.Text.Contains("Z1"))
                                                    //{
                                                    //    textBox15.Text = dr["packuprice"].ToString().Trim();
                                                    //}
                                                    else
                                                    {
                                                        textBox15.Text = "0";
                                                    }
                                                    dr.Close();
                                                }
                                                else
                                                {
                                                    textBox15.Text = "0";
                                                }
                                            }
                                            decimal uprice = decimal.Parse(textBox15.Text.Trim());//序价
                                            decimal buzhu = decimal.Parse(textBox16.Text.Trim());//补助
                                            decimal qt = decimal.Parse(textBox5.Text.Trim());//完成数
                                            Kogu();
                                            decimal total;
                                            if (formula == "1")
                                            {
                                                total = qt * uprice * _jgqt + buzhu;
                                            }
                                            else if (formula == "2")
                                            {
                                                total = qt * uprice + buzhu;
                                            }
                                            else
                                            {
                                                total = uprice * _jgqt + buzhu;
                                            }

                                            sql = "";
                                            sql =
                                                $"SELECT TOP 1 * FROM tf_sgdantwo WHERE shigongdanhao='{textBox1.Text.Trim()} ' AND xuhao=' {textBox2.Text.Trim()}'";
                                            dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                                            if (dr.HasRows)
                                            {
                                                dr.Read();
                                                if (!String.IsNullOrEmpty(dr["wanshengsuliang"].ToString().Trim()))
                                                {
                                                    MessageBox.Show($"此工序号< {textBox2.Text.Trim()} >已录入过，请重编序号或进行修改!!", "提示");
                                                    textBox2.Text = "";
                                                    textBox3.Text = "";
                                                    textBox4.Text = "";
                                                    textBox5.Text = "";
                                                    textBox6.Text = "";
                                                    textBox7.Text = "";
                                                    textBox15.Text = "";
                                                    textBox16.Text = "";
                                                    Koisd();
                                                    textBox2.Focus();
                                                }
                                                else
                                                {
                                                    sql = "";
                                                    sql =
                                                        $"UPDATE tf_sgdantwo set gongxumingcheng='{textBox3.Text.Trim()}',gongsi='{formula}',chanpintuhao='{textBox10.Text.Trim()}'," +
                                                        $"jhqi='{textBox12.Text.Trim()}',shengchanyuan='{gime}',buzu='{textBox16.Text.Trim()}',luruyan='{SqlHelper.UserName}',wanshengsuliang='{textBox5.Text.Trim()}',kehu='{textBox8.Text.Trim()}'," +
                                                        $"jgsu='{textBox6.Text.Trim()}',ddhao='{textBox9.Text.Trim()}',hezhi='{total}',lururiqi='{dateTimePicker1.Value.Date.ToShortDateString()}',xujah='{textBox7.Text.Trim().ToUpper()}',xujia='{textBox15.Text.Trim()}' WHERE " +
                                                        $"shigongdanhao ='{textBox1.Text.Trim()}' AND xuhao='{textBox2.Text.Trim()}'";
                                                    SqlConnection conn = SqlHelper.GetConnection();
                                                    conn.Open();
                                                    SqlTransaction tran = conn.BeginTransaction();
                                                    try
                                                    {
                                                        SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql);
                                                        tran.Commit();
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        MessageBox.Show("事务已回滚,尚未提交!" + ex);
                                                        tran.Rollback();
                                                    }
                                                    textBox2.Text = "";
                                                    textBox3.Text = "";
                                                    textBox4.Text = "";
                                                    textBox5.Text = "";
                                                    textBox6.Text = "";
                                                    textBox7.Text = "";
                                                    textBox15.Text = "";
                                                    textBox16.Text = "";
                                                    Koisd();
                                                    textBox2.Focus();
                                                    button3.Enabled = true;
                                                    button4.Enabled = true;
                                                }
                                            }
                                            else
                                            {
                                                sql = "";
                                                sql =
                                                    "insert into tf_sgdantwo(shigongdanhao,xuhao,gongxumingcheng,chanpintuhao,shengchanyuan," +
                                                    $"wanshengsuliang,jgsu,xujah,xujia,buzu,hezhi,lururiqi,luruyan,kehu,ddhao,jhqi,gongsi,jihuariqi) values('{textBox1.Text.Trim().ToUpper()}','{textBox2.Text.Trim()}','{textBox3.Text.Trim()}'," +
                                                    $"'{textBox10.Text.Trim()}','{gime}','{textBox5.Text.Trim()}','{textBox6.Text.Trim()}','{textBox7.Text.Trim().ToUpper()}','{textBox15.Text.Trim()}','{textBox16.Text.Trim()}','{total}','{dateTimePicker1.Value.Date.ToShortDateString()}','{SqlHelper.UserName}','{textBox8.Text.Trim()}','{textBox9.Text.Trim()}','{textBox12.Text.Trim()}','{formula}','{_jihuadate}')";
                                                SqlConnection conn = SqlHelper.GetConnection();
                                                conn.Open();
                                                SqlTransaction tran = conn.BeginTransaction();
                                                try
                                                {
                                                    SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql);
                                                    tran.Commit();
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("事务已回滚,尚未提交!" + ex);
                                                    tran.Rollback();
                                                }
                                                textBox2.Text = "";
                                                textBox3.Text = "";
                                                textBox4.Text = "";
                                                textBox5.Text = "";
                                                textBox6.Text = "";
                                                textBox7.Text = "";
                                                textBox15.Text = "";
                                                textBox16.Text = "";
                                                Koisd();
                                                textBox2.Focus();
                                                button3.Enabled = true;
                                                button4.Enabled = true;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show($"序价代码{textBox7.Text.Trim()}不存在，请重新输入!!", "提示");
                                            textBox7.Text = "";
                                            textBox7.Focus();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("序价代码不能为空！", "提示");
                                        textBox7.Text = "";
                                        textBox7.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("加工数不能为空！", "提示");
                                    textBox6.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("完成数量不能为空！", "提示");
                                textBox5.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("生产班组不能为空！", "提示");
                            textBox4.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("工序名称不能为空！", "提示");
                        textBox3.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("工序号不能为空！", "提示");
                    textBox2.Focus();
                }
            }
            else
            {
                MessageBox.Show("工单号不能为空！", "提示");
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("工单号不能为空！", "提示");
                textBox1.Focus();
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("序号不能为空！", "提示");
                textBox2.Focus();
            }
            else
            {
                string sql =
                    $"SELECT count(*) FROM tf_sgdantwo WHERE shigongdanhao='{textBox1.Text.Trim()}' AND xuhao between '{textBox2.Text.Trim()}' AND '{(decimal.Parse(textBox2.Text.Trim()) + 1)}'";
                SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (dr.HasRows)
                {
                    dr.Read();
                    sql = "";
                    if (int.Parse(dr[0].ToString().Trim()) > 2)
                    {
                        dr.Close();
                        sql =
                            $"SELECT TOP 1 * FROM tf_sgdantwo WHERE shigongdanhao='{textBox1.Text.Trim()}' AND xuhao like '{textBox2.Text.Trim()}.%' order by xuhao desc";
                        dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                        dr.Read();
                        textBox2.Text = dr["xuhao"].ToString().Trim();
                    }
                    decimal adde = decimal.Parse(textBox2.Text);
                    adde += 0.01M;
                    string rde = adde.ToString();
                    textBox2.Text = rde;
                    textBox3.Focus();
                    button3.Enabled = false;
                    button4.Enabled = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show("完成数量不能为空！！！", "提示");
            }
            else if (button1.Enabled != true)
            {
                if (textBox1.Text != "")
                {
                    if (textBox2.Text != "")
                    {

                        if (textBox3.Text != "")
                        {
                            if (textBox4.Text != "")
                            {
                                string gime;
                                const string fileType1 = "数冲";
                                const string fileType2 = "激光";
                                const string fileType3 = "折弯";
                                const string fileType4 = "焊接";
                                const string fileType5 = "装配";
                                const string fileType6 = "入仓";
                                string[] type = new[] { textBox3.Text.Trim() };
                                if (type.Contains(fileType1))   //判断Type 数组是否包含了fileType字符串
                                {
                                    gime = fileType1 + textBox4.Text.Trim();
                                    gime = gime.ToUpper();
                                }
                                else if (type.Contains(fileType2))
                                {
                                    gime = fileType2 + textBox4.Text.Trim();
                                    gime = gime.ToUpper();
                                }
                                else if (type.Contains(fileType3))
                                {
                                    gime = fileType3 + textBox4.Text.Trim();
                                    gime = gime.ToUpper();
                                }
                                else if (type.Contains(fileType4))
                                {
                                    gime = fileType4 + textBox4.Text.Trim();
                                    gime = gime.ToUpper();
                                }
                                else if (type.Contains(fileType5))
                                {
                                    gime = fileType5 + textBox4.Text.Trim();
                                    gime = gime.ToUpper();
                                }
                                else if (type.Contains(fileType6))
                                {
                                    gime = fileType5 + textBox4.Text.Trim();
                                    gime = gime.ToUpper();
                                }
                                else
                                {
                                    gime = textBox4.Text.Trim();
                                    gime = gime.ToUpper();
                                }
                                if (textBox5.Text != "")
                                {
                                    if (textBox6.Text != "")
                                    {
                                        ////MessageBox.Show("45456655464466", "提示");
                                        if (textBox7.Text != "")
                                        {
                                            string sql =
                                                $"SELECT TOP 1 * FROM xujia WHERE xujhao='{textBox7.Text.Trim()}'";
                                            SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                                            if (dr.HasRows)
                                            {
                                                dr.Read();
                                                textBox15.Text = dr["xujia"].ToString().Trim();
                                                string formula = dr["gongsi"].ToString().Trim();
                                                string strbu = dr["buzu"].ToString().Trim();
                                                //MessageBox.Show(ak97, "提示");
                                                dr.Close();
                                                sql = "";
                                                sql =
                                                    $"SELECT TOP 1 * FROM tf_sgdantwo WHERE shigongdanhao='{textBox1.Text.Trim()} ' AND xuhao=' {textBox2.Text.Trim()}'";
                                                dr.Close();
                                                dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                                                if (dr.HasRows)
                                                {
                                                    dr.Read();
                                                    if (StringHelper.IsInt(textBox2.Text))
                                                    {
                                                        textBox16.Text = strbu;
                                                    }
                                                    else
                                                    {
                                                        textBox16.Text = dr["buzu"].ToString().Trim();
                                                    }

                                                    //MessageBox.Show(textBox16.Text.Trim(), "提示1");
                                                    if (textBox16.Text.Trim() == "0")
                                                    {
                                                        textBox16.Text = "0";
                                                    }
                                                    else if (textBox16.Text.Trim() != "" || textBox16.Text.Trim() != strbu)
                                                    {
                                                        textBox16.Text = strbu;
                                                    }
                                                }
                                                dr.Close();
                                                if (textBox7.Text.Contains("C15") || textBox7.Text.Contains("H1") || textBox7.Text.Contains("Z1"))
                                                {
                                                    sql = "";
                                                    sql =
                                                        $"SELECT * FROM dzdj.dbo.bom WHERE customer='{textBox8.Text.Trim()}' AND  partno='{textBox10.Text.Trim()}'";
                                                    dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                                                    if (dr.HasRows)
                                                    {
                                                        dr.Read();
                                                        if (textBox7.Text.Contains("C15"))
                                                        {
                                                            textBox15.Text = dr["machuprice"].ToString().Trim();
                                                        }
                                                        //else if (textBox14.Text.Contains("H1"))
                                                        //{
                                                        //    textBox15.Text = dr["welduprice"].ToString().Trim();
                                                        //}
                                                        //else if (textBox14.Text.Contains("Z1"))
                                                        //{
                                                        //    textBox15.Text = dr["packuprice"].ToString().Trim();
                                                        //}
                                                        else
                                                        {
                                                            textBox15.Text = "0";
                                                        }
                                                        dr.Close();
                                                    }
                                                    else
                                                    {
                                                        textBox15.Text = "0";
                                                    }
                                                }
                                                //MessageBox.Show(textBox16.Text.Trim(), "提示2");
                                                decimal uprice = decimal.Parse(textBox15.Text.Trim());
                                                decimal buzhu = decimal.Parse(textBox16.Text.Trim());
                                                decimal qt = decimal.Parse(textBox5.Text.Trim());
                                                Kogu();
                                                decimal total;
                                                if (formula == "1")
                                                {
                                                    total = qt * uprice * _jgqt + buzhu;
                                                }
                                                else if (formula == "2")
                                                {
                                                    total = qt * uprice + buzhu;
                                                }
                                                else
                                                {
                                                    total = uprice * _jgqt + buzhu;
                                                }
                                                sql = "";
                                                sql =
                                                    $"UPDATE tf_sgdantwo set shengchanyuan='{gime}',jhqi='{textBox12.Text.Trim()}',wanshengsuliang='{textBox5.Text.Trim()}',qgren='{SqlHelper.UserName}',gongsi='{formula}',jgsu='{textBox6.Text.Trim()}',hezhi='{total}',qiugairiqi='{dateTimePicker1.Value.Date.ToShortDateString()}',xujah='{textBox7.Text.Trim().ToUpper()}',xujia='{textBox15.Text.Trim()}',buzu='{textBox16.Text.Trim()}' WHERE shigongdanhao ='{textBox1.Text.Trim()}' AND xuhao='{textBox2.Text.Trim()}'";
                                                SqlConnection conn = SqlHelper.GetConnection();
                                                conn.Open();
                                                SqlTransaction tran = conn.BeginTransaction();
                                                try
                                                {
                                                    SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql);
                                                    tran.Commit();
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("事务已回滚,尚未提交!" + ex);
                                                    tran.Rollback();
                                                }
                                                textBox2.Text = "";
                                                textBox3.Text = "";
                                                textBox4.Text = "";
                                                textBox5.Text = "";
                                                textBox6.Text = "";
                                                textBox7.Text = "";
                                                textBox15.Text = "";
                                                textBox16.Text = "";
                                                Koisd();
                                                textBox2.Focus();
                                                button3.Text = "&X修改";
                                                button1.Enabled = true;
                                                button1.Enabled = true;
                                                button2.Enabled = true;
                                                button4.Enabled = true;
                                            }
                                            else
                                            {
                                                MessageBox.Show($"序价代码{textBox7.Text.Trim()}不存在，请重新输入!!", "提示");
                                                textBox2.Text = "";
                                                textBox3.Text = "";
                                                textBox4.Text = "";
                                                textBox5.Text = "";
                                                textBox6.Text = "";
                                                textBox7.Text = "";
                                                textBox15.Text = "";
                                                textBox16.Text = "";
                                                Koisd();
                                                textBox2.Focus();
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("序价代码不能为空", "提示");
                                            textBox2.Text = "";
                                            textBox3.Text = "";
                                            textBox4.Text = "";
                                            textBox5.Text = "";
                                            textBox6.Text = "";
                                            textBox7.Text = "";
                                            textBox15.Text = "";
                                            textBox16.Text = "";
                                            Koisd();
                                            textBox2.Focus();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("加工数不能为空！", "提示");
                                        textBox2.Text = "";
                                        textBox3.Text = "";
                                        textBox4.Text = "";
                                        textBox5.Text = "";
                                        textBox6.Text = "";
                                        textBox7.Text = "";
                                        textBox15.Text = "";
                                        textBox16.Text = "";
                                        Koisd();
                                        textBox2.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("完成数量不能为空！", "提示");
                                    textBox2.Text = "";
                                    textBox3.Text = "";
                                    textBox4.Text = "";
                                    textBox5.Text = "";
                                    textBox6.Text = "";
                                    textBox7.Text = "";
                                    textBox15.Text = "";
                                    textBox16.Text = "";
                                    Koisd();
                                    textBox2.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("生产班组不能为空！", "提示");
                                textBox2.Text = "";
                                textBox3.Text = "";
                                textBox4.Text = "";
                                textBox5.Text = "";
                                textBox6.Text = "";
                                textBox7.Text = "";
                                textBox15.Text = "";
                                textBox16.Text = "";
                                Koisd();
                                textBox2.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("工序名称不能为空！", "提示");
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";
                            textBox6.Text = "";
                            textBox7.Text = "";
                            textBox15.Text = "";
                            textBox16.Text = "";
                            Koisd();
                            textBox2.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("工序号不能为空！", "提示");
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        textBox15.Text = "";
                        textBox16.Text = "";
                        Koisd();
                        textBox2.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("工单号不能为空！", "提示");
                    textBox1.Focus();
                }
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                button1.Enabled = true;
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox15.Text = "";
                textBox16.Text = "";
                Koisd();
                textBox2.Focus();
            }
            else if (button1.Enabled != false)
            {
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                button1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button4.Enabled = false;
                textBox4.Focus();
                button3.Text = "&X保存";
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("提示：请输入要删除的序号并按回车键！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (MessageBox.Show($"是否要删除< {textBox2.Text.Trim()} >工序号", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    string sql =
                        $"delete FROM tf_sgdantwo WHERE shigongdanhao='{textBox1.Text.Trim()}' AND xuhao='{textBox2.Text.Trim()}'";
                    SqlConnection conn = SqlHelper.GetConnection();
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();
                    try
                    {
                        SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql);
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("事务已回滚,尚未提交!" + ex);
                        tran.Rollback();
                    }
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox15.Text = "";
                    textBox16.Text = "";
                    Koisd();
                    textBox2.Focus();
                }
                else
                {
                    Koisd();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox1.ReadOnly = false;
            button1.Enabled = true;
            button2.Enabled = true;
            button4.Enabled = true;
            button3.Enabled = true;
            button3.Text = "&X修改";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox14.Text = "";
            textBox13.Text = "";
            textBox12.Text = "";
            textBox7.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox1.Focus();
            dataGridView1.DataSource = "";
        }

        private void Koisd()
        {
            if (textBox1.Text.Trim() != "")
            {
                string sql = "SELECT xuhao as '工序号',gongxumingcheng as '工序名称',jihuariqi as '计划完成日期'," +
                             "shengchanyuan as '生产班组',wanshengsuliang as'完成数量',jgsu as '加工数',xujah as'序价号'," +
                             "xujia as '序价',buzu as '补助',hezhi as '件资合计',lururiqi as '录入日期',luruyan as '录入人'," +
                             $"qiugairiqi as '修改日期',qgren as '修改人' FROM tf_sgdantwo WHERE shigongdanhao='{textBox1.Text.Trim()}'";
                DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 70;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[4].Width = 60;
                dataGridView1.Columns[5].Width = 60;
                dataGridView1.Columns[6].Width = 60;
                dataGridView1.Columns[7].Width = 70;
                dataGridView1.Columns[8].Width = 60;
                dataGridView1.Columns[9].Width = 100;
                dataGridView1.Columns[10].Width = 100;
                dataGridView1.Columns[11].Width = 65;
                dataGridView1.Columns[12].Width = 100;
                dataGridView1.Columns[13].Width = 65;
                dataGridView1.Sort(dataGridView1.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 7)
            {
                string sql = $"SELECT TOP 1 * FROM mf_sgdan WHERE shigongdanhao='{textBox1.Text.Trim()}'";
                SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (dr.HasRows)
                {
                    dr.Read();
                    if (!String.IsNullOrEmpty(dr["suhu"].ToString().Trim()) || !String.IsNullOrEmpty(dr["cls_id"].ToString().Trim()))
                    {
                        MessageBox.Show($"此工单号{textBox1.Text.Trim()}已审核锁定，请重新输入或解锁!!", "提示");
                        textBox1.Text = "";
                        dr.Close();
                    }
                    else
                    {
                        textBox8.Text = dr["kehu"].ToString().Trim();
                        textBox9.Text = dr["dingdanhao"].ToString().Trim();
                        textBox10.Text = dr["chanpintuhao"].ToString().Trim();
                        textBox11.Text = dr["chanpinmingcheng"].ToString().Trim();
                        textBox14.Text = dr["dingdansuliang"].ToString().Trim();
                        textBox12.Text = dr["jiaohuoqi"].ToString().Trim();
                        textBox13.Text = dr["tuzhiyema"].ToString().Trim();
                        dr.Close();
                        textBox1.ReadOnly = true;
                        textBox2.Focus();
                        Koisd();
                    }
                }
                else
                {
                    MessageBox.Show($"此工单号{textBox1.Text.Trim()}不存在，请重新输入!!", "提示");
                    textBox1.Text = "";
                    dr.Close();
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(textBox2.Text) && !String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("输入数据类型错误，请重新输入！", "提示");
                textBox2.Text = "";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsInt(textBox5.Text) && !String.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("只允许输入数字，请重新输入！", "提示");
                textBox5.Text = "";
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(textBox6.Text) && !String.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("输入数据类型错误，请重新输入！", "提示");
                textBox6.Text = "";
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.Text != "")
                {
                    string sql =
                        $"SELECT * FROM tf_sgdantwo WHERE shigongdanhao='{textBox1.Text.Trim()}' AND xuhao='{textBox2.Text.Trim()}'";
                    SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        textBox3.Text = dr["gongxumingcheng"].ToString().Trim();
                        textBox4.Text = dr["shengchanyuan"].ToString().Trim();
                        textBox5.Text = dr["wanshengsuliang"].ToString().Trim();
                        textBox6.Text = dr["jgsu"].ToString().Trim();
                        textBox7.Text = dr["xujah"].ToString().Trim();
                        _jihuadate = dr[5].ToString().Trim();
                        textBox4.Focus();
                    }
                    else
                    {
                        MessageBox.Show($"此工序号<{textBox2.Text.Trim()}>不存在，请重新输入!!", "提示");
                        textBox2.Text = "";
                    }
                    dr.Close();
                }
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox4.Focus();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox4.Text != "")
            {
                if (textBox3.Text == "激光")
                {
                    textBox7.Text = "G";
                }
                else if (textBox3.Text == "数冲")
                {
                    textBox7.Text = "S";
                }
                else
                {
                    if (textBox3.Text == "焊接")
                    {
                        textBox7.Text = "H1";
                    }
                    else if (textBox3.Text == "装配" || textBox3.Text == "入仓")
                    {
                        textBox7.Text = "Z1";
                    }
                    else if (textBox4.Text.Contains("机加") && !textBox4.Text.Contains("机加C"))
                    {
                        //string sql = "SELECT * FROM xujia WHERE lu='33F15E60-7C9B-4A2D-86FB-F15ED004BC40'";
                        //SqlDataReader sdddr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                        //if (sdddr.HasRows)
                        //{
                        //    sdddr.Read();
                        //    textBox14.Text = sdddr["xujhao"].ToString().Trim();
                        //}
                        //sdddr.Close();
                        textBox7.Text = "C15";
                    }
                    if (textBox3.Text != "折弯") { textBox6.Text = "0"; }
                    textBox5.Text = textBox14.Text;
                }
                textBox5.Focus();
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox5.Text != "")
            {
                //if (textBox6.Text != "" && textBox14.Text != "" && button1.Enabled && button3.Enabled)
                //{
                //    button1_Click(sender, e);
                //}
                //else
                //{
                textBox6.Focus();
                //}
                //上一个录入的工序与本工序对比,不能超
                int j = int.Parse(StringHelper.CutString(textBox2.Text.Trim(), 0, 1));
                //for (int i = 1; i < j; i++)
                //{
                //    string sql = "SELECT sum(wanshengsuliang) qt FROM tf_sgdantwo WHERE shigongdanhao='" + textBox1.Text.Trim() + "' AND xuhao like '" + (j - i) + "%'";
                //    string sql1 = "SELECT sum(wanshengsuliang) qt FROM tf_sgdantwo WHERE shigongdanhao='" + textBox1.Text.Trim() + "' AND xuhao like '" + j + "%'";
                //    SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                //    SqlDataReader dr1 = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql1);
                //    if (dr.HasRows && dr1.HasRows)
                //    {
                //        int qt = 0, qt1 = 0;
                //        dr.Read();
                //        dr1.Read();
                //        if (!String.IsNullOrEmpty(dr["qt"].ToString().Trim()))
                //        {
                //            qt = int.Parse(dr["qt"].ToString().Trim());
                //            if (!String.IsNullOrEmpty(dr1["qt"].ToString().Trim())) { qt1 = int.Parse(dr1["qt"].ToString().Trim()) + int.Parse(textBox5.Text.Trim()); }
                //            else { qt1 = int.Parse(textBox5.Text.Trim()); }
                //            if (qt < qt1)
                //            {
                //                MessageBox.Show("总数大于前工序数量,请修改!相差数量为:" + (qt1 - qt));
                //                //textBox5.Text = "";
                //                //textBox5.Focus();
                //                break;
                //            }
                //        }
                //        dr.Close();
                //    }
                //}

                string sql =
                    $"SELECT sum(wanshengsuliang) qt FROM tf_sgdantwo WHERE shigongdanhao='{textBox1.Text.Trim()}' AND xuhao like '{j}%'";
                SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (dr.HasRows)
                {
                    int qt = 0, qt1 = 0;
                    dr.Read();
                    if (!String.IsNullOrEmpty(textBox14.Text.Trim()))
                    {
                        qt = int.Parse(textBox14.Text.Trim());
                        if (!String.IsNullOrEmpty(dr["qt"].ToString().Trim())) { qt1 = int.Parse(dr["qt"].ToString().Trim()) + int.Parse(textBox5.Text.Trim()); }
                        else { qt1 = int.Parse(textBox5.Text.Trim()); }
                        if (qt < qt1)
                        {
                            MessageBox.Show("总数大于前工序数量,请修改!相差数量为:" + (qt1 - qt));
                        }
                    }
                    dr.Close();
                }
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox6.Text != "")
            {
                //if (textBox14.Text != "" && button1.Enabled && button3.Enabled)
                //{
                //    button1_Click(sender, e);
                //}
                //else
                //{
                textBox7.Focus();
                //}
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox7.Text != "")
            {
                if (button1.Enabled) button1_Click(sender, e);
                else button3_Click(sender, e);
            }
        }

        decimal _jgqt;
        private void Kogu()
        {
            if (textBox6.Text.Trim() == "0" || textBox6.Text.Trim() == "")
            {
                _jgqt = 1M;
            }
            else
            {
                _jgqt = decimal.Parse(textBox6.Text.Trim());
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
                label11.Text = "";
            }
            else
            {
                label11.Text = count.ToString();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            KeyEventArgs vKeyEventArgs = new KeyEventArgs(Keys.Enter);
            textBox2_KeyDown(sender, vKeyEventArgs);
        }

    }
}
