using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CRS.Sync.Watcher.DLL
{
    public static class LogHelper
    {

        /// <summary>
        /// 生成错误日志-TXT
        /// </summary>
        /// <param name="ErrorMessage"></param>
        public static void ErrorLog(Exception ex)
        {
            DateTime dt = DateTime.Now;
            string FilePath = "";
            if (System.Configuration.ConfigurationManager.AppSettings["ErrorLog_SavePath"] != null)
            {
                FilePath = System.Configuration.ConfigurationManager.AppSettings["ErrorLog_SavePath"];
            }
            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(FilePath)))
            {
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(FilePath));
            }
            string FileName = "ErrorLog_" + dt.ToString("yyyyMMddHHmm") + ".txt";
            string SavePath = System.Web.HttpContext.Current.Server.MapPath(FilePath + "\\" + DateTime.Now.ToString("yyyy-MM") + "\\");
            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);//在根目录下建立文件夹
            }
            //向文件写数据
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(SavePath + FileName, true, System.Text.Encoding.GetEncoding("utf-8"));
                sw.WriteLine();
                sw.WriteLine("-------------------------------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff") + "-------------------------------------");
                sw.WriteLine("友好消息 ： " + ex.Message);
                sw.WriteLine("Develop ： " + ex.StackTrace.Replace(";", "\r\n"));
                sw.WriteLine("错误源头 ： " + ex.Source);
            }
            catch
            {
                throw;
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="user_name"></param>
        /// <param name="type"></param>
        /// <param name="action"></param>
        /// <param name="ex"></param>
        public static void ErrorLog(Exception ex, string user_name)
        {
            //ipowdb_AdventureWaterWorldDataContext ydc = ConnHelper.ipowdb_AdventureWaterWorld();
            //ErrorLogInfo err = new ErrorLogInfo();
            //err.ClassName = type.ToString();
            //err.ActionName = action.ToString();
            //err.Operator = user_name;
            //err.ErrorType = ex.Message.ToString();
            //err.ErrorMessage = ex.ToString();
            //err.IpAddress = StringHelper.getRealIp();
            //err.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //ydc.ErrorLogInfo.InsertOnSubmit(err);
            //ydc.SubmitChanges();
        }

        //记录日志
        public static void log(Object c)
        {
            // string path = RequestAdress.ProramRunRootPath + "log.txt"; ;
            string path = "e:\\log.txt"; ;
            StreamWriter sw = null;
            //指定日志文件的目录
            if (!File.Exists(path))
            {
                sw = File.CreateText(path);

            }
            else
            {
                sw = new StreamWriter(path, true);
                sw.BaseStream.Seek(0, SeekOrigin.End);
            }

            sw.WriteLine(":" + DateTime.Now + "---The date is:------" + c);
            /// sw.WriteLine(ex);
            sw.WriteLine();
            sw.Close();
        }

    }
}
