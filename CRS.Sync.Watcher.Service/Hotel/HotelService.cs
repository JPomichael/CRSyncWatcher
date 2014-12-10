using CRS.Sync.Watcher.DLL;
using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CRS.Sync.Watcher.Service.Hotel
{
    public partial class HotelService : IHotelService
    {
        CRS.Sync.Watcher.Service.WCFMobileServer.MobileContractClient _WCFClient = WCFRefServieceFactory.GetProductServiceRef();
        estay_ecsdbDataContext dc = ConnHelper.estay_ecsdb();
        Expression<Func<hotel_info, bool>> expression = PredicateBuilder.True<hotel_info>();
        CRS.Sync.Watcher.Service.City.ICityService _cityService = new CRS.Sync.Watcher.Service.City.CityService();
        CRS.Sync.Watcher.Service.Province.IProvinceService _provinceService = new CRS.Sync.Watcher.Service.Province.ProvinceService();

    }
}
