using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.Province
{
    public interface IProvinceService
    {
        bool CheckIsAny(province_info provinceinfo);
        string FormatProvinceName(string province_name);
        bool AddList(IEnumerable<province_info> provincelist);
        bool Add(province_info provinceinfo);
        string SerialNumber();
        void Update(province_info provinceinfo);
        IEnumerable<province_info> DcPublicToModel(List<CRS.Sync.Watcher.Service.WCFMobileServer.DcPublic> _dcPublicList);
        province_info GetProvinceInfoByCode(string code);
    }
}
