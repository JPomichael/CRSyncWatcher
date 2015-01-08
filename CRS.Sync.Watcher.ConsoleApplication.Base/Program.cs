using CRS.Sync.Watcher.DLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace CRS.Sync.Watcher.ConsoleApplication.Base
{
    class Program
    {
        private static object ojb = new object();
        //!  通用代码保存目录
        public static string staticFolderSavePath = StringHelper.appSettings("staticFolderSavePath");
        //! 创建日志记录组件实例  
        public static ILog log = log4net.LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            //!  Title
            Console.Title = ("CRS BaseInfo Sync");

            #region 文件保存目录
            if (!Directory.Exists(staticFolderSavePath))
            {
                Directory.CreateDirectory(staticFolderSavePath);
            }
            #endregion

            BaseLocking baseLocking = new BaseLocking();
            //在t1线程中调用LockMe，并将deadlock设为true（将出现死锁）
            Thread T1 = new Thread(baseLocking.LockBaseMe);
            T1.Start(true);
            Thread.Sleep(Convert.ToInt32(StringHelper.appSettings("BaseSyncTime")));//x Convert.ToInt32(StringHelper.appSettings("BaseSyncTime"))
            //在主线程中lock c1
            lock (baseLocking)
            {
                //调用被lock的方法，并试图将deadlock解除
                baseLocking.LockBaseMe(false);
            }
        }

        public class BaseLocking
        {
            private bool deadlocked = true;
            //这个方法用到了lock，我们希望lock的代码在同一时刻只能由一个线程访问
            public void LockBaseMe(object o)
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
                        log.Info("\r\n Base同步于：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "");

                        //!  基础信息
                        Base _base = new Base();
                        _base.SyncService(staticFolderSavePath);

                        s.Stop();
                        log.Info("\r\n 耗时：" + s.Elapsed.Minutes + "");
                        deadlocked = (bool)o;
                        log.Info("\r\n 下次同步将于：" + Convert.ToInt32(StringHelper.appSettings("BaseSyncTime")) / 60000 + "分钟后");
                        Thread.Sleep(Convert.ToInt32(StringHelper.appSettings("BaseSyncTime")));



                    }
                }
            }
        }

    }
}
