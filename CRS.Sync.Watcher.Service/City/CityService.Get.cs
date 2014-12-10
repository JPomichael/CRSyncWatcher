using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.City
{
    public partial class CityService
    {
        public IEnumerable<city_info> DcPublicToModel(List<CRS.Sync.Watcher.Service.WCFMobileServer.DcPublic> _dcPublicList)
        {
            IEnumerable<city_info> cityList = _dcPublicList.Select(o => new city_info
            {
                city_id = o.code,
                city_name = FormatCityName(o.cname),
                city_name_en = o.ename,
                code = o.code,
                remarks = o.remarks,
                seqid = o.seqid,
                status = o.status == "1" ? true : false,
            }).AsEnumerable();
            return cityList;
        }

        public bool CheckIsAny(city_info city)
        {
            bool result = false;

            try
            {
                //!  此前已格式化名称
                if (dc.city_info.Where(o => o.code == city.code || (o.city_name.Contains(city.city_name) || o.city_name == city.city_name)).Any())
                    result = true;
            }
            catch (Exception)
            {

            }
            return result;
        }

        public string FormatCityName(string name)
        {
            if (name.Contains("市"))
            {
                name = name.Replace("市", "");
            }
            //if (name.Contains("自治区"))
            //{
            //    name = name.Replace("自治区", "");
            //    if (name.Contains("维吾尔"))
            //    {
            //        name = name.Replace("维吾尔", "");
            //    }
            //    if (name.Contains("回族"))
            //    {
            //        name = name.Replace("回族", "");
            //    }
            //    if (name.Contains("壮族"))
            //    {
            //        name = name.Replace("壮族", "");
            //    }
            //    if (name.Contains("壮族"))
            //    {
            //        name = name.Replace("壮族", "");
            //    }

            //}
            return name;
        }

        public city_info GetCityInfoByCode(string code)
        {
            return dc.city_info.Where(o => o.code != null ? o.code == code : false).FirstOrDefault();
        }
    }
}
