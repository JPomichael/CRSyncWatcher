using CRS.Sync.Watcher.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.Hotel
{
    public partial class HotelService
    {

        public bool AddList(IEnumerable<hotel_info> hotelist)
        {
            bool result = false;
            try
            {
                dc.hotel_info.InsertAllOnSubmit(hotelist);
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool Add(hotel_info hotel)
        {
            bool result = false;
            try
            {

                dc.hotel_info.InsertOnSubmit(hotel);
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
