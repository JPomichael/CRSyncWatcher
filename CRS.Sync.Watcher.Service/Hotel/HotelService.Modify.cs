using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRS.Sync.Watcher.Linq;
using CRS.Sync.Watcher.DLL;

namespace CRS.Sync.Watcher.Service.Hotel
{
    public partial class HotelService
    {
        /// <summary>
        /// 编辑酒店信息
        /// </summary>
        /// <param name="hotel"></param>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        //public bool Update(hotel_info hotel)
        //{
        //    bool result = false;
        //    using (estay_ecsdbDataContext db = ConnHelper.estay_ecsdb())
        //    {
        //        hotel_info _hotelinfo = db.hotel_info.Where(o => o.h_id == hotel.h_id || o.h_name_cn == hotel.h_name_cn && o.source_id == hotel.source_id).FirstOrDefault();
        //        hotel_info _hotel = db.hotel_info.Where(o => o.hotel_id == _hotelinfo.hotel_id).FirstOrDefault();
        //        _hotel.h_id = hotel.h_id;
        //        _hotel.u_id = hotel.u_id;
        //        _hotel.h_name_cn = hotel.h_name_cn;
        //        _hotel.h_name_en = hotel.h_name_en;
        //        _hotel.h_location_cn = hotel.h_location_cn;
        //        _hotel.h_location_en = hotel.h_location_en;
        //        _hotel.h_description_cn = hotel.h_description_cn;
        //        _hotel.h_description_en = hotel.h_description_en;
        //        _hotel.h_star = hotel.h_star;
        //        _hotel.h_organization = hotel.h_organization;
        //        _hotel.h_tel = hotel.h_tel;
        //        _hotel.h_fax = hotel.h_fax;
        //        _hotel.h_email = hotel.h_email;
        //        _hotel.h_check_in = hotel.h_check_in;
        //        _hotel.h_check_out = hotel.h_check_out;
        //        _hotel.h_room_count = hotel.h_room_count;
        //        _hotel.h_province = hotel.h_province;
        //        _hotel.h_city = hotel.h_city;
        //        _hotel.h_postcode = hotel.h_postcode;
        //        _hotel.h_state = hotel.h_state;
        //        _hotel.h_hotel_website = hotel.h_hotel_website;
        //        _hotel.h_sort = hotel.h_sort;
        //        _hotel.source_id = hotel.source_id;
        //        _hotel.availPolicy = _hotel.availPolicy;
        //        _hotel.CheckState = hotel.CheckState;
        //        _hotel.bounsItem = hotel.bounsItem;
        //        _hotel.fbcoef = hotel.fbcoef;
        //        _hotel.etcoef = hotel.etcoef;
        //        _hotel.otcoef = hotel.otcoef;
        //        _hotel.ttcoef = hotel.ttcoef;
        //        _hotel.hotelCard = hotel.hotelCard;
        //        _hotel.rmcoef = hotel.rmcoef;
        //        _hotel.code = hotel.code;
        //        try
        //        {
        //            db.SubmitChanges();
        //            result = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            result = false;
        //        }
        //    }
        //    return result;
        //}


        public int Update(hotel_info hotel)
        {
            int hotel_id = 0;
            using (estay_ecsdbDataContext db = ConnHelper.estay_ecsdb())
            {
                hotel_info _hotel = db.hotel_info.Where(o => o.h_id == hotel.h_id || o.h_name_cn == hotel.h_name_cn && o.source_id == hotel.source_id).FirstOrDefault();
                _hotel.h_id = hotel.h_id;
                _hotel.u_id = hotel.u_id;
                _hotel.h_name_cn = hotel.h_name_cn;
                _hotel.h_name_en = hotel.h_name_en;
                _hotel.h_location_cn = hotel.h_location_cn;
                _hotel.h_location_en = hotel.h_location_en;
                _hotel.h_description_cn = hotel.h_description_cn;
                _hotel.h_description_en = hotel.h_description_en;
                _hotel.h_star = hotel.h_star;
                _hotel.h_organization = hotel.h_organization;
                _hotel.h_tel = hotel.h_tel;
                _hotel.h_fax = hotel.h_fax;
                _hotel.h_email = hotel.h_email;
                _hotel.h_check_in = hotel.h_check_in;
                _hotel.h_check_out = hotel.h_check_out;
                _hotel.h_room_count = hotel.h_room_count;
                _hotel.h_province = hotel.h_province;
                _hotel.h_city = hotel.h_city;
                _hotel.h_postcode = hotel.h_postcode;
                _hotel.h_state = hotel.h_state;
                _hotel.h_hotel_website = hotel.h_hotel_website;
                _hotel.h_sort = hotel.h_sort;
                _hotel.source_id = hotel.source_id;
                _hotel.availPolicy = _hotel.availPolicy;
                _hotel.CheckState = hotel.CheckState;
                _hotel.bounsItem = hotel.bounsItem;
                _hotel.fbcoef = hotel.fbcoef;
                _hotel.etcoef = hotel.etcoef;
                _hotel.otcoef = hotel.otcoef;
                _hotel.ttcoef = hotel.ttcoef;
                _hotel.hotelCard = hotel.hotelCard;
                _hotel.rmcoef = hotel.rmcoef;
                _hotel.code = hotel.code;
                _hotel.h_utime = hotel.h_utime;
                db.SubmitChanges();
                hotel_id = _hotel.hotel_id;
            }
            return hotel_id;
        }
    }
}
