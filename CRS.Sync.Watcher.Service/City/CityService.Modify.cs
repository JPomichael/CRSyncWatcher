using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.City
{
    public partial class CityService
    {
        public void Update(city_info city)
        {
            city_info _city = dc.city_info.Where(o => o.code == city.code || (o.city_name.Contains(city.city_name) || o.city_name == city.city_name)).FirstOrDefault();
            _city.city_name = city.city_name;
            _city.city_name_en = city.city_name_en;
            _city.code = city.code;
            _city.remarks = city.remarks;
            _city.seqid = city.seqid;
            dc.SubmitChanges();
        }

    }
}
