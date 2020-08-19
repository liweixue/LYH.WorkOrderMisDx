using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public partial class FrmPrintRdlc : Form
    {
        public FrmPrintRdlc()
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

        //readonly ProcCardDataSet _ds = new ProcCardDataSet();
        private void FrmPrint_Load(object sender, EventArgs e)
        {
            //var sql = "SELECT a.*,c.Dept FROM udstr a LEFT JOIN dbo.udone b ON b.sgdhao=a.sgdhao " +
            //             "LEFT JOIN DZDJ.dbo.TB_Dept c ON c.ID=b.DeptId " +
            //             $"WHERE zling='{SqlHelper.ProcCardNo}' AND DeptId='{SqlHelper.DeptId}' ORDER BY a.gxone,a.gxtwo";

            //SqlHelper.FillDataset(SqlHelper.GetConnection(), CommandType.Text, sql, _ds, new string[] {"ProcCard"});

            var cardDataSet =ProcCard.GetProcCardDataSet(SqlHelper.ProcCardNo);
            var rds = new ReportDataSource("ProcCard", cardDataSet.Tables[0]);
            reportViewer1.LocalReport.ReportPath = "ProcCard.rdlc";
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }
    }
}
