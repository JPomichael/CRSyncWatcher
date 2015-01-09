using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace CRS.Sync.Watcher.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        //x CRServiceReference

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //TODO: 服务启动
            StartDoSync();
        }

        protected override void OnStop()
        {
            //TODO: 服务停止
        }

        protected override void OnContinue()
        {
            //服务恢复执行代码
            base.OnContinue();
        }

        private void StartDoSync()
        {

        }
    }
}
