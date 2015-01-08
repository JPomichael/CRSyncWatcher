using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.House
{
    public partial class HouseService
    {
        //public bool Update(hotel_room_info house)
        //{
        //    bool result = false;
        //    hotel_room_info _house = dc.hotel_room_info.Where(o => o.hotel_id == house.hotel_id && o.code == house.code).FirstOrDefault();
        //    _house = house;
        //    try
        //    {
        //        dc.SubmitChanges();
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        result = false;
        //    }
        //    return result;
        //}


        public int Update(hotel_room_info house)
        {
            hotel_room_info _house = dc.hotel_room_info.Where(o => o.hotel_id == house.hotel_id && o.code == house.code).FirstOrDefault();
            _house.h_r_id = house.h_r_id;
            _house.code = house.code;
            _house.hotel_id = house.hotel_id;
            _house.h_r_name_cn = house.h_r_name_cn;
            _house.h_r_name_en = house.h_r_name_en;
            _house.h_r_description_cn = house.h_r_description_cn;
            _house.h_r_description_en = house.h_r_description_en;
            _house.category = house.category;
            _house.house_service = house.house_service;
            _house.h_r_bed_number = house.h_r_bed_number;
            _house.h_r_house_number = house.h_r_house_number;
            _house.Comments = house.Comments;
            _house.h_r_state = house.h_r_state;
            _house.h_r_sort = house.h_r_sort;
            _house.DefaultPrice = house.DefaultPrice;
            _house.h_r_acreage = house.h_r_acreage;
            _house.h_r_floor = house.h_r_floor;
            _house.h_r_bed_type = house.h_r_bed_type;
            _house.h_r_utime = house.h_r_utime;
            dc.SubmitChanges();
            return _house.room_id;
        }
    }
}
