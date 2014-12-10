using CRS.Sync.Watcher.DLL;
using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.House
{
    public partial class HouseService : IHouseService
    {
        CRS.Sync.Watcher.Service.WCFMobileServer.MobileContractClient _WCFClient = WCFRefServieceFactory.GetProductServiceRef();
        estay_ecsdbDataContext dc = ConnHelper.estay_ecsdb();
    }
}
