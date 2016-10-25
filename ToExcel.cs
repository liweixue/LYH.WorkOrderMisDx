using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LYH.WorkOrder
{
    public class ToExcel
    {
        #region 将导出
        public bool DataGridViewToExcel(DataGridView dataGridView1)
        {
            var saveFileDialog = new SaveFileDialog() { Filter = "Execl files (*.xlsx)|*.xlsx", FilterIndex = 0, RestoreDirectory = true, CreatePrompt = true, Title = "Export Excel File" };
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == "")
                return false;
            var myStream = saveFileDialog.OpenFile();
            var sw = new StreamWriter(myStream, Encoding.GetEncoding(0));

            var str = "";
            try
            {
                for (var i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (i > 0)
                    {
                        str += "\t";
                    }
                    str += dataGridView1.Columns[i].HeaderText;
                }
                sw.WriteLine(str);
                for (var j = 0; j < dataGridView1.Rows.Count - 1; j++)
                {
                    var tempStr = "";
                    for (var k = 0; k < dataGridView1.Columns.Count; k++)
                    {
                        if (k > 0)
                        {
                            tempStr += "\t";
                        }
                        var dgcell = dataGridView1.Rows[j].Cells[k].Value.ToString().Trim();
                        if (dgcell.Contains("\r") || dgcell.Contains("\n"))
                        {
                            tempStr += dgcell.Replace("\r", "").Replace("\n", "");
                        }
                        else
                        {
                            tempStr += dgcell;
                        }
                    }
                    sw.WriteLine(tempStr);
                }
                sw.Close();
                myStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        #endregion
        }
    }
}
