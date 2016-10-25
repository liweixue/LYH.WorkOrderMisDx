using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using LYH.Framework.Commons;
using LYH.WorkOrder.Properties;
using LYH.WorkOrder.share;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmProcCardEntry : Form
    {
        private const string Fosoey = "完成";
        private string _zlioky;

        public FrmProcCardEntry()
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

                //case Keys.F2:
                //    this.button1_Click(RuntimeHelpers.GetObjectValue(sender), e);
                //    break;
            }
        }

        private void Jlss()
        {
            var sql = "SELECT distinct sgdhao as '工单号',ddhao as '生产单号',kehu as '客户'," +
                      $"jhqi as '订单交期',tuhao as '图号',suliang as '数量' FROM udktr WHERE zling='{textBox1.Text.Trim()}'";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].Width = 80;
            /*dataGridView2.Columns[6].Width = 60;
           dataGridView2.Columns[7].Width = 60;
           dataGridView2.Columns[8].Width = 50;
           dataGridView2.Columns[9].Width = 50;
           dataGridView2.Columns[10].Width = 100;
           dataGridView2.Columns[11].Width = 100;
           dataGridView2.Columns[12].Width = 60;
           dataGridView2.Columns[13].Width = 60;*/
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }

        private void Sstwo()
        {
            var sql = "SELECT idaa as '0', gxone as'序号',gxname as'工序名称',gxtwo as'加工工序'," +
                      "tiao as'调机时间',danjian as'单件时间',xuj as'单价',buzu as '调机补助',tiaoren as '调机员'," +
                      "wssulia as'加工数量',wsbanz as'加工班组',bei3 as '生产员',hujii as '件资',wsriqi as'完成日期'," +
                      "lururiqi as'录入日期',lururen as'录入人',qgriqi as'修改日期',qgren as'修改人' FROM udktr " +
                      $"WHERE zling='{textBox1.Text.Trim()}' ORDER BY gxone, gxtwo";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns[0].Width = 5;
            dataGridView2.Columns[1].Width = 60;
            dataGridView2.Columns[2].Width = 80;
            dataGridView2.Columns[3].Width = 80;
            dataGridView2.Columns[4].Width = 100;
            dataGridView2.Columns[5].Width = 100;
            dataGridView2.Columns[6].Width = 100;
            dataGridView2.Columns[7].Width = 100;
            dataGridView2.Columns[8].Width = 100;
            dataGridView2.Columns[9].Width = 100;
            dataGridView2.Columns[10].Width = 100;
            dataGridView2.Columns[11].Width = 100;
            dataGridView2.Columns[12].Width = 100;
            dataGridView2.Columns[13].Width = 100;
            dataGridView2.Columns[14].Width = 100;
            dataGridView2.Columns[15].Width = 100;
            dataGridView2.Columns[16].Width = 100;
            dataGridView2.Columns[17].Width = 100;
            dataGridView2.Sort(dataGridView2.Columns[1], ListSortDirection.Ascending);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 7)
            {
                string sql = $"SELECT TOP 1 * FROM udktr WHERE zling='{textBox1.Text.Trim()}'";
                var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (dr.HasRows)
                {
                    Jlss();
                    Sstwo();
                    textBox1.ReadOnly = true;
                    textBox2.Focus();
                }
                else
                {
                    MessageBox.Show($"此指令单号{textBox1.Text.Trim()}不存在，请重新输入!!", Resources.T提示);
                    textBox1.Text = "";
                    Jlss();
                    Sstwo();
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(textBox3.Text) && !string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("输入数据类型错误，请重新输入！", Resources.T提示);
                textBox3.Text = "";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(textBox4.Text) && !string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("只允许输入正整数数字，请重新输入！", Resources.T提示);
                textBox4.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            button1.Visible = true;
            button3.Visible = true;
            dataGridView1.Visible = true;
            button2.Text = Resources.X修改;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            cbno1.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            comboBox2.Text = "";
            comboBox1.Text = "";
            textBox1.Focus();
            Jlss();
            Sstwo();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (StringHelper.IsNumeric(textBox2.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                string sql =
                    $"SELECT TOP 1 * FROM udktr WHERE zling='{textBox1.Text.Trim()}'AND gxone=' {textBox2.Text.Trim()}'";
                var zkg = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (!zkg.HasRows)
                {
                    MessageBox.Show("此序号不存在！", Resources.T提示);
                    textBox2.Text = "";
                }
            }
            else
            {
                MessageBox.Show("只允许输入数字，请重新输入！", Resources.T提示);
                textBox2.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {
                    if (textBox4.Text != "")
                    {
                        if (cbno1.Text != "")
                        {
                            if (textBox3.Text != "")
                            {
                                string sql =
                                    $"SELECT TOP 1 * FROM udktr WHERE zling='{textBox1.Text.Trim()}'AND gxone=' {textBox2.Text.Trim()}'AND gxtwo=' {textBox3.Text.Trim()}'";
                                var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    var ak74 = dr["xuj"].ToString().Trim();
                                    var subsidy = dr["buzu"].ToString().Trim();
                                    var wONo = dr["sgdhao"].ToString().Trim();
                                    var pONo = dr["ddhao"].ToString().Trim();
                                    var cust = dr["kehu"].ToString().Trim();
                                    var planDate = dr["jhqi"].ToString().Trim();
                                    var prtDwgNo = dr["tuhao"].ToString().Trim();
                                    var prtName = dr["name"].ToString().Trim();
                                    var pageNo = dr["yema"].ToString().Trim();
                                    var orderQty = dr["suliang"].ToString().Trim();
                                    var material = dr["cailiao"].ToString().Trim();
                                    var craft = dr["gxname"].ToString().Trim();
                                    var debugTime = dr["tiao"].ToString().Trim();
                                    var processTime = dr["danjian"].ToString().Trim();
                                    var createdDate = dr["cjriqi"].ToString().Trim();
                                    var createdBy = dr["cjren"].ToString().Trim();
                                    _zlioky = dr["gxname"].ToString().Trim();
                                    var jIko = decimal.Parse(textBox4.Text.Trim());
                                    var jIkt = decimal.Parse(ak74);
                                    //decimal jIkf = decimal.Parse(ak94);
                                    var jIkr = jIko*jIkt;
                                    if (!string.IsNullOrEmpty(dr["hujii"].ToString().Trim()))
                                    {
                                        dr.Close();
                                        if (MessageBox.Show($"此加工工序{textBox3.Text.Trim()}已录入过，请确认是否创建副工序!!",
                                            Resources.T提示, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) ==
                                            DialogResult.OK)
                                        {
                                            var ak = decimal.Parse(textBox3.Text.Trim());
                                            var jk = ak + 1;
                                            sql =
                                                $"SELECT Max(gxtwo) FROM udktr WHERE zling='{textBox1.Text.Trim()}' AND gxone='{textBox2.Text.Trim()}' AND gxtwo>='{textBox3.Text.Trim()}' AND gxtwo<'{jk}'";
                                            var kq = SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text,
                                                sql);
                                            var ma = Convert.ToDecimal(kq.ToString().Trim());
                                            //decimal ma = (decimal)kq;
                                            ma += 0.01M;
                                            //MessageBox.Show(ma.ToString(), "提示");
                                            sql =
                                                $"SELECT sum(buzu) FROM udktr WHERE zling='{textBox1.Text.Trim()}' AND gxone='{textBox2.Text.Trim()}' AND gxtwo>='{textBox3.Text.Trim()}' AND gxtwo<'{jk}'";
                                            SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text, sql);
                                            var m5A = Convert.ToDecimal(kq.ToString().Trim());
                                            var ceyo = m5A > 0 ? 0 : decimal.Parse(subsidy);
                                            //MessageBox.Show(ceyo.ToString(), "提示");
                                            sql =
                                                "insert into udktr(zling,sgdhao,ddhao,kehu,jhqi,tuhao,name,yema,suliang,cailiao,gxone," +
                                                "gxname,gxtwo,tiao,danjian,xuj,buzu,cjriqi,cjren,wssulia,wsbanz,wsriqi,hujii,lururiqi,lururen,bei3,tiaoren) " +
                                                $"values('{textBox1.Text.Trim()}','{wONo}','{pONo}','{cust}','{planDate}','{prtDwgNo}','{prtName}','{pageNo}','{orderQty}','{material}','{textBox2.Text.Trim()}','{craft}','{ma}','{debugTime}','{processTime}'," +
                                                $"'{ak74}','{ceyo}','{createdDate}','{createdBy}','{textBox4.Text.Trim()}','{cbno1.Text.Trim().ToUpper()}','{dateTimePicker1.Text.Trim()}','{jIkr}','{DateTime.Now}','{SqlHelper.UserName}','{comboBox2.Text.Trim()}','{comboBox1.Text.Trim()}')";
                                            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                                            if (_zlioky == "完成")
                                            {
                                                sql =
                                                    $"SELECT sum(wssulia) FROM udktr WHERE zling='{textBox1.Text.Trim()}' AND gxname='{Fosoey}'";
                                                var jup = SqlHelper.ExecuteScalar(SqlHelper.GetConnection(),
                                                    CommandType.Text, sql);
                                                int mtetid;
                                                if (jup == null)
                                                {
                                                    mtetid = 0;
                                                }
                                                else
                                                {
                                                    mtetid = (int) jup;
                                                }
                                                //MessageBox.Show(mtetid.ToString(), "提示");
                                                if (mtetid >= int.Parse(orderQty))
                                                {
                                                    const string kojou = "已完成";
                                                    sql =
                                                        $"UPDATE udone set beistr='{kojou}',wsoo='{mtetid}' WHERE sgdhao ='{wONo}'";
                                                    SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(),
                                                        CommandType.Text, sql);
                                                }
                                                else
                                                {
                                                    sql = $"UPDATE udone set wsoo='{mtetid}' WHERE sgdhao ='{wONo}'";
                                                    SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(),
                                                        CommandType.Text, sql);
                                                }
                                            }
                                            textBox2.Text = "";
                                            textBox3.Text = "";
                                            textBox4.Text = "";
                                            cbno1.Text = "";
                                            comboBox2.Text = "";
                                            comboBox1.Text = "";
                                            Sstwo();
                                            textBox2.Focus();
                                        }
                                        else
                                        {
                                            textBox3.Text = "";
                                            textBox4.Text = "";
                                            cbno1.Text = "";
                                            comboBox2.Text = "";
                                            comboBox1.Text = "";
                                            textBox3.Focus();
                                        }
                                    }
                                    else
                                    {
                                        sql =
                                            $"UPDATE udktr set wssulia='{textBox4.Text.Trim()}',wsbanz='{cbno1.Text.Trim().ToUpper()}',wsriqi='{dateTimePicker1.Text.Trim()}',hujii='{jIkr}',bei3='{comboBox2.Text.Trim()}'," +
                                            $"lururiqi='{DateTime.Now}',lururen='{SqlHelper.UserName}',tiaoren='{comboBox1.Text.Trim()}' WHERE zling ='{textBox1.Text.Trim()}' AND gxone='{textBox2.Text.Trim()}' AND gxtwo='{textBox3.Text.Trim()}'";
                                        SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                                        if (_zlioky == "完成")
                                        {
                                            sql =
                                                $"SELECT sum(wssulia) as sum1 FROM udktr WHERE zling='{textBox1.Text.Trim()}'AND gxname='{Fosoey}'";
                                            var mtetid =
                                                (int)
                                                    SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text,
                                                        sql);
                                            //conn.Open();
                                            //object jup = (object)cmd987.ExecuteScalar();
                                            //int mtetid = (int)jup;
                                            //cmd987.
                                            //MessageBox.Show(mtetid.ToString(), "提示");
                                            if (mtetid >= int.Parse(orderQty))
                                            {
                                                const string kojou = "已完成";
                                                sql =
                                                    $"UPDATE udone set beistr='{kojou}',wsoo='{mtetid}' WHERE sgdhao ='{wONo}'";
                                                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text,
                                                    sql);
                                            }
                                            else
                                            {
                                                sql = $"UPDATE udone set wsoo='{mtetid}' WHERE sgdhao ='{wONo}'";
                                                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text,
                                                    sql);
                                            }
                                        }
                                        Sstwo();
                                        textBox2.Text = "";
                                        textBox3.Text = "";
                                        textBox4.Text = "";
                                        cbno1.Text = "";
                                        comboBox2.Text = "";
                                        comboBox1.Text = "";
                                        textBox2.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("此加工工序不存在！", Resources.T提示);
                                    textBox3.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("加工工序不能为空！", Resources.T提示);
                                textBox3.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("加工班组不能为空！", Resources.T提示);
                            cbno1.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("加工数量不能为空！", Resources.T提示);
                        textBox4.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("序号不能为空！", Resources.T提示);
                    textBox2.Focus();
                }
            }
            else
            {
                MessageBox.Show("指令单号不能为空！", Resources.T提示);
                textBox1.ReadOnly = false;
                textBox1.Focus();
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
            if (e.KeyCode == Keys.Enter)
            {
                cbno1.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e) //修改
        {
            //string kygo;
            //string ykgo;
            if (button2.Text == Resources.X修改)
            {
                if (textBox2.Text != "")
                {
                    if (MessageBox.Show("是否已经选择好所要修改的行，若未选好请按取消！",
                        Resources.T提示, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        var mMi = dataGridView2.SelectedCells[0].Value.ToString().Trim();
                        var zMi = dataGridView2.SelectedCells[1].Value.ToString().Trim();
                        if (textBox2.Text == zMi)
                        {
                            string sql = $"SELECT TOP 1 * FROM udktr WHERE idaa='{mMi}'";
                            var sqlDataReader = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                            if (sqlDataReader.HasRows)
                            {
                                sqlDataReader.Read();
                                textBox2.Text = sqlDataReader["gxone"].ToString().Trim();
                                _zlioky = sqlDataReader["gxname"].ToString().Trim();
                                textBox3.Text = sqlDataReader["gxtwo"].ToString().Trim();
                                textBox4.Text = sqlDataReader["wssulia"].ToString().Trim();
                                cbno1.Text = sqlDataReader["wsbanz"].ToString().Trim();
                                textBox6.Text = sqlDataReader["tiao"].ToString().Trim();
                                textBox7.Text = sqlDataReader["danjian"].ToString().Trim();
                                textBox8.Text = sqlDataReader["xuj"].ToString().Trim();
                                textBox9.Text = sqlDataReader["buzu"].ToString().Trim();
                                comboBox2.Text = sqlDataReader["bei3"].ToString().Trim();
                                comboBox1.Text = sqlDataReader["tiaoren"].ToString().Trim();
                                textBox2.ReadOnly = true;
                                textBox3.ReadOnly = true;
                                button1.Visible = false;
                                button3.Visible = false;
                                dataGridView1.Visible = false;
                                button2.Text = Resources.X保存;
                            }
                            sqlDataReader.Close();
                        }
                        else
                        {
                            MessageBox.Show("序号与选择的行不符，请重新输入或选择！", Resources.T提示);
                            textBox2.Focus();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("序号不能为空！", Resources.T提示);
                    textBox2.Focus();
                }
            }
            else
            {
                var jIko = decimal.Parse(textBox4.Text.Trim());
                var jIkt = decimal.Parse(textBox8.Text.Trim());
                //decimal jIkf = decimal.Parse(textBox9.Text.Trim());
                var jIkr = jIko*jIkt;
                var sql =
                    $"UPDATE udktr set wssulia='{textBox4.Text.Trim()}',wsbanz='{cbno1.Text.Trim().ToUpper()}',tiao='{textBox6.Text.Trim()}',danjian='{textBox7.Text.Trim()}',bei3='{comboBox2.Text.Trim()}'," +
                    $"tiaoren='{comboBox1.Text.Trim()}',xuj='{textBox8.Text.Trim()}',buzu='{textBox9.Text.Trim()}',qgriqi='{DateTime.Now}',qgren='{SqlHelper.UserName}',hujii='{jIkr}' WHERE zling ='{textBox1.Text.Trim()}'AND " +
                    $"gxone='{textBox2.Text.Trim()}' AND gxtwo='{textBox3.Text.Trim()}'";
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (_zlioky == "完成")
                {
                    sql = $"SELECT sum(wssulia) FROM udktr WHERE zling='{textBox1.Text.Trim()}'AND gxname='{Fosoey}'";
                    var mtetid = (int) SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text, sql);
                    sql = $"SELECT TOP 1 * FROM udktr WHERE zling='{textBox1.Text.Trim()}'";
                    var sdr593 = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                    if (sdr593.HasRows)
                    {
                        sdr593.Read();
                        var kygo = sdr593["suliang"].ToString().Trim();
                        var ykgo = sdr593["sgdhao"].ToString().Trim();
                        //MessageBox.Show(mtetid.ToString(), "提示");
                        if (mtetid >= int.Parse(kygo))
                        {
                            const string kojou = "已完成";
                            sql = $"UPDATE udone set beistr='{kojou}',wsoo='{mtetid}' WHERE sgdhao ='{ykgo}'";
                            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                        }
                        else
                        {
                            const string kojou = "未完成";
                            string sqlk897 =
                                $"UPDATE udone set beistr='{kojou}' ,wsoo='{mtetid}' WHERE sgdhao ='{ykgo}'";
                            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sqlk897);
                        }
                    }
                }
                button2.Text = Resources.X修改;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                dataGridView1.Visible = true;
                button1.Visible = true;
                button3.Visible = true;
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                cbno1.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                comboBox2.Text = "";
                comboBox1.Text = "";
                textBox2.Focus();
                Sstwo();
            }
        }

        private void button3_Click(object sender, EventArgs e) //删除
        {
            var hMi = dataGridView2.SelectedCells[1].Value.ToString().Trim();
            var aMi = dataGridView2.SelectedCells[2].Value.ToString().Trim();
            var oky = dataGridView2.SelectedCells[3].Value.ToString().Trim();
            var fky = dataGridView2.SelectedCells[9].Value.ToString().Trim();
            //MessageBox.Show(aMi, "提示");
            int i;
            var b = int.TryParse(oky, out i);
            if (b)
            {
                MessageBox.Show("此行不可删除，只可修改", Resources.T提示);
            }
            else
            {
                if (MessageBox.Show("是否要删除所选择的行", Resources.J警告, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) ==
                    DialogResult.OK)
                {
                    string sql;
                    if (aMi == "完成")
                    {
                        sql = $"SELECT sum(wssulia) FROM udktr WHERE zling='{textBox1.Text.Trim()}'AND gxname='{aMi}'";
                        var mtetid = (int) SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text, sql);
                        sql = $"SELECT TOP 1 * FROM udktr WHERE zling='{textBox1.Text.Trim()}'";
                        var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                        if (dr.HasRows)
                        {
                            dr.Read();
                            var kygo = dr["suliang"].ToString().Trim();
                            var ykgo = dr["sgdhao"].ToString().Trim();
                            dr.Close();
                            //MessageBox.Show(mtetid.ToString(), "提示");
                            mtetid = mtetid - int.Parse(fky);
                            if (mtetid >= int.Parse(kygo))
                            {
                                const string kojou = "已完成";
                                sql = $"UPDATE udone set beistr='{kojou}',wsoo='{mtetid}' WHERE sgdhao ='{ykgo}'";
                                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                                sql =
                                    $"delete FROM udktr WHERE zling='{textBox1.Text.Trim()}'AND gxone=' {hMi}'AND gxtwo=' {oky}'";
                                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                                Sstwo();
                            }
                            else
                            {
                                const string kojou = "未完成";
                                sql = $"UPDATE udone set beistr='{kojou}' ,wsoo='{mtetid}' WHERE sgdhao ='{ykgo}'";
                                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                                dr.Close();
                                sql =
                                    $"delete FROM udktr WHERE zling='{textBox1.Text.Trim()}'AND gxone=' {hMi}'AND gxtwo=' {oky}'";
                                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                                Sstwo();
                            }
                        }
                    }
                    else
                    {
                        sql =
                            $"delete FROM udktr WHERE zling='{textBox1.Text.Trim()}'AND gxone=' {hMi}'AND gxtwo=' {oky}'";
                        SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                        Sstwo();
                    }
                }
            }
        }

        /*
                private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
                {
                    if (cbno1.Text != "")
                    {
                        string sql3 = "SELECT distinct REN FROM udbz WHERE BZname='" + textBox1.Text.Trim() + "' ";
                        conn.Open();
                        comboBox1.Items.Clear();
                        comboBox1.Items.Add("");
                        comboBox1.Text = "";
                        comboBox2.Items.Clear();
                        comboBox2.Items.Add("");
                        comboBox2.Text = "";
                        SqlCommand cmd3 = new SqlCommand(sql3, conn);
                        SqlDataReader dr3 = cmd3.ExecuteReader();
                        while (dr3.Read())
                        {
                            this.comboBox1.Items.Add(dr3["BZname"].ToString().Trim());
                            this.comboBox2.Items.Add(dr3["BZname"].ToString().Trim());
                        }
                         conn.Close();
                    }
                }
                */

        private void Form18_Load(object sender, EventArgs e)
        {
            const string sql = "SELECT distinct BZname FROM udbz ";
            var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            while (dr.Read())
            {
                cbno1.Items.Add(dr["BZname"].ToString().Trim());
            }
            dr.Close();
        }

        private void cbno1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbno1.Text != "")
            {
                string sql = $"SELECT distinct REN FROM udbz WHERE BZname='{cbno1.Text.Trim()}'";
                comboBox1.Items.Clear();
                comboBox1.Items.Add("");
                comboBox1.Text = "";
                comboBox2.Items.Clear();
                comboBox2.Items.Add("");
                comboBox2.Text = "";
                var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["REN"].ToString().Trim());
                    comboBox2.Items.Add(dr["REN"].ToString().Trim());
                }
                dr.Close();
            }
        }
    }
}