using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using CRS.Sync.Watcher.DLL;
using CRS.Sync.Watcher.Linq;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace CRS.Sync.Watcher.Service.RatePlan
{
    public partial class RatePlanService : IRatePlanService
    {
        ILog log = log4net.LogManager.GetLogger(typeof(RatePlanService));
        CRS.Sync.Watcher.Service.WCFMobileServer.MobileContractClient _WCFClient = WCFRefServieceFactory.GetProductServiceRef();
        estay_ecsdbDataContext dc = ConnHelper.estay_ecsdb();
        Expression<Func<hotel_info, bool>> expression = PredicateBuilder.True<hotel_info>();
    }
}
