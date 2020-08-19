using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using LYH.Framework.BaseUI;
using LYH.Framework.Commons;
using LYH.WorkOrder.Properties;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmConstructionRpt : Form
    {
        public FrmConstructionRpt()
        {
            InitializeComponent();
            txtWONo.Focus();
            KeyDown += FrmWin_KeyDown;
        }

        /// <summary>
        ///     获取所有客户数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<string> GetSpellBoxSource(DataTable dt)
        {
            return
                (from DataRow dr in dt.Rows where !Convert.IsDBNull(dr["cust"]) select dr["cust"].ToString().Trim())
                    .ToList();
        }

        private void FrmConstructionRpt_Load(object sender, EventArgs e)
        {
            var sql =
                "SELECT c.name 字段,p.value 说明 FROM sys.syscolumns c JOIN sys.systypes t ON c.xusertype=t.xusertype JOIN sys.sysobjects o ON c.id=o.id LEFT JOIN sys.extended_properties p ON c.id=p.major_id AND c.colid=p.minor_id WHERE o.name='tf_sgdantwo' AND t.name LIKE '%date%'";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            comboBox2.DataSource = ds.Tables[0];
            comboBox2.DisplayMember = "说明";
            comboBox2.ValueMember = "字段";
            comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            if (SqlHelper.UserType == Resources.UT_Input || SqlHelper.UserType == Resources.UT_Admin)
            {
                checkBox4.Visible = true;
                checkBox5.Visible = true;
            }
            sql = "SELECT distinct cust FROM dzdj.dbo.customer";
            ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            txtCust.SpellSearchSource = GetSpellBoxSource(ds.Tables[0]).ToArray();
            var acsc = new AutoCompleteStringCollection();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                acsc.Add(dr["cust"].ToString());
            }
            txtCust.AutoCompleteCustomSource = acsc;
        }

        /// <summary>
        ///     获取按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        ///     根据条件查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e) //查询
        {
            CtrlUtil.ClearDgv(dataGridView1);
            
            var wheres = new List<string>();
            string d;
            if (!string.IsNullOrEmpty(comboBox2.Text.Trim()))
            {
                d = comboBox2.SelectedValue.ToString();
            }
            else
            {
                MessageBox.Show(@"请选择一个日期选项!");
                comboBox2.DroppedDown = true;
                return;
            }
            if (!string.IsNullOrEmpty(dateTimePicker1.Text))
            {
                wheres.Add($" {d}>='{dateTimePicker1.Text}' ");
            }
            if (!string.IsNullOrEmpty(dateTimePicker2.Text))
            {
                var dtp = DateTime.Parse(dateTimePicker2.Text).AddDays(1);
                wheres.Add($" {d}<'{dtp}' ");
            }
            if (txtWONo.Text.Trim().Length > 0)
            {
                wheres.Add($" a.shigongdanhao like '%{txtWONo.Text.Trim()}%'");
            }
            if (txtMONo.Text.Trim().Length > 0)
            {
                wheres.Add($" b.dingdanhao like '%{txtMONo.Text.Trim()}%'");
            }
            if (txtPartNo.Text.Trim().Length > 0)
            {
                wheres.Add($" b.chanpintuhao like '%{txtPartNo.Text.Trim()}%'");
            }
            if (txtTeam.Text.Trim().Length > 0)
            {
                wheres.Add($" a.shengchanyuan like '%{txtTeam.Text.Trim()}%'");
            }
            if (txtCust.Text.Trim().Length > 0)
            {
                wheres.Add($" b.kehu like '%{txtCust.Text.Trim()}%'");
            }
            if (txtPage.Text.Trim().Length > 0)
            {
                wheres.Add($" b.tuzhiyema like '%{txtPage.Text.Trim()}%'");
            }
            if (checkBox3.Checked)
            {
                wheres.Add(" a.xujah like '%C15%'");
            }
            if (checkBox2.Checked)
            {
                wheres.Add(" a.xujah not like '%C15%'");
            }
            if (SqlHelper.UserType == Resources.UT_Input || SqlHelper.UserType == Resources.UT_Admin)
            {
                if (checkBox4.Checked)
                {
                    wheres.Add(" a.xujah like '%W11.1%'");
                }
                else if (checkBox5.Checked)
                {
                    wheres.Add(" a.xujah not like '%W11.1%'");
                }
            }

            if (comboBox1.Text.Trim().Length > 0)
            {
                if (comboBox1.Text.Trim() == Resources.UT_Audit)
                {
                    wheres.Add(" a.suhu ='审核'");
                }
                else if (comboBox1.Text.Trim() == "未审核")
                {
                    wheres.Add(" a.suhu is null or a.suhu =''");
                }
            }
            //string sql = "SELECT shigongdanhao as  '工单号',ddhao as '生产单号',kehu as '客户',jhqi as '订单交期',"+
            //    "chanpintuhao as '产品图号', xuhao as '工序号',gongxumingcheng as '工序名称',shengchanyuan as '生产班组',"+
            //    "wanshengsuliang as'完成数量',jgsu as '加工数',xujah as'序价号',xujia as '序价',buzu as '补助',"+
            //    "gongsi as '公式类型',hezhi as '件资合计',lururiqi as '录入日期',luruyan as '录入人',"+
            //    "qiugairiqi as '修改日期',qgren as '修改人',suhu as '审核状态',suren as '审核人' FROM tf_sgdantwo ";
            var sql =
                "SELECT b.shigongdanhao '工单号',b.dingdanhao '生产单号',b.jiaohuoqi '订单交期',b.kehu '客户',b.tuzhiyema '页码',b.chanpintuhao '产品图号'," +
                "a.xuhao '工序号',a.gongxumingcheng '工序名称',a.shengchanyuan '生产班组',ISNULL(a.wanshengsuliang,0) '完成数量',a.jgsu '加工数'," +
                "RTRIM(a.xujah) '序价号',a.xujia '序价',a.buzu '补助',a.gongsi '公式类型',ISNULL(a.hezhi,0) '件资合计',a.lururiqi '录入日期'," +
                "RTRIM(a.luruyan) '录入人',a.qiugairiqi '修改日期',RTRIM(a.qgren) '修改人',RTRIM(a.suhu) '审核状态',RTRIM(a.suren) '审核人'," +
                "(CASE a.Checked WHEN 1 THEN '已核对' ELSE '' END) '核对状态',a.Checker '核对人',a.Chkdate '核对日期' " +
                "FROM dbo.tf_sgdantwo a LEFT JOIN dbo.mf_sgdan b ON a.shigongdanhao=b.shigongdanhao ";
            //判断用户是否选择了条件
            if (wheres.Count > 0)
            {
                var wh = string.Join(" AND ", wheres.ToArray());
                sql = $"{sql} WHERE {wh} ORDER BY a.shigongdanhao,xuhao";
            }
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }

        /// <summary>
        ///     导出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e) //导出
        {
            //var excel = new ToExcel();
            //var b = excel.DataGridViewToExcel(dataGridView1);
            //if (b)
            //{
            //    MessageBox.Show("导出成功", "提示");
            //}
            var file =
                FileDialogHelper.SaveExcel(
                    $"{dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "-" + dateTimePicker2.Value.Date.ToString("yyyyMMdd")}.xlsx");
            if (string.IsNullOrEmpty(file)) return;
            //var dt = (dataGridView1.DataSource as DataTable);
            var dt = DataTableHelper.GetDgvToTable(dataGridView1);
            try
            {
                string error;
                AsposeExcelTools.DataTableToExcel2(dt, file, out error);
                if (!string.IsNullOrEmpty(error))
                {
                    MessageDxUtil.ShowError($"导出Excel出现错误：{error}");
                }
                else
                {
                    if (MessageDxUtil.ShowYesNoAndTips("导出成功，是否打开文件？") == DialogResult.Yes)
                    {
                        Process.Start(file);
                    }
                }
            }
            catch (Exception ex)
            {
                LogTextHelper.Error(ex);
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        ///     工单号变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWONo_TextChanged(object sender, EventArgs e)
        {
            if (txtWONo.Text.Length != 7) return;
            var sql =
                "SELECT b.shigongdanhao '工单号',b.dingdanhao '生产单号',b.jiaohuoqi '订单交期',b.kehu '客户',b.tuzhiyema '页码',b.chanpintuhao '产品图号'," +
                "a.xuhao '工序号',a.gongxumingcheng '工序名称',a.shengchanyuan '生产班组',ISNULL(a.wanshengsuliang,0) '完成数量',a.jgsu '加工数'," +
                "RTRIM(a.xujah) '序价号',a.xujia '序价',a.buzu '补助',a.gongsi '公式类型',ISNULL(a.hezhi,0) '件资合计',a.lururiqi '录入日期'," +
                "RTRIM(a.luruyan) '录入人',a.qiugairiqi '修改日期',RTRIM(a.qgren) '修改人',RTRIM(a.suhu) '审核状态',RTRIM(a.suren) '审核人'," +
                "(CASE a.Checked WHEN 1 THEN '已核对' ELSE '' END) '核对状态',a.Checker '核对人',a.Chkdate '核对日期' " +
                $"FROM dbo.tf_sgdantwo a LEFT JOIN dbo.mf_sgdan b ON a.shigongdanhao=b.shigongdanhao WHERE a.shigongdanhao='{txtWONo.Text.Trim()}'";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnSting(), CommandType.Text, sql);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            //OnRowDataBound = "gvWOContent_RowDataBound";
        }

        /// <summary>
        ///     数据绑定完成后统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (txtWONo.Text == "")
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows[0].Selected = false;
                }

                //在首列自动排序号
                for (var i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    var j = i + 1;
                    dataGridView1.Rows[i].HeaderCell.Value = j.ToString();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].HeaderCell.Value = j.ToString("合计"); //最后一行显示合计
                }

                //底部合计
                Double he1 = 0;
                Double he3 = 0;
                var list = new List<string>();
                for (var ih = 0; ih < dataGridView1.Rows.Count - 1; ih++)
                {
                    he1 += Convert.ToDouble(dataGridView1.Rows[ih].Cells["完成数量"].Value);
                    he3 += Convert.ToDouble(dataGridView1.Rows[ih].Cells["件资合计"].Value);
                    //he4 += Convert.ToDouble(this.dataGridView1.Rows[ih].Cells["工单号"].Value);
                    var deg = Convert.ToString(dataGridView1.Rows[ih].Cells["工单号"].Value).ToUpper();
                    if (!list.Contains(deg))
                    {
                        list.Add(deg);
                    }
                }
                var dgr = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
                double he4 = list.Count;
                dgr.Cells["完成数量"].Value = he1;
                dgr.Cells["件资合计"].Value = he3.ToString("f6");
                dgr.Cells["工单号"].Value = he4;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox5.Checked = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                checkBox4.Checked = false;
            }
        }
    }
}