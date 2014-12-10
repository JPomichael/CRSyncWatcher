using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRS.Sync.Watcher.Linq;

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
        public bool Update(hotel_info hotel)
        {
            bool result = false;
            hotel_info _hotel = dc.hotel_info.Where(o => o.h_id == hotel.h_id || o.h_name_cn == hotel.h_name_cn && o.source_id == hotel.source_id).FirstOrDefault();
            _hotel = hotel;

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
