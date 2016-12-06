using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using LYH.WorkOrder.Properties;
using LYH.WorkOrder.share;

namespace LYH.WorkOrder
{
    public partial class FrmWorkOrderReceive : Form
    {
        private string _cust;
        private string _deliveryDate;
        private string _material;
        private string _orderNo;
        private string _orderQuantity;
        private string _pageNum;
        private string _planDate;
        private string _processName;
        private string _prtDwgNo;
        private string _prtName;
        private string _sn;

        public FrmWorkOrderReceive()
        {
            KeyDown += FrmWin_KeyDown;
            InitializeComponent();
            BindDataDgv2();
            BindDataDgv3();
        }

        private void FrmWorkOrderReceive_Load(object sender, EventArgs e)
        {
            txtWONo.Focus();
        }

        private void FrmWin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    DialogResult = DialogResult.Cancel;
                    break;
            }
        }

        private void txtWONo_TextChanged(object sender, EventArgs e)
        {
            if (txtWONo.Text.Length == 7)
            {
                var sql = $"SELECT TOP 1 * FROM mf_sgdan WHERE shigongdanhao='{txtWONo.Text.Trim()}'";
                var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (dr.HasRows)
                {
                    dr.Read();
                    _cust = dr["kehu"].ToString().Trim();
                    _deliveryDate = dr["jiaohuoqi"].ToString().Trim();
                    _prtDwgNo = dr["chanpintuhao"].ToString().Trim();
                    _prtName = dr["chanpinmingcheng"].ToString().Trim();
                    _pageNum = dr["tuzhiyema"].ToString().Trim();
                    _material = dr["cailiao"].ToString().Trim();
                    _orderNo = dr["dingdanhao"].ToString().Trim();
                    _orderQuantity = dr["dingdansuliang"].ToString().Trim();
                    dr.Close();
                    BindDataDgv1();
                    BindDataDgv2();
                    txtCraftSeq.Focus();
                }
                else
                {
                    MessageBox.Show($"此工单号{txtWONo.Text.Trim()}不存在，请重新输入!!", Resources.T提示);
                    txtWONo.Text = "";
                    txtWONo.Focus();
                    dr.Close();
                }
            }
        }

        private void BindDataDgv1()
        {
            if (txtWONo.Text.Trim() != "")
            {
                var sql = "SELECT xuhao '序号',gongxumingcheng '工序名称',jihuariqi '计划完成' " +
                          $"FROM tf_sgdan WHERE shigongdanhao='{txtWONo.Text.Trim()}'";
                var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 60;
                dataGridView1.Columns[1].Width = 75;
                dataGridView1.Columns[2].Width = 90;
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            }
        }

        private void BindDataDgv2()
        {
            const string abbc = "未完成";
            var sql = "SELECT IDL '序',sgdhao '工单号',ddhao '生产单号',kehu '客户'," +
                      "jhqi '交货期',tuhao '图号',name '名称',yema '页码',cailiao '材料',sulia '数量'," +
                      "wsoo '已完成',xuhao '工序号',gyname '工序名称',jhwxri '计划完成',jiesou '接收日期' " +
                      $"FROM udone WHERE beistr='{abbc}' AND DeptId='{SqlHelper.DeptId}'  ORDER BY jiesou DESC";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns[0].Width = 0;
            dataGridView2.Columns[1].Width = 80;
            dataGridView2.Columns[2].Width = 80;
            dataGridView2.Columns[3].Width = 80;
            dataGridView2.Columns[4].Width = 110;
            dataGridView2.Columns[5].Width = 110;
            dataGridView2.Columns[6].Width = 90;
            dataGridView2.Columns[7].Width = 100;
            dataGridView2.Columns[8].Width = 80;
            dataGridView2.Columns[9].Width = 80;
            dataGridView2.Columns[10].Width = 90;
            dataGridView2.Columns[11].Width = 90;
            dataGridView2.Columns[12].Width = 90;
            dataGridView2.Columns[13].Width = 90;
            dataGridView2.Columns[14].Width = 90;
        }

        private void BindDataDgv3()
        {
            const string abbc = "已完成";
            var sql = "SELECT IDL '序',sgdhao '工单号',ddhao '生产单号',kehu '客户'," +
                      "jhqi '交货期',tuhao '图号',name '名称',yema '页码',cailiao '材料',sulia '数量'," +
                      "xuhao '工序号',gyname '工序名称',jhwxri '计划完成',jiesou '接收日期'FROM udone " +
                      $"WHERE beistr='{abbc}' ORDER BY jiesou DESC";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView3.DataSource = ds.Tables[0];
            dataGridView3.Columns[0].Width = 0;
            dataGridView3.Columns[1].Width = 80;
            dataGridView3.Columns[2].Width = 80;
            dataGridView3.Columns[3].Width = 80;
            dataGridView3.Columns[4].Width = 110;
            dataGridView3.Columns[5].Width = 110;
            dataGridView3.Columns[6].Width = 90;
            dataGridView3.Columns[7].Width = 100;
            dataGridView3.Columns[8].Width = 80;
            dataGridView3.Columns[9].Width = 80;
            dataGridView3.Columns[10].Width = 90;
            dataGridView3.Columns[11].Width = 90;
            dataGridView3.Columns[12].Width = 90;
            dataGridView3.Columns[13].Width = 90;
        }

        private void txtCraftSeq_TextChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < txtCraftSeq.Text.Length; i++)
            {
                if (txtCraftSeq.Text[i] >= '0' && txtCraftSeq.Text[i] <= '9')
                {
                }
                else
                {
                    MessageBox.Show("只允许输入数字，请重新输入！", Resources.T提示);
                    txtCraftSeq.Text = "";
                }
            }
        }

        private void btnReceiveConfirm_Click(object sender, EventArgs e)
        {
            var sql =
                $"SELECT * FROM udone WHERE sgdhao='{txtWONo.Text.Trim()} ' AND DeptId='{SqlHelper.DeptId}'";
            var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            if (dr.HasRows)
            {
                MessageBox.Show($"此工单< {txtWONo.Text.Trim()} >已存在，请重新选择！", Resources.T提示);
                txtCraftSeq.Text = "";
                txtCraftSeq.Focus();
                dr.Close();
                return;
            }
            sql = $"SELECT TOP 1 * FROM udone WHERE sgdhao='{txtWONo.Text.Trim()} ' AND xuhao=' {txtCraftSeq.Text.Trim()}'";
            dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            if (dr.HasRows)
            {
                MessageBox.Show($"此工序号< {txtCraftSeq.Text.Trim()} >已存在，请重新选择！", Resources.T提示);
                txtCraftSeq.Text = "";
                txtCraftSeq.Focus();
                dr.Close();
                return;
            }
            sql =
                $"SELECT TOP 1 * FROM tf_sgdan WHERE shigongdanhao='{txtWONo.Text.Trim()}'AND xuhao='{txtCraftSeq.Text.Trim()} '";
            dr.Close();
            dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            if (dr.HasRows)
            {
                dr.Read();
                _sn = dr["xuhao"].ToString().Trim();
                _processName = dr["gongxumingcheng"].ToString().Trim();
                _planDate = dr["jihuariqi"].ToString().Trim();
                dr.Close();
                const string adb12 = "未完成";
                var sqltk = "insert into udone(DeptId,sgdhao,ddhao,kehu,jhqi,tuhao,name,yema,cailiao,xuhao," +
                            $"gyname,jhwxri,sulia,beistr) values('{SqlHelper.DeptId}','{txtWONo.Text.Trim().ToUpper()}','{_orderNo}','{_cust}','{_deliveryDate}','{_prtDwgNo}','{_prtName}','{_pageNum}','{_material}','{_sn}'," +
                            $"'{_processName}','{_planDate}','{_orderQuantity}','{adb12}')";
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sqltk);
                BindDataDgv2();
                BindDataDgv3();
                txtWONo.Text = "";
                txtCraftSeq.Text = "";
                ActiveFrmProcCardBom();
            }
            else
            {
                MessageBox.Show($"此工序号{txtCraftSeq.Text.Trim()}不存在，请重新输入!!", Resources.T提示);
                txtCraftSeq.Text = "";
                txtCraftSeq.Focus();
                dr.Close();
            }
        }

        private void ActiveFrmProcCardBom()
        {
            var frmProcCardBom = (FrmProcCardBom) ChildWinManage.LoadMdiForm(this.MdiParent, typeof(FrmProcCardBom));
            frmProcCardBom.InitProcCard(txtWONo.Text.Trim());
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("提示：请选择要删除的项目行！", Resources.J警告);
            }
            else
            {
                var mMidtw3 = dataGridView2.SelectedCells[0].Value.ToString().Trim();
                if (MessageBox.Show("是否要删除所选择的行", Resources.J警告, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) ==
                    DialogResult.OK)
                {
                    var sql = $"delete FROM udone WHERE IDL='{mMidtw3}'";
                    SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                    BindDataDgv2();
                    BindDataDgv3();
                }
                else
                {
                    BindDataDgv2();
                    BindDataDgv3();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var aboutForm = new FrmCncHandwriting();
            aboutForm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (btnRedirect.Text == Resources.C退出)
            {
                Close();
            }
            else
            {
                btnRedirect.Text = Resources.C退出;
                btnUpd.Text = Resources.X修改;
                btnReceiveConfirm.Visible = true;
                btnDel.Visible = true;
                btnAdd.Visible = true;
                btnRefresh.Visible = true;
                groupBox1.Visible = true;
                txtWONo.ReadOnly = false;
                txtCraftSeq.ReadOnly = false;
                txtPONo.Text = "";
                txtCust.Text = "";
                txtDwgNo.Text = "";
                txtPrtName.Text = "";
                txtPageNo.Text = "";
                txtMaterial.Text = "";
                txtQty.Text = "";
                txtWONo2.Text = "";
                BindDataDgv2();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindDataDgv2();
            BindDataDgv3();
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            if (txtWONo2.Text == "")
            {
                MessageBox.Show("请先选择行后在进行修改!!", Resources.T提示);
            }
            else if (btnUpd.Text == Resources.X修改)
            {
                btnUpd.Text = Resources.X保存;
                btnRedirect.Text = Resources.C取消;
                btnReceiveConfirm.Visible = false;
                btnDel.Visible = false;
                btnAdd.Visible = false;
                btnRefresh.Visible = false;
                groupBox1.Visible = false;
                txtWONo.ReadOnly = true;
                txtCraftSeq.ReadOnly = true;
                txtPONo.Focus();
            }
            else
            {
                if (txtPONo.Text == "")
                {
                    MessageBox.Show("生产单号不能为空！", Resources.T提示);
                    txtPONo.Focus();
                    return;;
                }
                if (txtCust.Text == "")
                {
                    MessageBox.Show("客户不能为空！", Resources.T提示);
                    txtCust.Focus();
                    return;
                }
                if (txtDwgNo.Text == "")
                {
                    MessageBox.Show("图号不能为空！", Resources.T提示);
                    txtDwgNo.Focus();
                    return;
                }
                if (txtQty.Text == "")
                {
                    MessageBox.Show("数量不能为空！", Resources.T提示);
                    txtQty.Focus();
                    return;
                }
                var sql =
                    $"UPDATE udone set ddhao='{txtPONo.Text.Trim()}',kehu='{txtCust.Text.Trim()}',tuhao='{txtDwgNo.Text.Trim()}',name='{txtPrtName.Text.Trim()}',yema='{txtPageNo.Text.Trim()}'," +
                    $"cailiao='{txtMaterial.Text.Trim()}',sulia='{txtQty.Text.Trim()}' WHERE sgdhao ='{txtWONo2.Text.Trim()}'";
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                btnRedirect.Text = Resources.C退出;
                btnUpd.Text = Resources.X修改;
                btnReceiveConfirm.Visible = true;
                btnDel.Visible = true;
                btnAdd.Visible = true;
                btnRefresh.Visible = true;
                groupBox1.Visible = true;
                txtWONo.ReadOnly = false;
                txtCraftSeq.ReadOnly = false;
                txtPONo.Text = "";
                txtCust.Text = "";
                txtDwgNo.Text = "";
                txtPrtName.Text = "";
                txtPageNo.Text = "";
                txtMaterial.Text = "";
                txtQty.Text = "";
                txtWONo2.Text = "";
                BindDataDgv2();
            }
        }
        
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtWONo.Text = dataGridView2.SelectedCells[1].Value.ToString().Trim();
            txtWONo2.Text = dataGridView2.SelectedCells[1].Value.ToString().Trim();
            txtPONo.Text = dataGridView2.SelectedCells[2].Value.ToString().Trim();
            txtCust.Text = dataGridView2.SelectedCells[3].Value.ToString().Trim();
            txtDwgNo.Text = dataGridView2.SelectedCells[5].Value.ToString().Trim();
            txtPrtName.Text = dataGridView2.SelectedCells[6].Value.ToString().Trim();
            txtPageNo.Text = dataGridView2.SelectedCells[7].Value.ToString().Trim();
            txtMaterial.Text = dataGridView2.SelectedCells[8].Value.ToString().Trim();
            txtQty.Text = dataGridView2.SelectedCells[9].Value.ToString().Trim();
            txtCraftSeq.Text = dataGridView2.SelectedCells[11].Value.ToString().Trim();
        }

        private void btnRedirect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWONo.Text.Trim()))
            {
                MessageBox.Show(@"工单号不能为空！", Resources.T提示);
                txtWONo.Focus();
                return;
            }
            ActiveFrmProcCardBom();
        }
    }
}