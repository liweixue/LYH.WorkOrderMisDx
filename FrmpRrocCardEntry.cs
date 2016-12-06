using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using LYH.WorkOrder.Properties;
using LYH.WorkOrder.share;

namespace LYH.WorkOrder
{
    public partial class FrmProcCardEntry : Form
    {
        private const string Fosoey = "完成";
        private string _zlioky;
        readonly Dictionary<string, string> _dict = new Dictionary<string, string>();

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

        private void Form18_Load(object sender, EventArgs e)
        {
            var sql =
                $"SELECT a.ID,a.Team FROM PD_ProcCard_Team a JOIN TB_Dept b ON b.ID=a.DeptId WHERE b.ID='{SqlHelper.DeptId}'";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString("dzdj"), CommandType.Text, sql);
            
            foreach (DataRow dr in ds.Tables[0].Rows)
                _dict.Add(dr[0].ToString(), dr[1].ToString());

            cbxTeam.SpellSearchSource = _dict.Values.ToArray();
                //(from DataRow dr in ds.Tables[0].Rows where !Convert.IsDBNull(dr[1]) select dr[1].ToString().Trim())
                //    .ToArray();
            var acsc = new AutoCompleteStringCollection();
            foreach (var dictValue in _dict)
                acsc.Add(dictValue.Value);
            cbxTeam.AutoCompleteCustomSource = acsc;
            //cbxTeam.Tag = _dict;

            //const string sql = "SELECT distinct BZname FROM udbz ";
            //var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            //while (dr.Read())
            //{
            //    cbxTeam.Items.Add(dr["BZname"].ToString().Trim());
            //}
            //dr.Close();
        }

        private void BindDataDgv1()
        {
            var sql = "SELECT distinct sgdhao as '工单号',ddhao as '生产单号',kehu as '客户'," +
                      $"jhqi as '订单交期',tuhao as '图号',suliang as '数量' FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'";
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

        private void BindDataDgv2()
        {
            var sql = "SELECT idaa as '0', gxone as'序号',gxname as'工序名称',gxtwo as'加工工序'," +
                      "tiao as'调机时间',danjian as'单件时间',xuj as'单价',buzu as '调机补助',tiaoren as '调机员'," +
                      "wssulia as'加工数量',wsbanz as'加工班组',bei3 as '生产员',hujii as '件资',wsriqi as'完成日期'," +
                      "lururiqi as'录入日期',lururen as'录入人',qgriqi as'修改日期',qgren as'修改人' FROM udktr " +
                      $"WHERE zling='{txtProcCardNo.Text.Trim()}' ORDER BY gxone, gxtwo";
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
            if (txtProcCardNo.Text.Length == 7)
            {
                string sql = $"SELECT TOP 1 * FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'";
                var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (dr.HasRows)
                {
                    BindDataDgv1();
                    BindDataDgv2();
                    txtProcCardNo.ReadOnly = true;
                    txtSeq.Focus();
                }
                else
                {
                    MessageBox.Show($"此工艺卡号{txtProcCardNo.Text.Trim()}不存在，请重新输入!!", Resources.T提示);
                    txtProcCardNo.Text = "";
                    BindDataDgv1();
                    BindDataDgv2();
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(txtProcessSeq.Text) && !string.IsNullOrEmpty(txtProcessSeq.Text))
            {
                MessageBox.Show("输入数据类型错误，请重新输入！", Resources.T提示);
                txtProcessSeq.Text = "";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(txtProcessQty.Text) && !string.IsNullOrEmpty(txtProcessQty.Text))
            {
                MessageBox.Show("只允许输入正整数数字，请重新输入！", Resources.T提示);
                txtProcessQty.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtProcCardNo.ReadOnly = false;
            txtSeq.ReadOnly = false;
            txtProcessSeq.ReadOnly = false;
            button1.Visible = true;
            button3.Visible = true;
            dataGridView1.Visible = true;
            button2.Text = Resources.X修改;
            txtProcCardNo.Text = "";
            txtSeq.Text = "";
            ClearTxt();
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            txtProcCardNo.Focus();
            BindDataDgv1();
            BindDataDgv2();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (StringHelper.IsNumeric(txtSeq.Text) && !string.IsNullOrEmpty(txtSeq.Text))
            {
                string sql =
                    $"SELECT TOP 1 * FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'AND gxone=' {txtSeq.Text.Trim()}'";
                var zkg = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (!zkg.HasRows)
                {
                    MessageBox.Show("此序号不存在！", Resources.T提示);
                    txtSeq.Text = "";
                }
            }
            else
            {
                MessageBox.Show("只允许输入数字，请重新输入！", Resources.T提示);
                txtSeq.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtProcCardNo.Text == "")
            {
                MessageBox.Show("工艺卡号不能为空！", Resources.T提示);
                txtProcCardNo.ReadOnly = false;
                txtProcCardNo.Focus();
                return;
            }
            if (txtSeq.Text == "")
            {
                MessageBox.Show("序号不能为空！", Resources.T提示);
                txtSeq.Focus();
                return;
            }
            if (txtProcessQty.Text == "")
            {
                MessageBox.Show("加工数量不能为空！", Resources.T提示);
                txtProcessQty.Focus();
                return;
            }
            if (cbxTeam.Text == "")
            {
                MessageBox.Show("加工班组不能为空！", Resources.T提示);
                cbxTeam.Focus();
                return;
            }
            if (txtProcessSeq.Text == "")
            {
                MessageBox.Show("加工工序不能为空！", Resources.T提示);
                txtProcessSeq.Focus();
                return;
            }
            string sql =
                $"SELECT TOP 1 * FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'AND gxone=' {txtSeq.Text.Trim()}'AND gxtwo=' {txtProcessSeq.Text.Trim()}'";
            var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            if (!dr.HasRows)
            {
                MessageBox.Show("此加工工序不存在！", Resources.T提示);
                txtProcessSeq.Focus();
            }
            else
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
                var jIko = decimal.Parse(txtProcessQty.Text.Trim());
                var jIkt = decimal.Parse(ak74);
                //decimal jIkf = decimal.Parse(ak94);
                var jIkr = jIko*jIkt;
                if (!string.IsNullOrEmpty(dr["hujii"].ToString().Trim()))
                {
                    dr.Close();
                    if (MessageBox.Show($"此加工工序{txtProcessSeq.Text.Trim()}已录入过，请确认是否创建副工序!!",
                            Resources.T提示, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) ==
                        DialogResult.OK)
                    {
                        var ak = decimal.Parse(txtProcessSeq.Text.Trim());
                        var jk = ak + 1;
                        sql =
                            $"SELECT Max(gxtwo) FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}' AND gxone='{txtSeq.Text.Trim()}' AND gxtwo>='{txtProcessSeq.Text.Trim()}' AND gxtwo<'{jk}'";
                        var kq = SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text,
                            sql);
                        var ma = Convert.ToDecimal(kq.ToString().Trim());
                        //decimal ma = (decimal)kq;
                        ma += 0.01M;
                        //MessageBox.Show(ma.ToString(), "提示");
                        sql =
                            $"SELECT sum(buzu) FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}' AND gxone='{txtSeq.Text.Trim()}' AND gxtwo>='{txtProcessSeq.Text.Trim()}' AND gxtwo<'{jk}'";
                        SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text, sql);
                        var m5A = Convert.ToDecimal(kq.ToString().Trim());
                        var ceyo = m5A > 0 ? 0 : decimal.Parse(subsidy);
                        //MessageBox.Show(ceyo.ToString(), "提示");
                        sql =
                            "insert into udktr(zling,sgdhao,ddhao,kehu,jhqi,tuhao,name,yema,suliang,cailiao,gxone," +
                            "gxname,gxtwo,tiao,danjian,xuj,buzu,cjriqi,cjren,wssulia,wsbanz,wsriqi,hujii,lururiqi,lururen,bei3,tiaoren) " +
                            $"values('{txtProcCardNo.Text.Trim()}','{wONo}','{pONo}','{cust}','{planDate}','{prtDwgNo}','{prtName}','{pageNo}','{orderQty}','{material}','{txtSeq.Text.Trim()}','{craft}','{ma}','{debugTime}','{processTime}'," +
                            $"'{ak74}','{ceyo}','{createdDate}','{createdBy}','{txtProcessQty.Text.Trim()}','{cbxTeam.Text.Trim().ToUpper()}','{dateTimePicker1.Text.Trim()}','{jIkr}','{DateTime.Now}','{SqlHelper.UserName}','{cbxProcesser.Text.Trim()}','{cbxAdjust.Text.Trim()}')";
                        SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                        if (_zlioky == "完成")
                        {
                            sql =
                                $"SELECT sum(wssulia) FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}' AND gxname='{Fosoey}'";
                            var jup = SqlHelper.ExecuteScalar(SqlHelper.GetConnection(),
                                CommandType.Text, sql);
                            int mtetid;
                            if (jup == null)
                                mtetid = 0;
                            else
                                mtetid = (int) jup;
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
                        txtSeq.Text = "";
                        ClearTxt();
                        BindDataDgv2();
                        txtSeq.Focus();
                    }
                    else
                    {
                        ClearTxt();
                        txtProcessSeq.Focus();
                    }
                }
                else
                {
                    sql =
                        $"UPDATE udktr set wssulia='{txtProcessQty.Text.Trim()}',wsbanz='{cbxTeam.Text.Trim().ToUpper()}',wsriqi='{dateTimePicker1.Text.Trim()}',hujii='{jIkr}',bei3='{cbxProcesser.Text.Trim()}'," +
                        $"lururiqi='{DateTime.Now}',lururen='{SqlHelper.UserName}',tiaoren='{cbxAdjust.Text.Trim()}' WHERE zling ='{txtProcCardNo.Text.Trim()}' AND gxone='{txtSeq.Text.Trim()}' AND gxtwo='{txtProcessSeq.Text.Trim()}'";
                    SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                    if (_zlioky == "完成")
                    {
                        sql =
                            $"SELECT sum(wssulia) as sum1 FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'AND gxname='{Fosoey}'";
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
                    BindDataDgv2();
                    txtSeq.Text = "";
                    ClearTxt();
                    txtSeq.Focus();
                }
            }
        }

        private void ClearTxt()
        {
            txtProcessSeq.Text = "";
            txtProcessQty.Text = "";
            cbxTeam.Text = "";
            cbxProcesser.Text = "";
            cbxAdjust.Text = "";
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProcessQty.Focus();
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cbxTeam.Focus();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProcessSeq.Focus();
        }

        private void button2_Click(object sender, EventArgs e) //修改
        {
            //string kygo;
            //string ykgo;
            if (button2.Text == Resources.X修改)
            {
                if (txtSeq.Text != "")
                {
                    if (MessageBox.Show("是否已经选择好所要修改的行，若未选好请按取消！",
                            Resources.T提示, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        var mMi = dataGridView2.SelectedCells[0].Value.ToString().Trim();
                        var zMi = dataGridView2.SelectedCells[1].Value.ToString().Trim();
                        if (txtSeq.Text == zMi)
                        {
                            string sql = $"SELECT TOP 1 * FROM udktr WHERE idaa='{mMi}'";
                            var sqlDataReader = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                            if (sqlDataReader.HasRows)
                            {
                                sqlDataReader.Read();
                                txtSeq.Text = sqlDataReader["gxone"].ToString().Trim();
                                _zlioky = sqlDataReader["gxname"].ToString().Trim();
                                txtProcessSeq.Text = sqlDataReader["gxtwo"].ToString().Trim();
                                txtProcessQty.Text = sqlDataReader["wssulia"].ToString().Trim();
                                cbxTeam.Text = sqlDataReader["wsbanz"].ToString().Trim();
                                textBox6.Text = sqlDataReader["tiao"].ToString().Trim();
                                textBox7.Text = sqlDataReader["danjian"].ToString().Trim();
                                textBox8.Text = sqlDataReader["xuj"].ToString().Trim();
                                textBox9.Text = sqlDataReader["buzu"].ToString().Trim();
                                cbxProcesser.Text = sqlDataReader["bei3"].ToString().Trim();
                                cbxAdjust.Text = sqlDataReader["tiaoren"].ToString().Trim();
                                txtSeq.ReadOnly = true;
                                txtProcessSeq.ReadOnly = true;
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
                            txtSeq.Focus();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("序号不能为空！", Resources.T提示);
                    txtSeq.Focus();
                }
            }
            else
            {
                var jIko = decimal.Parse(txtProcessQty.Text.Trim());
                var jIkt = decimal.Parse(textBox8.Text.Trim());
                //decimal jIkf = decimal.Parse(textBox9.Text.Trim());
                var jIkr = jIko*jIkt;
                var sql =
                    $"UPDATE udktr set wssulia='{txtProcessQty.Text.Trim()}',wsbanz='{cbxTeam.Text.Trim().ToUpper()}',tiao='{textBox6.Text.Trim()}',danjian='{textBox7.Text.Trim()}',bei3='{cbxProcesser.Text.Trim()}'," +
                    $"tiaoren='{cbxAdjust.Text.Trim()}',xuj='{textBox8.Text.Trim()}',buzu='{textBox9.Text.Trim()}',qgriqi='{DateTime.Now}',qgren='{SqlHelper.UserName}',hujii='{jIkr}' WHERE zling ='{txtProcCardNo.Text.Trim()}'AND " +
                    $"gxone='{txtSeq.Text.Trim()}' AND gxtwo='{txtProcessSeq.Text.Trim()}'";
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (_zlioky == "完成")
                {
                    sql =
                        $"SELECT sum(wssulia) FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'AND gxname='{Fosoey}'";
                    var mtetid = (int) SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text, sql);
                    sql = $"SELECT TOP 1 * FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'";
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
                txtSeq.ReadOnly = false;
                txtProcessSeq.ReadOnly = false;
                dataGridView1.Visible = true;
                button1.Visible = true;
                button3.Visible = true;
                txtSeq.Text = "";
                ClearTxt();
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                txtSeq.Focus();
                BindDataDgv2();
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
                        sql =
                            $"SELECT sum(wssulia) FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'AND gxname='{aMi}'";
                        var mtetid = (int) SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text, sql);
                        sql = $"SELECT TOP 1 * FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'";
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
                                    $"delete FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'AND gxone=' {hMi}'AND gxtwo=' {oky}'";
                                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                                BindDataDgv2();
                            }
                            else
                            {
                                const string kojou = "未完成";
                                sql = $"UPDATE udone set beistr='{kojou}' ,wsoo='{mtetid}' WHERE sgdhao ='{ykgo}'";
                                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                                dr.Close();
                                sql =
                                    $"delete FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'AND gxone=' {hMi}'AND gxtwo=' {oky}'";
                                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                                BindDataDgv2();
                            }
                        }
                    }
                    else
                    {
                        sql =
                            $"delete FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'AND gxone=' {hMi}'AND gxtwo=' {oky}'";
                        SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                        BindDataDgv2();
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

        private void cbxAdjust_Enter(object sender, EventArgs e)
        {
            cbxAdjust.Focus();
            var dictName = new Dictionary<string, string>();
            if (cbxTeam.Text == "") return;
            if ((_dict != null) && _dict.ContainsValue(cbxTeam.Text))
            {
                var id = _dict.Where(d => d.Value == cbxTeam.Text).Select(d => d.Key).SingleOrDefault();
                string sql = $"SELECT ID,Name FROM DZDJ.dbo.PD_ProcCard_Employee WHERE TeamId={id}";
                //cbxAdjust.Items.Clear();
                //cbxAdjust.Items.Add("");
                //cbxAdjust.Text = "";
                //cbxProcesser.Items.Clear();
                //cbxProcesser.Items.Add("");
                //cbxProcesser.Text = "";

                var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString("dzdj"), CommandType.Text, sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                    dictName.Add(dr[0].ToString(), dr[1].ToString());

                cbxAdjust.SpellSearchSource = cbxProcesser.SpellSearchSource = dictName.Values.ToArray();
                var acsc = new AutoCompleteStringCollection();
                foreach (var dictValue in dictName)
                    acsc.Add(dictValue.Value);
                cbxAdjust.AutoCompleteCustomSource = cbxProcesser.AutoCompleteCustomSource = acsc;
            }
        }
    }
}