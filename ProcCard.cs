using System;
using System.Data;
using System.Transactions;
using System.Windows.Forms;
using LYH.WorkOrder.Properties;
using LYH.WorkOrder.share;

namespace LYH.WorkOrder
{
    public class ProcCard
    {
        public string GetProcCardNo()
        {
            var orderId = DateTime.Now.ToString("yyMM").Substring(1, 1);
            var noStart = orderId + "000001";
            var noEnd = orderId + "999999";
            var sql = $"SELECT Max(zling) FROM udstr WHERE zling>={noStart} AND zling<={noEnd}";
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

        public bool Insert(string wONo)
        {
            var sql = $"SELECT TOP 1 * FROM udone WHERE sgdhao='{wONo}'";
            var dataReader = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            if (dataReader.HasRows)
            {
                dataReader.Read();
                var pONo = dataReader["ddhao"].ToString().Trim();
                var cust = dataReader["kehu"].ToString().Trim();
                var planDate = dataReader["jhwxri"].ToString().Trim();
                var prtDwgNo = dataReader["tuhao"].ToString().Trim();
                var prtName = dataReader["name"].ToString().Trim();
                var pageNo = dataReader["yema"].ToString().Trim();
                var meatrial = dataReader["cailiao"].ToString().Trim();
                var qty = dataReader["sulia"].ToString().Trim();
                dataReader.Close();

                sql = "SELECT xuhaoone '工序号',xuhaoname '工序名称',xuhaotwo '加工工序',zuone '调机时间',zutwo '单件时间'," +
                      "xuj1 '序价',xuj2 '公式',xuj3 '补助' FROM udtwo a LEFT JOIN udone b ON b.tuhao=a.tuhao " +
                      $"AND b.DeptId = a.DeptId WHERE b.sgdhao='{wONo}' AND a.DeptId ='{SqlHelper.DeptId}'";
                var ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);

                using (var tran = new TransactionScope())
                {
                    try
                    {
                        SqlHelper.InstructionNo = new ProcCard().GetProcCardNo();
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
                            if ((craftSeq == "") || (processCardSeq == ""))
                                continue;
                            var strSql =
                                "insert into udstr(zling,sgdhao,ddhao,kehu,jhqi,tuhao,name,yema,suliang,cailiao,gxone," +
                                $"gxname,gxtwo,tiao,danjian,xuj,gongsi,buzu,cjriqi,cjren) values('{SqlHelper.InstructionNo}','{wONo}','{pONo}','{cust}','{planDate}','{prtDwgNo}','{prtName}'," +
                                $"'{pageNo}','{qty}','{meatrial}','{craftSeq}','{craft}','{processCardSeq}','{debugTime}','{singleProcTime}','{processUPrice}','{formula}','{subsidy}','{DateTime.Now}','{SqlHelper.UserName}');";

                            strSql +=
                                "insert into udktr(zling,sgdhao,ddhao,kehu,jhqi,tuhao,name,yema,suliang,cailiao,gxone," +
                                $"gxname,gxtwo,tiao,danjian,xuj,gongsi,buzu,cjriqi,cjren) values('{SqlHelper.InstructionNo}','{wONo}','{pONo}','{cust}','{planDate}','{prtDwgNo}','{prtName}'," +
                                $"'{pageNo}','{qty}','{meatrial}','{craftSeq}','{craft}','{processCardSeq}','{debugTime}','{singleProcTime}','{processUPrice}','{formula}','{subsidy}','{DateTime.Now}','{SqlHelper.UserName}');";
                            strSql += $"UPDATE udone set beione='1' WHERE sgdhao='{wONo}'";
                            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, strSql);
                        }
                        MessageBox.Show(@"保存成功", Resources.T提示);
                    }
                    catch (Exception ex)
                    {
                        tran.Dispose();
                        MessageBox.Show(ex.Message);
                    }
                    tran.Complete();
                    return true;
                }
            }
            return false;
        }
    }
}