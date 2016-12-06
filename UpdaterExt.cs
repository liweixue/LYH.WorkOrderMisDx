using System;
using FSLib.App.SimpleUpdater;
using LYH.Framework.BaseUI;
using LYH.Framework.Commons;

namespace LYH.WorkOrder
{
    public class UpdaterExt
    {
        private static AppConfig _appConfig;

        public static void CheckUpdate()
        {
            //_updateWorker = new BackgroundWorker();
            //_updateWorker.DoWork += updateWorker_DoWork;
            //_updateWorker.RunWorkerCompleted += updateWorker_RunWorkerCompleted;
            _appConfig = new AppConfig();
            var autoUpdate = _appConfig.AppConfigGet("AutoUpdate").ToBoolean();
            if (autoUpdate)
            {
                //bool result;
                //bool.TryParse(str, out result);
                //if (result)
                //{
                //    _updateWorker.RunWorkerAsync();
                //}

                try
                {

                    Updater.CheckUpdateSimple(_appConfig.AppConfigGet("VersionUpdateUrl"), _appConfig.AppConfigGet("UpdateXml"));

                    ////获得当前的更新实例
                    //var updater = Updater.CreateUpdaterInstance(_appConfig.AppConfigGet("VersionUpdateUrl"), _appConfig.AppConfigGet("UpdateXml"));
                    ////当检查发生错误时,这个事件会触发
                    //updater.Error += (s, e) =>
                    //{
                    //    MessageBox.Show("更新发生了错误：" + updater.Context.Exception.Message);
                    //};
                    ////找到更新的事件
                    //updater.UpdatesFound += (s, e) =>
                    //{
                    //    if (MessageDxUtil.ShowYesNoAndTips(string.Format("有新的版本:{0}，是否需要更新!", updater.Context.UpdateInfo.AppVersion)) == DialogResult.Yes)
                    //    {
                    //        updater.StartExternalUpdater();
                    //    }
                    //};

                    ////没有找到更新的事件
                    //updater.NoUpdatesFound += (s, e) =>
                    //{
                    //    MessageDxUtil.ShowWarning("没有新版本！");
                    //};

                    ////更新时程序最低版本
                    //updater.MinmumVersionRequired += (s, e) =>
                    //{
                    //    MessageDxUtil.ShowError("当前版本过低无法使用自动更新！");
                    //};

                    //updater.Context.EnableEmbedDialog = false;

                    //updater.BeginCheckUpdateInProcess();

                }
                catch (Exception exception)
                {
                    MessageDxUtil.ShowError(exception.Message);
                }
            }
        }
    }
}