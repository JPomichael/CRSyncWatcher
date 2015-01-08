using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.House
{
    public partial class HouseService
    {
        public bool AddList(IEnumerable<hotel_room_info> roomlist)
        {
            bool result = false;
            try
            {
                dc.hotel_room_info.InsertAllOnSubmit(roomlist);
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        //public bool Add(hotel_room_info room)
        //{
        //    bool result = false;
        //    try
        //    {

        //        dc.hotel_room_info.InsertOnSubmit(room);
        //        dc.SubmitChanges();
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return result;
        //}

        public int Add(hotel_room_info room)
        {
            dc.hotel_room_info.InsertOnSubmit(room);
            dc.SubmitChanges();
            return room.room_id;
        }
    }
}
