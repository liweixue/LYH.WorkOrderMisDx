using System;
using System.Data;
using System.Windows.Forms;
using LYH.Framework.Commons;
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

        readonly DataSet1 _ds = new DataSet1();
        private void FrmPrint_Load(object sender, EventArgs e)
        {
            var sql = $"SELECT * FROM udstr WHERE zling = '{SqlHelper.InstructionNo}'";
            SqlHelper.FillDataset(SqlHelper.GetConnection(), CommandType.Text, sql, _ds, new string[] {"cncwuden"});
            var rds = new ReportDataSource("DataSet1", _ds.Tables[0]);
            reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }
    }
}
