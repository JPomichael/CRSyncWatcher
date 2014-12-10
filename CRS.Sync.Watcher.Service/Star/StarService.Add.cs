using CRS.Sync.Watcher.DLL;
using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.Star
{
    public partial class StarService
    {
        public bool add(hotel_star_info star)
        {
            bool result = false;
            try
            {

                dc.hotel_star_info.InsertOnSubmit(star);
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public bool addList(IEnumerable<hotel_star_info> starlist)
        {
            bool result = false;
            try
            {
                dc.hotel_star_info.InsertAllOnSubmit(starlist);
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

    }
}
