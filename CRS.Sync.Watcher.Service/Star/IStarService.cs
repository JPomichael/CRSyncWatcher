using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.Star
{
    public interface IStarService
    {
        bool add(hotel_star_info star);
        bool addList(IEnumerable<hotel_star_info> starlist);
        void Update(hotel_star_info star);
        bool CheckIsAny(hotel_star_info star);
        IEnumerable<hotel_star_info> DcPublicToModel(List<CRS.Sync.Watcher.Service.WCFMobileServer.DcPublic> _dcPublicList);
    }
}
