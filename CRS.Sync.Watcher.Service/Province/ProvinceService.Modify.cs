using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.Province
{
    public partial class ProvinceService
    {
        public void Update(province_info provinceinfo)
        {
            province_info _province = dc.province_info.Where(o => o.code == provinceinfo.code || (o.province_name.Contains(provinceinfo.province_name) || o.province_name == provinceinfo.province_name)).FirstOrDefault();
            _province.ename = provinceinfo.ename;
            _province.code = provinceinfo.code;
            _province.remarks = provinceinfo.remarks;
            _province.seqid = provinceinfo.seqid;
            dc.SubmitChanges();
        }

    }
}
