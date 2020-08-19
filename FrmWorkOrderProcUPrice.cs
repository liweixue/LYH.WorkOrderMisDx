using System;
using System.Data;
using System.Windows.Forms;
using LYH.WorkOrder.share;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmWorkOrderProcUPrice : Form
    {
        public FrmWorkOrderProcUPrice()
        {
            KeyDown += FrmWin_KeyDown;
            InitializeComponent();
        }
        private string _mMidtwo;
        //delegate void MyDele(string bt, DataGridView dataGridView1, int dy, string filename);
        private void Form3_Load(object sender, EventArgs e)
        {
            BindData();
            if (SqlHelper.UserType != Properties.Resources.UT_Check && SqlHelper.UserType != Properties.Resources.UT_Admin)
            {
                btnAdd.Enabled = false;
                btnUpd.Enabled = false;
                btnDel.Enabled = false;
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
            }
        }

        private void BindData()
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = true;

            const string sql = "SELECT xujhao '序价号',xujia '序价',buzu '补助',gongsi '公式',"+
                "shiyong '适用班组',beizu '序价说明',cjren '创建',cjriqi '创建日期',qgren '修改',qgriqi '修改日期' FROM xujia ";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 55;
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].Width = 222;
            dataGridView1.Columns[6].Width = 55;
            dataGridView1.Columns[7].Width = 80;
            dataGridView1.Columns[8].Width = 55;
            dataGridView1.Columns[9].Width = 80;
            dataGridView1.Sort(dataGridView1.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel3.Visible = false;
            txtAddPriceNo.Focus();
        }

        private void btnUpd_Click(object sender, EventArgs e)//修改
        {
            _mMidtwo = dataGridView1.SelectedCells[0].Value.ToString().Trim();
            //MMid = Convert.ToInt32(dataGridView1.SelectedCells[0].Value);
            panel3.Visible = false;
            panel2.Visible = true;
            var sql = $"SELECT * FROM xujia WHERE xujhao='{_mMidtwo}'";
            var sdrtwo = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            sdrtwo.Read();
            txtUPriceNo.Text = sdrtwo["xujhao"].ToString().Trim();
            txtUPrice.Text = sdrtwo["xujia"].ToString().Trim();
            txtUTeam.Text = sdrtwo["shiyong"].ToString().Trim();
            txtUFormulaType.Text = sdrtwo["gongsi"].ToString().Trim();
            txtUSubsidy.Text = sdrtwo["buzu"].ToString().Trim();
            rtxUPriceDesc.Text = sdrtwo["beizu"].ToString().Trim();
        }

        private void btnDel_Click(object sender, EventArgs e)//删除
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("提示：请选择要删除的用户信息！", "警告");
            }
            else
            {
                var mMidtwo = dataGridView1.SelectedCells[0].Value.ToString().Trim();
                if (MessageBox.Show($"是否要删除< {mMidtwo} >序价号", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    var sql = $"delete FROM xujia WHERE xujhao='{mMidtwo}'";
                    SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                }
                BindData();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)//退出
        {
            Close();
        }

        private void btnExport_Click(object sender, EventArgs e)//导出
        {

            var excel = new ToExcel();
            var b = excel.DataGridViewToExcel(dataGridView1);
            if (b)
            {
                MessageBox.Show("导出成功", "提示");
            }
            else
            {
                MessageBox.Show("导出失败", "提示");
            }
        }

        private void txtAddPriceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtAddPriceNo.Text != "")
                {
                    txtAddPrice.Focus();
                }
                else
                {
                    MessageBox.Show("序价代码不能为空！", "提示");
                    txtAddPriceNo.Focus();
                }
            }
        }

        private void txtAddPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtAddPrice.Text != "")
                {
                    txtAddSubsidy.Focus();
                }
                else
                {
                    MessageBox.Show("序价不能为空！", "提示");
                    txtAddPrice.Focus();
                }
            }
        }

        private void txtAddTeam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rtxAPriceDesc.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)//新建保存
        {
            if (txtAddPriceNo.Text != "")
            {
                if (txtAddPrice.Text != "" && txtAddFormulaType.Text != "")
                {
                    var sql = $"SELECT xujhao FROM xujia WHERE xujhao='{txtAddPriceNo.Text.Trim()}'";
                    var sdddr2Q = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                    if (sdddr2Q.HasRows)
                    {
                        MessageBox.Show($"此序价号< {txtAddPriceNo.Text.Trim()} >已存在，请重新输入!!", "提示");
                        txtAddPriceNo.Text = "";
                        txtAddPrice.Text = "";
                        txtAddTeam.Text = "";
                        txtAddSubsidy.Text = "";
                        txtAddFormulaType.Text = "";
                        rtxAPriceDesc.Text = "";
                    }
                    else
                    {
                        sql = "";
                        sql = "insert into xujia(xujhao,xujia,buzu,shiyong,gongsi,beizu,cjren,cjriqi) values" +
                              $"('{txtAddPriceNo.Text.Trim().ToUpper()}','{txtAddPrice.Text.Trim()}','{txtAddSubsidy.Text.Trim()}','{txtAddTeam.Text.Trim()}','{txtAddFormulaType.Text.Trim()}','{rtxAPriceDesc.Text.Trim()}','{SqlHelper.UserName}','{DateTime.Now}')";
                        SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                        txtAddPriceNo.Text = "";
                        txtAddPrice.Text = "";
                        txtAddTeam.Text = "";
                        txtAddSubsidy.Text = "";
                        txtAddFormulaType.Text = "";
                        rtxAPriceDesc.Text = "";
                        BindData();

                    }
                }
                else
                {
                    MessageBox.Show("序价与公式不能为空！", "提示");
                    txtAddPrice.Focus();
                }
            }
            else
            {
                MessageBox.Show("序价号不能为空！", "提示");
                txtAddPriceNo.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtAddPriceNo.Text = "";
            txtAddPrice.Text = "";
            txtAddTeam.Text = "";
            txtAddSubsidy.Text = "";
            txtAddFormulaType.Text = "";
            rtxAPriceDesc.Text = "";
            panel3.Visible = true;
            BindData();
        }

        private void btnUpdCancel_Click(object sender, EventArgs e)
        {
            txtUTeam.Text = "";
            txtUPrice.Text = "";
            txtUPriceNo.Text = "";
            txtUSubsidy.Text = "";
            txtUFormulaType.Text = "";
            rtxUPriceDesc.Text = "";
            panel3.Visible = true;
            BindData();
        }

        private void btnUpdSave_Click(object sender, EventArgs e)//修改保存
        {
            if (txtUPriceNo.Text != "" && txtUFormulaType.Text != "")
            {
                var sql =
                    $"UPDATE xujia set xujia='{txtUPrice.Text.Trim()}',gongsi='{txtUFormulaType.Text.Trim()}',buzu='{txtUSubsidy.Text.Trim()}',shiyong='{txtUTeam.Text.Trim()}',beizu='{rtxUPriceDesc.Text.Trim()}'," +
                    $"qgren='{SqlHelper.UserName}',qgriqi='{DateTime.Now}' WHERE xujhao='{_mMidtwo}'";
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                txtUTeam.Text = "";
                txtUPrice.Text = "";
                txtUPriceNo.Text = "";
                txtUSubsidy.Text = "";
                txtUFormulaType.Text = "";
                rtxUPriceDesc.Text = "";
                BindData();
            }
            else
            {
                MessageBox.Show("序价与公式不能为空！", "提示");
                txtUPrice.Focus();
            }
        }

        private void txtAddPrice_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(txtAddPrice.Text) && !String.IsNullOrEmpty(txtAddPrice.Text))
            {
                MessageBox.Show("输入数据类型错误，请重新输入！", "提示");
                txtAddPrice.Text = "";
            }
        }

        private void txtUPrice_TextChanged(object sender, EventArgs e)
        {
            if (!StringHelper.IsNumeric(txtUPrice.Text) && !String.IsNullOrEmpty(txtUPrice.Text))
            {
                MessageBox.Show("输入数据类型错误，请重新输入！", "提示");
                txtUPrice.Text = "";
            }
        }

        private void txtAddSubsidy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddFormulaType.Focus();
            }
        }

        private void txtUSubsidy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUFormulaType.Focus();
            }
        }

        private void richTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnUpdSave.Focus();
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private void txtAddFormulaType_TextChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < txtAddFormulaType.Text.Length; i++)
            {
                if (txtAddFormulaType.Text[0] < '1' && txtAddFormulaType.Text[0] > '3')
                {
                    MessageBox.Show("只允许输入数字1/2/3，请重新输入！", "提示");
                    txtAddFormulaType.Text = "";
                }
                if (txtAddFormulaType.Text.Length == 2)
                {
                    MessageBox.Show("只允许输入数字1/2/3且只能输入1位，请重新输入！", "提示");
                    txtAddFormulaType.Text = "";
                }
            }
        }

        private void txtUFormulaType_TextChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < txtUFormulaType.Text.Length; i++)
            {
                if (txtUFormulaType.Text[0] < '1' && txtUFormulaType.Text[0] > '3')
                {
                    MessageBox.Show("只允许输入数字1/2/3，请重新输入！", "提示");
                    txtUFormulaType.Text = "";
                }
                if (txtUFormulaType.Text.Length == 2)
                {
                    MessageBox.Show("只允许输入数字1/2/3且只能输入1位，请重新输入！", "提示");
                    txtUFormulaType.Text = "";
                }
            }
        }

        private void txtAddFormulaType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddTeam.Focus();
            }
        }

        private void txtUFormulaType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUTeam.Focus();
            }

        }
    }
}
