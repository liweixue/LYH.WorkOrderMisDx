using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using LYH.Framework.Commons;
using LYH.WorkOrder.Properties;
using LYH.WorkOrder.share;

namespace LYH.WorkOrder
{
    public partial class FrmProcCardInput : Form
    {
        private const string Finished = "已完成";
        private const string NotFinish = "未完成";
        private const string Finish = "完成";
        private const decimal Increment = 0.0001M;
        private readonly Dictionary<string, string> _dictTeam = new Dictionary<string, string>();
        private string _finishCraft;

        public FrmProcCardInput()
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

        private void FrmProcCardEntry_Load(object sender, EventArgs e)
        {
            InitDictItem();
        }

        private void InitDictItem()
        {
            _dictTeam.Clear();
            var sql =
                $"SELECT a.ID,a.Team FROM PD_ProcCard_Team a JOIN TB_Dept b ON b.ID=a.DeptId WHERE b.ID='{SqlHelper.DeptId}'";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString("dzdj"), CommandType.Text, sql);

            FillAutoCompleteDataSet(ssbTeam,_dictTeam, ds);

            sql =
                $"SELECT ID,Craft FROM dbo.PD_ProcCard_Craft WHERE DeptId='{SqlHelper.DeptId}'";
            ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString("dzdj"), CommandType.Text, sql);

            var dictName = new Dictionary<string, string>();
            FillAutoCompleteDataSet(ssbCraft, dictName, ds);

            //cbxTeam.Tag = _dict;

            //const string sql = "SELECT distinct BZname FROM udbz ";
            //var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            //while (dr.Read())
            //{
            //    cbxTeam.Items.Add(dr["BZname"].ToString().Trim());
            //}
            //dr.Close();
        }

        private void FillAutoCompleteDataSet(SpellSearchBoxEx.SpellSearchBoxEx ssBox, Dictionary<string, string> dictionary, DataSet ds)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
                dictionary.Add(dr[0].ToString(), dr[1].ToString());
            ssBox.SpellSearchSource = dictionary.Values.ToArray();
            var autoCompleteCustomSource = new AutoCompleteStringCollection();
            foreach (var dictValue in dictionary)
                autoCompleteCustomSource.Add(dictValue.Value);
            ssBox.AutoCompleteCustomSource = autoCompleteCustomSource;
        }

        private void BindDataDgv1()
        {
            var sql = "SELECT distinct sgdhao \'工单号\',ddhao \'生产单号\',kehu \'客户\',jhqi \'订单交期\'," +
                      $"tuhao '图号',suliang '数量' FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'";
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
            var sql = "SELECT idaa '0',gxone '序号',gxname'工序名称',gxtwo'加工工序'," +
                      "tiao'调机时间',danjian'单件时间',xuj'单价',buzu '调机补助',tiaoren '调机员'," +
                      "wssulia'加工数量',wsbanz'加工班组',bei3 '生产员',hujii '件资',wsriqi'完成日期'," +
                      "lururiqi'录入日期',lururen'录入人',qgriqi'修改日期',qgren'修改人' FROM udktr " +
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

        private void txtProcCardNo_TextChanged(object sender, EventArgs e)
        {
            if (txtProcCardNo.Text.Length != 7) return;
            var sql = $"SELECT TOP 1 * FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'";
            var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            if (dr.HasRows)
            {
                dr.Read();
                txtOrderQty.Text = dr["suliang"].ToString().Trim();
                BindDataDgv1();
                BindDataDgv2();
                txtProcCardNo.ReadOnly = true;
                txtSeq.Focus();
            }
            else
            {
                MessageBox.Show($"此工艺卡号{txtProcCardNo.Text.Trim()}不存在，请重新输入!!", Resources.T提示);
                txtProcCardNo.ResetText();
                BindDataDgv1();
                BindDataDgv2();
            }

            dr.Close();
        }

        private void txtProcessSeq_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(txtProcessSeq.Text) && !string.IsNullOrEmpty(txtProcessSeq.Text))
            {
                MessageBox.Show("输入数据类型错误，请重新输入！", Resources.T提示);
                txtProcessSeq.ResetText();
            }
            else
            {
                var sql =
                    $"SELECT TOP 1 * FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'" +
                    $"AND gxone=' {txtSeq.Text.Trim()}'AND gxtwo=' {txtProcessSeq.Text.Trim()}'";
                var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (dr.HasRows)
                {
                    dr.Read();
                    ssbCraft.Text = dr["gxname"].ToString().Trim();
                    txtAdjust.Text = dr["tiao"].ToString().Trim();
                    txtSingle.Text = dr["danjian"].ToString().Trim();
                    txtSeqPrice.Text = dr["xuj"].ToString().Trim();
                    txtSubsidy.Text = dr["buzu"].ToString().Trim();
                }
            }
        }

        //private void txtProcessQty_TextChanged(object sender, EventArgs e)
        //{
        //    if (!StringHelper.IsNumeric(txtProcessQty.Text) && !string.IsNullOrEmpty(txtProcessQty.Text))
        //    {
        //        MessageBox.Show("只允许输入正整数数字，请重新输入！", Resources.T提示);
        //        txtProcessQty.ResetText();
        //    }
        //}

        private void btnFinish_Click(object sender, EventArgs e)
        {
            txtProcCardNo.ReadOnly = false;
            txtSeq.ReadOnly = false;
            txtProcessSeq.ReadOnly = false;
            btnSave.Visible = true;
            btnDel.Visible = true;
            dataGridView1.Visible = true;
            btnUpd.Text = Resources.X修改;
            txtProcCardNo.ResetText();
            txtSeq.ResetText();
            ClearCtrlText();
            txtAdjust.ResetText();
            txtSingle.ResetText();
            txtSeqPrice.ResetText();
            txtSubsidy.ResetText();
            txtProcCardNo.Focus();
            BindDataDgv1();
            BindDataDgv2();
        }

        private void txtSeq_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSeq.Text) && !string.IsNullOrEmpty(txtProcCardNo.Text))
            {
                MessageUtil.ShowError("序号不能为空！");
                txtSeq.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(txtSeq.Text) && !StringHelper.IsNumeric(txtSeq.Text))
            {
                MessageBox.Show(Resources.IsNotNumber, Resources.T提示);
                txtSeq.ResetText();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (TextIsEmpty()) return;

            var procCardNo = txtProcCardNo.Text.Trim();
            var sql =
                $"SELECT TOP 1 * FROM udktr WHERE zling='{procCardNo}'" +
                $"AND gxone=' {txtSeq.Text.Trim()}'AND gxtwo=' {txtProcessSeq.Text.Trim()}'";
            var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            if (!dr.HasRows)
            {
                btnIns_Click(null, null);
            }
            else
            {
                dr.Read();
                var seqPrice = decimal.Parse(dr["xuj"].ToString());
                var subsidy = decimal.Parse(dr["buzu"].ToString());
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
                _finishCraft = dr["gxname"].ToString().Trim();
                var processQty = decimal.Parse(txtProcessQty.Text);
                var subtotal = processQty * seqPrice + subsidy;
                var team = ssbTeam.Text.Trim().ToUpper();
                var finishedDate = dateTimePicker1.Text.Trim();
                var processer = ssbProcesser.Text.Trim();
                var debugger = ssbDebugger.Text.Trim();
                var processSeq = decimal.Parse(txtProcessSeq.Text.Trim());
                if (!string.IsNullOrEmpty(dr["lururiqi"].ToString()))
                {
                    dr.Close();
                    if (MessageBox.Show($"此加工工序{processSeq}已录入过，请确认是否创建副工序!!",
                            Resources.T提示, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) ==
                        DialogResult.OK)
                    {
                        sql =
                            $"SELECT Max(gxtwo) FROM udktr WHERE zling='{procCardNo}' AND " +
                            $"gxone='{txtSeq.Text.Trim()}' AND gxtwo>='{processSeq}' AND gxtwo<'{processSeq + 1}'";
                        var maxProcessSeq = SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text, sql);
                        var newProcessSeq = Convert.ToDecimal(maxProcessSeq.ToString().Trim());
                        newProcessSeq += Increment;
                        //sql =
                        //    $"SELECT sum(buzu) FROM udktr WHERE zling='{procCardNo}' AND " +
                        //    $"gxone='{txtSeq.Text.Trim()}' AND gxtwo>='{processSeq}' AND gxtwo<'{processSeq + 1}'";
                        //var subsidyScalar =
                        //    (decimal) SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text, sql);
                        //subsidy = subsidyScalar > 0 ? 0 : subsidy;
                        subsidy = 0;
                        subtotal = processQty * seqPrice;
                        sql =
                            "insert into udktr(zling,sgdhao,ddhao,kehu,jhqi,tuhao,name,yema,suliang,cailiao,gxone,gxname,gxtwo," +
                            "tiao,danjian,xuj,buzu,cjriqi,cjren,wssulia,wsbanz,wsriqi,hujii,lururiqi,lururen,bei3,tiaoren) values" +
                            $"({procCardNo},'{wONo}','{pONo}','{cust}','{planDate}','{prtDwgNo}','{prtName}'," +
                            $"'{pageNo}',{orderQty},'{material}',{txtSeq.Text.Trim()},'{craft}',{newProcessSeq},{debugTime}," +
                            $"{processTime},{seqPrice},{subsidy},'{createdDate}','{createdBy}',{processQty},'{team}','{finishedDate}'," +
                            $"{subtotal},GETDATE(),'{SqlHelper.UserName}','{processer}','{debugger}')";
                        SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                        UpdateFinishRemark(orderQty, wONo);
                    }
                    else
                    {
                        ClearCtrlText();
                        txtProcessSeq.Focus();
                    }
                }
                else
                {
                    txtId.Text = dr["idaa"].ToString().Trim();
                    dr.Close();
                    UpdateProcCardEntry(subtotal, true);
                    UpdateFinishRemark(orderQty, wONo);
                }
            }
        }

        private bool TextIsEmpty()
        {
            if (string.IsNullOrEmpty(txtProcCardNo.Text.Trim()))
            {
                MessageBox.Show("工艺卡号不能为空！", Resources.T提示);
                txtProcCardNo.ReadOnly = false;
                txtProcCardNo.Focus();
                return true;
            }

            if (string.IsNullOrEmpty(txtSeq.Text.Trim()))
            {
                MessageBox.Show("序号不能为空！", Resources.T提示);
                txtSeq.Focus();
                return true;
            }

            if (string.IsNullOrEmpty(txtProcessQty.Text.Trim()))
            {
                MessageBox.Show("加工数量不能为空！", Resources.T提示);
                txtProcessQty.Focus();
                return true;
            }

            if (string.IsNullOrEmpty(ssbTeam.Text.Trim()))
            {
                MessageBox.Show("加工班组不能为空！", Resources.T提示);
                ssbTeam.Focus();
                return true;
            }

            if (string.IsNullOrEmpty(txtProcessSeq.Text.Trim()))
            {
                MessageBox.Show("加工工序不能为空！", Resources.T提示);
                txtProcessSeq.Focus();
                return true;
            }

            return false;
        }

        private void UpdateFinishRemark(string orderQty, string wONo)
        {
            if (_finishCraft == Finish)
            {
                var sql =
                    $"SELECT SUM(wssulia) FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}' " +
                    $"AND gxone={txtSeq.Text.Trim()} AND gxname='{_finishCraft}'";
                var finishedQty = (int) SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text, sql);
                sql = finishedQty >= int.Parse(orderQty)
                    ? $"UPDATE udone set beistr='{Finished}',wsoo='{finishedQty}' WHERE sgdhao ='{wONo}'"
                    : $"UPDATE udone set wsoo='{finishedQty}' WHERE sgdhao ='{wONo}'";
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
            }

            BindDataDgv2();
            ClearCtrlText();
            txtSeq.Focus();
            txtSeq.SelectAll();
        }

        private void btnUpd_Click(object sender, EventArgs e) //修改
        {
            if (btnUpd.Text == Resources.X修改)
            {
                if (string.IsNullOrEmpty(txtSeq.Text))
                {
                    MessageBox.Show("序号不能为空！", Resources.T提示);
                    txtSeq.Focus();
                    return;
                }

                if (MessageBox.Show("是否已经选择好所要修改的行，若未选好请按取消！",
                        Resources.T提示, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    var id = dataGridView2.SelectedCells[0].Value.ToString().Trim();
                    txtId.Text = id;
                    var seq = dataGridView2.SelectedCells[1].Value.ToString().Trim();
                    if (txtSeq.Text == seq)
                    {
                        var sql = $"SELECT TOP 1 * FROM udktr WHERE idaa='{id}'";
                        var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                        if (dr.HasRows)
                        {
                            dr.Read();
                            txtSeq.Text = dr["gxone"].ToString().Trim();
                            _finishCraft = dr["gxname"].ToString().Trim();
                            txtProcessSeq.Text = dr["gxtwo"].ToString().Trim();
                            txtProcessQty.Text = dr["wssulia"].ToString().Trim();
                            ssbTeam.Text = dr["wsbanz"].ToString().Trim();
                            InitProcesser();
                            txtAdjust.Text = dr["tiao"].ToString().Trim();
                            txtSingle.Text = dr["danjian"].ToString().Trim();
                            txtSeqPrice.Text = dr["xuj"].ToString().Trim();
                            txtSubsidy.Text = dr["buzu"].ToString().Trim();
                            ssbProcesser.Text = dr["bei3"].ToString().Trim();
                            ssbDebugger.Text = dr["tiaoren"].ToString().Trim();
                            txtSeq.ReadOnly = true;
                            txtProcessSeq.ReadOnly = true;
                            btnSave.Visible = false;
                            btnDel.Visible = false;
                            dataGridView1.Visible = false;
                            btnUpd.Text = Resources.X保存;
                        }

                        dr.Close();
                    }
                    else
                    {
                        MessageBox.Show("序号与选择的行不符，请重新输入或选择！", Resources.T提示);
                        txtSeq.Focus();
                        txtSeq.SelectAll();
                    }
                }
            }
            else
            {
                var processQty = decimal.Parse(txtProcessQty.Text.Trim());
                var seqPrice = decimal.Parse(txtSeqPrice.Text.Trim());
                var subsidy = decimal.Parse(txtSubsidy.Text.Trim());
                var subtotal = processQty * seqPrice + subsidy;
                UpdateProcCardEntry(subtotal, false);
                var sql = $"SELECT TOP 1 * FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'";
                var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (dr.HasRows)
                {
                    dr.Read();
                    var orderQty = dr["suliang"].ToString().Trim();
                    var wONo = dr["sgdhao"].ToString().Trim();
                    UpdateFinishRemark(orderQty, wONo);
                }

                btnUpd.Text = Resources.X修改;
                txtSeq.ReadOnly = false;
                txtProcessSeq.ReadOnly = false;
                dataGridView1.Visible = true;
                btnSave.Visible = true;
                btnDel.Visible = true;
                ClearCtrlText();
                txtAdjust.ResetText();
                txtSingle.ResetText();
                txtSeqPrice.ResetText();
                txtSubsidy.ResetText();
                txtSeq.Focus();
                txtSeq.SelectAll();
                BindDataDgv2();
            }
        }

        public void UpdateProcCardEntry(decimal subtotal, bool isIns)
        {
            var sql =
                $"UPDATE udktr set wssulia='{txtProcessQty.Text.Trim()}',wsbanz='{ssbTeam.Text.Trim().ToUpper()}'," +
                $"wsriqi='{dateTimePicker1.Text.Trim()}',gxname='{ssbCraft.Text}'," +
                $"hujii={subtotal},bei3='{ssbProcesser.Text.Trim()}',{(isIns ? "lururiqi" : "qgriqi")}='{DateTime.Now}'," +
                $"{(isIns ? "lururen" : "qgren")}='{SqlHelper.UserName}',tiaoren='{ssbDebugger.Text.Trim()}'" +
                $" WHERE zling ='{txtProcCardNo.Text.Trim()}' AND gxone='{txtSeq.Text.Trim()}'" +
                $" AND gxtwo='{txtProcessSeq.Text.Trim()}' AND idaa='{txtId.Text}'";
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
        }

        private void ClearCtrlText()
        {
            txtProcessQty.ResetText();
            txtProcessSeq.ResetText();
            ssbCraft.ResetText();
            ssbTeam.ResetText();
            ssbProcesser.ResetText();
            ssbDebugger.ResetText();
            ssbCraft.Focus();
            ssbTeam.Focus();
            ssbProcesser.Focus();
            ssbDebugger.Focus();
        }

        private void txtProcessQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ssbTeam.Focus();
        }

        private void ssbTeam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ssbDebugger.Focus();
        }

        private void ssbTeam_Leave(object sender, EventArgs e)
        {
            InitProcesser();
        }
        
        private void InitProcesser()
        {
            var dictName = new Dictionary<string, string>();
            if (ssbTeam.Text != "" && _dictTeam != null && _dictTeam.ContainsValue(ssbTeam.Text))
            {
                var id = _dictTeam.Where(d => d.Value == ssbTeam.Text).Select(d => d.Key).SingleOrDefault();
                var sql = $"SELECT ID,Name FROM DZDJ.dbo.PD_ProcCard_Employee WHERE TeamId={id}";
                var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString("dzdj"), CommandType.Text, sql);

                dictName.Clear();
                FillAutoCompleteDataSet(ssbDebugger, dictName, ds);
                dictName.Clear();
                FillAutoCompleteDataSet(ssbProcesser, dictName, ds);
            }
        }

        private void txtProcessSeq_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ssbCraft.Focus();
        }

        private void btnDel_Click(object sender, EventArgs e) //删除
        {
            var id = dataGridView2.SelectedCells[0].Value.ToString().Trim();
            var seq = dataGridView2.SelectedCells[1].Value.ToString().Trim();
            var processSeq = dataGridView2.SelectedCells[3].Value.ToString().Trim();
            var isOriginalRow = int.TryParse(processSeq, out _);
            if (isOriginalRow)
            {
                MessageBox.Show("此行不可删除，只可修改", Resources.T提示);
            }
            else
            {
                if (MessageBox.Show("是否要删除所选择的行", Resources.J警告, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) ==
                    DialogResult.OK)
                {
                    var sql =
                        $"DELETE FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'AND gxone='{seq}'AND gxtwo='{processSeq}' AND idaa='{id}'";
                    SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                    BindDataDgv2();
                    sql = $"SELECT TOP 1 * FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'";
                    var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        var orderQty = dr["suliang"].ToString().Trim();
                        var wONo = dr["sgdhao"].ToString().Trim();
                        dr.Close();
                        UpdateFinishRemark(orderQty, wONo);
                    }
                }
            }
        }

        private void txtSeq_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtProcessSeq.Focus();

                var sql =
                    $"SELECT TOP 1 * FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'AND gxone=' {txtSeq.Text.Trim()}'";
                var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (!dr.HasRows)
                {
                    MessageBox.Show("此序号不存在！", Resources.T提示);
                    txtSeq.Focus();
                    txtSeq.SelectAll();
                }
            }
        }

        private void ssbDebugger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ssbProcesser.Focus();
        }

        private void btnIns_Click(object sender, EventArgs e)
        {
            if (TextIsEmpty()) return;
            var sql =
                $"SELECT TOP 1 * FROM udktr WHERE zling='{txtProcCardNo.Text.Trim()}'";
            var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            if (dr.HasRows)
            {
                dr.Read();
                var wONo = dr["sgdhao"].ToString().Trim();
                var pONo = dr["ddhao"].ToString().Trim();
                var cust = dr["kehu"].ToString().Trim();
                var planDate = dr["jhqi"].ToString().Trim();
                var prtDwgNo = dr["tuhao"].ToString().Trim();
                var prtName = dr["name"].ToString().Trim();
                var pageNo = dr["yema"].ToString().Trim();
                var orderQty = dr["suliang"].ToString().Trim();
                var material = dr["cailiao"].ToString().Trim();
                var createdDate = dr["cjriqi"].ToString().Trim();
                var createdBy = SqlHelper.UserName;
                var prodCardNo = txtProcCardNo.Text.Trim();
                var seqPrice = txtSeqPrice.Text.ToDecimal();
                var subsidy = 0; //txtSubsidy.Text.ToDecimal()插入时不能有补助
                var debugTime = txtAdjust.Text.ToDecimal();
                var processTime = txtSingle.Text.ToDecimal();
                var craft = ssbCraft.Text.Trim();
                _finishCraft = craft;
                var processQty = decimal.Parse(txtProcessQty.Text.Trim());
                var subtotal = processQty * seqPrice;
                var team = ssbTeam.Text.Trim().ToUpper();
                var finishedDate = dateTimePicker1.Text.Trim();
                var processer = ssbProcesser.Text.Trim();
                var debugger = ssbDebugger.Text.Trim();
                var processSeq = decimal.Parse(txtProcessSeq.Text.Trim()) + Increment;
                var seq = txtSeq.Text.Trim();
                sql = //$"UPDATE udktr SET gxone=gxone+1 WHERE zling='{prodCardNo}' AND gxone>={seq};" +
                    "insert into udktr(zling,sgdhao,ddhao,kehu,jhqi,tuhao,name,yema,suliang,cailiao,gxone,gxname,gxtwo," +
                    "tiao,danjian,xuj,buzu,cjriqi,cjren,wssulia,wsbanz,wsriqi,hujii,lururiqi,lururen,bei3,tiaoren) values" +
                    $"({prodCardNo},'{wONo}','{pONo}','{cust}','{planDate}','{prtDwgNo}','{prtName}'," +
                    $"'{pageNo}',{orderQty},'{material}',{seq},'{craft}',{processSeq},{debugTime}," +
                    $"{processTime},{seqPrice},{subsidy},'{createdDate}','{createdBy}',{processQty},'{team}','{finishedDate}'," +
                    $"{subtotal},GETDATE(),'{SqlHelper.UserName}','{processer}','{debugger}')";
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
                UpdateFinishRemark(orderQty, wONo);
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentRow != null) txtSeq.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            var keyEventArgs = new KeyEventArgs(Keys.Enter);
            txtSeq_KeyDown(sender, keyEventArgs);
        }

        private void ssbCraft_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtProcessQty.Focus();
        }

        private void txtProcessQty_Leave(object sender, EventArgs e)
        {
            var currentSeq = txtProcessSeq.Text.Trim().ToDecimal();
            var currentInputQty = txtProcessQty.Text.Trim().ToInt32();
            var orderQty = txtOrderQty.Text.Trim().ToInt32();
            var successed = GetPreviousAndCurrentQuantity(!btnSave.Enabled, currentSeq, out var previousCompletedQty,
                out var completedQty);

            if (successed)
            {
                completedQty = completedQty + currentInputQty;
                //if ((int)currentSeq == 1)
                //{
                if (completedQty > orderQty)
                {
                    MessageBox.Show(@"总数大于下单数量,请修改!超出数量为:" + (completedQty - orderQty));
                    txtProcessQty.Text = (currentInputQty - (completedQty - orderQty)).ToString();
                    txtProcessQty.Focus();
                }

                //}
                //else
                //{
                //    if (completedQty > previousCompletedQty)
                //    {
                //        MessageBox.Show(@"总数大于前工序数量,请修改!超出数量为:" + (completedQty - previousCompletedQty));
                //        txtProcessQty.Text =
                //            (currentInputQty - (completedQty - previousCompletedQty)).ToString();
                //        txtProcessQty.Focus();
                //    }
                //}
            }
            else
            {
                txtProcessQty.ResetText();
                txtProcessQty.Focus();
            }
        }

        private bool GetPreviousAndCurrentQuantity(bool isUpdate, decimal currentSeq, out decimal previousCompletedQty,
            out decimal completedQty)
        {
            try
            {
                var sql =
                    "SELECT SUM(ISNULL(wssulia,0)) currentQty,gxone,gxtwo FROM dzscb.dbo.udktr " +
                    $"WHERE zling='{txtProcCardNo.Text.Trim()}' GROUP BY gxone,gxtwo";
                var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);

                var table = ds.Tables[0];
                var condition = isUpdate ? $" AND gxtwo<>{currentSeq}" : "";
                condition =
                    $"CONVERT(gxtwo,'System.Int32')={(int) currentSeq}{condition} AND gxone={txtSeq.Text.Trim()}";
                completedQty = table.Compute("SUM(currentQty)", condition).ToString().ToDecimal();

                if ((int) currentSeq > 1)
                {
                    var rowIndex = table.Rows.IndexOf(table.Select($"gxtwo={(int) currentSeq}").First());
                    var previousSeq = table.Rows[rowIndex - 1]["gxtwo"].ToString().ToDecimal();
                    previousCompletedQty = table.Compute("SUM(currentQty)",
                            $"CONVERT(gxtwo,'System.Int32')={(int) previousSeq}").ToString()
                        .ToInt32();
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
    }
}