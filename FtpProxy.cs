using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WebApplication1
{
    public class FtpProxy
    {
        string userName;
        string pwd;
        string rootUri;
        public FtpProxy(string userName, string pwd, string rootUri)
        {
            this.userName = userName;
            this.pwd = pwd;
            this.rootUri = rootUri;

        }

        private FtpWebRequest GetFtpRequest(string uri, string ftpMethod)
        {
            FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create(System.IO.Path.Combine(this.rootUri,uri));
            requestFileDownload.Credentials = new NetworkCredential(this.userName, this.pwd);
            requestFileDownload.Method = ftpMethod;

            return requestFileDownload;
        }

        /// <summary>
        /// ftp 資料夾是否存在
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public bool FolderExists(string folderPath)
        {
            FtpWebRequest requestFileDownload = GetFtpRequest(folderPath, WebRequestMethods.Ftp.ListDirectory);
            
            FtpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                response = (FtpWebResponse)requestFileDownload.GetResponse();

                Stream responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
            }
            catch (Exception)
            {

                return false;

            }
            finally
            {
                if(reader != null)
                    reader.Close();

                if(response != null)
                    response.Close();
            }

            return true;
        }

        public bool FileExists(string filePath)
        {
            FtpWebRequest request = GetFtpRequest(filePath, WebRequestMethods.Ftp.GetFileSize);
            FtpWebResponse response = null;
            try
            {
                response = (FtpWebResponse)request.GetResponse();
                long size = response.ContentLength;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if(response != null)
                    response.Close();
            }

            return true;

        }

        public void Download(string remoteFilePath, string localFilePath)
        {
            
            FtpWebRequest request = GetFtpRequest(remoteFilePath, WebRequestMethods.Ftp.DownloadFile);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using (var fileStream = File.Create(localFilePath))
            {
                responseStream.Seek(0, SeekOrigin.Begin);
                responseStream.CopyTo(fileStream);
            }
        }


        public void Upload(string loaclFilePath, string remoteFilePath)
        {
            FtpWebRequest request = GetFtpRequest(remoteFilePath, WebRequestMethods.Ftp.UploadFile);
            request.UseBinary = true;
            byte[] dataBytes = File.ReadAllBytes(loaclFilePath);
            request.ContentLength = dataBytes.Length;
            using (Stream s = request.GetRequestStream())
            {
                s.Write(dataBytes, 0, dataBytes.Length);
            }
            
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            //Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

            response.Close();

        }

    }
}