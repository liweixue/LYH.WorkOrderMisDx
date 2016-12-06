using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using FSLib.App.SimpleUpdater;
using LYH.Framework.BaseUI;
using LYH.Framework.BaseUI.SplashScreen;
using LYH.Framework.Commons;
using LYH.WorkOrder.Properties;
using SqlHelper = LYH.WorkOrder.share.SqlHelper;

namespace LYH.WorkOrder
{
    public static class Program
    {
        public static GlobalControl Gc = new GlobalControl();
        //private static BackgroundWorker _updateWorker;
        
        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            //SqlHelper.SaveDefaultConnectString("");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            UpdaterExt.CheckUpdate();
            
            if (args.Length > 0)
            {
                LoginByArgs(args);
            }
            else
            {
                LoginNormal();
            }

            //var log = new Logon(); 
            //使用模式对话框方法显示Log
            //log.ShowDialog(); 
            //DialogResult就是用来判断是否返回父窗体的
            //if (log.DialogResult == DialogResult.OK)
            //{
            //    Application.Run(new MainForm()); //在线程中打开主窗体
            //}
        }

        private static void LoginNormal()
        {
            var login = new Logon
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            if (DialogResult.OK == login.ShowDialog() && login.BLogin)
            {
                Splasher.Show(typeof (FrmSplash));
                Gc.MainDialog = new MainForm {StartPosition = FormStartPosition.CenterScreen};
                Application.Run(Gc.MainDialog);
            }
            login.Dispose();
        }

        /// <summary>
        ///     使用参数化登录
        /// </summary>
        /// <param name="args"></param>
        private static void LoginByArgs(string[] args)
        {
            var commandArgs = CommandLine.Parse(args);
            if (commandArgs.ArgPairs.Count <= 0) return;

            #region 获取用户参数

            var userName = string.Empty;
            var userPassword = string.Empty;
            foreach (var pair in commandArgs.ArgPairs)
            {
                if ("U".Equals(pair.Key, StringComparison.OrdinalIgnoreCase))
                {
                    userName = pair.Value;
                }
                if ("P".Equals(pair.Key, StringComparison.OrdinalIgnoreCase))
                {
                    userPassword = pair.Value;
                }
            }

            #endregion

            //if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(identity))
            //{
            //    bool bLogin = Portal.gc.LoginByIdentity(userName.Trim(), identity);
            //    if (bLogin)
            //    {
            //        ShowMainDialog();
            //    }
            //    else
            //    {
            //        LoginNormal(args);
            //    }
            //}
            //string userName = commandArgs.ArgPairs["U"];
            //string userPassword = commandArgs.ArgPairs.ContainsKey("P") ? commandArgs.ArgPairs["P"] : "";
            if (!string.IsNullOrEmpty(userName))
            {
                //var localIp = NetworkUtil.GetLocalIP();
                //if (localIp == null) throw new ArgumentNullException("args");
                //var macAddress = HardwareInfoHelper.GetMacAddress();
                //if (macAddress == null) throw new ArgumentNullException("args");
                //if (
                //    !string.IsNullOrEmpty(BLLFactory<User>.Instance.VerifyUser(userName, userPassword, Gc.AppWholeName,
                //        localIp, macAddress)))
                //{
                //    var userByName = BLLFactory<User>.Instance.GetUserByName(userName);
                //    var functionsByUser = BLLFactory<Function>.Instance.GetFunctionsByUser(userByName.ID,
                //        Gc.AppWholeName);
                //    if ((functionsByUser != null) && (functionsByUser.Count > 0))
                //    {
                //        foreach (
                //            var functionInfo in
                //                functionsByUser.Where(
                //                    functionInfo => !Gc.FunctionDict.ContainsKey(functionInfo.ControlID)))
                //        {
                //            Gc.FunctionDict.Add(functionInfo.ControlID, functionInfo.ControlID);
                //        }
                //    }
                //    Gc.UserInfo = userByName;
                //    Gc.LoginUserInfo = Gc.ConvertToLoginUser(userByName);
                //    Cache.Instance.Add("LoginUserInfo", Gc.LoginUserInfo);
                //    Cache.Instance.Add("FunctionDict", Gc.FunctionDict);
                //    //gc.ManagedWareHouse = gc.GetWareHouse(userByName);
                //    //if ((gc.ManagedWareHouse != null) && (gc.ManagedWareHouse.Count > 0))
                //    //{
                //    Splasher.Show(typeof(FrmSplash));
                //    Gc.MainDialog = new MainForm { StartPosition = FormStartPosition.CenterScreen };
                //    Application.Run(Gc.MainDialog);
                //    //}
                //    //else
                //    //{
                //    //    MessageUtil.ShowTips("用户登录信息正确，但未授权管理库房信息，请联系管理人员授权管理");
                //    //    GetAllURL(string_0);
                //    //}
                //else
                //{
                //    MessageUtil.ShowTips("用户帐号密码不正确");
                //    LoginNormal();
                //}
                var sql = $"SELECT TOP 1 * FROM IDPASS WHERE ID='{userName}'";
                try
                {
                    var dr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, sql);
                    if (dr.Read())
                    {
                        var pass = dr["pass"].ToString().Trim();
                        if (userPassword == pass)
                        {
                            SqlHelper.UserName = dr["name"].ToString().Trim();
                            SqlHelper.UserType = dr["leiq"].ToString().Trim();
                            SqlHelper.DeptId = dr["DeptId"].ToString().Trim();
                            SqlHelper.UserId = userName;

                            Splasher.Show(typeof (FrmSplash));
                            Gc.MainDialog = new MainForm {StartPosition = FormStartPosition.CenterScreen};
                            Application.Run(Gc.MainDialog);
                        }
                        else
                        {
                            MessageBox.Show(@"密码错误，请重新输入！", Resources.T提示);
                            LoginNormal();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"此帐号< {userName} >不存在，请重新输入!!", Resources.T提示);
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"数据库连接错误!" + ex.Message);
                }
            }
            else
            {
                MessageUtil.ShowTips("命令格式有误");
                LoginNormal();
            }
        }


        public static void StartLogin()
        {
            //BonusSkins.Register();
            //SkinManager.EnableFormSkins();
            //UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");
            var login = new Logon
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            if ((DialogResult.OK == login.ShowDialog()) && login.BLogin)
            {
                var form = new MainForm();
                Gc.MainDialog = form;
                form.ShowDialog();
            }
            login.Dispose();
            }
    }

}