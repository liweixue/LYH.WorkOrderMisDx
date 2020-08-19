using System;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace LYH.WorkOrder.share
{
    /// <summary>
    ///     常用字符串操作工具类
    /// </summary>
    public class StringHelper
    {
        private static readonly Regex RegexBr = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);

        public static Regex RegexFont = new Regex(@"<font color=" + "\".*?\"" + @">([\s\S]+?)</font>",
            GetRegexCompiledOptions());

        private static readonly Regex RegNumber = new Regex("^[0-9]+$");
        private static readonly Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static readonly Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static readonly Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //^[+-]?\d+[.]?\d+$

        private static readonly Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");
        //w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样

        private static readonly Regex RegChzn = new Regex("[\u4e00-\u9fa5]");

        /// <summary>
        ///     根据阿拉伯数字返回月份的名称(可更改为某种语言)
        /// </summary>
        public static string[] Monthes
        {
            get
            {
                return new[]
                {
                    "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
                    "November", "December"
                };
            }
        }

        /// <summary>
        ///     得到正则编译参数设置
        /// </summary>
        /// <returns></returns>
        public static RegexOptions GetRegexCompiledOptions()
        {
            return RegexOptions.None;
        }

        /// <summary>
        ///     返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns></returns>
        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        /// <summary>
        ///     判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (var i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                    {
                        return i;
                    }
                }
                else
                {
                    if (strSearch == stringArray[i])
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        ///     判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            return GetInArrayID(strSearch, stringArray, true);
        }

        /// <summary>
        ///     判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return GetInArrayID(strSearch, stringArray, caseInsensetive) >= 0;
        }

        /// <summary>
        ///     判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">字符串数组</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string[] stringarray)
        {
            return InArray(str, stringarray, false);
        }

        /// <summary>
        ///     判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray)
        {
            return InArray(str, SplitString(stringarray, ","), false);
        }

        /// <summary>
        ///     判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit)
        {
            return InArray(str, SplitString(stringarray, strsplit), false);
        }

        /// <summary>
        ///     判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit, bool caseInsensetive)
        {
            return InArray(str, SplitString(stringarray, strsplit), caseInsensetive);
        }

        /// <summary>
        ///     分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (strContent.IndexOf(strSplit, StringComparison.Ordinal) < 0)
            {
                string[] tmp = {strContent};
                return tmp;
            }
            return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
        }

        /// <summary>
        ///     分割字符串
        /// </summary>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, int p3)
        {
            var result = new string[p3];

            var splited = SplitString(strContent, strSplit);

            for (var i = 0; i < p3; i++)
            {
                if (i < splited.Length)
                    result[i] = splited[i];
                else
                    result[i] = string.Empty;
            }

            return result;
        }

        /// <summary>
        ///     删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RTrim(string str)
        {
            for (var i = str.Length; i >= 0; i--)
            {
                if (str[i].ToString().Equals(" ") || str[i].ToString().Equals("\r") || str[i].ToString().Equals("\n"))
                {
                    str = str.Remove(i, 1);
                }
            }
            return str;
        }

        /// <summary>
        ///     清除给定字符串中的回车及换行符
        /// </summary>
        /// <param name="str">要清除的字符串</param>
        /// <returns>清除后返回的字符串</returns>
        public static string ClearBr(string str)
        {
            //Regex r = null;
            Match m;

            //r = new Regex(@"(\r\n)",RegexOptions.IgnoreCase);
            for (m = RegexBr.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "");
            }

            return str;
        }

        /// <summary>
        ///     从字符串的指定位置截取指定长度的子字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length = length*-1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex = startIndex - length;
                    }
                }


                if (startIndex > str.Length)
                {
                    return "";
                }
            }
            else
            {
                if (length < 0)
                {
                    return "";
                }
                if (length + startIndex > 0)
                {
                    length = length + startIndex;
                    startIndex = 0;
                }
                else
                {
                    return "";
                }
            }

            if (str.Length - startIndex < length)
            {
                length = str.Length - startIndex;
            }

            return str.Substring(startIndex, length);
        }

        /// <summary>
        ///     从字符串的指定位置开始截取到字符串结尾的字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex)
        {
            return CutString(str, startIndex, str.Length);
        }

        /// <summary>
        ///     获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }

        /// <summary>
        ///     返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filename)
        {
            return File.Exists(filename);
        }

        /// <summary>
        ///     以指定的ContentType输出指定文件文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="filename">输出的文件名</param>
        /// <param name="filetype">将文件输出时设置的ContentType</param>
        public static void ResponseFile(string filepath, string filename, string filetype)
        {
            Stream iStream = null;

            // 缓冲区为10k
            var buffer = new Byte[10000];

            try
            {
                // 打开文件
                iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                // 需要读的数据长度
                var dataToRead = iStream.Length;

                HttpContext.Current.Response.ContentType = filetype;
                HttpContext.Current.Response.AddHeader("Content-Disposition",
                    "attachment;filename=" + UrlEncode(filename.Trim()).Replace("+", " "));

                while (dataToRead > 0)
                {
                    // 检查客户端是否还处于连接状态
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        // 文件长度
                        var length = iStream.Read(buffer, 0, 10000);
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        // 如果不再连接则跳出死循环
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    // 关闭文件
                    iStream.Close();
                }
            }
            HttpContext.Current.Response.End();
        }

        /// <summary>
        ///     判断文件名是否为浏览器可以直接显示的图片文件名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否可以直接显示</returns>
        public static bool IsImgFilename(string filename)
        {
            filename = filename.Trim();
            if (filename.EndsWith(".") || filename.IndexOf(".", StringComparison.Ordinal) == -1)
            {
                return false;
            }
            var extname = filename.Substring(filename.LastIndexOf(".", StringComparison.Ordinal) + 1).ToLower();
            return (extname == "jpg" || extname == "jpeg" || extname == "png" || extname == "bmp" || extname == "gif");
        }

        /// <summary>
        ///     int型转换为string型
        /// </summary>
        /// <returns>转换后的string类型结果</returns>
        public static string IntToStr(int intValue)
        {
            return Convert.ToString(intValue);
        }

        /// <summary>
        ///     MD5函数(加密方法)
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string Md5(string str)
        {
            var b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            return b.Aggregate("", (current, t) => current + t.ToString("x").PadLeft(2, '0'));
        }

        /// <summary>
        ///     SHA256函数(加密方法，该加密算法比MD5更强大)
        /// </summary>
        /// ///
        /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果</returns>
        public static string Sha256(string str)
        {
            var sha256Data = Encoding.UTF8.GetBytes(str);
            var sha256 = new SHA256Managed();
            var result = sha256.ComputeHash(sha256Data);
            return Convert.ToBase64String(result); //返回长度为44字节的字符串
        }

        /// <summary>
        ///     字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="pSrcString">要检查的字符串</param>
        /// <param name="pLength">指定长度</param>
        /// <param name="pTailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string pSrcString, int pLength, string pTailString)
        {
            return GetSubString(pSrcString, 0, pLength, pTailString);
        }

        /// <summary>
        ///     取指定长度的字符串
        /// </summary>
        /// <param name="pSrcString">要检查的字符串</param>
        /// <param name="pStartIndex">起始位置</param>
        /// <param name="pLength">指定长度</param>
        /// <param name="pTailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string pSrcString, int pStartIndex, int pLength, string pTailString)
        {
            var myResult = pSrcString;

            //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
            if (Regex.IsMatch(pSrcString, "[\u0800-\u4e00]+") ||
                Regex.IsMatch(pSrcString, "[\xAC00-\xD7A3]+"))
            {
                //当截取的起始位置超出字段串长度时
                if (pStartIndex >= pSrcString.Length)
                {
                    return "";
                }
                return pSrcString.Substring(pStartIndex,
                    ((pLength + pStartIndex) > pSrcString.Length) ? (pSrcString.Length - pStartIndex) : pLength);
            }


            if (pLength >= 0)
            {
                var bsSrcString = Encoding.Default.GetBytes(pSrcString);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > pStartIndex)
                {
                    var pEndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (pStartIndex + pLength))
                    {
                        pEndIndex = pLength + pStartIndex;
                    }
                    else
                    {
                        //当不在有效范围内时,只取到字符串的结尾

                        pLength = bsSrcString.Length - pStartIndex;
                        pTailString = "";
                    }


                    var nRealLength = pLength;
                    var anResultFlag = new int[pLength];

                    var nFlag = 0;
                    for (var i = pStartIndex; i < pEndIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[pEndIndex - 1] > 127) && (anResultFlag[pLength - 1] == 1))
                    {
                        nRealLength = pLength + 1;
                    }

                    var bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, pStartIndex, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);

                    myResult = myResult + pTailString;
                }
            }

            return myResult;
        }

        /// <summary>
        ///     自定义的替换字符串函数
        /// </summary>
        public static string ReplaceString(string sourceString, string searchString, string replaceString,
            bool isCaseInsensetive)
        {
            return Regex.Replace(sourceString, Regex.Escape(searchString), replaceString,
                isCaseInsensetive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        /// <summary>
        ///     生成指定数量的html空格符号
        /// </summary>
        public static string Spaces(int nSpaces)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < nSpaces; i++)
            {
                sb.Append(" &nbsp;&nbsp;");
            }
            return sb.ToString();
        }

        /// <summary>
        ///     检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail,
                @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static bool IsValidDoEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail,
                @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        ///     检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsUrl(string strUrl)
        {
            return Regex.IsMatch(strUrl,
                @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        /// <summary>
        ///     获取Email主机名(如:abc@163.com的主机是163.com)
        /// </summary>
        /// <param name="strEmail"></param>
        /// <returns></returns>
        public static string GetEmailHostName(string strEmail)
        {
            if (strEmail.IndexOf("@", StringComparison.Ordinal) < 0)
            {
                return "";
            }
            return strEmail.Substring(strEmail.LastIndexOf("@", StringComparison.Ordinal)).ToLower();
        }

        /// <summary>
        ///     判断是否为base64字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64String(string str)
        {
            //A-Z, a-z, 0-9, +, /, =
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }

        /// <summary>
        ///     检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        ///     检测是否有危险的可能用于链接的字符串
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|游客|^Guest");
        }

        /// <summary>
        ///     清理字符串
        /// </summary>
        public static string CleanInput(string strIn)
        {
            return Regex.Replace(strIn.Trim(), @"[^\w\.@-]", "");
        }

        /// <summary>
        ///     返回URL中结尾的参数名
        /// </summary>
        public static string GetFilename(string url)
        {
            if (url == null)
            {
                return "";
            }
            var strs1 = url.Split('/');
            return strs1[strs1.Length - 1].Split('?')[0];
        }

        /// <summary>
        ///     返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        ///     替换回车换行符为html换行符
        /// </summary>
        public static string StrFormat(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("\r\n", "<br />");
                str = str.Replace("\n", "<br />");
                str2 = str;
            }
            return str2;
        }

        /// <summary>
        ///     返回标准日期格式string
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        ///     返回指定日期格式
        /// </summary>
        public static string GetDate(string datetimestr, string replacestr)
        {
            if (datetimestr == null)
            {
                return replacestr;
            }

            if (datetimestr.Equals(""))
            {
                return replacestr;
            }

            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;
        }

        /// <summary>
        ///     返回标准时间格式string
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        ///     返回标准时间格式string
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        ///     返回标准时间格式string(包含微妙)
        /// </summary>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }

        /// <summary>
        ///     返回相对于当前时间的相对天数
        /// </summary>
        public static string GetDateTime(int relativeday)
        {
            return DateTime.Now.AddDays(relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        ///     返回标准时间
        /// </summary>
        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
            {
                return fDateTime;
            }
            var s = Convert.ToDateTime(fDateTime);
            return s.ToString(formatStr);
        }

        /// <summary>
        ///     返回标准时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        ///     返回是不是有效的时间格式
        /// </summary>
        /// <returns></returns>
        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }

        /// <summary>
        ///     获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIp()
        {
            var result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (string.IsNullOrEmpty(result) || !IsIp(result))
            {
                return "0.0.0.0";
            }

            return result;
        }

        /// <summary>
        ///     是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIp(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        ///     改正sql语句中的转义字符
        /// </summary>
        public static string MashSql(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("\'", "'");
                str2 = str;
            }
            return str2;
        }

        /// <summary>
        ///     替换sql语句中的有问题符号
        /// </summary>
        public static string ChkSql(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("'", "''");
                str2 = str;
            }
            return str2;
        }

        /// <summary>
        ///     转换为静态html
        /// </summary>
        public void TransHtml(string path, string outpath)
        {
            var page = new Page();
            var writer = new StringWriter();
            page.Server.Execute(path, writer);
            FileStream fs;
            if (File.Exists($"{page.Server.MapPath("")}\\{outpath}"))
            {
                File.Delete($"{page.Server.MapPath("")}\\{outpath}");
                fs = File.Create($"{page.Server.MapPath("")}\\{outpath}");
            }
            else
            {
                fs = File.Create($"{page.Server.MapPath("")}\\{outpath}");
            }
            var bt = Encoding.Default.GetBytes(writer.ToString());
            fs.Write(bt, 0, bt.Length);
            fs.Close();
        }

        /// <summary>
        ///     转换为简体中文
        /// </summary>
        public static string ToSChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.SimplifiedChinese);
        }

        /// <summary>
        ///     转换为繁体中文
        /// </summary>
        public static string ToTChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.TraditionalChinese);
        }

        /// <summary>
        ///     替换html字符(只替换,';三种符号)
        /// </summary>
        public static string EncodeHtml(string strHtml)
        {
            if (strHtml != "")
            {
                strHtml = strHtml.Replace(",", "&def");
                strHtml = strHtml.Replace("'", "&dot");
                strHtml = strHtml.Replace(";", "&dec");
                return strHtml;
            }
            return "";
        }

        /// <summary>
        ///     进行指定的替换(脏字过滤,有多个脏字的用"&"符号隔开)
        /// </summary>
        public static string StrFilter(string str, string bantext)
        {
            var textArray1 = SplitString(bantext, "&");
            foreach (var txtStr in textArray1)
            {
                var text1 = txtStr.Substring(0, txtStr.IndexOf("=", StringComparison.Ordinal));
                var text2 = txtStr.Substring(txtStr.IndexOf("=", StringComparison.Ordinal) + 1);
                str = str.Replace(text1, text2);
            }
            return str;
        }

        /// <summary>
        ///     返回 HTML 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        ///     返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        ///     返回 URL 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        /// <summary>
        ///     返回指定目录下的非 UTF8 字符集文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>文件名的字符串数组</returns>
        public static string[] FindNoUtf8File(string path)
        {
            //System.IO.StreamReader reader = null;
            var filelist = new StringBuilder();
            var folder = new DirectoryInfo(path);
            //System.IO.DirectoryInfo[] subFolders = Folder.GetDirectories(); 
            /*
        for (int i=0;i<subFolders.Length;i++) 
        { 
            FindNoUTF8File(subFolders[i].FullName); 
        }
        */
            var subFiles = folder.GetFiles();
            foreach (var fileInfo in subFiles)
            {
                if (fileInfo.Extension.ToLower().Equals(".htm"))
                {
                    var fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
                    var bUtf8 = IsUtf8(fs);
                    fs.Close();
                    if (!bUtf8)
                    {
                        filelist.Append(fileInfo.FullName);
                        filelist.Append("\r\n");
                    }
                }
            }
            return SplitString(filelist.ToString(), "\r\n");
        }

        /// <summary>
        ///     判断文件流是否为UTF8字符集
        /// </summary>
        /// <param name="sbInputStream">文件流</param>
        /// <returns>判断结果</returns>
        private static bool IsUtf8(FileStream sbInputStream)
        {
            int i;
            byte cOctets = 0; // octets to go in this UTF-8 encoded character 
            var bAllAscii = true;
            var iLen = sbInputStream.Length;
            for (i = 0; i < iLen; i++)
            {
                var chr = (byte) sbInputStream.ReadByte();

                if ((chr & 0x80) != 0) bAllAscii = false;

                if (cOctets == 0)
                {
                    if (chr >= 0x80)
                    {
                        do
                        {
                            chr <<= 1;
                            cOctets++;
                        } while ((chr & 0x80) != 0);

                        cOctets--;
                        if (cOctets == 0) return false;
                    }
                }
                else
                {
                    if ((chr & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    cOctets--;
                }
            }

            if (cOctets > 0)
            {
                return false;
            }

            if (bAllAscii)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     格式化字节数字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatBytesStr(int bytes)
        {
            if (bytes > 1073741824)
            {
                return ((double) bytes/1073741824).ToString("0") + "G";
            }
            if (bytes > 1048576)
            {
                return ((double) bytes/1048576).ToString("0") + "M";
            }
            if (bytes > 1024)
            {
                return ((double) bytes/1024).ToString("0") + "K";
            }
            return bytes + "Bytes";
        }

        /// <summary>
        ///     将long型数值转换为Int32类型
        /// </summary>
        /// <param name="objNum"></param>
        /// <returns></returns>
        public static int SafeInt32(object objNum)
        {
            if (objNum == null)
            {
                return 0;
            }
            var strNum = objNum.ToString();
            if (IsNumeric(strNum))
            {
                if (strNum.Length > 9)
                {
                    if (strNum.StartsWith("-"))
                    {
                        return int.MinValue;
                    }
                    return int.MaxValue;
                }
                return Int32.Parse(strNum);
            }
            return 0;
        }

        /// <summary>
        ///     判断对象是否为数字
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object expression)
        {
            if (expression != null)
            {
                var str = expression.ToString();
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') ||
                        (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        ///     判断是不是Double类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsDouble(object expression)
        {
            if (expression != null)
            {
                return Regex.IsMatch(expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");
            }
            return false;
        }

        /// <summary>
        ///     string型转换为bool型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(object expression, bool defValue)
        {
            if (expression == null) return defValue;
            if (String.Compare(expression.ToString(), "true", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return true;
            }
            if (String.Compare(expression.ToString(), "false", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return false;
            }
            return defValue;
        }

        /// <summary>
        ///     将对象转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(object expression, int defValue)
        {
            if (expression != null)
            {
                var str = expression.ToString();
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') ||
                        (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return Convert.ToInt32(str);
                    }
                }
            }
            return defValue;
        }

        /// <summary>
        ///     string型转换为decimal型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static decimal StrToFloat(object strValue, decimal defValue)
        {
            if ((strValue == null) || (strValue.ToString().Length > 10))
            {
                return defValue;
            }

            var intValue = defValue;
            var isFloat = Regex.IsMatch(strValue.ToString(), @"^([-]|[0-9])[0-9]*(\.\w*)?$");
            if (isFloat)
            {
                intValue = Convert.ToDecimal(strValue);
            }
            return intValue;
        }

        /// <summary>
        ///     判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            return strNumber.All(IsNumeric);
        }

        /// <summary>
        ///     返回相差的秒数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        public static int StrDateDiffSeconds(string time, int sec)
        {
            var ts = DateTime.Now - DateTime.Parse(time).AddSeconds(sec);
            if (ts.TotalSeconds > int.MaxValue)
            {
                return int.MaxValue;
            }
            if (ts.TotalSeconds < int.MinValue)
            {
                return int.MinValue;
            }
            return (int) ts.TotalSeconds;
        }

        /// <summary>
        ///     返回相差的分钟数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static int StrDateDiffMinutes(string time, int minutes)
        {
            if (string.IsNullOrEmpty(time))
                return 1;
            var ts = DateTime.Now - DateTime.Parse(time).AddMinutes(minutes);
            if (ts.TotalMinutes > int.MaxValue)
            {
                return int.MaxValue;
            }
            if (ts.TotalMinutes < int.MinValue)
            {
                return int.MinValue;
            }
            return (int) ts.TotalMinutes;
        }

        /// <summary>
        ///     返回相差的小时数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public static int StrDateDiffHours(string time, int hours)
        {
            if (string.IsNullOrEmpty(time))
                return 1;
            var ts = DateTime.Now - DateTime.Parse(time).AddHours(hours);
            if (ts.TotalHours > int.MaxValue)
            {
                return int.MaxValue;
            }
            if (ts.TotalHours < int.MinValue)
            {
                return int.MinValue;
            }
            return (int) ts.TotalHours;
        }

        /// <summary>
        ///     从HTML中获取文本,保留br,p,img
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string GetTextFromHtml(string html)
        {
            var regEx = new Regex(@"</?(?!br|/?p|img)[^>]*>", RegexOptions.IgnoreCase);

            return regEx.Replace(html, "");
        }

        /// <summary>
        ///     验证是否为正整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {
            return Regex.IsMatch(str, @"^[0-9]*$");
        }

        /// <summary>
        ///     删除最后一个字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearLastChar(string str)
        {
            if (str == "")
                return "";
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        ///     备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="overwrite">当目标文件存在时是否覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            if (!overwrite && File.Exists(destFileName))
            {
                return false;
            }
            File.Copy(sourceFileName, destFileName, true);
            return true;
        }

        /// <summary>
        ///     备份文件,当目标文件存在时覆盖
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }

        /// <summary>
        ///     拷贝文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要拷贝的文件名</param>
        /// <param name="backupTargetFileName">要拷贝文件再次备份的名称,如果为null,则不再备份拷贝文件</param>
        /// <returns>操作是否成功</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            if (!File.Exists(backupFileName))
            {
                throw new FileNotFoundException(backupFileName + "文件不存在！");
            }
            if (backupTargetFileName != null)
            {
                if (!File.Exists(targetFileName))
                {
                    throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                }
                File.Copy(targetFileName, backupTargetFileName, true);
            }
            File.Delete(targetFileName);
            File.Copy(backupFileName, targetFileName);
            return true;
        }

        /// <summary>
        ///     拷贝文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要拷贝的文件名</param>
        /// <returns>操作结果</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return RestoreFile(backupFileName, targetFileName, null);
        }

        /// <summary>
        ///     将全角数字转换为数字
        /// </summary>
        /// <param name="sbcCase"></param>
        /// <returns></returns>
        public static string SbcCaseToNumberic(string sbcCase)
        {
            var c = sbcCase.ToCharArray();
            for (var i = 0; i < c.Length; i++)
            {
                var b = Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte) (b[0] + 32);
                        b[1] = 0;
                        c[i] = Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }

        /// <summary>
        ///     将字符串转换为Color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ToColor(string color)
        {
            int red, green, blue;
            char[] rgb;
            color = color.TrimStart('#');
            color = Regex.Replace(color.ToLower(), "[g-zG-Z]", "");
            switch (color.Length)
            {
                case 3:
                    rgb = color.ToCharArray();
                    red = Convert.ToInt32(rgb[0].ToString() + rgb[0], 16);
                    green = Convert.ToInt32(rgb[1].ToString() + rgb[1], 16);
                    blue = Convert.ToInt32(rgb[2].ToString() + rgb[2], 16);
                    return Color.FromArgb(red, green, blue);
                case 6:
                    rgb = color.ToCharArray();
                    red = Convert.ToInt32(rgb[0] + rgb[1].ToString(), 16);
                    green = Convert.ToInt32(rgb[2] + rgb[3].ToString(), 16);
                    blue = Convert.ToInt32(rgb[4] + rgb[5].ToString(), 16);
                    return Color.FromArgb(red, green, blue);
                default:
                    return Color.FromName(color);
            }
        }

        /// <summary>
        ///     检查字符串是否为空（null或者长度为0）
        /// </summary>
        /// <param name="argName">字符串名</param>
        /// <param name="argValue">被检查的字符串</param>
        /// <param name="throwError">为空时是否抛出异常</param>
        /// <returns>为空则返回true</returns>
        public static bool CheckEmptyString(string argName, string argValue, bool throwError)
        {
            CheckArgumentNull("argName", argName, true);
            var ret = string.IsNullOrEmpty(argValue);
            if (ret && throwError)
            {
                throw new ArgumentException(@"字符串为空", argName);
            }
            return ret;
        }

        /// <summary>
        ///     检查参数是否为空引用（null）
        /// </summary>
        /// <param name="argName">参数名</param>
        /// <param name="argValue">被检查的参数</param>
        /// <param name="throwError">为空引用时是否抛出异常</param>
        /// <returns>为空则返回true</returns>
        public static bool CheckArgumentNull(string argName, object argValue, bool throwError)
        {
            if (argName == null)
            {
                throw new ArgumentNullException(nameof(argName));
            }
            if (argValue == null)
            {
                if (throwError)
                {
                    throw new ArgumentNullException(argName);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        ///     检查数组是否为空（长度为0）
        /// </summary>
        /// <param name="argName">数组名</param>
        /// <param name="argValue">被检查的数组实例</param>
        /// <param name="throwError">为空引用时是否抛出异常</param>
        /// <returns>为空则返回true</returns>
        public static bool CheckEmptyArray(string argName, Array argValue, bool throwError)
        {
            if (argName == null)
            {
                throw new ArgumentNullException(nameof(argName));
            }
            var ret = (argValue == null || argValue.Length == 0);
            if (ret && throwError)
            {
                throw new ArgumentException(@"数组为空", argName);
            }
            return ret;
        }

        /// <summary>
        ///     判断某值是否在枚举内（位枚举）
        /// </summary>
        /// <param name="checkingValue">被检测的枚举值</param>
        /// <param name="expectedValue">期望的枚举值</param>
        /// <returns>是否包含</returns>
        public static bool CheckFlagsEnumEquals(Enum checkingValue, Enum expectedValue)
        {
            var intCheckingValue = Convert.ToInt32(checkingValue);
            var intExpectedValue = Convert.ToInt32(expectedValue);
            return (intCheckingValue & intExpectedValue) == intExpectedValue;
        }

        /// <summary>
        ///     判断Url是否相同（QueryString参数允许不同）
        /// </summary>
        /// <param name="expectedUrl">被比较的Url</param>
        /// <param name="httpRequest">HttpRequest实例</param>
        /// <returns>相同返回true</returns>
        public static bool IsUrlEquals(string expectedUrl, HttpRequest httpRequest)
        {
            if (string.IsNullOrEmpty(expectedUrl) || httpRequest == null)
            {
                return false;
            }

            var url = MakeUrlRelative(httpRequest.Url.AbsolutePath, httpRequest.ApplicationPath);
            return expectedUrl.ToLower().StartsWith(url.ToLower());
        }

        /// <summary>
        ///     把指定的Url转换成相对地址（以"~/"打头）
        /// </summary>
        /// <param name="url">被转换的Url</param>
        /// <param name="basePath">基地址（比如虚拟目录的地址：/WebApplication1）</param>
        /// <returns>转换后的相对地址</returns>
        public static string MakeUrlRelative(string url, string basePath)
        {
            if (string.IsNullOrEmpty(url))
            {
                return "~/";
            }

            if (basePath == null || !basePath.StartsWith("/"))
            {
                basePath = "/" + basePath;
            }
            if (url.StartsWith("http://", true, null))
            {
                var uri = new Uri(url);
                url = uri.PathAndQuery;
            }
            else if (!url.StartsWith("/"))
            {
                url = "/" + url;
            }
            if (basePath == "/")
            {
                return "~" + url;
            }
            basePath = basePath.ToLower();
            var url1 = url.ToLower();
            url = url.Substring(url1.IndexOf(basePath, StringComparison.Ordinal) + basePath.Length);
            if (url.StartsWith("/"))
            {
                url = "~" + url;
            }
            else
            {
                url = "~/" + url;
            }
            return url;
        }

        /// <summary>
        ///     把指定的路径信息和相对地址（以"~/"打头）合并成绝对路径（以"/"打头）
        /// </summary>
        /// <param name="basePath">基地址（比如虚拟目录的地址：/WebApplication1）</param>
        /// <param name="url">被转换的Url</param>
        /// <param name="includeQueryString">是否包含Url参数</param>
        /// <returns>转换后的绝对地址，如果url不是以"~/"打头，则返回原url</returns>
        public static string UrlCombine(string basePath, string url, bool includeQueryString)
        {
            if (basePath == null || !basePath.StartsWith("/"))
            {
                basePath = "/" + basePath;
            }
            if (basePath.Length > 1 && !basePath.EndsWith("/"))
            {
                basePath = basePath + "/";
            }
            if (string.IsNullOrEmpty(url))
            {
                return basePath;
            }
            if (url.StartsWith("~/"))
            {
                var length = url.Length - 2;
                if (!includeQueryString)
                {
                    var i = url.IndexOf('?');
                    if (i > 2)
                    {
                        length = i - 2;
                    }
                }
                url = basePath + url.Substring(2, length);
            }
            return url;
        }

        /// <summary>
        ///     名/值集合转换成QueryString形式
        /// </summary>
        /// <param name="collection">名/值集合</param>
        /// <param name="encoding">编码</param>
        /// <returns>QueryString</returns>
        public static string NameValueCollectionToQueryString(NameValueCollection collection, Encoding encoding)
        {
            var sb = new StringBuilder();
            foreach (string name in collection.Keys)
            {
                var @value = collection[name];
                sb.AppendFormat("&{0}={1}", HttpUtility.UrlEncode(name, encoding),
                    HttpUtility.UrlEncode(@value, encoding));
            }
            return sb.ToString().TrimStart('&');
        }

        /// <summary>
        ///     名/值集合转换成QueryString形式
        /// </summary>
        /// <param name="collection">名/值集合</param>
        /// <param name="encodingName">编码名称</param>
        /// <returns>QueryString</returns>
        public static string NameValueCollectionToQueryString(NameValueCollection collection, string encodingName)
        {
            return NameValueCollectionToQueryString(collection, Encoding.GetEncoding(encodingName));
        }

        #region 中文检测

        /// <summary>
        ///     检测是否有中文字符
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsHasChzn(string inputData)
        {
            var m = RegChzn.Match(inputData);
            return m.Success;
        }

        #endregion

        #region Email地址

        /// <summary>
        ///     检查Email地址
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string inputData)
        {
            var m = RegEmail.Match(inputData);
            return m.Success;
        }

        #endregion

        #region 数字字符串检查

        /// <summary>
        ///     检查Request查询字符串的键值，是否是数字，最大长度限制
        /// </summary>
        /// <param name="req">Request</param>
        /// <param name="inputKey">Request的键值</param>
        /// <param name="maxLen">最大长度</param>
        /// <returns>返回Request查询字符串</returns>
        public static string IsDigit(HttpRequest req, string inputKey, int maxLen)
        {
            var retVal = string.Empty;
            if (!string.IsNullOrEmpty(inputKey))
            {
                retVal = req.QueryString[inputKey] ?? req.Form[inputKey];
                if (null != retVal)
                {
                    retVal = SqlText(retVal, maxLen);
                    if (!IsNumber(retVal))
                        retVal = string.Empty;
                }
            }
            return retVal ?? (string.Empty);
        }

        /// <summary>
        ///     是否数字字符串,不带负数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            var m = RegNumber.Match(inputData);
            return m.Success;
        }

        /// <summary>
        ///     是否数字字符串 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            var m = RegNumberSign.Match(inputData);
            return m.Success;
        }

        /// <summary>
        ///     是否是浮点数,不带负数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            var m = RegDecimal.Match(inputData);
            return m.Success;
        }

        /// <summary>
        ///     是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            var m = RegDecimalSign.Match(inputData);
            return m.Success;
        }

        #endregion

        #region 其他

        /// <summary>
        ///     检查字符串最大长度，返回指定长度的串
        /// </summary>
        /// <param name="sqlInput">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>
        public static string SqlText(string sqlInput, int maxLength)
        {
            if (!string.IsNullOrEmpty(sqlInput))
            {
                sqlInput = sqlInput.Trim();
                if (sqlInput.Length > maxLength) //按最大长度截取字符串   
                    sqlInput = sqlInput.Substring(0, maxLength);
            }
            return sqlInput;
        }

        /// <summary>
        ///     设置Label显示Encode的字符串
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="txtInput"></param>
        public static void SetLabel(Label lbl, string txtInput)
        {
            lbl.Text = HtmlEncode(txtInput);
        }

        public static void SetLabel(Label lbl, object inputObj)
        {
            SetLabel(lbl, inputObj.ToString());
        }

        #endregion
    }
}