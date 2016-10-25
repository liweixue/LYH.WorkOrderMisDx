using System;
using Excel;
using DataTable = System.Data.DataTable;

namespace LYH.WorkOrder
{
    public class DgvExcelExtend
    {
        #region 获取报表的总行数及总列数

        /// <summary>
        ///     获取报表的总行数及总列数
        /// </summary>
        /// <param name="pFilePath">EXCEL文件路径</param>
        /// <param name="pStartRow">开始行号</param>
        /// <param name="pStartColumn">开始列号</param>
        /// <param name="pCheckColumn">检测共有多少行的列序号</param>
        /// <param name="pRowQuantity">总行数</param>
        /// <param name="pColumnQuantity">总列数</param>
        /// <param name="pSheetIndex">工作表序号</param>
        /// <param name="pMessage">错误信息</param>
        public static DataTable GetExcelRcQuantity(string pFilePath, int pStartRow, int pStartColumn, int pCheckColumn,
            ref int pRowQuantity, ref int pColumnQuantity, int pSheetIndex, ref string pMessage)
        {
            var xls = new Application();
            var xlsBook = xls.Workbooks.Open(pFilePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing);
            var xlsSheet = (Worksheet) xlsBook.Worksheets[pSheetIndex];

            var dt = new DataTable();

            try
            {
                var iStartRow = pStartRow;
                var iRange =
                    xlsSheet.Range[xlsSheet.Cells[iStartRow, pCheckColumn], xlsSheet.Cells[iStartRow, pCheckColumn]];
                while (iRange.Value != null)
                {
                    iStartRow++;
                    iRange =
                        xlsSheet.Range[xlsSheet.Cells[iStartRow, pCheckColumn], xlsSheet.Cells[iStartRow, pCheckColumn]];
                }

                pRowQuantity = iStartRow - 1;

                iStartRow = pStartRow;
                var iStartColumn = pStartColumn;
                iRange =
                    xlsSheet.Range[xlsSheet.Cells[iStartRow, iStartColumn], xlsSheet.Cells[iStartRow, iStartColumn]];
                while (iRange.Value != null)
                {
                    iStartColumn++;
                    dt.Columns.Add(iRange.Value.ToString(), typeof (String));
                    iRange =
                        xlsSheet.Range[xlsSheet.Cells[iStartRow, iStartColumn], xlsSheet.Cells[iStartRow, iStartColumn]];
                }

                pColumnQuantity = iStartColumn - 1;
            }
            catch (Exception ex)
            {
                pMessage = ex.Source + ex.StackTrace + ex.Message;
            }
            finally
            {
                xls.Quit();
                //xls = null;
                //xlsBook = null;
                //xlsSheet = null;
                GC.Collect();
            }

            return dt;
        }

        #endregion

        #region 获取指定EXCEL文件的内容

        /// <summary>
        ///     获取指定EXCEL文件的内容
        /// </summary>
        /// <param name="pFilePath">EXCEL文件路径</param>
        /// <param name="pStartRow">开始行号</param>
        /// <param name="pStartColumn">开始列号</param>
        /// <param name="pCheckColumn">检测共有多少行的列序号</param>
        /// <param name="pExcelSheetIndex">需要获取的工作表序号</param>
        /// <param name="pErrorMessage">错误信息</param>
        public static DataTable GetExcelData(string pFilePath, int pStartRow, int pStartColumn, int pCheckColumn,
            int pExcelSheetIndex, ref string pErrorMessage)
        {
            var xls = new Application();
            var xlsBook = xls.Workbooks.Open(pFilePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing);
            var xlsSheet = (Worksheet) xlsBook.Worksheets[pExcelSheetIndex];

            var dt = new DataTable();
            try
            {
                int pRowCount = 0, pColumnCount = 0;

                dt = GetExcelRcQuantity(pFilePath, pStartRow, pStartColumn, pCheckColumn, ref pRowCount,
                    ref pColumnCount, pExcelSheetIndex, ref pErrorMessage);

                if (pRowCount > 0 && pColumnCount > 0)
                {
                    var iColumn = 0;

                    for (var i = pStartRow + 1; i < pRowCount + 1; i++)
                    {
                        var dr = dt.NewRow();
                        for (var j = pStartColumn; j < pColumnCount + 1; j++)
                        {
                            var pTempData = xlsSheet.Range[xlsSheet.Cells[i, j], xlsSheet.Cells[i, j]].Value;
                            dr[iColumn] = pTempData != null ? pTempData.ToString() : "";
                            iColumn++;
                        }
                        iColumn = 0;
                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    pErrorMessage = "无法读取报表数据,请重新检查报表。";
                    return null;
                }
            }
            catch (Exception ex)
            {
                pErrorMessage = ex.Source + ex.Message + ex.StackTrace;
            }
            finally
            {
                xls.Quit();
                //xls = null;
                //xlsBook = null;
                //xlsSheet = null;
                GC.Collect();
            }

            return dt;
        }

        #endregion
    }
}