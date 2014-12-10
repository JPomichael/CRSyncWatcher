using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRS.Sync.Watcher.Linq;

namespace CRS.Sync.Watcher.DLL
{
    public static class ErrorHelper
    {
        /// <summary>
        /// 生成错误日志
        /// </summary>
        /// <param name="ErrorMessage"></param>
        public static void CreateErrorLog(string ErrorMessage)
        {
            DateTime dt = DateTime.Now;
            string FilePath = "";
            if (System.Configuration.ConfigurationManager.AppSettings["IpowCms_ErrorLog"] != null)
            {
                FilePath = System.Configuration.ConfigurationManager.AppSettings["IpowCms_ErrorLog"];
            }
            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(FilePath)))
            {
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(FilePath));
            }
            string FileName = "IpowCms_ErrorLog_" + dt.ToString().Replace(":", " ").Replace("/", "-") + ".txt";
            string SavePath = FilePath + FileName;
            SavePath = System.Web.HttpContext.Current.Server.MapPath(FilePath);
            //向文件写数据
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter
                        (SavePath + FileName,
                        false, System.Text.Encoding.GetEncoding("utf-8"));
                sw.WriteLine(ErrorMessage);
            }
            catch
            {
                throw;
            }
            sw.Flush();
            sw.Close();

        }

    }
}
