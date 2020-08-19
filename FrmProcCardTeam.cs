using System;
using System.Data;
using System.Windows.Forms;
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

        private void FrmProcCardTeam_Load(object sender, EventArgs e)
        {
            BindData();
        }


        private void BindData()
        {
            var sql = $"SELECT Id,Team FROM PD_ProcCard_Team WHERE DeptId={SqlHelper.DeptId}";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("");
            comboBox1.Text = "";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString("dzdj"), CommandType.Text, sql);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "Team";
            comboBox1.ValueMember = "Id";
        }

        private void InitData()
        {
            const string sql =
                "SELECT a.ID,b.Team '班组名称',a.Name '员工' FROM PD_ProcCard_Employee a JOIN dbo.PD_ProcCard_Team b ON b.ID=a.TeamId ";
            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString("dzdj"), CommandType.Text, sql);
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns[0].Width = 120;
            dataGridView2.Columns[1].Width = 200;
            dataGridView2.Columns[2].Width = 200;
            dataGridView2.Sort(dataGridView2.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == Resources.A新增)
            {
                btnUpd.Visible = false;
                btnDel.Visible = false;
                btnAdd.Text = Resources.A保存;
            }
            else
            {
                if (textBox2.Text != "" && comboBox1.Text != "")
                {
                    var sql =
                        $"INSERT INTO PD_ProcCard_Employee(TeamId,Name) VALUES({comboBox1.SelectedValue},'{textBox2.Text.Trim().ToUpper()}')";
                    SqlHelper.ExecuteNonQuery(SqlHelper.GetConnectionString("dzdj"), CommandType.Text,sql);
                    btnUpd.Visible = true;
                    btnDel.Visible = true;
                    btnAdd.Text = Resources.A新增;
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

        private void btnUpd_Click(object sender, EventArgs e)
        {
            if (btnUpd.Text == Resources.X修改)
            {
                if (MessageBox.Show("是否已经选择好所要修改的行，若未选好请按取消！", Resources.T提示, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    var employeeId = dataGridView2.SelectedCells[0].Value.ToString().Trim();
                    
                    var sql = $"SELECT a.ID,b.Team '班组名称',a.Name '员工' FROM PD_ProcCard_Employee a JOIN dbo.PD_ProcCard_Team b ON b.ID=a.TeamId WHERE a.ID='{employeeId}'";
                    var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnectionString("dzdj"), CommandType.Text, sql);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        comboBox1.Text = dr[1].ToString().Trim();
                        textBox2.Text = dr[2].ToString().Trim();
                        comboBox1.Tag = dr[0].ToString().Trim();
                        dr.Close();
                        btnAdd.Visible = false;
                        btnDel.Visible = false;
                        btnUpd.Text = Resources.X保存;
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
                    $"UPDATE PD_ProcCard_Employee SET TeamId='{comboBox1.SelectedValue}',Name='{textBox2.Text.Trim().ToUpper()}'WHERE id ={Convert.ToInt32(comboBox1.Tag)}";
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnectionString("dzdj"), CommandType.Text,sql);
                btnUpd.Text = Resources.X修改;
                btnAdd.Visible = true;
                btnDel.Visible = true;
                textBox2.Text = "";
                comboBox1.Text = "";
                InitData();
            }     
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var id = dataGridView2.SelectedCells[0].Value.ToString().Trim();
            if (MessageBox.Show("是否要删除所选择的行", Resources.J警告, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                var sql = $"DELETE FROM PD_ProcCard_Employee WHERE ID='{id}'";
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnectionString("dzdj"), CommandType.Text,sql);
                InitData();
            } 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnAdd.Visible = true;
            btnUpd.Visible = true;
            btnDel.Visible = true;
            btnAdd.Text = Resources.A新增;
            btnUpd.Text = Resources.X修改;
            textBox2.Text = "";
            comboBox1.Text = "";
            InitData();
        }
    }
}
