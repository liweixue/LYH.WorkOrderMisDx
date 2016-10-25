using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using LYH.Framework.Commons;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmDataImport : Form
    {
        public FrmDataImport()
        {
            InitializeComponent();
            Load += Form9_Load;
        }
        private void Form9_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog() { Filter = "Excel文件(2007及以上版本)|*.xlsx|Excel文件(2003)|*.xls",
                RestoreDirectory = false, Multiselect = false };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SFilePath = ofd.FileName;
                textBox1.Text = SFilePath;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请先选择文件", "提示");
            }
            else
            {
                var thread = new Thread(ReadExcelToDgvFuc) { IsBackground = true };
                thread.Start();
            }
        }

        private void txbFilePath_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != "")
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        #region 自定义变量区
        public static string SFilePath = "";
        #endregion

        #region 自定义方法区
        private void ReadExcelToDgvFuc()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(ReadExcelToDgv));
            }
            else
            {
                ReadExcelToDgv(this, null);
            }
        }

        private void ReadExcelToDgv(object sender, EventArgs e)
        {
            var sErrorMessage = "";
            try
            {
                var dtExcelData = DgvExcelExtend.GetExcelData(SFilePath, 1, 1, 1, 1, ref sErrorMessage);

                dgvExcelView.DataSource = dtExcelData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{sErrorMessage}\n{ex.Message}{ex.Source}{ex.StackTrace}");
            }
        }
        #endregion

        //格式化DGV控件
        private void dgvExcelView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var iRowCount = dgvExcelView.Rows.Count;
            var iColumnCount = dgvExcelView.Columns.Count;
            if (iRowCount > 0)
            {
                for (var i = 0; i < iRowCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        dgvExcelView.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Honeydew;
                    }
                    else
                    {
                        dgvExcelView.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.GreenYellow;
                    }
                }
            }

            if (iColumnCount > 0)
            {
                for (var i = 0; i < iColumnCount; i++)
                {
                    if (i != (iColumnCount - 1))
                    {
                        dgvExcelView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    else
                    {
                        dgvExcelView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            dgvExcelView.DataSource = "";
        }

        private void button3_Click(object sender, EventArgs e)//导入数据库
        {
            var strSql = "";
            if (dgvExcelView.RowCount == 0) { return; };
            var iUpdateCount = 0;
            using (var conn = SqlHelper.GetConnection())
            {
                conn.Open();

                using (var st = conn.BeginTransaction())
                {
                    try
                    {
                        for (var i = 0; i < dgvExcelView.RowCount - 1; i++)
                        {
                            var strId1 = dgvExcelView.Rows[i].Cells["工单号"].Value.ToString();
                            var strId2 = dgvExcelView.Rows[i].Cells["工序号"].Value.ToString();
                            var strId3 = decimal.Parse(dgvExcelView.Rows[i].Cells["序价"].Value + "");
                            var strId4 = decimal.Parse(dgvExcelView.Rows[i].Cells["件资合计"].Value + "");
                            //string strId4 = dgvExcelView.Rows[i].Cells["件资合计"].Value.ToString();
                            //string strId5 = dgvExcelView.Rows[i].Cells["id5"].Value.ToString();
                            if (strId1 == "" || strId2 == "")
                                continue;
                            strSql =
                                $"UPDATE tf_sgdantwo set xujia={strId3},hezhi='{strId4}' WHERE shigongdanhao='{strId1}' AND xuhao='{strId2}'";
                            iUpdateCount += SqlHelper.ExecuteNonQuery(st, CommandType.Text, strSql);
                        }
                        st.Commit();
                        MessageBox.Show($"更新成功{iUpdateCount}条记录");
                    }
                    catch (Exception ex)
                    {
                        st.Rollback();
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
