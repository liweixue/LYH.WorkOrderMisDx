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

namespace LYH.WorkOrder
{
    public partial class FrmConstructionInput : Form
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
        private string _wONo;

        public FrmConstructionInput()
        {
            InitializeComponent();
        }

        public DataSet NotSumCraftDataSet { get; set; }

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

            var sql = "SELECT * FROM BaseNotSumCraft";
            NotSumCraftDataSet = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(SqlHelper.GetConndzdjSting()),
                CommandType.Text, sql);
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
            _wONo = txtWONo.Text.Trim();
            if (_wONo == "") return;
            var sql = "SELECT xuhao '工序号',gongxumingcheng '工序名称',jihuariqi '计划完成日期'," +
                      "shengchanyuan '生产班组',wanshengsuliang '完成数量',jgsu '加工数',RTRIM(xujah) '序价号'," +
                      "xujia '序价',buzu '补助',hezhi '件资合计',lururiqi '录入日期',RTRIM(luruyan) '录入人'," +
                      $"qiugairiqi '修改日期',RTRIM(qgren) '修改人' FROM tf_sgdantwo WHERE shigongdanhao='{_wONo}'";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnSting(), CommandType.Text, sql);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            if (IsChecked(_wONo)) lblCheck.Visible = true;
        }

        /// <summary>
        ///     工单号内容更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWONo_TextChanged(object sender, EventArgs e)
        {
            if (txtWONo.Text.Length != 7) return;
            _wONo = txtWONo.Text.Trim();
            var sql = $"SELECT TOP 1 * FROM mf_sgdan WHERE shigongdanhao='{_wONo}'";
            var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
            if (dr.HasRows)
            {
                dr.Read();
                var disable = dr["cls_id"].ToString().Trim();
                if (!string.IsNullOrEmpty(dr["suhu"].ToString().Trim()) || disable == "T")
                {
                    MessageBox.Show($"此工单号{_wONo}已审核或停用锁定，请重新输入或解锁!!", Resources.T提示);
                    txtWONo.Text = "";
                }
                else
                {
                    txtCust.Text = dr["kehu"].ToString().Trim();
                    txtOrderNo.Text = dr["dingdanhao"].ToString().Trim();
                    txtPartNo.Text = dr["chanpintuhao"].ToString().Trim();
                    txtPartName.Text = dr["chanpinmingcheng"].ToString().Trim();
                    txtOrderQty.Text = dr["dingdansuliang"].ToString().Trim();
                    txtDeliveryDate.Text = dr["jiaohuoqi"].ToString().Trim();
                    txtPageNo.Text = dr["tuzhiyema"].ToString().Trim();
                    EnableCtrls(!IsChecked(_wONo));
                    txtWONo.ReadOnly = true;
                    var minSeq = GetCurrentSeq();
                    if (minSeq == 0)
                        minSeq = 1;
                    txtSn.Text = minSeq.ToString();
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
            if (StringHelper.IsNumeric(txtSn.Text) || string.IsNullOrEmpty(txtSn.Text)) return;
            MessageBox.Show(Resources.IsNotNumber, Resources.T提示);
            txtSn.Text = "";
        }

        ///// <summary>
        /////     校验完成数量是否是正整数
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void txtCompleteNum_TextChanged(object sender, EventArgs e)
        //{
        //    if (StringHelper.IsInt(txtCompleteNum.Text) || string.IsNullOrEmpty(txtCompleteNum.Text)) return;
        //    MessageBox.Show(@"只允许输入正整数，请重新输入！", Resources.T提示);
        //    txtCompleteNum.Text = "";
        //}

        /// <summary>
        ///     校验加工数是否是数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtProcessNum_TextChanged(object sender, EventArgs e)
        {
            if (StringHelper.IsNumeric(txtProcessNum.Text) || string.IsNullOrEmpty(txtProcessNum.Text)) return;
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
            if (e.KeyCode != Keys.Enter || txtSn.Text == "") return;
            txtTeam.Focus();
        }

        /// <summary>
        ///     判断是否存在未录入的前工序
        /// </summary>
        /// <param name="snInt"></param>
        /// <param name="minSeq"></param>
        /// <returns></returns>
        private bool ExistsUnfinishedCraft(int snInt, out int minSeq)
        {
            var sql =
                $"SELECT MIN(CONVERT(INT,xuhao)) FROM tf_sgdantwo WHERE shigongdanhao='{txtWONo.Text.Trim()}' AND lururiqi IS NULL";
            var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
            if (dr.HasRows)
            {
                dr.Read();
                minSeq = dr[0].ToString().ToInt32();
                dr.Close();
                if (minSeq < snInt)
                    return true;
            }

            minSeq = 1;
            return false;
        }

        private int GetMinSeqOfNotInput()
        {
            var sql =
                $"SELECT ISNULL(MIN(CONVERT(INT,xuhao)),0) FROM tf_sgdantwo WHERE shigongdanhao='{txtWONo.Text.Trim()}' AND lururiqi IS NULL";
            var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
            if (dr.HasRows)
            {
                dr.Read();
                var minSeq = dr[0].ToString().ToInt32();
                dr.Close();
                return minSeq;
            }

            return 0;
        }

        /// <summary>
        ///     工序名称keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCraft_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTeam.Focus();
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
                    txtPriceNo.Text = Resources.HJ_H1;
                else if (txtCraft.Text == Resources.Z装配 || txtCraft.Text == Resources.Z入仓)
                    txtPriceNo.Text = Resources.ZP_Z1;
                else if (txtTeam.Text.Contains("机加") && !txtTeam.Text.Contains("机加C"))
                    txtPriceNo.Text = Resources.JJ_C15;
                if (txtCraft.Text != Resources.W折弯)
                    txtProcessNum.Text = @"0";
                txtCompleteNum.Text = txtOrderQty.Text;
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
        }

        /// <summary>
        ///     加工数keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtProcessNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtProcessNum.Text != "")
                txtPriceNo.Focus();
        }

        /// <summary>
        ///     序价代码keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPriceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsChecked(_wONo)) return;
            if (e.KeyCode == Keys.Enter && txtPriceNo.Text != "")
                if (btnSave.Enabled)
                    btnSave_Click(sender, e);
                else
                    btnUpd_Click(sender, e);
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
                string[] arrCraftStr = {txtCraft.Text.Trim()};
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
                        $"SELECT count(*) icount FROM tf_sgdantwo WHERE gongxumingcheng='{txtCraft.Text.Trim()}' " +
                        $"AND shigongdanhao='{txtWONo.Text.Trim()}' AND xujah='{txtPriceNo.Text.Trim()} 'AND buzu>0 ";
                    dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        if ((int) dr["icount"] > 0)
                            txtSubsidy.Text = @"0";
                        dr.Close();
                    }

                    if (txtPriceNo.Text.Contains(Resources.JJ_C15) || txtPriceNo.Text.Contains(Resources.HJ_H1) ||
                        txtPriceNo.Text.Contains(Resources.ZP_Z1))
                    {
                        sql =
                            $"SELECT * FROM dzdj.dbo.bom WHERE customer='{txtCust.Text.Trim()}' AND partno='{txtPartNo.Text.Trim()}'";
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
                        $"SELECT TOP 1 * FROM tf_sgdantwo WHERE shigongdanhao='{txtWONo.Text.Trim()}' AND xuhao=' {txtSn.Text.Trim()}'";
                    dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        if (!string.IsNullOrEmpty(dr["wanshengsuliang"].ToString().Trim()))
                        {
                            MessageBox.Show($"工序号<{txtSn.Text.Trim()}>已录入过，请修改!!!", Resources.T提示);
                            return;
                        }

                        dr.Close();
                        sql =
                            $"UPDATE tf_sgdantwo set gongxumingcheng='{txtCraft.Text.Trim()}',gongsi='{formula}'," +
                            $"chanpintuhao='{txtPartNo.Text.Trim()}',jhqi='{txtDeliveryDate.Text.Trim()}'," +
                            $"shengchanyuan='{mergeTeamStr}',buzu='{txtSubsidy.Text.Trim()}',luruyan='{SqlHelper.UserName}'," +
                            $"wanshengsuliang='{txtCompleteNum.Text.Trim()}',kehu='{txtCust.Text.Trim()}'," +
                            $"jgsu='{txtProcessNum.Text.Trim()}',ddhao='{txtOrderNo.Text.Trim()}',hezhi='{total}'," +
                            $"lururiqi='{DateTime.Now}',xujah='{txtPriceNo.Text.Trim().ToUpper()}',xujia='{txtPrice.Text.Trim()}' " +
                            $"WHERE shigongdanhao ='{txtWONo.Text.Trim()}' AND xuhao='{txtSn.Text.Trim()}'";
                    }
                    else
                    {
                        sql =
                            "INSERT INTO tf_sgdantwo(shigongdanhao,xuhao,gongxumingcheng,chanpintuhao,shengchanyuan," +
                            "wanshengsuliang,jgsu,xujah,xujia,buzu,hezhi,lururiqi,luruyan,kehu,ddhao,jhqi,gongsi,jihuariqi) " +
                            $"VALUES('{txtWONo.Text.Trim().ToUpper()}','{txtSn.Text.Trim()}','{txtCraft.Text.Trim()}'," +
                            $"'{txtPartNo.Text.Trim()}','{mergeTeamStr}','{txtCompleteNum.Text.Trim()}'," +
                            $"'{txtProcessNum.Text.Trim()}','{txtPriceNo.Text.Trim().ToUpper()}','{txtPrice.Text.Trim()}'," +
                            $"'{txtSubsidy.Text.Trim()}','{total}','{DateTime.Now}','{SqlHelper.UserName}'," +
                            $"'{txtCust.Text.Trim()}','{txtOrderNo.Text.Trim()}','{txtDeliveryDate.Text.Trim()}'," +
                            $"'{formula}','{_planDate}')";
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
                tempStr = (_teamSc + txtTeamStr).ToUpper();
            else if (arrTeamStr.Contains(_teamJg))
                tempStr = (_teamJg + txtTeamStr).ToUpper();
            else if (arrTeamStr.Contains(_teamZw))
                tempStr = (_teamZw + txtTeamStr).ToUpper();
            else if (arrTeamStr.Contains(_teamHj))
                tempStr = (_teamHj + txtTeamStr).ToUpper();
            else if (arrTeamStr.Contains(_teamZp))
                tempStr = (_teamZp + txtTeamStr).ToUpper();
            else if (arrTeamStr.Contains(_teamRc))
                tempStr = (_teamZp + txtTeamStr).ToUpper();
            else
                tempStr = txtTeamStr.ToUpper();
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
                    $"SELECT COUNT(*) FROM tf_sgdantwo WHERE shigongdanhao='{txtWONo.Text.Trim()}' " +
                    $"AND xuhao BETWEEN '{txtSn.Text.Trim()}' AND '{decimal.Parse(txtSn.Text.Trim()) + 1}'";
                var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
                if (dr.HasRows)
                {
                    dr.Read();
                    if (int.Parse(dr[0].ToString().Trim()) > 2)
                    {
                        dr.Close();
                        sql =
                            $"SELECT TOP 1 * FROM tf_sgdantwo WHERE shigongdanhao='{txtWONo.Text.Trim()}' " +
                            $"AND xuhao LIKE '{txtSn.Text.Trim()}.%' ORDER BY xuhao DESC";
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
                    string[] arrCraftStr = {txtCraft.Text.Trim()};
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
                            $"SELECT TOP 1 * FROM tf_sgdantwo WHERE shigongdanhao='{txtWONo.Text.Trim()}' " +
                            $"AND xuhao=' {txtSn.Text.Trim()}'";
                        dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text,
                            sql);
                        if (dr.HasRows)
                        {
                            dr.Read();
                            txtSubsidy.Text = StringHelper.IsInt(txtSn.Text)
                                ? subsidy
                                : dr["buzu"].ToString().Trim();
                            if (txtSubsidy.Text.Trim() == "0")
                                txtSubsidy.Text = @"0";
                            else if (txtSubsidy.Text.Trim() != "" ||
                                     txtSubsidy.Text.Trim() != subsidy)
                                txtSubsidy.Text = subsidy;
                            dr.Close();
                        }

                        if (txtPriceNo.Text.Contains(Resources.JJ_C15) ||
                            txtPriceNo.Text.Contains(Resources.HJ_H1) ||
                            txtPriceNo.Text.Contains(Resources.ZP_Z1))
                        {
                            sql =
                                $"SELECT * FROM dzdj.dbo.bom WHERE customer='{txtCust.Text.Trim()}' " +
                                $"AND partno='{txtPartNo.Text.Trim()}'";
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
                            $"UPDATE tf_sgdantwo SET shengchanyuan='{mergeTeamStr}',jhqi='{txtDeliveryDate.Text.Trim()}'," +
                            $"wanshengsuliang='{txtCompleteNum.Text.Trim()}',qgren='{SqlHelper.UserName}',gongsi='{formula}'," +
                            $"jgsu='{txtProcessNum.Text.Trim()}',hezhi='{total}',qiugairiqi='{DateTime.Now}'," +
                            $"xujah='{txtPriceNo.Text.Trim().ToUpper()}',xujia='{txtPrice.Text.Trim()}'," +
                            $"buzu='{txtSubsidy.Text.Trim()}' WHERE shigongdanhao ='{txtWONo.Text.Trim()}' " +
                            $"AND xuhao='{txtSn.Text.Trim()}'";
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
            txtSn.ReadOnly = false;
            txtCraft.ReadOnly = false;
            lblCheck.Visible = false;
            btnUpd.Text = Resources.X修改;
            InitData();
            txtWONo.Text = "";
            txtCust.Text = "";
            txtOrderNo.Text = "";
            txtPartNo.Text = "";
            txtPartName.Text = "";
            txtOrderQty.Text = "";
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
                if (dataGridView1.SelectedCells[i].Value != null)
                    try
                    {
                        sumFinishQt = sumFinishQt + decimal.Parse(dataGridView1.SelectedCells[i].Value.ToString());
                    }
                    catch (Exception)
                    {
                        // ignored
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
            txtSn_Leave(null, null);
            //var vKeyEventArgs = new KeyEventArgs(Keys.Enter);
            //txtSn_KeyDown(sender, vKeyEventArgs);
        }

        /// <summary>
        ///     核对
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            _wONo = txtWONo.Text.Trim();
            var isCheck = IsChecked(_wONo);
            var sql =
                $"UPDATE tf_sgdantwo set Checked={(isCheck ? 0 : 1)},Checker='{SqlHelper.UserName}'," +
                $"Chkdate='{DateTime.Now}' WHERE shigongdanhao ='{_wONo}'";
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnSting(), CommandType.Text, sql);
            isCheck = IsChecked(_wONo);
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
            var isChecked = (bool) SqlHelper.ExecuteScalar(SqlHelper.GetConnSting(), CommandType.Text, sql);
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

        private void txtCompleteNum_Leave(object sender, EventArgs e)
        {
            var currentSeq = txtSn.Text.Trim().ToDecimal();
            var currentInputQty = txtCompleteNum.Text.Trim().ToDecimal();
            var orderQty = txtOrderQty.Text.Trim().ToDecimal();
            var successed = GetPreviousAndCurrentQuantity(!btnSave.Enabled, currentSeq, out var previousCompletedQty,
                out var completedQty);

            if (successed)
            {
                completedQty = completedQty + currentInputQty;
                if ((int) currentSeq == 1)
                {
                    if (completedQty > orderQty)
                    {
                        MessageBox.Show(@"总数大于下单数量,请修改!超出数量为:" + (completedQty - orderQty));
                        txtCompleteNum.Text = (currentInputQty - (completedQty - orderQty)).ToString(CultureInfo.CurrentCulture);
                        txtCompleteNum.Focus();
                    }
                }
                else
                {
                    if (completedQty > previousCompletedQty)
                    {
                        MessageBox.Show(@"总数大于前工序数量,请修改!超出数量为:" + (completedQty - previousCompletedQty));
                        txtCompleteNum.Text =
                            (currentInputQty - (completedQty - previousCompletedQty)).ToString(CultureInfo.CurrentCulture);
                        txtCompleteNum.Focus();
                    }
                }
            }
            else
            {
                txtCompleteNum.Text = "";
                txtCompleteNum.Focus();
            }
        }

        private bool GetPreviousAndCurrentQuantity(bool isUpdate, decimal craftSeq, out decimal previousCompletedQty,
            out decimal completedQty)
        {
            //判断完成数量:本工序完成数量总和不能超上工序,且不能超出下单数量
            try
            {
                var sql =
                    $"SELECT ISNULL(SUM(wanshengsuliang),0) currentQty,xuhao FROM dzscb.dbo.tf_sgdantwo WHERE shigongdanhao='{txtWONo.Text.Trim()}' GROUP BY xuhao";
                var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);

                var table = ds.Tables[0];
                var condition = isUpdate ? $" AND xuhao<>{craftSeq}" : "";
                condition = $"CONVERT(xuhao,'System.Int32')={(int) craftSeq}{condition}";
                completedQty = table.Compute("sum(currentQty)", condition).ToString().ToDecimal();

                //if (NotSumCraftDataSet.Tables[0].Rows.Cast<DataRow>()
                //    .Any(row => row[0].ToString() == txtCraft.Text.Trim()))
                //{
                //    completedQty = 0;
                //    var rowIndex = table.Rows.IndexOf(table.Select($"xuhao={(int)craftSeq}").First());
                //    craftSeq = rowIndex>0 ? table.Rows[rowIndex - 1]["xuhao"].ToString().ToInt32() : 0;
                //}
                if ((int) craftSeq > 1)
                {
                    var rowIndex = table.Rows.IndexOf(table.Select($"xuhao={(int) craftSeq}").First());
                    var previousSeq = table.Rows[rowIndex - 1]["xuhao"].ToString().ToDecimal();
                    previousCompletedQty = table.Compute("sum(currentQty)",
                            $"CONVERT(xuhao,'System.Int32')={(int) previousSeq}").ToString()
                        .ToDecimal();
                }
                else
                {
                    previousCompletedQty = 0;
                }

                return true;
            }
            catch (Exception)
            {
                previousCompletedQty = 0;
                completedQty = 0;
                return false;
            }
        }

        private void txtSn_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSn.Text.Trim()))
                return;
            var seqInput = txtSn.Text.Trim().ToInt32();

            var minSeq = GetCurrentSeq();
            if (seqInput > minSeq && minSeq > 0)
            {
                MessageBox.Show("前工序未录入,请完成前工序的录入");
                txtSn.Text = minSeq.ToString();
                txtSn.Focus();
                return;
            }

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
                _planDate = dr["jihuariqi"].ToString().Trim();
            }

            //else
            //{
            //    MessageBox.Show($"此工序号<{txtSn.Text.Trim()}>不存在，下面内容请直接输入!!", Resources.T提示);
            //}
            dr.Close();
        }

        private int GetCurrentSeq()
        {
            var sql =
                $"SELECT CONVERT(INT,a.xuhao) sn,ISNULL(SUM(wanshengsuliang),0) qty,b.dingdansuliang FROM tf_sgdantwo a JOIN mf_sgdan b ON b.shigongdanhao=a.shigongdanhao WHERE a.shigongdanhao='{txtWONo.Text.Trim()}' GROUP BY CONVERT(INT,a.xuhao),b.dingdansuliang";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            var table = ds.Tables[0];
            for (var i = table.Rows.Count - 1; i >= 0; i--)
            {
                var currentQty = table.Rows[i]["qty"].ToString().ToDecimal();
                var previousQty = i > 0 ? table.Rows[i - 1]["qty"].ToString().ToDecimal() : 0;
                var orderQty = table.Rows[i]["dingdansuliang"].ToString().ToDecimal();
                var minSeq = table.Rows[i]["sn"].ToString().ToInt32();

                if (i > 0)
                {
                    if (i == table.Rows.Count - 1 && currentQty >= previousQty && currentQty > 0)
                        return minSeq;
                    if (currentQty > 0 && currentQty < previousQty || currentQty == 0 && previousQty > 0)
                        return minSeq;
                }
                else
                {
                    if (currentQty < orderQty)
                        return minSeq;
                }
            }

            return 0;
        }
    }
}