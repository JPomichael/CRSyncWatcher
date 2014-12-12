using CRS.Sync.Watcher.DLL;
using CRS.Sync.Watcher.Domain.Dto;
using CRS.Sync.Watcher.Linq;
using CRS.Sync.Watcher.Service;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace CRS.Sync.Watcher.ConsoleApplication.Demo
{
    /// <summary>
    /// demo 尝试调用接口
    /// </summary>
    public class Program
    {
        #region 服务引用
        //! 服务引用
        public static CRS.Sync.Watcher.Service.Public.IPublicService _publicService = new CRS.Sync.Watcher.Service.Public.PublicService();
        public static CRS.Sync.Watcher.Service.Province.IProvinceService _provinceService = new CRS.Sync.Watcher.Service.Province.ProvinceService();
        public static CRS.Sync.Watcher.Service.City.ICityService _cityService = new CRS.Sync.Watcher.Service.City.CityService();
        public static CRS.Sync.Watcher.Service.Star.IStarService _starService = new CRS.Sync.Watcher.Service.Star.StarService();
        #endregion

        //!  通用代码保存目录
        public static string staticFolderSavePath = StringHelper.appSettings("staticFolderSavePath");
        //! 创建日志记录组件实例  
        public static ILog log = log4net.LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            //! 定义返回的消息
            string messages = "";
            //!  Title
            Console.Title = ("CRS SYNC WATHER");
            Stopwatch t = new Stopwatch();
            t.Start();
            Tip("同步于：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "", ConsoleColor.Green);
            log.Info("\r\n====================系统运行信息将会记录====================\r\n");

            #region 文件保存目录
            if (!Directory.Exists(staticFolderSavePath))
            {
                Directory.CreateDirectory(staticFolderSavePath);
            }
            #endregion

            //! -============================================================分隔符============================================================

            #region Start Sync...

            //!  基础信息
            //Base _base = new Base();
            //messages += _base.SyncService(staticFolderSavePath);
            //LogErrorMessages(messages);

            //!  公寓信息
            //Hotel _hotel = new Hotel();
            //CRS.Sync.Watcher.Service.WCFMobileServer.CRSHotelParamsDTO _CRSHotelParamsDTO = new Service.WCFMobileServer.CRSHotelParamsDTO();
            //messages += _hotel.SyncService(_CRSHotelParamsDTO, staticFolderSavePath);
            //LogErrorMessages(messages);

            //! 收费计划
            RatePlan _ratePlan = new RatePlan();
            messages += _ratePlan.SyncService();
            LogErrorMessages(messages);

            #endregion

            t.Stop();
            Tip("程序总耗时：" + t.Elapsed.Seconds + "", ConsoleColor.Green);
            Console.ReadKey();
        }




        #region 控制台设置
        /// <summary>
        /// 设置控制台字体颜色
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="color"></param>
        public static void Tip(string documents, ConsoleColor? color)
        {
            Console.ForegroundColor = color != null ? (ConsoleColor)color : ConsoleColor.Gray; //设置前景色，即字体颜色
            Console.WriteLine(documents);
            Console.BackgroundColor = ConsoleColor.Black;
        }
        #endregion

        /// <summary>
        /// 封装窗体文字提示和log的记录
        /// </summary>
        /// <param name="messages"></param>
        public static void LogErrorMessages(string messages)
        {
            Tip(messages, ConsoleColor.Yellow);
            if (!string.IsNullOrEmpty(messages))
            {
                log.Warn(messages);
            }
        }

        #region log4net

        //x ILog log = log4net.LogManager.GetLogger(typeof(Program));
        //记录错误日志  
        //x log.Error("error", new Exception("在这里发生了一个异常,Error Number:"+random.Next()));  
        //记录严重错误  
        //x log.Fatal("fatal", new Exception("在发生了一个致命错误，Exception Id："+random.Next()));  
        //记录一般信息  
        //x log.Info("提示：系统正在运行");  
        //记录调试信息  

        //记录警告信息  
        //log.Warn("警告：warn");   
        #endregion

    }
}
