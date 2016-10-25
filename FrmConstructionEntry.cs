using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using LYH.Framework.Commons;
using LYH.WorkOrder.Properties;
using LYH.WorkOrder.share;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmConstructionEntry : Form
    {
        private readonly string _teamHj = Resources.H焊接;
        private readonly string _teamJg = Resources.G激光;
        private readonly string _teamRc = Resources.Z入仓;
        private readonly string _teamSc = Resources.S数冲;
        private readonly string _teamZp = Resources.Z装配;
        private readonly string _teamZw = Resources.W折弯;

        /// <summary>
        ///     计划日期
        /// </summary>
        private string _planDate;

        /// <summary>
        ///     加工数
        /// </summary>
        private decimal _processNum;

        /// <summary>
        ///     工单号
        /// </summary>
        private string _wono;

        public FrmConstructionEntry()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (SqlHelper.UserType == Resources.UT_Check || SqlHelper.UserType == Resources.UT_Admin)
            {
                btnCheck.Enabled = true;
                btnCheck.Visible = true;
            }
            else
            {
                btnCheck.Enabled = false;
                btnCheck.Visible = false;
            }
            //string sql = "SELECT deptname FROM dept";
            //DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnSting(SqlHelper.connectionString), CommandType.Text, sql);
            //textBox4.SpellSearchSource = GetSpellBoxSource(ds.Tables[0]);
        }

        public string[] GetSpellBoxSource(DataTable dt)
        {
            return
                (from DataRow dr in dt.Rows
                 where !Convert.IsDBNull(dr["deptname"])
                 select dr["deptname"].ToString().Trim()).ToArray();
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
                //    this.btnSave_Click(RuntimeHelpers.GetObjectValue(sender), e);
                //    break;
            }
        }

        /// <summary>
        ///     DataGridView的绑定数据
        /// </summary>
        private void BindData()
        {
            _wono = txtWONo.Text.Trim();
            if (_wono == "") return;
            var sql = "SELECT xuhao '工序号',gongxumingcheng '工序名称',jihuariqi '计划完成日期'," +
                      "shengchanyuan '生产班组',wanshengsuliang '完成数量',jgsu '加工数',RTRIM(xujah) '序价号'," +
                      "xujia '序价',buzu '补助',hezhi '件资合计',lururiqi '录入日期',RTRIM(luruyan) '录入人'," +
                      $"qiugairiqi '修改日期',RTRIM(qgren) '修改人' FROM tf_sgdantwo WHERE shigongdanhao='{_wono}'";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnSting(), CommandType.Text, sql);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            if (IsChecked(_wono)) lblCheck.Visible = true;
        }

        /// <summary>
        ///     工单号内容更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWONo_TextChanged(object sender, EventArgs e)
        {
            if (txtWONo.Text.Length != 7) return;
            _wono = txtWONo.Text.Trim();
            var sql = $"SELECT TOP 1 * FROM mf_sgdan WHERE shigongdanhao='{_wono}'";
            var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
            if (dr.HasRows)
            {
                dr.Read();
                var disable = dr["cls_id"].ToString().Trim();
                if (!String.IsNullOrEmpty(dr["suhu"].ToString().Trim()) || disable == "T")
                {
                    MessageBox.Show($"此工单号{_wono}已审核或停用锁定，请重新输入或解锁!!", Resources.T提示);
                    txtWONo.Text = "";
                }
                else
                {
                    txtCust.Text = dr["kehu"].ToString().Trim();
                    txtOrderNo.Text = dr["dingdanhao"].ToString().Trim();
                    txtPartNo.Text = dr["chanpintuhao"].ToString().Trim();
                    txtPartName.Text = dr["chanpinmingcheng"].ToString().Trim();
                    txtOrderQt.Text = dr["dingdansuliang"].ToString().Trim();
                    txtDeliveryDate.Text = dr["jiaohuoqi"].ToString().Trim();
                    txtPageNo.Text = dr["tuzhiyema"].ToString().Trim();
                    EnableCtrls(!IsChecked(_wono));
                    txtWONo.ReadOnly = true;
                    txtSn.Focus();
                    BindData();
                }
                dr.Close();
            }
            else
            {
                MessageBox.Show($"此工单号{txtWONo.Text.Trim()}不存在，请重新输入!!", Resources.T提示);
                txtWONo.Text = "";
            }
        }

        /// <summary>
        ///     校验工序号是否是数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSn_TextChanged(object sender, EventArgs e)
        {
            if (StringHelper.IsNumeric(txtSn.Text) || String.IsNullOrEmpty(txtSn.Text)) return;
            MessageBox.Show(Resources.IsNotNumber, Resources.T提示);
            txtSn.Text = "";
        }

        /// <summary>
        ///     校验完成数量是否是正整数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCompleteNum_TextChanged(object sender, EventArgs e)
        {
            if (StringHelper.IsInt(txtCompleteNum.Text) || String.IsNullOrEmpty(txtCompleteNum.Text)) return;
            MessageBox.Show(@"只允许输入正整数，请重新输入！", Resources.T提示);
            txtCompleteNum.Text = "";
        }

        /// <summary>
        ///     校验加工数是否是数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtProcessNum_TextChanged(object sender, EventArgs e)
        {
            if (StringHelper.IsNumeric(txtProcessNum.Text) || String.IsNullOrEmpty(txtProcessNum.Text)) return;
            MessageBox.Show(Resources.IsNotNumber, Resources.T提示);
            txtProcessNum.Text = "";
        }

        /// <summary>
        ///     工序号keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (txtSn.Text == "") return;
            var sql =
                $"SELECT * FROM tf_sgdantwo WHERE shigongdanhao='{txtWONo.Text.Trim()}' AND xuhao='{txtSn.Text.Trim()}'";
            var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
            if (dr.HasRows)
            {
                dr.Read();
                txtCraft.Text = dr["gongxumingcheng"].ToString().Trim();
                txtTeam.Text = dr["shengchanyuan"].ToString().Trim();
                txtCompleteNum.Text = dr["wanshengsuliang"].ToString().Trim();
                txtProcessNum.Text = dr["jgsu"].ToString().Trim();
                txtPriceNo.Text = dr["xujah"].ToString().Trim();
                _planDate = dr[5].ToString().Trim();
                txtTeam.Focus();
            }
            else
            {
                MessageBox.Show($"此工序号<{txtSn.Text.Trim()}>不存在，请重新输入!!", Resources.T提示);
                txtSn.Text = "";
            }
            dr.Close();
        }

        /// <summary>
        ///     工序名称keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCraft_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTeam.Focus();
            }
        }

        /// <summary>
        ///     班组keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTeam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || txtTeam.Text == "") return;
            if (txtCraft.Text == Resources.G激光)
            {
                txtPriceNo.Text = Resources.JG_G;
            }
            else if (txtCraft.Text == Resources.S数冲)
            {
                txtPriceNo.Text = Resources.SC_S;
            }
            else
            {
                if (txtCraft.Text == Resources.H焊接)
                {
                    txtPriceNo.Text = Resources.HJ_H1;
                }
                else if (txtCraft.Text == Resources.Z装配 || txtCraft.Text == Resources.Z入仓)
                {
                    txtPriceNo.Text = Resources.ZP_Z1;
                }
                else if (txtTeam.Text.Contains("机加") && !txtTeam.Text.Contains("机加C"))
                {
                    txtPriceNo.Text = Resources.JJ_C15;
                }
                if (txtCraft.Text != Resources.W折弯)
                {
                    txtProcessNum.Text = @"0";
                }
                txtCompleteNum.Text = txtOrderQt.Text;
            }
            txtCompleteNum.Focus();
        }

        /// <summary>
        ///     完成数量keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCompleteNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || txtCompleteNum.Text == "") return;
            txtProcessNum.Focus();
            //判断完成数量:本工序完成数量总和不能超上工序
            var j = int.Parse(StringHelper.CutString(txtSn.Text.Trim(), 0, 1));
            var sql =
                $"SELECT sum(wanshengsuliang) qt FROM tf_sgdantwo WHERE shigongdanhao='{txtWONo.Text.Trim()}' AND xuhao like '{j}%'";
            var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
            if (dr.HasRows)
            {
                dr.Read();
                if (!String.IsNullOrEmpty(txtOrderQt.Text.Trim()))
                {
                    var orderQt = int.Parse(txtOrderQt.Text.Trim());
                    int finishQt;
                    if (!String.IsNullOrEmpty(dr["qt"].ToString().Trim()))
                    {
                        finishQt = int.Parse(dr["qt"].ToString().Trim()) + int.Parse(txtCompleteNum.Text.Trim());
                    }
                    else
                    {
                        finishQt = int.Parse(txtCompleteNum.Text.Trim());
                    }
                    if (orderQt < finishQt)
                    {
                        MessageBox.Show(@"总数大于前工序数量,请修改!相差数量为:" + (finishQt - orderQt));
                    }
                }
                dr.Close();
            }
        }

        /// <summary>
        ///     加工数keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtProcessNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtProcessNum.Text != "")
            {
                txtPriceNo.Focus();
            }
        }

        /// <summary>
        ///     序价代码keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPriceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsChecked(_wono)) return;
            if (e.KeyCode == Keys.Enter && txtPriceNo.Text != "")
            {
                if (btnSave.Enabled)
                    btnSave_Click(sender, e);
                else
                    btnUpd_Click(sender, e);
            }
        }

        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtWONo.Text.Trim() == "")
            {
                MessageBox.Show(@"工单号不能为空！", Resources.T提示);
                txtWONo.Focus();
            }
            else if (txtSn.Text.Trim() == "")
            {
                MessageBox.Show(@"工序号不能为空！", Resources.T提示);
                txtSn.Focus();
            }
            else if (txtCraft.Text.Trim() == "")
            {
                MessageBox.Show(@"工序名称不能为空！", Resources.T提示);
                txtCraft.Focus();
            }
            else if (txtTeam.Text.Trim() == "")
            {
                MessageBox.Show(@"生产班组不能为空！", Resources.T提示);
                txtTeam.Focus();
            }
            else if (txtCompleteNum.Text.Trim() == "")
            {
                MessageBox.Show(@"完成数量不能为空！", Resources.T提示);
                txtCompleteNum.Focus();
            }
            else if (txtProcessNum.Text.Trim() == "")
            {
                MessageBox.Show(@"加工数不能为空！", Resources.T提示);
                txtProcessNum.Focus();
            }
            else if (txtPriceNo.Text.Trim() == "")
            {
                MessageBox.Show(@"序价代码不能为空！", Resources.T提示);
                txtPriceNo.Focus();
            }
            else
            {
                string[] arrCraftStr = { txtCraft.Text.Trim() };
                var mergeTeamStr = MergeTeamStr(arrCraftStr, txtTeam.Text.Trim());
                var sql = $"SELECT TOP 1 * FROM xujia WHERE xujhao='{txtPriceNo.Text.Trim()}'";
                var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text,
                    sql);
                if (!dr.HasRows)
                {
                    MessageBox.Show($"序价代码{txtPriceNo.Text.Trim()}不存在，请重新输入!!",
                        Resources.T提示);
                    txtPriceNo.Text = "";
                    txtPriceNo.Focus();
                }
                else
                {
                    dr.Read();
                    txtPrice.Text = dr["xujia"].ToString().Trim();
                    txtSubsidy.Text = dr["buzu"].ToString().Trim();
                    var formula = dr["gongsi"].ToString().Trim();
                    dr.Close();
                    sql =
                        $"SELECT count(*) icount FROM tf_sgdantwo WHERE gongxumingcheng='{txtCraft.Text.Trim()}' AND shigongdanhao='{txtWONo.Text.Trim()}' " +
                        $"AND xujah='{txtPriceNo.Text.Trim()} 'AND buzu>0 ";
                    dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        if ((int)dr["icount"] > 0)
                        {
                            txtSubsidy.Text = @"0";
                        }
                        dr.Close();
                    }
                    if (txtPriceNo.Text.Contains(Resources.JJ_C15) || txtPriceNo.Text.Contains(Resources.HJ_H1) ||
                        txtPriceNo.Text.Contains(Resources.ZP_Z1))
                    {
                        sql =
                            $"SELECT * FROM dzdj.dbo.bom WHERE customer='{txtCust.Text.Trim()}' AND  partno='{txtPartNo.Text.Trim()}'";
                        dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
                        if (dr.HasRows)
                        {
                            dr.Read();
                            txtPrice.Text = txtPriceNo.Text.Contains(Resources.JJ_C15)
                                ? dr["machuprice"].ToString().Trim()
                                : @"0";
                            dr.Close();
                        }
                        else
                        {
                            txtPrice.Text = @"0";
                        }
                    }
                    var uprice = decimal.Parse(txtPrice.Text.Trim()); //序价
                    var subsidy = decimal.Parse(txtSubsidy.Text.Trim()); //补助
                    var qt = decimal.Parse(txtCompleteNum.Text.Trim()); //完成数
                    _processNum = txtProcessNum.Text.Trim() == "0" || txtProcessNum.Text.Trim() == ""
                        ? 1M
                        : decimal.Parse(txtProcessNum.Text.Trim());
                    decimal total;
                    switch (formula)
                    {
                        case "1":
                            total = qt * uprice * _processNum + subsidy;
                            break;
                        case "2":
                            total = qt * uprice + subsidy;
                            break;
                        default:
                            total = uprice * _processNum + subsidy;
                            break;
                    }

                    sql =
                        $"SELECT TOP 1 * FROM tf_sgdantwo WHERE shigongdanhao='{txtWONo.Text.Trim()} ' AND xuhao=' {txtSn.Text.Trim()}'";
                    dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        if (!String.IsNullOrEmpty(dr["wanshengsuliang"].ToString().Trim()))
                        {
                            MessageBox.Show($"工序号<{txtSn.Text.Trim()}>已录入过，请修改!!!", Resources.T提示);
                            return;
                        }
                        dr.Close();
                        sql =
                            $"UPDATE tf_sgdantwo set gongxumingcheng='{txtCraft.Text.Trim()}',gongsi='{formula}',chanpintuhao='{txtPartNo.Text.Trim()}'," +
                            $"jhqi='{txtDeliveryDate.Text.Trim()}',shengchanyuan='{mergeTeamStr}',buzu='{txtSubsidy.Text.Trim()}',luruyan='{SqlHelper.UserName}',wanshengsuliang='{txtCompleteNum.Text.Trim()}',kehu='{txtCust.Text.Trim()}'," +
                            $"jgsu='{txtProcessNum.Text.Trim()}',ddhao='{txtOrderNo.Text.Trim()}',hezhi='{total}',lururiqi='{DateTime.Now}',xujah='{txtPriceNo.Text.Trim().ToUpper()}',xujia='{txtPrice.Text.Trim()}' WHERE " +
                            $"shigongdanhao ='{txtWONo.Text.Trim()}' AND xuhao='{txtSn.Text.Trim()}'";
                    }
                    else
                    {
                        sql =
                            "INSERT INTO tf_sgdantwo(shigongdanhao,xuhao,gongxumingcheng,chanpintuhao,shengchanyuan," +
                            $"wanshengsuliang,jgsu,xujah,xujia,buzu,hezhi,lururiqi,luruyan,kehu,ddhao,jhqi,gongsi,jihuariqi) VALUES('{txtWONo.Text.Trim().ToUpper()}','{txtSn.Text.Trim()}'," +
                            $"'{txtCraft.Text.Trim()}','{txtPartNo.Text.Trim()}','{mergeTeamStr}','{txtCompleteNum.Text.Trim()}','{txtProcessNum.Text.Trim()}','{txtPriceNo.Text.Trim().ToUpper()}','{txtPrice.Text.Trim()}','{txtSubsidy.Text.Trim()}','{total}','{DateTime.Now}','{SqlHelper.UserName}','{txtCust.Text.Trim()}','{txtOrderNo.Text.Trim()}','{txtDeliveryDate.Text.Trim()}','{formula}','{_planDate}')";
                    }
                    var conn = SqlHelper.GetConnection();
                    conn.Open();
                    var tran = conn.BeginTransaction();
                    try
                    {
                        SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql);
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(@"事务已回滚,尚未提交!" + ex);
                        tran.Rollback();
                    }
                    InitData();
                    BindData();
                    txtSn.Focus();
                    btnUpd.Enabled = true;
                    btnDel.Enabled = true;
                }
            }
        }

        /// <summary>
        ///     清空控件内容
        /// </summary>
        private void InitData()
        {
            txtSn.Text = "";
            txtCraft.Text = "";
            txtTeam.Text = "";
            txtCompleteNum.Text = "";
            txtProcessNum.Text = "";
            txtPriceNo.Text = "";
            txtPrice.Text = "";
            txtSubsidy.Text = "";
        }

        /// <summary>
        ///     合并班组和字母字符串
        /// </summary>
        /// <param name="arrTeamStr">班组字符串数组</param>
        /// <param name="txtTeamStr">文本框内容</param>
        /// <returns>返回合并后字符串</returns>
        protected string MergeTeamStr(string[] arrTeamStr, string txtTeamStr)
        {
            string tempStr;
            if (arrTeamStr.Contains(_teamSc))
            {
                tempStr = (_teamSc + txtTeamStr).ToUpper();
            }
            else if (arrTeamStr.Contains(_teamJg))
            {
                tempStr = (_teamJg + txtTeamStr).ToUpper();
            }
            else if (arrTeamStr.Contains(_teamZw))
            {
                tempStr = (_teamZw + txtTeamStr).ToUpper();
            }
            else if (arrTeamStr.Contains(_teamHj))
            {
                tempStr = (_teamHj + txtTeamStr).ToUpper();
            }
            else if (arrTeamStr.Contains(_teamZp))
            {
                tempStr = (_teamZp + txtTeamStr).ToUpper();
            }
            else if (arrTeamStr.Contains(_teamRc))
            {
                tempStr = (_teamZp + txtTeamStr).ToUpper();
            }
            else
            {
                tempStr = txtTeamStr.ToUpper();
            }
            return tempStr;
        }

        /// <summary>
        ///     插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIns_Click(object sender, EventArgs e)
        {
            if (txtWONo.Text == "")
            {
                MessageBox.Show(@"工单号不能为空！", Resources.T提示);
                txtWONo.Focus();
            }
            else if (txtSn.Text == "")
            {
                MessageBox.Show(@"序号不能为空！", Resources.T提示);
                txtSn.Focus();
            }
            else
            {
                var sql =
                    $"SELECT count(*) FROM tf_sgdantwo WHERE shigongdanhao='{txtWONo.Text.Trim()}' AND xuhao between '{txtSn.Text.Trim()}' AND '{(decimal.Parse(txtSn.Text.Trim()) + 1)}'";
                var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
                if (dr.HasRows)
                {
                    dr.Read();
                    if (int.Parse(dr[0].ToString().Trim()) > 2)
                    {
                        dr.Close();
                        sql =
                            $"SELECT TOP 1 * FROM tf_sgdantwo WHERE shigongdanhao='{txtWONo.Text.Trim()}' AND xuhao like '{txtSn.Text.Trim()}.%' " +
                            "order by xuhao desc";
                        dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
                        dr.Read();
                        txtSn.Text = dr["xuhao"].ToString().Trim();
                    }
                    var sn = decimal.Parse(txtSn.Text);
                    sn += 0.01M;
                    txtSn.Text = sn.ToString(CultureInfo.InvariantCulture);
                    txtCraft.Focus();
                    btnUpd.Enabled = false;
                    btnDel.Enabled = false;
                }
            }
        }

        /// <summary>
        ///     修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpd_Click(object sender, EventArgs e)
        {
            if (btnSave.Enabled != true)
            {
                if (txtCompleteNum.Text == "")
                {
                    MessageBox.Show(@"完成数量不能为空！！！", Resources.T提示);
                }
                else if (txtWONo.Text == "")
                {
                    MessageBox.Show(@"工单号不能为空！", Resources.T提示);
                    txtWONo.Focus();
                }
                else if (txtSn.Text == "")
                {
                    MessageBox.Show(@"工序号不能为空！", Resources.T提示);
                    txtSn.Focus();
                }
                else if (txtCraft.Text == "")
                {
                    MessageBox.Show(@"工序名称不能为空！", Resources.T提示);
                    txtSn.Focus();
                }
                else if (txtTeam.Text == "")
                {
                    MessageBox.Show(@"生产班组不能为空！", Resources.T提示);
                    txtSn.Focus();
                }
                else if (txtCompleteNum.Text == "")
                {
                    MessageBox.Show(@"完成数量不能为空！", Resources.T提示);
                    txtSn.Focus();
                }
                else if (txtProcessNum.Text == "")
                {
                    MessageBox.Show(@"加工数不能为空！", Resources.T提示);
                    txtSn.Focus();
                }
                else if (txtPriceNo.Text == "")
                {
                    MessageBox.Show(@"序价代码不能为空", Resources.T提示);
                    txtSn.Focus();
                }
                else
                {
                    string[] arrCraftStr = { txtCraft.Text.Trim() };
                    var mergeTeamStr = MergeTeamStr(arrCraftStr, txtTeam.Text.Trim());
                    var sql = $"SELECT TOP 1 * FROM xujia WHERE xujhao='{txtPriceNo.Text.Trim()}'";
                    var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text,
                        sql);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        txtPrice.Text = dr["xujia"].ToString().Trim();
                        var formula = dr["gongsi"].ToString().Trim();
                        var subsidy = dr["buzu"].ToString().Trim();
                        dr.Close();
                        sql =
                            $"SELECT TOP 1 * FROM tf_sgdantwo WHERE shigongdanhao='{txtWONo.Text.Trim()} ' AND xuhao=' {txtSn.Text.Trim()}'";
                        dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text,
                            sql);
                        if (dr.HasRows)
                        {
                            dr.Read();
                            txtSubsidy.Text = StringHelper.IsInt(txtSn.Text)
                                ? subsidy
                                : dr["buzu"].ToString().Trim();
                            if (txtSubsidy.Text.Trim() == "0")
                            {
                                txtSubsidy.Text = @"0";
                            }
                            else if (txtSubsidy.Text.Trim() != "" ||
                                     txtSubsidy.Text.Trim() != subsidy)
                            {
                                txtSubsidy.Text = subsidy;
                            }
                            dr.Close();
                        }
                        if (txtPriceNo.Text.Contains(Resources.JJ_C15) ||
                            txtPriceNo.Text.Contains(Resources.HJ_H1) ||
                            txtPriceNo.Text.Contains(Resources.ZP_Z1))
                        {
                            sql =
                                $"SELECT * FROM dzdj.dbo.bom WHERE customer='{txtCust.Text.Trim()}' AND  partno='{txtPartNo.Text.Trim()}'";
                            dr =
                                SqlHelper.ExecuteReader(
                                    SqlHelper.GetConnSting(),
                                    CommandType.Text, sql);
                            if (dr.HasRows)
                            {
                                dr.Read();
                                txtPrice.Text = txtPriceNo.Text.Contains(Resources.JJ_C15)
                                    ? dr["machuprice"].ToString().Trim()
                                    : @"0";
                                dr.Close();
                            }
                            else
                            {
                                txtPrice.Text = @"0";
                            }
                        }
                        var uprice = decimal.Parse(txtPrice.Text.Trim());
                        var buzhu = decimal.Parse(txtSubsidy.Text.Trim());
                        var qt = decimal.Parse(txtCompleteNum.Text.Trim());
                        _processNum = txtProcessNum.Text.Trim() == "0" ||
                                      txtProcessNum.Text.Trim() == ""
                            ? 1M
                            : decimal.Parse(txtProcessNum.Text.Trim());
                        decimal total;
                        switch (formula)
                        {
                            case "1":
                                total = qt * uprice * _processNum + buzhu;
                                break;
                            case "2":
                                total = qt * uprice + buzhu;
                                break;
                            default:
                                total = uprice * _processNum + buzhu;
                                break;
                        }
                        sql =
                            $"UPDATE tf_sgdantwo set shengchanyuan='{mergeTeamStr}',jhqi='{txtDeliveryDate.Text.Trim()}',wanshengsuliang='{txtCompleteNum.Text.Trim()}',qgren='{SqlHelper.UserName}'," +
                            $"gongsi='{formula}',jgsu='{txtProcessNum.Text.Trim()}',hezhi='{total}',qiugairiqi='{DateTime.Now}',xujah='{txtPriceNo.Text.Trim().ToUpper()}',xujia='{txtPrice.Text.Trim()}',buzu='{txtSubsidy.Text.Trim()}' " +
                            $"WHERE shigongdanhao ='{txtWONo.Text.Trim()}' AND xuhao='{txtSn.Text.Trim()}'";
                        var conn = SqlHelper.GetConnection();
                        conn.Open();
                        var tran = conn.BeginTransaction();
                        try
                        {
                            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(@"事务已回滚,尚未提交!" + ex);
                            tran.Rollback();
                        }
                        conn.Close();
                        txtSn.Focus();
                        btnUpd.Text = Resources.X修改;
                        EnableCtrls(true);
                    }
                    else
                    {
                        MessageBox.Show(
                            $"序价代码{txtPriceNo.Text.Trim()}不存在，请重新输入!!",
                            Resources.T提示);
                        txtSn.Focus();
                    }
                }
                txtSn.ReadOnly = false;
                txtCraft.ReadOnly = false;
                btnSave.Enabled = true;
                InitData();
                BindData();
                txtSn.Focus();
            }
            else if (btnSave.Enabled)
            {
                txtSn.ReadOnly = true;
                txtCraft.ReadOnly = true;
                EnableCtrls(false);
                btnUpd.Enabled = true;
                btnUpd.Visible = true;
                btnUpd.Text = Resources.X保存;
                txtTeam.Focus();
            }
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtSn.Text == "" || txtCraft.Text == "")
            {
                MessageBox.Show(@"提示：请输入要删除的序号并按回车键,或直接双击表中要删除的行！", Resources.J警告, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                if (
                    MessageBox.Show($"是否要删除< {txtSn.Text.Trim()} >工序号", Resources.J警告,
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    var sql =
                        $"delete FROM tf_sgdantwo WHERE shigongdanhao='{txtWONo.Text.Trim()}' AND xuhao='{txtSn.Text.Trim()}'";
                    var conn = SqlHelper.GetConnection();
                    conn.Open();
                    var tran = conn.BeginTransaction();
                    try
                    {
                        SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql);
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(@"事务已回滚,尚未提交!" + ex);
                        tran.Rollback();
                    }
                    conn.Close();
                    InitData();
                    txtSn.Focus();
                }
                BindData();
            }
        }

        /// <summary>
        ///     下一单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            EnableCtrls(true);
            lblCheck.Visible = false;
            btnUpd.Text = Resources.X修改;
            InitData();
            txtWONo.Text = "";
            txtCust.Text = "";
            txtOrderNo.Text = "";
            txtPartNo.Text = "";
            txtPartName.Text = "";
            txtOrderQt.Text = "";
            txtPageNo.Text = "";
            txtDeliveryDate.Text = "";
            dataGridView1.DataSource = "";
            txtWONo.Focus();
        }

        /// <summary>
        ///     单元格CellMouseUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            decimal sumFinishQt = 0;
            for (var i = 0; i < dataGridView1.SelectedCells.Count; i++)
            {
                if (dataGridView1.SelectedCells[i].Value != null)
                {
                    try
                    {
                        sumFinishQt = sumFinishQt + decimal.Parse(dataGridView1.SelectedCells[i].Value.ToString());
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
            lblSumQt.Text = sumFinishQt == 0 ? "" : sumFinishQt.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     单元格双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Debug.Assert(dataGridView1.CurrentRow != null, "dataGridView1.CurrentRow != null");
            txtSn.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            var vKeyEventArgs = new KeyEventArgs(Keys.Enter);
            txtSn_KeyDown(sender, vKeyEventArgs);
        }

        /// <summary>
        ///     核对
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            _wono = txtWONo.Text.Trim();
            var isCheck = IsChecked(_wono) ? true : false;
            var sql =
                $"UPDATE tf_sgdantwo set Checked={(isCheck == true ? 0 : 1)},Checker='{SqlHelper.UserName}',Chkdate='{DateTime.Now}' WHERE shigongdanhao ='{_wono}'";
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnSting(), CommandType.Text, sql);
            isCheck = IsChecked(_wono) ? true : false;
            EnableCtrls(!isCheck);
        }

        /// <summary>
        ///     查询是否已核对
        /// </summary>
        /// <param name="wonoStr">工单号</param>
        /// <returns></returns>
        private bool IsChecked(string wonoStr)
        {
            var sql = $"SELECT checked FROM tf_sgdantwo WHERE shigongdanhao='{wonoStr}'";
            var isChecked = (bool)SqlHelper.ExecuteScalar(SqlHelper.GetConnSting(), CommandType.Text, sql);
            lblCheck.Visible = isChecked;
            return isChecked;
        }

        /// <summary>
        ///     启用控件
        /// </summary>
        private void EnableCtrls(bool isCheck)
        {
            txtWONo.ReadOnly = !isCheck;
            btnSave.Visible = isCheck;
            btnIns.Visible = isCheck;
            btnUpd.Visible = isCheck;
            btnDel.Visible = isCheck;
            btnSave.Enabled = isCheck;
            btnIns.Enabled = isCheck;
            btnUpd.Enabled = isCheck;
            btnDel.Enabled = isCheck;
        }
    }
}