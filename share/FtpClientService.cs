/*****************************************************************
// Copyright (C) 2006-2007 Newegg Corporation
// All rights reserved.
// 
// Author:       Charles Ge (Charles.Y.Ge@Newegg.com)
// Create Date:  04/12/2007 15:33:42
// Usage: DFIS Ftp Client Service,add ftp site,upload files,download files
//
// RevisionHistory
// Date         Author               Description
// Asp.net2.0中FTP支持断点续传源码
*****************************************************************/

using System;
using System.IO;
using System.Net;
using System.Web.Configuration;

namespace LYH.WorkOrder.share
{
    public sealed class FtpClientService
    {
        #region Internal Members

        private NetworkCredential _certificate;

        #endregion

        /// <summary>
        /// 构造函数，提供初始化数据的功能，打开Ftp站点
        /// </summary>
        public FtpClientService()
        {
            _certificate = new NetworkCredential(WebConfigurationManager.AppSettings["UserName"], WebConfigurationManager.AppSettings["PassWord"]);
        }

        /// <summary>
        /// 创建FTP请求
        /// </summary>
        /// <param name="uri">ftp://myserver/upload.txt</param>
        /// <param name="method">Upload/Download</param>
        /// <returns></returns>
        private FtpWebRequest CreateFtpWebRequest(Uri uri, string method)
        {
            var ftpClientRequest = (FtpWebRequest) WebRequest.Create(uri);

            ftpClientRequest.Proxy = null;
            ftpClientRequest.Credentials = _certificate;
            ftpClientRequest.KeepAlive = true;
            ftpClientRequest.UseBinary = true;
            ftpClientRequest.UsePassive = true;
            ftpClientRequest.Method = method;

            //ftpClientRequest.Timeout = -1;

            return ftpClientRequest;
        }
        #region 支持断点续传

        public bool UploadFile(string sourceFile, Uri destinationPath, int offSet, string ftpMethod)
        {
            try
            {
                var file = new FileInfo(sourceFile);
                var uri = new Uri($"{destinationPath.AbsoluteUri}/{file.Name}");
                var request = CreateFtpWebRequest(uri, ftpMethod);
                request.ContentOffset = offSet;
                var requestStream = request.GetRequestStream();//需要获取文件的流
                var fileStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);//创建存储文件的流
                //int sourceLength = (int) fileStream.Length;
                offSet = CopyDataToDestination(fileStream, requestStream, offSet);
                var response = request.GetResponse();
                response.Close();
                if (offSet != 0)
                {
                    UploadFile(sourceFile, destinationPath, offSet, WebRequestMethods.Ftp.AppendFile);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }

            return true;
        }

        private int CopyDataToDestination(Stream sourceStream, Stream destinationStream, int offSet)
        {
            try
            {
                var sourceLength = (int) sourceStream.Length;
                var length = sourceLength - offSet;
                var buffer = new byte[length + offSet];
                var bytesRead = sourceStream.Read(buffer, offSet, length);
                while (bytesRead != 0)
                {
                    destinationStream.Write(buffer, 0, bytesRead);
                    bytesRead = sourceStream.Read(buffer, 0, length);
                    length = length - bytesRead;
                    offSet = (bytesRead == 0) ? 0 : (sourceLength - length);//(length - bytesRead);                  
                }
            }
            catch
            {
                //string error = ex.ToString();
                return offSet;
            }
            finally
            {
                destinationStream.Close();
                sourceStream.Close();
            }
            return offSet;
        }
        #endregion
    }
}