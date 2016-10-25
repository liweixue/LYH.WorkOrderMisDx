using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace LYH.WorkOrder.share
{
    public class ExcelOperations : IDisposable
    {
        private Excel._Application _mExcelApplication = null;
        private Excel._Workbook _mWorkbook = null;
        public Excel._Worksheet MWorksheet = null;
        private object _missing = System.Reflection.Missing.Value;

        public ExcelOperations()
        {
            if (_mExcelApplication == null)
            {
                _mExcelApplication = new Excel.Application();
            }
        }

        ~ExcelOperations()
        {
            try
            {
                if (_mExcelApplication != null)
                    _mExcelApplication.Quit();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }
        /// <summary>
        /// 获取或设置当前工作表
        /// </summary>
        public int CurrentWorksheetIndex
        {
            set
            {
                if (value <= 0 || value > _mWorkbook.Worksheets.Count)
                    throw new Exception("索引超出范围");
                else
                {
                    object index = value;
                    MWorksheet = _mWorkbook.Worksheets[index] as Excel._Worksheet;
                }
            }
        }
        /// <summary>
        /// 打开一个Excel工作薄
        /// </summary>
        /// <param name="fileName"></param>
        public void OpenWorkbook(string fileName)
        {
            _mWorkbook = _mExcelApplication.Workbooks.Open(fileName, _missing, _missing, _missing, _missing, _missing,
                _missing, _missing, _missing, _missing, _missing, _missing, _missing);

            if (_mWorkbook.Worksheets.Count > 0)
            {
                object index = 1;
                MWorksheet = _mWorkbook.Worksheets[index] as Excel._Worksheet;

            }
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        public void Save()
        {
            if (_mWorkbook != null)
            {
                _mWorkbook.Save();
            }
        }
        /// <summary>
        /// 关闭文档
        /// </summary>
        /// <param name="isSave"></param>
        public void Close(bool isSave)
        {
            ClearClipboard();

            object objSave = isSave;
            if (_mWorkbook != null)
                _mWorkbook.Close(objSave, _missing, _missing);
        }
        /// <summary>
        /// 设置当前工作表中某单元格的值
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <param name="value"></param>
        public void SetCellValue(string cellIndex, object value)
        {
            if (MWorksheet != null)
            {
                object cell1 = cellIndex;
                var range = MWorksheet.get_Range(cell1, _missing);
                if (range != null)
                {
                    range.Value2 = value;
                }
            }
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="cellIndex1"></param>
        /// <param name="cellIndex2"></param>
        public void Merge(string cellIndex1, string cellIndex2)
        {
            if (MWorksheet != null)
            {
                object cell1 = cellIndex1;
                object cell2 = cellIndex2;
                var range = MWorksheet.get_Range(cell1, cell2);
                range.Merge(true);
            }
        }
        /// <summary>
        /// 将当前工作表中的表格数据复制到剪切板
        /// </summary>
        public void Copy()
        {
            if (MWorksheet != null)
            {
                try
                {
                    MWorksheet.UsedRange.Select();
                }
                catch { throw; }
                MWorksheet.UsedRange.Copy(_missing);
            }
        }
        /// <summary>
        /// 清空剪切板
        /// </summary>
        public void ClearClipboard()
        {
            Clipboard.Clear();
        }

        #region IDisposable 成员

        public void Dispose()
        {
            try
            {
                if (_mExcelApplication != null)
                {
                    Close(false);
                    _mExcelApplication.Quit();
                    _mExcelApplication = null;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion
    }
}
