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

        private static object ojb = new object();
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

            //!  基础信息
            //Base _base = new Base();
            //messages += _base.SyncService(staticFolderSavePath);

            //!  公寓信息
            //Hotel _hotel = new Hotel();
            //CRS.Sync.Watcher.Service.WCFMobileServer.CRSHotelParamsDTO _CRSHotelParamsDTO = new Service.WCFMobileServer.CRSHotelParamsDTO();
            //messages += _hotel.SyncService(_CRSHotelParamsDTO, staticFolderSavePath);

            locking locking = new locking();
            //在t1线程中调用LockMe，并将deadlock设为true（将出现死锁）
            Thread T = new Thread(locking.LockMe);
            T.Start(true);
            Thread.Sleep(Convert.ToInt32(StringHelper.appSettings("RatePlanSyncTime")));
            //在主线程中lock c1
            lock (locking)
            {
                //调用被lock的方法，并试图将deadlock解除
                locking.LockMe(false);
            }

            #endregion
        }

        /// <summary>
        /// 到达时间的时候执行事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            Tip("\r\n 同步于：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "", ConsoleColor.Green);
            Tip("", ConsoleColor.Gray);
            //! 收费计划
            RatePlan _ratePlan = new RatePlan();
            //x _ratePlan.PriceService();
            //x _ratePlan.PriceDescriptService();
            _ratePlan.SyncDataBaseService();
            s.Stop();
        }

        /// <summary>
        /// 加锁
        /// </summary>
        public class locking
        {
            private bool deadlocked = true;
            //这个方法用到了lock，我们希望lock的代码在同一时刻只能由一个线程访问
            public void LockMe(object o)
            {
                lock (this)
                {
                    while (deadlocked)
                    {
                        //x Console.WriteLine("Foo: I am locked :(");
                        Stopwatch s = new Stopwatch();
                        s.Start();

                        Tip("\r\n 同步于：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "", ConsoleColor.Green);
                        Tip("", ConsoleColor.Gray);
                        //! 收费计划
                        RatePlan _ratePlan = new RatePlan();
                        //x _ratePlan.PriceService();
                        //x _ratePlan.PriceDescriptService();
                        _ratePlan.SyncDataBaseService();
                        s.Stop();
                        Console.WriteLine("耗时：" + s.Elapsed.Minutes);
                        deadlocked = (bool)o;
                        Thread.Sleep(100);
                    }
                }
            }
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
    }
}
