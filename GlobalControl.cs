using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LYH.Framework.BaseUI;
using LYH.Framework.Commons;
using LYH.Framework.ControlUtil;
using LYH.Security.BLL;
using LYH.Security.Entity;
using LYH.Security.UI;
using LYH.WorkOrder.Properties;
using Microsoft.Win32;
using MyConstants = LYH.Security.MyConstants;

namespace LYH.WorkOrder
{
    public class GlobalControl
    {
        public string GAppMsgboxTitle = string.Empty;
        public string GAppUnit = string.Empty;
        public LicenseCheckResult LicenseResult = new LicenseCheckResult();
        [CompilerGenerated]
        private List<RoleInfo> _roleInfos;
        [CompilerGenerated]
        private UserInfo _userInfo;
        public string AppName = string.Empty;
        public string AppUnit = string.Empty;
        public string AppWholeName = "EMS";
        [CompilerGenerated]
        private bool _registed;
        [CompilerGenerated]
        private DateTime _firstRunTime;
        public bool EnableRegister = true;
        public Dictionary<string, string> FunctionDict = new Dictionary<string, string>();
        [CompilerGenerated]
        private int _daysLeft;
        [CompilerGenerated]
        private List<DateTime> _timeList;
        public LoginUserInfo LoginUserInfo = null;
        public MainForm MainDialog;
        public string SystemType = "EMS";
        public List<CListItem> Pid = new List<CListItem>();

        public void About()
        {
            new AboutBox { StartPosition = FormStartPosition.CenterScreen }.ShowDialog();
        }

        /// <summary>
        ///     调用非对称加密方式对序列号进行验证
        /// </summary>
        /// <param name="serialNumber">正确的序列号</param>
        /// <returns>如果成功返回True，否则为False</returns>
        public RegisterResult Register(string serialNumber)
        {
            var result = new RegisterResult();
            var hardNumber = HardwareInfoHelper.GetCpuId();
            try
            {
                var str2 = EncodeHelper.DesDecrypt(serialNumber);
                if (!string.IsNullOrEmpty(str2))
                {
                    var encrytedString = str2.Split('|')[0];
                    var time = Convert.ToDateTime(EncodeHelper.DesDecrypt(str2.Split('|')[1]));
                    var flag2 = RsaSecurityHelper.Validate(hardNumber, encrytedString);
                    var flag = time.AddDays(1.0) > DateTime.Now;
                    result.IsValid = flag2 && flag;
                    result.ValideTo = time;
                    return result;
                }
            }
            catch
            {
                // ignored
            }
            return result;
        }

        /// <summary>    
        /// 每次程序运行时候,检查用户是否注册    
        /// </summary>    
        /// <returns>如果用户已经注册, 那么返回True, 否则为False</returns> 
        public bool CheckRegister()
        {
            // 先获取用户的注册码进行比较   
            var key = Registry.CurrentUser.OpenSubKey(UiConstants.SoftwareRegistryKey, true);
            if (null == key) return false;
            var serialNumber = (string)key.GetValue("SerialNumber");
            var isValid = Program.Gc.Register(serialNumber).IsValid;
            Program.Gc.Registed = isValid;
            return isValid;
        }

        public bool CheckTimeString()
        {
            var strArray = IsolatedStorageHelper.GetDataTime().Split(';');
            var now = DateTime.Now;
            TimeList = new List<DateTime>();
            var index = 0;
            while (true)
            {
                DateTime time2;
                if (index >= strArray.Length)
                {
                    var time3 = DateTime.Now;
                    if (time3 < now)
                    {
                        MessageDxUtil.ShowWarning("对不起，您在本软件的试用期内不可以修改系统日期。\r\n如果您想继续使用本软件，请您恢复系统日期。谢谢合作");
                        return false;
                    }
                    var span = new TimeSpan(time3.Ticks - now.Ticks);
                    if (!(Registed || (span.Days <= UiConstants.SoftwareProbationDay)))
                    {
                        MessageDxUtil.ShowYesNoAndTips("您使用本软件已经过了试用期，如果您想继续使用本软件，请您联系我们。");
                        return false;
                    }
                    IsolatedStorageHelper.SaveDataTime();
                    return true;
                }
                try
                {
                    time2 = Convert.ToDateTime(strArray[index]);
                }
                catch
                {
                    time2 = DateTime.Now.AddMinutes(-1.0);
                }
                if (index == 0)
                {
                    now = time2;
                    FirstRunTime = now;
                }
                TimeList.Add(time2);
                index++;
            }
        }

        public LoginUserInfo ConvertToLoginUser(UserInfo info)
        {
            return new LoginUserInfo { ID = info.ID, Name = info.Name, FullName = info.FullName, IdentityCard = info.IdentityCard, MobilePhone = info.MobilePhone, QQ = info.QQ, Email = info.Email, Gender = info.Gender, DeptId = info.Dept_ID, CompanyId = info.Company_ID };
        }

        public LicenseCheckResult CheckLicense()
        {
            var result = new LicenseCheckResult();
            var license = MyConstants.License;
            if (!string.IsNullOrEmpty(license))
            {
                try
                {
                    var strArray = Base64Util.Decrypt(MD5Util.RemoveMD5Profix(license)).Split('|');
                    if (strArray.Length < 4)
                    {
                        return result;
                    }
                    var str3 = strArray[0];
                    if (str3.ToLower() == "lwx.security")
                    {
                        result.IsValided = true;
                    }
                    result.Username = strArray[1];
                    result.CompanyName = strArray[2];
                    try
                    {
                        result.DisplayCopyright = Convert.ToBoolean(strArray[3]);
                    }
                    catch
                    {
                        result.DisplayCopyright = true;
                    }
                    LicenseResult = result;
                }
                catch
                {
                    // ignored
                }
            }
            return result;
        }

        public bool DataCanManage(object companyId)
        {
            var flag = false;
            if (UserInRole("超级管理员"))
            {
                return true;
            }
            if (UserInRole("系统管理员"))
            {
                flag = UserInfo.Company_ID == companyId.ToString();
            }
            return flag;
        }

        public int GetImageIndex(string category)
        {
            
            {
                var num = 0;
                if (category == OUCategoryEnum.公司.ToString())
                {
                    return 1;
                }
                if (category != OUCategoryEnum.部门.ToString())
                {
                    if (category == OUCategoryEnum.工作组.ToString())
                    {
                        num = 3;
                    }
                    return num;
                }
            }
            return 2;
        }

        public List<OUInfo> GetMyTopGroup()
        {
            var list = new List<OUInfo>();
            if (UserInRole("超级管理员"))
            {
                list.AddRange(BLLFactory<OU>.Instance.GetTopGroup());
                return list;
            }
            var item = BLLFactory<OU>.Instance.FindById(UserInfo.Company_ID);
            list.Add(item);
            return list;
        }

        public void Help()
        {
            try
            {
                Process.Start("Help.chm");
            }
            catch (Exception)
            {
                MessageBox.Show(@"文件打开失败", Resources.J警告, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void Quit()
        {
            Application.Exit();
        }

        public bool UserInRole(string roleName)
        {
            if (RoleList != null)
            {
                return RoleList.Any(info => info.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
            }
            return false;
        }

        public List<RoleInfo> RoleList
        {
            [CompilerGenerated]
            get
            {
                return _roleInfos;
            }
            [CompilerGenerated]
            set
            {
                _roleInfos = value;
            }
        }

        public UserInfo UserInfo
        {
            [CompilerGenerated]
            get
            {
                return _userInfo;
            }
            [CompilerGenerated]
            set
            {
                _userInfo = value;
            }
        }

        public bool Registed
        {
            [CompilerGenerated]
            get
            {
                return _registed;
            }
            [CompilerGenerated]
            set
            {
                _registed = value;
            }
        }

        public DateTime FirstRunTime
        {
            [CompilerGenerated]
            get
            {
                return _firstRunTime;
            }
            [CompilerGenerated]
            set
            {
                _firstRunTime = value;
            }
        }

        public int DaysLeft
        {
            [CompilerGenerated]
            get
            {
                return _daysLeft;
            }
            [CompilerGenerated]
            set
            {
                _daysLeft = value;
            }
        }

        public List<DateTime> TimeList
        {
            [CompilerGenerated]
            get
            {
                return _timeList;
            }
            [CompilerGenerated]
            set
            {
                _timeList = value;
            }
        }
    }

    public class RegisterResult
    {
        // Fields
        public bool IsValid;
        public DateTime ValideTo;

        // Methods
        public RegisterResult()
        {
            ValideTo = DateTime.Now.AddYears(-1);
        }
    }
}

