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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using System.Xml;
using System.Xml.Linq;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace CRS.Sync.Watcher.ConsoleApplication.Hotel
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

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        extern static int RemoveMenu(IntPtr hMenu, int nPos, int flags);


        //	// 用API安装事件处理
        //	static ConsoleCtrlDelegate newDelegate=new ConsoleCtrlDelegate(HandlerRoutine);

     
        public static void Main(string[] args)
        {
            //!  Title
            Console.Title = ("CRS HotelInfo Sync");

            IntPtr windowHandle = (IntPtr)FindWindow(null, Process.GetCurrentProcess().MainWindowTitle);
            IntPtr closeMenu = GetSystemMenu(windowHandle, IntPtr.Zero);
            int SC_CLOSE = 0xF060;
            RemoveMenu(closeMenu, SC_CLOSE, 0x0);

            #region 文件保存目录
            if (!Directory.Exists(staticFolderSavePath))
            {
                Directory.CreateDirectory(staticFolderSavePath);
            }
            #endregion

            #region Start Sync...


            HoteLocking hoteLocking = new HoteLocking();
            Thread T2 = new Thread(hoteLocking.LockHotelMe);
            T2.Start(true);
            Thread.Sleep(Convert.ToInt32(StringHelper.appSettings("HotelSyncTime")));//x Convert.ToInt32(StringHelper.appSettings("HotelSyncTime"))
            //在主线程中lock c1
            lock (hoteLocking)
            {
                //调用被lock的方法，并试图将deadlock解除
                hoteLocking.LockHotelMe(false);
            }

            #endregion
        }

        public class HoteLocking
        {
            private bool deadlocked = true;
            //这个方法用到了lock，我们希望lock的代码在同一时刻只能由一个线程访问
            public void LockHotelMe(object o)
            {
                lock (this)
                {
                    while (deadlocked)
                    {
                        //x Console.WriteLine("Foo: I am locked :(");
                        Stopwatch s = new Stopwatch();
                        s.Start();

                        //x Tip("\r\n 同步于：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "", ConsoleColor.Green);
                        //x Tip("", ConsoleColor.Gray);
                        log.Info("\r\n HotelSync同步于：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "");

                        //!  公寓信息
                        Hotel _hotel = new Hotel();
                        CRS.Sync.Watcher.Service.WCFMobileServer.CRSHotelParamsDTO _CRSHotelParamsDTO = new Service.WCFMobileServer.CRSHotelParamsDTO();
                        _hotel.SyncService(_CRSHotelParamsDTO, staticFolderSavePath);

                        s.Stop();
                        log.Info("\r\n 耗时：" + s.Elapsed.Minutes + "");
                        deadlocked = (bool)o;
                        log.Info("\r\n 下次同步将于：" + Convert.ToInt32(StringHelper.appSettings("HotelSyncTime")) / 60000 + "分钟后");
                        Thread.Sleep(Convert.ToInt32(StringHelper.appSettings("HotelSyncTime")));

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
