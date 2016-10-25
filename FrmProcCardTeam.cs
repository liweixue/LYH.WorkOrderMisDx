using System;
using System.Data;
using System.Windows.Forms;
using LYH.Framework.Commons;
using LYH.WorkOrder.Properties;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmProcCardTeam : Form
    {
        public FrmProcCardTeam()
        {
            KeyDown+=FrmWin_KeyDown;
            InitializeComponent();
            InitData();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == Resources.A新增)
            {
                const string sql = "SELECT distinct BZname FROM udbz ";
                comboBox1.Items.Clear();
                comboBox1.Items.Add("");
                comboBox1.Text = "";
                var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["BZname"].ToString().Trim());
                }
                dr.Close();
                button2.Visible = false;
                button3.Visible = false;
                button1.Text = Resources.A保存;
            }
            else
            {

                if (textBox2.Text != "" && comboBox1.Text != "")
                {
                    var sql =
                        $"insert into udbz(BZname,REN) values('{comboBox1.Text.Trim().ToUpper()}','{textBox2.Text.Trim().ToUpper()}')";
                    SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text,sql);
                    button2.Visible = true;
                    button3.Visible = true;
                    button1.Text = Resources.A新增;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    comboBox1.Text = "";
                    InitData();

                }
                else
                {
                    MessageBox.Show("班组与员工都不可为空！", Resources.T提示);
                }
            }
        }

        private void InitData()
        {
            const string sql = "SELECT id as '序号',BZname as '班组名称',REN as '员工' FROM udbz";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns[0].Width = 120;
            dataGridView2.Columns[1].Width = 200;
            dataGridView2.Columns[2].Width = 200;
            dataGridView2.Sort(dataGridView2.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == Resources.X修改)
            {
                if (MessageBox.Show("是否已经选择好所要修改的行，若未选好请按取消！", Resources.T提示, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    var mMi = dataGridView2.SelectedCells[0].Value.ToString().Trim();
                    var sql = "SELECT distinct BZname FROM udbz ";
                    comboBox1.Items.Add("");
                    var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                    while (dr.Read())
                    {
                        comboBox1.Items.Add(dr["BZname"].ToString().Trim());
                    }
                    dr.Close();
                    sql = $"SELECT TOP 1 * FROM udbz WHERE id='{mMi}'";
                    dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        comboBox1.Text = dr["BZname"].ToString().Trim();
                        textBox2.Text = dr["REN"].ToString().Trim();
                        textBox1.Text = dr["id"].ToString().Trim();
                        dr.Close();
                        button1.Visible = false;
                        button3.Visible = false;
                        button2.Text = Resources.X保存;
                    }
                    else
                    {
                        dr.Close();
                    }
                }
            }
            else
            {
                var sql =
                    $"UPDATE udbz set BZname='{comboBox1.Text.Trim().ToUpper()}',REN='{textBox2.Text.Trim().ToUpper()}'WHERE id ='{textBox1.Text.Trim()}'";
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text,sql);
                button2.Text = Resources.X修改;
                button1.Visible = true;
                button3.Visible = true;
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.Text = "";
                InitData();
            }     
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var id = dataGridView2.SelectedCells[0].Value.ToString().Trim();
            if (MessageBox.Show("是否要删除所选择的行", Resources.J警告, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                var sql = $"delete FROM udbz WHERE id='{id}'";
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text,sql);
                InitData();
            } 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button1.Text = Resources.A新增;
            button2.Text = Resources.X修改;
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            InitData();
        }
    }
}
