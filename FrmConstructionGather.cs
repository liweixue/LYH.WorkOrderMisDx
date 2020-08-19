using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using LYH.Framework.Commons;
using LYH.WorkOrder.Properties;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmConstructionGather : Form
    {
        private DataSet _ds;

        public FrmConstructionGather()
        {
            InitializeComponent();
            KeyDown += FrmWin_KeyDown;
            dtBegin.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
        }

        private void FrmConstructionGather_Load(object sender, EventArgs e)
        {
            const string sql = "SELECT distinct cust FROM dzdj.dbo.customer";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            ssbCust.SpellSearchSource = GetSpellBoxSource(ds.Tables[0]).ToArray();
            var acsc = new AutoCompleteStringCollection();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                acsc.Add(dr["cust"].ToString());
            }
            ssbCust.AutoCompleteCustomSource = acsc;
        }

        /// <summary>
        ///     从DataTable中取数据源
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<string> GetSpellBoxSource(DataTable dt)
        {
            return
                (from DataRow dr in dt.Rows where !Convert.IsDBNull(dr["cust"]) select dr["cust"].ToString().Trim())
                    .ToList();
        }

        private void FrmWin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode){
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
        ///     导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            var excel = new ToExcel();
            if (excel.DataGridViewToExcel(dataGridView1))
            {
                MessageBox.Show(@"导出成功", Resources.T提示);
            }
        }

        /// <summary>
        ///     查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            CtrlUtil.ClearDgv(dataGridView1);
            var wheres = new List<string>();
            if (!string.IsNullOrEmpty(dtBegin.Text))
            {
                var time1 = Convert.ToDateTime(dtBegin.Value.Date.ToString("yyyy-MM-dd"));
                wheres.Add($" luodanriqi>='{time1}' ");
            }
            if (!string.IsNullOrEmpty(dtEnd.Text))
            {
                var dt = DateTime.Parse(dtEnd.Text).AddDays(1);
                wheres.Add($" luodanriqi<'{dt}' ");
            }
            if (txtWONo.Text.Trim().Length > 0)
            {
                wheres.Add($" shigongdanhao='{txtWONo.Text.Trim()}'");
            }
            if (txtOrderNo.Text.Trim().Length > 0)
            {
                wheres.Add($" dingdanhao like '%{txtOrderNo.Text.Trim()}%'");
            }
            if (ssbCust.Text.Trim().Length > 0)
            {
                wheres.Add($" kehu like '%{ssbCust.Text.Trim()}%'");
            }
            if (txtPartNo.Text.Trim().Length > 0)
            {
                wheres.Add($" chanpintuhao like '%{txtPartNo.Text.Trim()}%'");
            }
            if (txtPartName.Text.Trim().Length > 0)
            {
                wheres.Add($" chanpinmingcheng like '%{txtPartName.Text.Trim()}%'");
            }
            if (txtPageNo.Text.Trim().Length > 0)
            {
                wheres.Add($" tuzhiyema like '%{txtPageNo.Text.Trim()}%'");
            }

            var sql =
                "SELECT shigongdanhao 工单号,dingdanhao 生产单号,kehu 客户,jiaohuoqi 交货期,tuzhiyema 页码,DXFbianhao DXF," +
                "chanpintuhao 零件图号,chanpinmingcheng 零件名称,cailiao 材料,houdu 厚,changdu 长,kuandu 宽,dingdansuliang 下单数量," +
                "dantaoyongliang 单套用量,beizu 备注,gongyiliusheng 工艺流程,luodanriqi 下单日期,luodanyuan 录入人," +
                "xiugairiqi 修改日期,xiugairen 修改人,CASE WHEN cls_id='T' THEN '停用' ELSE '' END 是否停用,suhu 审核状态 FROM mf_sgdan ";
            //判断用户是否选择了条件
            if (wheres.Count > 0)
            {
                var where = string.Join(" AND ", wheres.ToArray());
                sql = $"{sql} WHERE {@where} ORDER BY luodanriqi,dingdanhao,shigongdanhao";
            }
            _ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView1.DataSource = _ds.Tables[0];
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }

        /// <summary>
        ///     自动排列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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
            }
        }

        /// <summary>
        ///     工单单号变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWONo_TextChanged(object sender, EventArgs e)
        {
            if (txtWONo.Text.Length != 7) return;
            var sql =
                "SELECT shigongdanhao 工单号,dingdanhao 生产单号,kehu 客户,jiaohuoqi 交货期,tuzhiyema 页码,DXFbianhao DXF," +
                "chanpintuhao 零件图号,chanpinmingcheng 零件名称,cailiao 材料,houdu 厚,changdu 长,kuandu 宽,dingdansuliang 下单数量," +
                "dantaoyongliang 单套用量,beizu 备注,gongyiliusheng 工艺流程,luodanriqi 下单日期,luodanyuan 录入人," +
                "xiugairiqi 修改日期,xiugairen 修改人,CASE WHEN cls_id='T' THEN '停用' ELSE '' END 是否停用,suhu 审核状态 FROM " +
                $"mf_sgdan WHERE shigongdanhao='{txtWONo.Text.Trim()}'";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnSting(), CommandType.Text, sql);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }
    }
}