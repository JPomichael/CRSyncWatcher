using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.Star
{
    public partial class StarService
    {
        public IEnumerable<hotel_star_info> DcPublicToModel(List<CRS.Sync.Watcher.Service.WCFMobileServer.DcPublic> _dcPublicList)
        {
            IEnumerable<hotel_star_info> cityList = _dcPublicList.Select(o => new hotel_star_info
            {
                id = o.code,
                cname = o.cname,
                ename = o.ename,
                remarks = o.remarks,
                seqid = o.seqid,
                status = o.status == "1" ? true : false,
            }).AsEnumerable();
            return cityList;
        }

        public bool CheckIsAny(hotel_star_info star)
        {
            bool result = false;

            try
            {
                //!  此前已格式化名称
                if (dc.hotel_star_info.Where(o => o.id == star.id || (o.cname.Contains(star.cname) || o.cname == star.cname)).Any())
                    result = true;
            }
            catch (Exception)
            {

            }
            return result;
        }

    }
}
