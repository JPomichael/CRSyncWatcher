using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.House
{
    public partial class HouseService
    {
        public bool Update(hotel_room_info house)
        {
            bool result = false;
            hotel_room_info _house = dc.hotel_room_info.Where(o => o.hotel_id == house.hotel_id && o.code == house.code).FirstOrDefault();
            _house = house;
            try
            {
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
