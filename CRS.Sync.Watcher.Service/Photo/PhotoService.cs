using CRS.Sync.Watcher.DLL;
using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.Photo
{
    public partial class PhotoService : IPhotoService
    {
        CRS.Sync.Watcher.Service.WCFMobileServer.MobileContractClient _WCFClient = WCFRefServieceFactory.GetProductServiceRef();
        estay_ecsdbDataContext dc = ConnHelper.estay_ecsdb();
        CRS.Sync.Watcher.Service.Hotel.IHotelService _hotelService = new CRS.Sync.Watcher.Service.Hotel.HotelService();

    }
}
