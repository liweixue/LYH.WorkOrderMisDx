using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Transactions;
using System.Windows.Forms;
using Aspose.Cells;
using LYH.Framework.BaseUI;
using LYH.Framework.Commons;
using LYH.WorkOrder.Properties;
using LYH.WorkOrder.share;

namespace LYH.WorkOrder
{
    public class ProcCard
    {
        public string ExcelTemplateFile { get; set; } = "PrintTemplate\\工艺卡模板.xls";

        public string GetProcCardNo()
        {
            var orderId = DateTime.Now.ToString("yyMM").Substring(1, 1);
            var noStart = "1000000";
            var noEnd = "3080001";
            var sql = $"SELECT MAX(zling) FROM udstr WHERE zling>{noStart} AND zling<{noEnd}";
            var scalar = SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text, sql);
            var prodCardNo = "";
            if (scalar.ToString() == "")
            {
                prodCardNo = noStart;
            }
            else
            {
                if (scalar.ToString() == noEnd)
                {
                    MessageBox.Show(@"工艺卡号已达上限！", Resources.T提示);
                }
                else
                {
                    var maxtid = (int) scalar;
                    maxtid += 1;
                    prodCardNo = maxtid.ToString();
                }
            }

            return prodCardNo;
        }

            SqlConnection _defaultConnection = SqlHelper.GetConnection();
        public bool InsertOrUpdate(string wONo)
        {
            var sql = $"SELECT TOP 1 * FROM udone WHERE sgdhao='{wONo}' AND DeptId={SqlHelper.DeptId}";
            var dataReader = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            if (dataReader.HasRows)
            {
                dataReader.Read();
                var pONo = dataReader["ddhao"].ToString().Trim();
                var cust = dataReader["kehu"].ToString().Trim();
                var planDate = dataReader["jhwxri"].ToString().Trim();
                var prtDwgNo = dataReader["tuhao"].ToString().Trim();
                var prtName = dataReader["name"].ToString().Trim().Replace("'", "''");
                var pageNo = dataReader["yema"].ToString().Trim();
                var meatrial = dataReader["cailiao"].ToString().Trim();
                var qty = dataReader["sulia"].ToString().Trim();
                dataReader.Close();
                sql = "SELECT xuhaoone '工序号',xuhaoname '工序名称',xuhaotwo '加工工序',zuone '调机时间',zutwo '单件时间'," +
                      "xuj1 '序价',xuj2 '公式',xuj3 '补助' FROM udtwo a LEFT JOIN udone b ON b.tuhao=a.tuhao " +
                      $"AND b.DeptId = a.DeptId WHERE b.sgdhao='{wONo}' AND a.DeptId ='{SqlHelper.DeptId}'";
                var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);

                if (!IsExistsProcCardNo(wONo)) SqlHelper.ProcCardNo = new ProcCard().GetProcCardNo();

                using (var tran = new TransactionScope())
                {
                    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var craftSeq = ds.Tables[0].Rows[i]["工序号"].ToString();
                        var craft = ds.Tables[0].Rows[i]["工序名称"].ToString();
                        var processCardSeq = ds.Tables[0].Rows[i]["加工工序"].ToString();
                        var debugTime = decimal.Parse(ds.Tables[0].Rows[i]["调机时间"] + "");
                        var singleProcTime = decimal.Parse(ds.Tables[0].Rows[i]["单件时间"] + "");
                        var processUPrice = decimal.Parse(ds.Tables[0].Rows[i]["序价"] + "");
                        var formula = int.Parse(ds.Tables[0].Rows[i]["公式"] + "");
                        var subsidy = decimal.Parse(ds.Tables[0].Rows[i]["补助"] + "");
                        sql =
                            $"SELECT zling FROM udstr WHERE sgdhao='{wONo}' AND DeptId={SqlHelper.DeptId}" +
                            $" AND gxone='{craftSeq}' AND gxtwo='{processCardSeq}'";
                        var dr = SqlHelper.ExecuteReader(_defaultConnection, CommandType.Text, sql);
                        string strSql;
                        if (dr.HasRows)
                        {
                            dr.Close();
                            strSql =
                                $"UPDATE udstr SET gxname='{craft}',tiao={debugTime},danjian={singleProcTime}," +
                                $"xuj={processUPrice},gongsi={formula},buzu={subsidy},cjriqi='{DateTime.Now}'," +
                                $"cjren='{SqlHelper.UserName}',DeptId='{SqlHelper.DeptId}' WHERE zling='{SqlHelper.ProcCardNo}'" +
                                $"AND gxone={craftSeq} AND gxtwo={processCardSeq};";
                            strSql +=
                                $"UPDATE udktr SET gxname='{craft}',tiao={debugTime},danjian={singleProcTime}," +
                                $"xuj={processUPrice},gongsi={formula},buzu={subsidy},cjriqi='{DateTime.Now}'," +
                                $"cjren='{SqlHelper.UserName}' WHERE zling='{SqlHelper.ProcCardNo}'" +
                                $"AND gxone={craftSeq} AND gxtwo={processCardSeq};";
                        }
                        else
                        {
                            //using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                            //{
                            //    using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(txtProcCardNo.Text, (QRCodeGenerator.ECCLevel)_appConfig.AppConfigGet("eccLevel").ToInt32()))
                            //    {
                            //        using (QRCode qrCode = new QRCode(qrCodeData))
                            //        {
                            //            var img= qrCode.GetGraphic(20, Color.Black, Color.White,new Bitmap("/ICO/dz.png"), 18);
                            //        }
                            //    }
                            //}

                            if (craftSeq == "" || processCardSeq == "")
                                continue;
                            strSql =
                                "insert into udstr(zling,sgdhao,ddhao,kehu,jhqi,tuhao,name,yema,suliang,cailiao,gxone," +
                                $"gxname,gxtwo,tiao,danjian,xuj,gongsi,buzu,cjriqi,cjren,DeptId) values('{SqlHelper.ProcCardNo}'," +
                                $"'{wONo}','{pONo}','{cust}','{planDate}','{prtDwgNo}','{prtName}','{pageNo}','{qty}','{meatrial}'," +
                                $"'{craftSeq}','{craft}','{processCardSeq}','{debugTime}','{singleProcTime}','{processUPrice}'," +
                                $"'{formula}','{subsidy}','{DateTime.Now}','{SqlHelper.UserName}','{SqlHelper.DeptId}');";

                            strSql +=
                                "insert into udktr(zling,sgdhao,ddhao,kehu,jhqi,tuhao,name,yema,suliang,cailiao,gxone," +
                                $"gxname,gxtwo,tiao,danjian,xuj,gongsi,buzu,cjriqi,cjren) values('{SqlHelper.ProcCardNo}'," +
                                $"'{wONo}','{pONo}','{cust}','{planDate}','{prtDwgNo}','{prtName}','{pageNo}','{qty}'," +
                                $"'{meatrial}','{craftSeq}','{craft}','{processCardSeq}','{debugTime}','{singleProcTime}'," +
                                $"'{processUPrice}','{formula}','{subsidy}','{DateTime.Now}','{SqlHelper.UserName}');";
                            strSql += $"UPDATE udone set beione='1' WHERE sgdhao='{wONo}';";
                        }
                        dr.Close();
                        try
                        {
                            SqlHelper.ExecuteNonQuery(_defaultConnection, CommandType.Text, strSql);
                        }
                        catch (Exception ex)
                        {
                            tran.Dispose();
                            MessageBox.Show(ex.Message);
                        }
                    }
                    tran.Complete();
                    MessageBox.Show(@"保存成功", Resources.T提示);
                    return true;
                }
            }
            return false;
        }

        public bool IsExistsProcCardNo(string wONo)
        {
            var sql = $"SELECT zling FROM dbo.udstr WHERE sgdhao='{wONo}' AND DeptId={SqlHelper.DeptId}";
            var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            if (dr.HasRows)
            {
                dr.Read();
                SqlHelper.ProcCardNo = dr["zling"].ToString();
                dr.Close();
                return true;
            }
            return false;
        }

        public bool SaveExcel(string procCardNo, out string error)
        {
            var xlsxTemplateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ExcelTemplateFile);
            try
            {
                if (!File.Exists(xlsxTemplateFile))
                {
                    error = "文件不存在";
                    return false;
                }

                var ds = GetProcCardDataSet(procCardNo);
                var dataTable = ds.Tables[0];
                dataTable.TableName = "ProcCard";
                //var dictReplace = new Dictionary<string, object>();
                //foreach (DataColumn dataColumn in dataTable.Columns)
                //    dictReplace.Add(dataColumn.ColumnName, dataTable.Rows[0][dataColumn]);

                var excelFileName = string.Format("{0}.xlsx", dataTable.Rows[0][0].ToString());
                var file = Path.Combine(new SpecialDirectories().Desktop, excelFileName);
                //var file = FileDialogHelper.SaveExcel(string.Format("{0}.xlsx", dataTable.Rows[0][0].ToString()));
                //if (string.IsNullOrEmpty(file))
                //{
                //    error = null;
                //    return false;
                //}

                //var path = Path.Combine(Application.StartupPath, xlsxTemplateFile);
                //var designer = new WorkbookDesigner();
                //try
                //{
                //    designer.Open(path);
                //}
                //catch (Exception ex)
                //{
                //    if (string.IsNullOrEmpty(file))
                //        MessageDxUtil.ShowError(string.Format("导出Excel出现错误,未找到模板文件：{0}", file));
                //    MessageDxUtil.ShowError(ex.Message);
                //    error = ex.Message;
                //    return false;
                //}

                ////设置数据源
                //designer.SetDataSource(dataTable);
                //if (dictReplace.Count > 0)
                //    foreach (var keyValuePair in dictReplace)
                //        designer.SetDataSource(keyValuePair.Key, keyValuePair.Value);
                //designer.Process();
                //if (!string.IsNullOrEmpty(file))
                //    try
                //    {
                //        var saveFile = Path.Combine(Application.StartupPath, file);
                //        designer.Save(saveFile);
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageDxUtil.ShowError(ex.Message);
                //    }

                var workbook = new Workbook();
                workbook.Open(xlsxTemplateFile);
                var worksheet = workbook.Worksheets[0];
                //var beInsertRow = dataTable.Rows.Count > 2;
                //worksheet.Cells.ImportDataTable(dataTable, false, 10, 1, 1, 1, beInsertRow);
                string[] arr = {"J1", "A2", "A4", "E2", "J2", "E3", "J3", "E4", "J4", "E5", "J5"};

                for (var i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (dataTable.Rows.Count > 1 && i > 0) worksheet.Cells.InsertRows(worksheet.Cells.MaxRow - 1, 1);

                    for (var j = 0; j < dataTable.Columns.Count; j++)
                        if (j < arr.Length)
                            worksheet.Cells[arr[j]].PutValue(dataTable.Rows[i][j].ToString());
                        else
                            worksheet.Cells.Rows[i + 6][j - arr.Length].PutValue(dataTable.Rows[i][j].ToString());
                }

                worksheet.AutoFitRows();

                worksheet.Cells["A1"].Formula = "=\"*\"&" + worksheet.Cells["J2"].Value + "&\"*\"";
                workbook.CalculateFormula();
                workbook.Save(file);

                //TemplateExcelHelper.TemplateExportExcel(file, dataGridView3, dataTable.TableName,
                //    dictReplace);

                worksheet.PageSetup.PaperSize = PaperSizeType.PaperA5;
                worksheet.PageSetup.Orientation = PageOrientationType.Landscape;
                worksheet.PageSetup.CenterHorizontally = true;
                worksheet.PageSetup.FitToPagesWide = 1;
                worksheet.PageSetup.LeftMargin = 1;
                worksheet.PageSetup.RightMargin = 1;
                worksheet.PageSetup.TopMargin = 1;
                worksheet.PageSetup.BottomMargin = 1;
                var printSettings = new PrinterSettings();
                var printerName = printSettings.PrinterName;

                //var options = new ImageOrPrintOptions();
                //var sheetRender = new SheetRender(worksheet,options);
                //var image = sheetRender.ToImage(0);
                //sheetRender.ToPrinter(printerName);

                if (MessageDxUtil.ShowYesNoAndTips("导出成功，是否打开文件？否则是直接打印到默认打印机") == DialogResult.Yes)
                    Process.Start(file);
                else
                    worksheet.SheetToPrinter(printerName);

                error = null;
                return true;
            }
            catch (Exception exception)
            {
                error = exception.Message;
                return false;
            }
        }

        public static DataSet GetProcCardDataSet(string procCardNo)
        {
            var sql = "SELECT c.Dept,a.kehu '客户',a.ddhao '生产单号',a.sgdhao '工单号',a.zling '工艺卡号',a.tuhao '图号'," +
                      "a.name '产品名称',a.cailiao '材料',a.yema '页码',a.suliang '数量',a.jhqi '订单交期',a.gxone '序号'," +
                      "a.gxname '工序名称',a.gxtwo '加工工序',a.buzu '补助',a.danjian '单件时间',a.xuj '单价'" +
                      " FROM udstr a INNER JOIN udone b ON b.sgdhao=a.sgdhao " +
                      "INNER JOIN DZDJ.dbo.TB_Dept c ON c.ID = b.DeptId " +
                      $"WHERE zling='{procCardNo}' AND b.DeptId=" + SqlHelper.DeptId + " ORDER BY gxone, gxtwo";

            var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            return ds;
        }
    }
}