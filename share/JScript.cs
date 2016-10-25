using System;
using System.Web;
using System.Web.UI;

namespace LYH.WorkOrder.share
{
    /// <summary>
    /// 一些常用的Js调用
    /// 添加新版说明：由于旧版普遍采用Response.Write(string msg)的方式输出js脚本，这种
    /// 方式输出的js脚本会在html元素的&lt;html&gt;&lt;/html&gt;标签之外，破坏了整个xhtml的结构,
    /// 而新版本则采用ClientScript.RegisterStartupScript(string msg)的方式输出，不会改变xhtml的结构,
    /// 不会影响执行效果。
    /// 为了向下兼容，所以新版本采用了重载的方式，新版本中要求一个System.Web.UI.Page类的实例。
    /// 创建时间：2006-9-13
    /// 创建者：马先光
    /// 新版作者：周公
    /// 修改日期：2007-4-17
    /// 修改版发布网址：http://blog.csdn.net/zhoufoxcn
    /// </summary>
    public class JScript
    {
        #region 旧版本
        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="js">窗口信息</param>
        public static void Alert(string message)
        {
            #region
            var js =
                $@"<Script language='JavaScript'>
                                        alert('{message}'');</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="toUrl">连接地址</param>
        public static void AlertAndRedirect(string message, string toUrl)
        {
            #region
            const string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toUrl));
            #endregion
        }

        /// <summary>
        /// 回到历史页面
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value)
        {
            #region
            const string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
            #endregion
        }

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public static void CloseWindow()
        {
            #region
            const string js = @"<Script language='JavaScript'>
                    parent.opener=null;window.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
            #endregion
        }

        /// <summary>
        /// 刷新父窗口
        /// </summary>
        public static void RefreshParent(string url)
        {
            #region
            var js =
                $@"<Script language='JavaScript'>
                                        window.opener.location.href='{url}'';window.close();</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// 刷新打开窗口
        /// </summary>
        public static void RefreshOpener()
        {
            #region
            const string js = @"<Script language='JavaScript'>
                    opener.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// 打开指定大小的新窗体
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="width">宽</param>
        /// <param name="heigth">高</param>
        /// <param name="top">头位置</param>
        /// <param name="left">左位置</param>
        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
        {
            #region
            var js = $@"<Script language='JavaScript'>window.open('{url}','','height={heigth},width={width}," +
                     $"TOP={top},left={left},location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no," +
                     "toolbar=no,directories=no'');</Script>";

            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// 转向Url制定的页面
        /// </summary>
        /// <param name="url">连接地址</param>
        public static void JavaScriptLocationHref(string url)
        {
            #region
            var js = @"<Script language='JavaScript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// 打开指定大小位置的模式对话框
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="top">距离上位置</param>
        /// <param name="left">距离左位置</param>
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
        {
            #region
            var features = $"dialogWidth:{width}px;dialogHeight:{height}px;dialogLeft:{left}px;dialogTop:{top}px;" +
                           "center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features);
            #endregion
        }
        /// <summary>
        /// 弹出模态窗口
        /// </summary>
        /// <param name="webFormUrl"></param>
        /// <param name="features"></param>
        public static void ShowModalDialogWindow(string webFormUrl, string features)
        {
            var js = ShowModalDialogJavascript(webFormUrl, features);
            HttpContext.Current.Response.Write(js);
        }
        /// <summary>
        /// 弹出模态窗口
        /// </summary>
        /// <param name="webFormUrl"></param>
        /// <param name="features"></param>
        /// <returns></returns>
        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            #region
            var js =
                $@"<script language=javascript>                            
                                                showModalDialog('{webFormUrl}'','''',''{features}'');</script>";
            return js;
            #endregion
        }
        #endregion

        #region 新版本
        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="js">窗口信息</param>
        public static void Alert(string message, Page page)
        {
            #region
            var js =
                $@"<Script language='JavaScript'>
                                        alert('{message}'');</Script>";
            //HttpContext.Current.Response.Write(js);
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "alert"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "alert", js);
            }
            #endregion
        }

        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="toUrl">连接地址</param>
        public static void AlertAndRedirect(string message, string toUrl, Page page)
        {
            #region
            const string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            //HttpContext.Current.Response.Write(string.Format(js, message, toURL));
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "AlertAndRedirect"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "AlertAndRedirect", string.Format(js, message, toUrl));
            }
            #endregion
        }

        /// <summary>
        /// 回到历史页面
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value, Page page)
        {
            #region
            const string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
            //HttpContext.Current.Response.Write(string.Format(js, value));
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "GoHistory"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "GoHistory", string.Format(js, value));
            }
            #endregion
        }

        //        /// <summary>
        //        /// 关闭当前窗口
        //        /// </summary>
        //        public static void CloseWindow()
        //        {
        //            #region
        //            string js = @"<Script language='JavaScript'>
        //                    parent.opener=null;window.close();  
        //                  </Script>";
        //            HttpContext.Current.Response.Write(js);
        //            HttpContext.Current.Response.End();
        //            #endregion
        //        }

        /// <summary>
        /// 刷新父窗口
        /// </summary>
        public static void RefreshParent(string url, Page page)
        {
            #region
            var js =
                $@"<Script language='JavaScript'>
                                        window.opener.location.href='{url}'';window.close();</Script>";
            //HttpContext.Current.Response.Write(js);
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "RefreshParent"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "RefreshParent", js);
            }
            #endregion
        }


        /// <summary>
        /// 刷新打开窗口
        /// </summary>
        public static void RefreshOpener(Page page)
        {
            #region
            const string js = @"<Script language='JavaScript'>
                    opener.location.reload();
                  </Script>";
            //HttpContext.Current.Response.Write(js);
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "RefreshOpener"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "RefreshOpener", js);
            }
            #endregion
        }


        /// <summary>
        /// 打开指定大小的新窗体
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="width">宽</param>
        /// <param name="heigth">高</param>
        /// <param name="top">头位置</param>
        /// <param name="left">左位置</param>
        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left, Page page)
        {
            #region
            var js =
                $@"<Script language='JavaScript'>window.open('{url}','','height={heigth},width={width},TOP={top},left={left}," +
                "location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no'');</Script>";
            //HttpContext.Current.Response.Write(js);
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "OpenWebFormSize"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "OpenWebFormSize", js);
            }
            #endregion
        }


        /// <summary>
        /// 转向Url制定的页面
        /// </summary>
        /// <param name="url">连接地址</param>
        public static void JavaScriptLocationHref(string url, Page page)
        {
            #region
            var js = @"<Script language='JavaScript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            //HttpContext.Current.Response.Write(js);
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "JavaScriptLocationHref"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "JavaScriptLocationHref", js);
            }
            #endregion
        }

        /// <summary>
        /// 打开指定大小位置的模式对话框
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="top">距离上位置</param>
        /// <param name="left">距离左位置</param>
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left, Page page)
        {
            #region
            var features =
                $"dialogWidth:{width}px;dialogHeight:{height}px;dialogLeft:{left}px;dialogTop:{top}px;center:yes;" +
                "help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features, page);
            #endregion
        }
        /// <summary>
        /// 弹出模态窗口
        /// </summary>
        /// <param name="webFormUrl"></param>
        /// <param name="features"></param>
        public static void ShowModalDialogWindow(string webFormUrl, string features, Page page)
        {
            var js = ShowModalDialogJavascript(webFormUrl, features);
            //HttpContext.Current.Response.Write(js);
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "ShowModalDialogWindow"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "ShowModalDialogWindow", js);
            }
        }
        //        /// <summary>
        //        /// 弹出模态窗口
        //        /// </summary>
        //        /// <param name="webFormUrl"></param>
        //        /// <param name="features"></param>
        //        /// <returns></returns>
        //        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        //        {
        //            #region
        //            string js = @"<script language=javascript>                            
        //    showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
        //            return js;
        //            #endregion
        //        }
        #endregion
    }
}