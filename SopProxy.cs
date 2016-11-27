using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class SopProxy
    {
        FtpProxy ftpProxy = new FtpProxy("","","ftp://1.1.1.1/");
        private readonly string pn;
        private readonly string stage;
        public SopProxy(string pn, string stage)
        {
            this.pn = pn;
            this.stage = stage;
        }

        public bool DownloadLastVer()
        {
            string plmSopName = GetPLMSopName();
            Esop tempSop = GetTempSop();
            if (string.IsNullOrEmpty(plmSopName) && tempSop == null)
                return false;


            if (string.IsNullOrEmpty(plmSopName))
            {
                ftpProxy.Download()
                //download tempSop
                return true;
            }

            if (tempSop == null)
            {
                //download plmSop
                return true;
            }

            string plmVer = ParseVer(plmSopName);
            int tempVerNum = int.Parse(tempSop.Ver.Substring(0, 1));
            int plmNum = int.Parse(plmVer);
            if (tempVerNum >= plmNum)
            {
                //download tempSop

            }
            else
            {
                //download plmSop
            }
            return true;
        }

        public string ParseVer(string fileName)
        {
            string keyWord = "Ver";

            string ver = fileName.Substring(fileName.IndexOf(keyWord) + keyWord.Length);
            if (ver.IndexOf("_") > -1)
                ver = ver.Substring(0, ver.IndexOf("_"));
            else
                ver = ver.Substring(0, ver.IndexOf("."));

            return ver;

        }
        
        

        private string GetPLMSopName()
        {
            string path = System.IO.Path.Combine("ftp://1.1.1.1/PLM", pn);
            string sopName = "";
            //ftp search file by pn and stage
            //if(pn + stage) 找不到就用 pn去找
            
            return sopName;
        }


        private Esop GetTempSop()
        {
            //query db by this.pn, this.stage
            //
            return null;
        }
    }
}