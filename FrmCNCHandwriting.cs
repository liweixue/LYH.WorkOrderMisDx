using System;
using System.Data;
using System.Windows.Forms;
using LYH.WorkOrder.share;

namespace LYH.WorkOrder
{
    public partial class FrmCncHandwriting : Form
    {
        public FrmCncHandwriting()
        {
            KeyDown += FrmWin_KeyDown;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void FrmWin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    DialogResult = DialogResult.Cancel;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                if (textBox3.Text != "")
                {
                    if (textBox4.Text != "")
                    {
                        if (textBox5.Text != "")
                        {
                            if (textBox7.Text != "")
                            {
                                if (textBox9.Text != "")
                                {
                                    var sql = "SELECT Max(sgdhaotwo) FROM udone WHERE sgdhaotwo>=" + 6000001 +
                                              " AND sgdhaotwo<=" + 6999999;
                                    var scalar = SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text,
                                        sql);
                                    if (scalar.ToString() == "")
                                    {
                                        textBox1.Text = "6000001";
                                    }
                                    else
                                    {
                                        if (scalar.ToString() == "6999999")
                                        {
                                            MessageBox.Show("系统已过期！", "提示");
                                        }
                                        else
                                        {
                                            var maxtid = (int) scalar;
                                            maxtid += 1;
                                            textBox1.Text = maxtid.ToString();
                                        }
                                    }
                                    const string budf = "未完成";
                                    //MessageBox.Show(dateTimePicker1.Text.Trim(), "提示");
                                    sql =
                                        "insert into udone(DeptId,sgdhao,sgdhaotwo,ddhao,kehu,jhqi,tuhao,name,yema,cailiao,xuhao,gyname,jhwxri,jiesou,sulia,beistr)" +
                                        $" values('{SqlHelper.DeptId}','{textBox1.Text.Trim()}','{textBox1.Text.Trim()}','{textBox2.Text.Trim()}'," +
                                        $"'{textBox3.Text.Trim()}','{dateTimePicker1.Text.Trim()}','{textBox5.Text.Trim()}'," +
                                        $"'{textBox6.Text.Trim()}','{textBox7.Text.Trim()}','{textBox8.Text.Trim()}'," +
                                        $"'{textBox9.Text.Trim()}','{dateTimePicker2.Text.Trim()}','{textBox10.Text.Trim()}'," +
                                        $"'{DateTime.Now}','{textBox4.Text.Trim()}','{budf}')";
                                    SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sql);
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show("工序号不能为空！", "提示");
                                    textBox9.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("页码不能为空！", "提示");
                                textBox7.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("产品图号不能为空！", "提示");
                            textBox5.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("生产数量不能为空！", "提示");
                        textBox4.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("客户不能为空！", "提示");
                    textBox3.Focus();
                }
            }
            else
            {
                MessageBox.Show("生产单号不能为空！", "提示");
                textBox2.Focus();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < textBox4.Text.Length; i++)
            {
                if (textBox4.Text[i] >= '0' && textBox4.Text[i] <= '9')
                {
                }
                else
                {
                    MessageBox.Show("只允许输入数字，请重新输入！", "提示");
                    textBox4.Text = "";
                }
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < textBox9.Text.Length; i++)
            {
                if (textBox9.Text[i] >= '0' && textBox9.Text[i] <= '9')
                {
                }
                else
                {
                    MessageBox.Show("只允许输入数字，请重新输入！", "提示");
                    textBox9.Text = "";
                }
            }
        }
    }
}