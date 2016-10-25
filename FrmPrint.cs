using System;
using System.Data;
using System.Windows.Forms;
using LYH.Framework.Commons;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmPrint : Form
    {
        public FrmPrint()
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

        readonly DataSet1 _ds = new DataSet1();
        private void FrmPrint_Load(object sender, EventArgs e)
        {
            var sql = "SELECT a.*,c.Dept FROM udstr a LEFT JOIN dbo.udone b ON b.sgdhao=a.sgdhao " +
                      "LEFT JOIN DZDJ.dbo.TB_Dept c ON c.ID=b.DeptId " +
                      $"WHERE zling='{SqlHelper.InstructionNo}' AND DeptId='{SqlHelper.DeptId}' ORDER BY a.gxone,a.gxtwo";
            SqlHelper.FillDataset(SqlHelper.GetConnection(), CommandType.Text, sql, _ds, new string[] {"cncwuden"});
            var cr = new cncCryst();
            cr.SetDataSource(_ds);
            crystalReportViewer1.ReportSource = cr;
        }
    }
}
