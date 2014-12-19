using CRS.Sync.Watcher.DLL;
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
using System.Threading;
using System.Timers;
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
            //!  Title
            Console.Title = ("CRS SYNC WATHER");


            #region 文件保存目录
            if (!Directory.Exists(staticFolderSavePath))
            {
                Directory.CreateDirectory(staticFolderSavePath);
            }
            #endregion

            #region Start Sync...
            System.Timers.Timer t = new System.Timers.Timer();

            //!  基础信息
            //Base _base = new Base();
            //messages += _base.SyncService(staticFolderSavePath);

            //!  公寓信息
            //Hotel _hotel = new Hotel();
            //CRS.Sync.Watcher.Service.WCFMobileServer.CRSHotelParamsDTO _CRSHotelParamsDTO = new Service.WCFMobileServer.CRSHotelParamsDTO();
            //messages += _hotel.SyncService(_CRSHotelParamsDTO, staticFolderSavePath);




            t = new System.Timers.Timer(60000);//实例化Timer类，设置时间间隔 1分钟
            t.Start();
            t.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);//到达时间的时候执行事件
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件
            while (true)
            {
                //Tip("定时启动于： " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 线程：" + Thread.CurrentThread.ManagedThreadId.ToString() + "", null);
                Thread.Sleep(100);
            }

            #endregion
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

        #region 封装窗体文字提示和log的记录
        /// <summary>
        /// 封装窗体文字提示和log的记录
        /// </summary>
        /// <param name="messages"></param>
        public static void LogErrorMessages(string messages)
        {
            if (!string.IsNullOrEmpty(messages))
            {
                Tip(messages, ConsoleColor.Yellow);
                log.Warn(messages);
            }
        }
        #endregion

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


        public static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            Tip("\r\n 同步于：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "", ConsoleColor.Green);
            Tip("", ConsoleColor.Gray);
            //! 收费计划
            RatePlan _ratePlan = new RatePlan();
            //x _ratePlan.PriceService();
            _ratePlan.PriceDescriptService();
            s.Stop();
        }
    }
}
