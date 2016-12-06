using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using LYH.Framework.Commons;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmCncInstructionRpt : Form
    {
        public FrmCncInstructionRpt()
        {
            KeyDown+=FrmWin_KeyDown;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            var wheres = new List<string>();

            if (!string.IsNullOrEmpty(dateTimePicker3.Text))
            {
                wheres.Add($" lururiqi>='{dateTimePicker3.Text}' ");
            }
            if (!string.IsNullOrEmpty(dateTimePicker4.Text))
            {
                var dt = new DateTime();
                dt = DateTime.Parse(dateTimePicker4.Text).AddDays(1);
                wheres.Add($" lururiqi<'{dt}' ");
            }
            if (DateTime.Compare(dateTimePicker1.Value, dateTimePicker2.Value) < 0)
            {
                wheres.Add($" cjriqi>='{dateTimePicker1.Text}' ");
                var zdt = new DateTime();
                zdt = DateTime.Parse(dateTimePicker2.Text).AddDays(1);
                wheres.Add($" cjriqi<'{zdt}' ");
            }
            if (textBox1.Text.Trim().Length > 0)
            {
                wheres.Add($" ddhao like '%{textBox1.Text.Trim()}%'");
            }
            if (textBox2.Text.Trim().Length > 0)
            {
                wheres.Add($" kehu like '%{textBox2.Text.Trim()}%'");
            }
            if (textBox5.Text.Trim().Length > 0)
            {
                wheres.Add($" tuhao like '%{textBox5.Text.Trim()}%'");
            }
            if (textBox6.Text.Trim().Length > 0)
            {
                wheres.Add($" yema like '%{textBox6.Text.Trim()}%'");
            }
            if (textBox7.Text.Trim().Length > 0)
            {
                wheres.Add($" wsbanz like '%{textBox7.Text.Trim()}%'");
            }
            var sql = "SELECT zling as '工艺卡号',sgdhao as '工单号',ddhao as '生产单号',kehu as '客户',"+
                "jhqi as '订单交期',tuhao as '产品图号', name as '产品名称',yema as '页码', cailiao as '材料',"+
                "suliang as '订单数量',gxone as '序号',gxname as '工序名称',gxtwo as '加工工序',tiao as '调机时间',"+
                "danjian as '单件加工',xuj as '序价',buzu as '补助',wssulia as '完成数量',hujii as '件资合计',"+
                "wsbanz as'加工班组',bei3 as '生产员',wsriqi as '完成日期',cjriqi as '指令创日',lururiqi as '录入日期',"+
                "lururen as '录入人',qgriqi as '修改日期',qgren as '修改人' FROM udktr";
            //判断用户是否选择了条件
            if (wheres.Count > 0)
            {
                var wh = string.Join(" AND ", wheres.ToArray());
                sql = $"{sql} WHERE {wh}ORDER BY zling, gxone,gxtwo";
            }
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 65;
            dataGridView1.Columns[1].Width = 65;
            dataGridView1.Columns[2].Width = 65;
            dataGridView1.Columns[3].Width = 65;
            dataGridView1.Columns[4].Width = 65;
            dataGridView1.Columns[5].Width = 65;
            dataGridView1.Columns[6].Width = 65;
            dataGridView1.Columns[7].Width = 65;
            dataGridView1.Columns[8].Width = 65;
            dataGridView1.Columns[9].Width = 65;
            dataGridView1.Columns[10].Width = 65;
            dataGridView1.Columns[11].Width = 65;
            dataGridView1.Columns[12].Width = 65;
            dataGridView1.Columns[13].Width = 65;
            dataGridView1.Columns[14].Width = 90;
            dataGridView1.Columns[15].Width = 90;
            dataGridView1.Columns[16].Width = 65;
            dataGridView1.Columns[17].Width = 85;
            dataGridView1.Columns[18].Width = 65;
            dataGridView1.Columns[19].Width = 65;
            dataGridView1.Columns[20].Width = 65;
            dataGridView1.Columns[21].Width = 65;
            dataGridView1.Columns[22].Width = 65;
            dataGridView1.Columns[23].Width = 65;
            dataGridView1.Columns[24].Width = 65;
            dataGridView1.Columns[25].Width = 65;
            dataGridView1.Columns[26].Width = 65;
            dataGridView1.Sort(dataGridView1.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var excel = new ToExcel();
            var b = excel.DataGridViewToExcel(dataGridView1);
            if (b)
            {
                MessageBox.Show("导出成功", "提示");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 7)
            {
                var sql = "SELECT zling as '工艺卡号',sgdhao as  '工单号',ddhao as '生产单号',kehu as '客户'," +
                          "jhqi as '订单交期',tuhao as '产品图号', name as '产品名称',yema as '页码', cailiao as '材料'," +
                          "suliang as '订单数量',gxone as '序号',gxname as '工序名称',gxtwo as '加工工序',tiao as '调机时间'," +
                          "danjian as '单件加工',xuj as '序价',buzu as '补助',wssulia as '完成数量',hujii as '件资合计', " +
                          "wsbanz as'加工班组',bei3 as '生产员',wsriqi as '完成日期',cjriqi as '指令创日',lururiqi as '录入日期'," +
                          $"lururen as '录入人',qgriqi as '修改日期',qgren as '修改人' FROM udktr WHERE zling='{textBox3.Text.Trim()}'";
                var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 65;
                dataGridView1.Columns[1].Width = 65;
                dataGridView1.Columns[2].Width = 65;
                dataGridView1.Columns[3].Width = 65;
                dataGridView1.Columns[4].Width = 65;
                dataGridView1.Columns[5].Width = 65;
                dataGridView1.Columns[6].Width = 65;
                dataGridView1.Columns[7].Width = 65;
                dataGridView1.Columns[8].Width = 65;
                dataGridView1.Columns[9].Width = 65;
                dataGridView1.Columns[10].Width = 65;
                dataGridView1.Columns[11].Width = 65;
                dataGridView1.Columns[12].Width = 65;
                dataGridView1.Columns[13].Width = 65;
                dataGridView1.Columns[14].Width = 90;
                dataGridView1.Columns[15].Width = 90;
                dataGridView1.Columns[16].Width = 65;
                dataGridView1.Columns[17].Width = 85;
                dataGridView1.Columns[18].Width = 65;
                dataGridView1.Columns[19].Width = 65;
                dataGridView1.Columns[20].Width = 65;
                dataGridView1.Columns[21].Width = 65;
                dataGridView1.Columns[22].Width = 65;
                dataGridView1.Columns[23].Width = 65;
                dataGridView1.Columns[24].Width = 65;
                dataGridView1.Columns[25].Width = 65;
                dataGridView1.Columns[26].Width = 65;
                dataGridView1.Sort(dataGridView1.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                //OnRowDataBound = "gvWOContent_RowDataBound";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 4)
            {
                var sql = "SELECT zling as '工艺卡号',sgdhao as '工单号',ddhao as '生产单号',kehu as '客户'," +
                          "jhqi as '订单交期',tuhao as '产品图号', name as '产品名称',yema as '页码', cailiao as '材料'," +
                          "suliang as '订单数量',gxone as '序号',gxname as '工序名称',gxtwo as '加工工序',tiao as '调机时间'," +
                          "danjian as '单件加工',xuj as '序价',buzu as '补助',wssulia as '完成数量',hujii as '件资合计'," +
                          "wsbanz as'加工班组',bei3 as '生产员',wsriqi as '完成日期',cjriqi as '指令创日',lururiqi as '录入日期'," +
                          $"lururen as '录入人',qgriqi as '修改日期',qgren as '修改人' FROM udktr WHERE zling='{textBox4.Text.Trim()}'";
                var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 65;
                dataGridView1.Columns[1].Width = 65;
                dataGridView1.Columns[2].Width = 65;
                dataGridView1.Columns[3].Width = 65;
                dataGridView1.Columns[4].Width = 65;
                dataGridView1.Columns[5].Width = 65;
                dataGridView1.Columns[6].Width = 65;
                dataGridView1.Columns[7].Width = 65;
                dataGridView1.Columns[8].Width = 65;
                dataGridView1.Columns[9].Width = 65;
                dataGridView1.Columns[10].Width = 65;
                dataGridView1.Columns[11].Width = 65;
                dataGridView1.Columns[12].Width = 65;
                dataGridView1.Columns[13].Width = 65;
                dataGridView1.Columns[14].Width = 90;
                dataGridView1.Columns[15].Width = 90;
                dataGridView1.Columns[16].Width = 65;
                dataGridView1.Columns[17].Width = 85;
                dataGridView1.Columns[18].Width = 65;
                dataGridView1.Columns[19].Width = 65;
                dataGridView1.Columns[20].Width = 65;
                dataGridView1.Columns[21].Width = 65;
                dataGridView1.Columns[22].Width = 65;
                dataGridView1.Columns[23].Width = 65;
                dataGridView1.Columns[24].Width = 65;
                dataGridView1.Columns[25].Width = 65;
                dataGridView1.Columns[26].Width = 65;
                dataGridView1.Sort(dataGridView1.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                //OnRowDataBound = "gvWOContent_RowDataBound";
            }
        }

    }
}
