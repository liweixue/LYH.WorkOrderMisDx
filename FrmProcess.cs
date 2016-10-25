using System;
using System.Data;
using System.Windows.Forms;
using LYH.Framework.Commons;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmProcess : Form
    {
        public FrmProcess()
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

        private void Goiss()
        {
            const string sql = "SELECT id '序号',name '工序名称'FROM udbz ";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Sort(dataGridView1.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                var sql = $"SELECT TOP 1 * FROM udbz WHERE name='{textBox1.Text.Trim().ToUpper()}'";
                var sdra5 = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                if (sdra5.HasRows)
                {
                    sdra5.Close();
                    MessageBox.Show($"此工序名称【{textBox1.Text.Trim()}】已存在，请重新输入!!", "提示");
                    textBox1.Text = "";
                    textBox1.Focus();
                    Goiss();
                }
                else
                {
                    sdra5.Close();
                    var sqltk = $"insert into udbz(name) values('{textBox1.Text.Trim().ToUpper()}')";
                    SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sqltk);
                    textBox1.Text = "";
                    textBox1.Focus();
                }
            }
            else
            {
                MessageBox.Show("工序名称不能为空", "提示");
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var kMMi = dataGridView1.SelectedCells[0].Value.ToString().Trim();
            if (MessageBox.Show("是否要删除所选择的行", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                var sql = $"delete FROM udodt WHERE IDO='{kMMi}'";
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                Goiss();
            }
        }
    }
}
