using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.Star
{
    public partial class StarService
    {
        public void Update(hotel_star_info star)
        {
            hotel_star_info _star = dc.hotel_star_info.Where(o => o.id == star.id || (o.cname.Contains(star.cname) || o.cname == star.cname)).FirstOrDefault();
            _star.cname = star.cname;
            _star.ename = star.ename;
            _star.remarks = star.remarks;
            _star.seqid = star.seqid;
            dc.SubmitChanges();
        }

    }
}
