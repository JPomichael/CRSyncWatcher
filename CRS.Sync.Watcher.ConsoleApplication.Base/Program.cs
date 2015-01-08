using CRS.Sync.Watcher.DLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        extern static int RemoveMenu(IntPtr hMenu, int nPos, int flags);


        //	// 用API安装事件处理
        //	static ConsoleCtrlDelegate newDelegate=new ConsoleCtrlDelegate(HandlerRoutine);


        static void Main(string[] args)
        {
            //!  Title
            Console.Title = ("CRS BaseInfo Sync");

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
                        log.Info("本次耗时：" + s.Elapsed.Minutes + " 分钟.");
                        deadlocked = (bool)o;
                        //log.Info("\r\n 下次同步将于：" + Convert.ToInt32(StringHelper.appSettings("BaseSyncTime")) / 60000 + "分钟后");
                        TipTime(Convert.ToInt32(StringHelper.appSettings("BaseSyncTime")) / 60000);
                        Thread.Sleep(Convert.ToInt32(StringHelper.appSettings("BaseSyncTime")));
                    }
                }
            }
        }

        public static void TipTime(int dueInMinutes)
        {
            DateTime dueTime = DateTime.Now + TimeSpan.FromMinutes(dueInMinutes);
            int top = Console.CursorTop, left = Console.CursorLeft;

            System.Threading.ThreadPool.QueueUserWorkItem(delegate(object o)
            {
                while (true)
                {
                    TimeSpan remaining = dueTime - DateTime.Now;
                    if (remaining.Minutes <= 0) return;

                    Console.SetCursorPosition(left, top);
                    Console.Write(string.Format("距下次执行时间：{0,2}", (int)remaining.TotalMinutes) + " 分钟...");
                    System.Threading.Thread.Sleep(1000);
                }
            });
        }

    }
}
