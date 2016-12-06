using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LYH.Framework.Commons;
using LYH.WorkOrder.Properties;
using QRCoder;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmProcCard : Form
    {
        AppConfig _appConfig=new AppConfig();

        public FrmProcCard()
        {
            KeyDown+=FrmWin_KeyDown;
            InitializeComponent();
            ProcCard = new ProcCard();
        }

        public ProcCard ProcCard { get; }

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
        
        private void BindDataDgvProcCardNo()
        {
            var sql =
                $"SELECT distinct zling '工艺卡号',cjriqi '创建日期',cjren '创建人' FROM udstr WHERE sgdhao='{txtWONo2.Text.Trim()}'";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dgvProcCardNo.DataSource = ds.Tables[0];
            dgvProcCardNo.Columns[0].Width = 200;
            dgvProcCardNo.Columns[1].Width = 200;
            dgvProcCardNo.Sort(dgvProcCardNo.Columns[0], ListSortDirection.Ascending);
        }

        private void InitDataGridView2()
        {
            var sql = "SELECT sgdhao '工单号', ddhao '生产单号',kehu '客户',jhqi '订单交期'," +
                      "tuhao '产品图号',name '产品名称',yema '页码',cailiao '材料',sulia '订单数量'," +
                      $"jiesou '接收时间' FROM udone WHERE sgdhao='{txtWONo.Text.Trim()}'";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns[0].Width = 80;
            dataGridView2.Columns[1].Width = 80;
            dataGridView2.Columns[2].Width = 80;
            dataGridView2.Columns[3].Width = 80;
            dataGridView2.Columns[4].Width = 90;
            dataGridView2.Columns[5].Width = 80;
            dataGridView2.Columns[6].Width = 60;
            dataGridView2.Columns[7].Width = 80;
            dataGridView2.Columns[8].Width = 80;
            dataGridView2.Sort(dataGridView2.Columns[1], ListSortDirection.Ascending);
        }
        private void InitDataGridView1()
        {
            //AND b.yema = a.yema
            var sql = "SELECT xuhaoone '工序号',xuhaoname '工序名称',xuhaotwo '加工工序',zuone '调机时间',zutwo '单件时间'," +
                      "xuj1 '序价',xuj2 '公式',xuj3 '补助' FROM udtwo a LEFT JOIN udone b ON b.tuhao=a.tuhao " +
                      $"AND b.DeptId = a.DeptId WHERE b.sgdhao='{txtWONo.Text.Trim()}' AND a.DeptId ='{SqlHelper.DeptId}' ORDER BY xuhaoone,xuhaotwo";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
        }
        private void InitDataGridView3()
        {
            var sql = "SELECT  zling '工艺卡号',sgdhao '工单号',ddhao '生产单号',kehu '客户'," +
                      "jhqi '订单交期',tuhao '图号',gxone '序号',gxname '工序名称',gxtwo '加工工序',tiao '调机时间'," +
                      $"danjian '单件时间' FROM udstr WHERE zling='{txtProcCardNo2.Text.Trim()}' ORDER BY gxone, gxtwo";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView3.DataSource = ds.Tables[0];
            dataGridView3.Columns[0].Width = 100;
            dataGridView3.Columns[1].Width = 100;
            dataGridView3.Columns[2].Width = 100;
            dataGridView3.Columns[3].Width = 100;
            dataGridView3.Columns[4].Width = 100;
            dataGridView3.Columns[5].Width = 100;
            dataGridView3.Columns[6].Width = 100;
            dataGridView3.Columns[7].Width = 100;
            dataGridView3.Columns[8].Width = 100;
            dataGridView3.Columns[9].Width = 100;
            dataGridView3.Columns[10].Width = 100;
            dataGridView3.Sort(dataGridView3.Columns[1], ListSortDirection.Ascending);
        }

        private void btnCancel_Click(object sender, EventArgs e)//取消
        {
            txtWONo.Text = "";
            txtPrtDwgNo.Text = "";
            txtProcCardNo.Text = "";
            txtWONo.Focus();
            txtWONo.ReadOnly = true;
            txtPrtDwgNo.ReadOnly = true;
            btnAdd.Text = Resources.A新增;
            InitDataGridView2();
            InitDataGridView1();
        }

        private void btnAdd_Click(object sender, EventArgs e)//保存
        {
            if (btnAdd.Text == Resources.A新增)
            {
                txtWONo.Text = "";
                txtPrtDwgNo.Text = "";
                txtProcCardNo.Text = "";
                txtWONo.Focus();
                btnAdd.Text = Resources.A保存;
                txtWONo.ReadOnly = false;
                InitDataGridView2();
                InitDataGridView1();
                txtProcCardNo.Text = ProcCard.GetProcCardNo();
            }
            else
            {
                var sql = $"SELECT TOP 1 * FROM udone WHERE sgdhao='{txtWONo.Text.Trim()}'";
                var dataReader = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    var pONo = dataReader["ddhao"].ToString().Trim();
                    var cust = dataReader["kehu"].ToString().Trim();
                    var planDate = dataReader["jhwxri"].ToString().Trim();
                    var prtDwgNo = dataReader["tuhao"].ToString().Trim();
                    var prtName = dataReader["name"].ToString().Trim();
                    var pageNo = dataReader["yema"].ToString().Trim();
                    var meatrial = dataReader["cailiao"].ToString().Trim();
                    var qty = dataReader["sulia"].ToString().Trim();
                    dataReader.Close();

                    if (dataGridView1.RowCount == 0) { return; }
                    var conn = SqlHelper.GetConnection();
                    conn.Open();
                    var tran = conn.BeginTransaction();
                    using (tran)
                    {
                        try
                        {
                            txtProcCardNo.Text = ProcCard.GetProcCardNo();

                            //using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                            //{
                            //    using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(txtProcCardNo.Text, (QRCodeGenerator.ECCLevel)_appConfig.AppConfigGet("eccLevel").ToInt32()))
                            //    {
                            //        using (QRCode qrCode = new QRCode(qrCodeData))
                            //        {
                            //            var img= qrCode.GetGraphic(20, Color.Black, Color.White,new Bitmap("/ICO/dz.png"), 18);
                            //        }
                            //    }
                            //}

                            for (var i = 0; i < dataGridView1.RowCount - 1; i++)
                            {
                                var craftSeq = dataGridView1.Rows[i].Cells["工序号"].Value.ToString();
                                var craft = dataGridView1.Rows[i].Cells["工序名称"].Value.ToString();
                                var processCardSeq = dataGridView1.Rows[i].Cells["加工工序"].Value.ToString();
                                var debugTime = decimal.Parse(dataGridView1.Rows[i].Cells["调机时间"].Value + "");
                                var singleProcTime = decimal.Parse(dataGridView1.Rows[i].Cells["单件时间"].Value + "");
                                var processUPrice = decimal.Parse(dataGridView1.Rows[i].Cells["序价"].Value + "");
                                var formula = int.Parse(dataGridView1.Rows[i].Cells["公式"].Value + "");
                                var subsidy = decimal.Parse(dataGridView1.Rows[i].Cells["补助"].Value + "");
                                //string strId4 = dgvExcelView.Rows[i].Cells["件资合计"].Value.ToString();
                                //string strId5 = dgvExcelView.Rows[i].Cells["id5"].Value.ToString();
                                if (craftSeq == "" || processCardSeq == "")
                                    continue;
                                var strSql =
                                    "insert into udstr(zling,sgdhao,ddhao,kehu,jhqi,tuhao,name,yema,suliang,cailiao,gxone," +
                                    $"gxname,gxtwo,tiao,danjian,xuj,gongsi,buzu,cjriqi,cjren) values('{txtProcCardNo.Text.Trim()}','{txtWONo.Text.Trim()}','{pONo}','{cust}','{planDate}','{prtDwgNo}','{prtName}'," +
                                    $"'{pageNo}','{qty}','{meatrial}','{craftSeq}','{craft}','{processCardSeq}','{debugTime}','{singleProcTime}','{processUPrice}','{formula}','{subsidy}','{DateTime.Now}','{SqlHelper.UserName}')";

                                SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql);
                                strSql =
                                    "insert into udktr(zling,sgdhao,ddhao,kehu,jhqi,tuhao,name,yema,suliang,cailiao,gxone," +
                                    $"gxname,gxtwo,tiao,danjian,xuj,gongsi,buzu,cjriqi,cjren) values('{txtProcCardNo.Text.Trim()}','{txtWONo.Text.Trim()}','{pONo}','{cust}','{planDate}','{prtDwgNo}','{prtName}'," +
                                    $"'{pageNo}','{qty}','{meatrial}','{craftSeq}','{craft}','{processCardSeq}','{debugTime}','{singleProcTime}','{processUPrice}','{formula}','{subsidy}','{DateTime.Now}','{SqlHelper.UserName}')";
                                SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql);
                            }
                            tran.Commit();
                            MessageBox.Show(@"保存成功", Resources.T提示);
                            SqlHelper.InstructionNo = txtProcCardNo.Text.Trim();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            MessageBox.Show(ex.Message);
                        }
                    }
                    var sqlkgy = $"UPDATE udone set beione='1' WHERE sgdhao='{txtWONo.Text.Trim()}'";
                    SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sqlkgy);
                }
                dataReader.Close();
                btnAdd.Text = Resources.A新增;
                txtWONo.Focus();
                txtWONo.ReadOnly = true;
                InitDataGridView2();
                InitDataGridView1();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (var i = 0; i < dataGridView1.Rows.Count; i++)
            {
                var j = i + 1;
                dataGridView1.Rows[i].HeaderCell.Value = j.ToString();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var aboutForm = new FrmPrint();
            aboutForm.ShowDialog();
        }

        private void btnPrint2_Click(object sender, EventArgs e)
        {
            if (txtProcCardNo2.Text.Length < 7)
            {
                MessageBox.Show("单号长度不能小于7", Resources.T提示);
                txtProcCardNo2.Focus();
            }
            else
            {
                var gl = SqlHelper.InstructionNo;
                var lg = gl.ToString();
                if (lg == "")
                {
                    MessageBox.Show("单号不能为空", Resources.T提示);
                    txtProcCardNo2.Focus();
                }
                else
                {
                    var aboutForm = new FrmPrint();
                    aboutForm.ShowDialog();
                }
            }
        }

        private void txtWONo2_TextChanged(object sender, EventArgs e)
        {
            if (txtWONo2.Text.Length == 7)
            {
                var sql = $"SELECT TOP 1 * FROM udstr WHERE sgdhao='{txtWONo2.Text.Trim()}'";
                var adsu = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (adsu.HasRows)
                {
                    BindDataDgvProcCardNo();
                }
                else
                {
                    MessageBox.Show($"此工单号{txtWONo2.Text.Trim()}未生成过工艺卡!!", Resources.T提示);
                    txtWONo2.Text = "";
                    BindDataDgvProcCardNo();
                    txtWONo2.Focus();
                }
                adsu.Close();
            }
        }

        private void txtProcCardNo2_TextChanged(object sender, EventArgs e)
        {
            if (txtProcCardNo2.Text.Length == 7)
            {
                var sql = $"SELECT TOP 1 * FROM udstr WHERE zling='{txtProcCardNo2.Text.Trim()}'";
                var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (dr.HasRows)
                {
                    dr.Close();
                    InitDataGridView3();
                    SqlHelper.InstructionNo = txtProcCardNo2.Text.Trim();
                }
                else
                {
                    MessageBox.Show($"此工艺卡号{txtProcCardNo2.Text.Trim()}不存在，请重新输入!!", Resources.T提示);
                    txtProcCardNo2.Text = "";
                    dr.Close();
                    InitDataGridView3();
                    txtProcCardNo2.Focus();
                }
                dr.Close();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtProcCardNo2.Text.Length == 7)
            {
                var sql = $"SELECT TOP 1 * FROM udstr WHERE zling='{txtProcCardNo2.Text.Trim()}'";
                var adksu = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (adksu.HasRows)
                {
                    adksu.Close();
                    InitDataGridView3();
                    if (MessageBox.Show(@"是否要删除该工艺卡号", Resources.J警告, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        sql = $"delete FROM udstr WHERE zling='{txtProcCardNo2.Text.Trim()}'";
                        SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                        var ksql = $"delete FROM udktr WHERE zling='{txtProcCardNo2.Text.Trim()}'";
                        SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, ksql);
                        txtProcCardNo2.Text = "";
                        InitDataGridView3();
                    }
                }
                else
                {
                    MessageBox.Show($"此工艺卡号{txtProcCardNo2.Text.Trim()}不存在，请重新输入!!", Resources.T提示);
                    txtProcCardNo2.Text = "";
                    txtProcCardNo2.Focus();
                }
                adksu.Close();
            }
            else
            {
                MessageBox.Show(@"请输入完整的工艺卡号！", Resources.T提示);
                txtProcCardNo2.Focus();
            }
        }
        
        private void dgvProcCardNo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProcCardNo2.Text = dgvProcCardNo.CurrentCell.Value.ToString();
            txtProcCardNo2_TextChanged(sender, e);
        }

        private void txtWONo_TextChanged(object sender, EventArgs e)
        {
            //if (!ValidateUtil.IsNumber(txtWONo.Text))
            //{
            //    MessageBox.Show(Resources.IsNotNumber);
            //    return;
            //}
            if (txtWONo.Text.Length != 7) return;
            var sql = $"SELECT TOP 1 * FROM udone WHERE sgdhao='{txtWONo.Text.Trim()}' AND DeptId='{SqlHelper.DeptId}'";
            var adsu = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            if (adsu.HasRows)
            {
                adsu.Read();
                txtPrtDwgNo.Text = adsu["tuhao"].ToString().Trim();
                if (!string.IsNullOrEmpty(adsu["beione"].ToString().Trim()))
                {
                    adsu.Close();
                    if (MessageBox.Show($"此工单号{txtWONo.Text.Trim()}已创建过工艺卡，请确认是否创建!!", Resources.T提示, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        txtWONo.ReadOnly = true;
                        InitDataGridView2();
                        InitDataGridView1();
                    }
                    else
                    {
                        txtWONo.Text = "";
                        txtPrtDwgNo.Text = "";
                        txtProcCardNo.Text = "";
                        txtWONo.Focus();
                        txtWONo.ReadOnly = true;
                        btnAdd.Text = Resources.A新增;
                        InitDataGridView2();
                        InitDataGridView1();
                    }
                }
                else
                {
                    adsu.Close();
                    txtPrtDwgNo.ReadOnly = true;
                    InitDataGridView2();
                    InitDataGridView1();
                }
            }
            else
            {
                MessageBox.Show($"工单{txtWONo.Text.Trim()}未接收，请重新输入或先接收工单!!", Resources.T提示);
                txtWONo.Focus();
                adsu.Close();
            }
        }
    }
}
