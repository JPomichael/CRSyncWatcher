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

        public static void Main(string[] args)
        {
            Console.Title = ("CRS SYNC WATHER");
            Stopwatch t = new Stopwatch();
            t.Start();

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
            //Tip(_base.SyncService(staticFolderSavePath), null);

            //!  公寓信息
            Hotel _hotel = new Hotel();
            CRS.Sync.Watcher.Service.WCFMobileServer.CRSHotelParamsDTO _CRSHotelParamsDTO = new Service.WCFMobileServer.CRSHotelParamsDTO();
            Tip(_hotel.SyncService(_CRSHotelParamsDTO, staticFolderSavePath), null);

            #endregion

            t.Stop();
            Tip("程序总耗时：" + t.Elapsed.Seconds + "", null);
            System.Console.ReadLine();
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

        #region log4net
        //创建日志记录组件实例  
        //x ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //x ILog log = log4net.LogManager.GetLogger(typeof(Program));
        //记录错误日志  
        //log.Error("error", new Exception("在这里发生了一个异常,Error Number:"+random.Next()));  
        //记录严重错误  
        //log.Fatal("fatal", new Exception("在发生了一个致命错误，Exception Id："+random.Next()));  
        //记录一般信息  
        //log.Info("提示：系统正在运行");  
        //记录调试信息  
        //log.Debug("调试信息：debug");  
        //记录警告信息  
        //log.Warn("警告：warn");   
        #endregion

    }
}
