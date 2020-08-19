using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using LYH.WorkOrder.Properties;
using LYH.WorkOrder.share;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmContructionHandwriting : Form
    {
        private readonly string _teamHj = Resources.H焊接;
        private readonly string _teamJg = Resources.G激光;
        private readonly string _teamRc = Resources.Z入仓;
        private readonly string _teamSc = Resources.S数冲;
        private readonly string _teamZp = Resources.Z装配;
        private readonly string _teamZw = Resources.W折弯;

        /// <summary>
        ///     加工数
        /// </summary>
        private decimal _processNum;

        /// <summary>
        ///     工单号
        /// </summary>
        private string _wono;

        public FrmContructionHandwriting()
        {
            InitializeComponent();
            KeyDown += FrmWin_KeyDown;
            HasFunction();
            btnSave.Enabled = false;
            dtpDelivery.Value = DateTime.Now;
        }

        /// <summary>
        ///     权限检查
        /// </summary>
        private void HasFunction()
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

        private void InitData()
        {
            txtWONo.Text = "";
            txtOriWONo.Text = "";
            txtPartNo.Text = @"如单";
            txtPartName.Text = @"如单";
            txtOrderQt.Text = "";
            txtSn.Text = "";
            txtCraft.Text = "";
            txtTeam.Text = "";
            txtCompleteNum.Text = "";
            txtProcessNum.Text = "";
            txtPriceNo.Text = "";
            txtOrderNo.Text = "";
            txtPrice.Text = "";
            txtSubsidy.Text = "";
            cbxCust.Text = @"德展自用";
        }

        /// <summary>
        ///     DataGridView的绑定数据
        /// </summary>
        private void BindData()
        {
            _wono = txtWONo.Text.Trim();
            if (_wono == "") return;
            var sql = "SELECT xuhao '工序号',gongxumingcheng '工序名称',jihuariqi '计划完成日期'," +
                      "shengchanyuan '生产班组',wanshengsuliang '完成数量',jgsu '加工数',xujah '序价号',xujia '序价'," +
                      "buzu '补助',hezhi '件资合计',lururiqi '录入日期',luruyan '录入人',qiugairiqi '修改日期'," +
                      $"qgren '修改人' FROM tf_sgdantwo WHERE shigongdanhao='{txtWONo.Text.Trim()}'";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            if (IsChecked(_wono)) lblCheck.Visible = true;
        }

        /// <summary>
        ///     新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsert_Click(object sender, EventArgs e)
        {
            var orderId = DateTime.Now.ToString("yyMM").Substring(1, 3);
            var ddaa = orderId + "0001";
            var aadd = orderId + "9999";
            var sql = $"SELECT Max(twoid) FROM mf_sgdan WHERE twoid>={ddaa} AND twoid<={aadd}";
            var scalar = SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text, sql);
            if (scalar.ToString() == "")
            {
                txtWONo.Text = ddaa;
                btnInsert.Enabled = false;
                btnSave.Enabled = true;
                txtOriWONo.Focus();
                txtSn.Text = @"1";
            }
            else
            {
                if (scalar.ToString() == aadd)
                {
                    MessageBox.Show(@"手工工单已经达到9999份上限，编号要重新生成！", Resources.T提示);
                }
                else
                {
                    var maxtid = (int) scalar;
                    maxtid += 1;
                    txtWONo.Text = maxtid.ToString();
                    btnInsert.Enabled = false;
                    btnSave.Enabled = true;
                    txtOriWONo.Focus();
                    txtSn.Text = @"1";
                }
            }
            EnableCtrls(true);
        }

        /// <summary>
        ///     添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtOrderQt.Text == "")
            {
                MessageBox.Show(@"工单数量不能为空！", Resources.T提示);
                txtOrderQt.Focus();
            }
            else if (txtCraft.Text == "")
            {
                MessageBox.Show(@"工序名称不能为空！", Resources.T提示);
                txtCraft.Focus();
            }
            else if (txtTeam.Text == "")
            {
                MessageBox.Show(@"生产班组不能为空！", Resources.T提示);
                txtTeam.Focus();
            }
            else if (txtCompleteNum.Text == "")
            {
                MessageBox.Show(@"完成数量不能为空！", Resources.T提示);
                txtCompleteNum.Focus();
            }
            else if (txtProcessNum.Text == "")
            {
                MessageBox.Show(@"加工数不能为空！", Resources.T提示);
                txtProcessNum.Focus();
            }
            else if (txtPriceNo.Text == "")
            {
                MessageBox.Show(@"序价代码不能为空！", Resources.T提示);
                txtPriceNo.Focus();
            }
            else
            {
                string[] arrCraftStr = {txtCraft.Text.Trim()};
                var mergeTeamStr = MergeTeamStr(arrCraftStr, txtTeam.Text.Trim());
                var sql = $"SELECT TOP 1 * FROM xujia WHERE xujhao='{txtPriceNo.Text.Trim()}'";
                var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (!dr.HasRows)
                {
                    MessageBox.Show($"序价代码{txtPriceNo.Text.Trim()}不存在，请重新输入!!", Resources.T提示);
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
                    if (txtOrderNo.Text.Trim() == "")
                    {
                        txtOrderNo.Text = txtWONo.Text.Trim();
                    }
                    if (txtPriceNo.Text.Contains("C15") || txtPriceNo.Text.Contains("H1") ||
                        txtPriceNo.Text.Contains("Z1"))
                    {
                        sql =
                            $"SELECT * FROM dzdj.dbo.bom WHERE customer='{txtTeam.Text.Trim()}' AND partno='{txtProcessNum.Text.Trim()}'";
                        dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text,
                            sql);
                        if (dr.HasRows)
                        {
                            dr.Read();
                            txtPrice.Text = txtPriceNo.Text.Contains("C15") ? dr["machuprice"].ToString().Trim() : @"0";
                            dr.Close();
                        }
                        else
                        {
                            txtPrice.Text = @"0";
                        }
                    }
                    var uprice = decimal.Parse(txtPrice.Text.Trim());
                    var subsidy = decimal.Parse(txtSubsidy.Text.Trim());
                    var qt = decimal.Parse(txtCompleteNum.Text.Trim());
                    _processNum = txtProcessNum.Text.Trim() == "0" || txtProcessNum.Text.Trim() == ""
                        ? 1M
                        : decimal.Parse(txtProcessNum.Text.Trim());
                    decimal total;
                    switch (formula)
                    {
                        case "1":
                            total = qt*uprice*_processNum + subsidy;
                            break;
                        case "2":
                            total = qt*uprice + subsidy;
                            break;
                        default:
                            total = uprice*_processNum + subsidy;
                            break;
                    }
                    var sn = decimal.Parse(txtSn.Text.Trim());
                    if (txtSn.Text.Trim() == "" || txtSn.Text.Trim() == "1")
                    {
                        sql =
                            String.Format(
                                "insert into mf_sgdan(shigongdanhao,dingdanhao,kehu,jiaohuoqi,tuzhiyema,chanpintuhao,chanpinmingcheng," +
                                "dingdansuliang,beizu,twoid) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{0}')",
                                txtWONo.Text.Trim(), txtOrderNo.Text.Trim(), cbxCust.Text.Trim(),
                                dtpDelivery.Text.Trim(), txtPageNo.Text.Trim(),
                                txtPartNo.Text.Trim(), txtPartName.Text.Trim(), txtOrderQt.Text.Trim(),
                                txtOriWONo.Text.Trim());
                        SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                        sql =
                            "insert into tf_sgdantwo(shigongdanhao,xuhao,gongxumingcheng,chanpintuhao,shengchanyuan," +
                            "wanshengsuliang,jgsu,xujah,xujia,buzu,hezhi,lururiqi,luruyan,kehu,ddhao,jhqi,gongsi,jihuariqi) values" +
                            $"('{txtWONo.Text.Trim()}','{txtSn.Text.Trim()}','{txtCraft.Text.Trim()}','{txtPartNo.Text.Trim()}','{mergeTeamStr}','{txtCompleteNum.Text.Trim()}','{txtProcessNum.Text.Trim()}','{txtPriceNo.Text.Trim().ToUpper()}','{txtPrice.Text.Trim()}','{txtSubsidy.Text.Trim()}','{total}','{DateTime.Now}','{SqlHelper.UserName}','{cbxCust.Text.Trim()}','{txtOrderNo.Text.Trim()}','{dtpDelivery.Text.Trim()}','{formula}',{0})";
                        SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                        EnableCtrls(true);
                        txtCraft.Text = "";
                        txtTeam.Text = "";
                        txtCompleteNum.Text = "";
                        txtProcessNum.Text = "";
                        txtPriceNo.Text = "";
                        txtPrice.Text = "";
                        txtSubsidy.Text = "";
                        sn += 1;
                        txtSn.Text = sn.ToString(CultureInfo.InvariantCulture);
                        BindData();
                        txtCraft.Focus();
                    }
                    else
                    {
                        sql =
                            $"SELECT count(*) icount FROM tf_sgdantwo WHERE gongxumingcheng='{txtCraft.Text.Trim()}' AND shigongdanhao='{txtWONo.Text.Trim()} '" +
                            $" AND xujah='{txtPriceNo.Text.Trim()} 'AND buzu>0 ";
                        dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(),
                            CommandType.Text, sql);
                        if (dr.HasRows)
                        {
                            dr.Read();
                            if ((int) dr["icount"] > 0)
                            {
                                txtSubsidy.Text = @"0";
                            }
                            dr.Close();
                        }
                        subsidy = decimal.Parse(txtSubsidy.Text.Trim());
                        switch (formula)
                        {
                            case "1":
                                total = qt*uprice*_processNum + subsidy;
                                break;
                            case "2":
                                total = qt*uprice + subsidy;
                                break;
                            default:
                                total = uprice*_processNum + subsidy;
                                break;
                        }
                        sql =
                            "insert into tf_sgdantwo(shigongdanhao,xuhao,gongxumingcheng,chanpintuhao,shengchanyuan," +
                            "wanshengsuliang,jgsu,xujah,xujia,buzu,hezhi,lururiqi,luruyan,kehu,ddhao,jhqi,gongsi,jihuariqi) values" +
                            $"('{txtWONo.Text.Trim()}','{txtSn.Text.Trim()}','{txtCraft.Text.Trim()}','{txtPartNo.Text.Trim()}','{mergeTeamStr}','{txtCompleteNum.Text.Trim()}','{txtProcessNum.Text.Trim()}','{txtPriceNo.Text.Trim().ToUpper()}','{txtPrice.Text.Trim()}','{txtSubsidy.Text.Trim()}','{total}','{DateTime.Now}','{SqlHelper.UserName}','{cbxCust.Text.Trim()}','{txtOrderNo.Text.Trim()}','{dtpDelivery.Text.Trim()}','{formula}',{0})";
                        SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text,
                            sql);
                        txtCraft.Text = "";
                        txtTeam.Text = "";
                        txtCompleteNum.Text = "";
                        txtProcessNum.Text = "";
                        txtPriceNo.Text = "";
                        txtPrice.Text = "";
                        txtSubsidy.Text = "";
                        sn += 1;
                        txtSn.Text = sn.ToString(CultureInfo.InvariantCulture);
                        txtCraft.Focus();
                        BindData();
                    }
                }
                dr.Close();
            }
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
        ///     完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (txtOrderQt.Text != "" || txtCraft.Text != "")
            {
                if (MessageBox.Show(@"是否确认要完成清空", Resources.J警告, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) ==
                    DialogResult.OK)
                {
                    btnInsert.Enabled = true;
                    InitData();
                }
            }
        }

        /// <summary>
        ///     客户KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxCust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPartNo.Focus();
            }
        }

        /// <summary>
        ///     交期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpDelivery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCraft.Focus();
            }
        }

        /// <summary>
        ///     零件图号KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPartNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPartName.Focus();
            }
        }

        /// <summary>
        ///     零件名称KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPartName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOrderQt.Focus();
            }
        }

        /// <summary>
        ///     下单数量KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrderQt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpDelivery.Focus();
            }
        }

        /// <summary>
        ///     工序号KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCraft.Focus();
            }
        }

        /// <summary>
        ///     工艺KeyDown
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
        ///     生产班组KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTeam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtTeam.Text != "")
            {
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
                    else if (txtOrderQt.Text.Contains("机加") && !txtOrderQt.Text.Contains("机加C"))
                    {
                        txtPriceNo.Text = Resources.JJ_C15;
                    }
                    txtProcessNum.Text = @"0";
                }
                txtCompleteNum.Focus();
            }
        }

        /// <summary>
        ///     完成数量KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCompleteNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtCompleteNum.Text != "")
            {
                //if (textBox9.Text != "" && textBox10.Text != "" && button2.Enabled)
                //{
                //    button2_Click(sender, e);
                //}
                //else
                //{
                txtProcessNum.Focus();
                //}
            }
        }

        /// <summary>
        ///     加工数KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtProcessNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtProcessNum.Text != "")
            {
                //if (textBox10.Text != "" && button2.Enabled)
                //{
                //    button2_Click(sender, e);
                //}
                //else
                //{
                txtPriceNo.Focus();
                //}
            }
        }

        /// <summary>
        ///     序价代码KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPriceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtPriceNo.Text != "")
            {
                btnSave_Click(sender, e);
            }
        }

        /// <summary>
        ///     生产单号KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbxCust.Focus();
            }
        }

        /// <summary>
        ///     下单数量数字校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrderQt_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsInt(txtOrderQt.Text) && !String.IsNullOrEmpty(txtOrderQt.Text))
            {
                MessageBox.Show(@"只允许输入正整数数字，请重新输入！", Resources.T提示);
                txtOrderQt.Text = "";
            }
        }

        /// <summary>
        ///     完成数量数字校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCompleteNum_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsInt(txtCompleteNum.Text) && !String.IsNullOrEmpty(txtCompleteNum.Text))
            {
                MessageBox.Show(@"只允许输入正整数数字，请重新输入！", Resources.T提示);
                txtCompleteNum.Text = "";
            }
        }

        /// <summary>
        ///     加工数数字校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtProcessNum_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(txtProcessNum.Text) && !String.IsNullOrEmpty(txtProcessNum.Text))
            {
                MessageBox.Show(@"输入数据类型错误，请重新输入！", Resources.T提示);
                txtProcessNum.Text = "";
            }
        }

        /// <summary>
        ///     原始单号变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOriWONo_TextChanged(object sender, EventArgs e)
        {
            if (txtOriWONo.Text.Length == 7)
            {
                var sql = $"SELECT TOP 1 * FROM mf_sgdan WHERE shigongdanhao='{txtOriWONo.Text.Trim()}'";
                var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (dr.HasRows)
                {
                    dr.Read();
                    if (!String.IsNullOrEmpty(dr["suhu"].ToString().Trim()) || dr["cls_id"].ToString().Trim() == "T")
                    {
                        MessageBox.Show($"此工单号{txtOriWONo.Text.Trim()}已审核锁定，请重新输入或解锁!!", Resources.T提示);
                        txtOriWONo.Text = "";
                    }
                    else
                    {
                        cbxCust.Text = dr["kehu"].ToString().Trim();
                        txtOrderNo.Text = dr["dingdanhao"].ToString().Trim();
                        txtPartNo.Text = dr["chanpintuhao"].ToString().Trim();
                        txtPartName.Text = dr["chanpinmingcheng"].ToString().Trim();
                        dtpDelivery.Text = dr["jiaohuoqi"].ToString().Trim();
                        txtPageNo.Text = dr["tuzhiyema"].ToString().Trim();
                        txtOrderQt.Focus();
                        BindData();
                    }
                    dr.Close();
                }
                else
                {
                    MessageBox.Show($"此工单号{txtOriWONo.Text.Trim()}不存在，请重新输入!!", Resources.T提示);
                    txtOriWONo.Text = "";
                }
            }
        }

        /// <summary>
        ///     原始单号KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOriWONo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOrderNo.Focus();
            }
        }

        /// <summary>
        ///     核对
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            _wono = txtWONo.Text.Trim();
            var isChecked = IsChecked(_wono) ? 0 : 1;
            var sql =
                $"UPDATE tf_sgdantwo set Checked={isChecked},Checker='{SqlHelper.UserName}',Chkdate='{DateTime.Now}' WHERE shigongdanhao ='{_wono}'";
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
            EnableCtrls(!Convert.ToBoolean(isChecked));
        }

        /// <summary>
        ///     查询是否已核对
        /// </summary>
        /// <param name="wonoStr">工单号</param>
        /// <returns></returns>
        private static bool IsChecked(string wonoStr)
        {
            var sql = $"SELECT checked FROM tf_sgdantwo WHERE shigongdanhao='{wonoStr}'";
            var isChecked = SqlHelper.ExecuteScalar(SqlHelper.GetConnSting(), CommandType.Text, sql);
            return isChecked != null && (bool) isChecked;
        }

        /// <summary>
        ///     启用控件
        /// </summary>
        private void EnableCtrls(bool isCheck)
        {
            lblCheck.Visible = !isCheck;
            txtPartNo.ReadOnly = !isCheck;
            txtOrderNo.ReadOnly = !isCheck;
            txtPartName.ReadOnly = !isCheck;
            txtOrderQt.ReadOnly = !isCheck;
            btnFinish.Visible = isCheck;
            btnSave.Visible = isCheck;
            btnFinish.Enabled = isCheck;
            btnSave.Enabled = isCheck;
            cbxCust.Enabled = isCheck;
            dtpDelivery.Enabled = isCheck;
        }
    }
}