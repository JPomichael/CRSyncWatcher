using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.City
{
    public interface ICityService
    {
        bool Add(city_info city);
        bool AddList(IEnumerable<city_info> citylist);
        string SerialNumber();
        void Update(city_info cityinfo);
        bool CheckIsAny(city_info city);
        IEnumerable<city_info> DcPublicToModel(List<CRS.Sync.Watcher.Service.WCFMobileServer.DcPublic> _dcPublicList);
        city_info GetCityInfoByCode(string code);
    }
}
