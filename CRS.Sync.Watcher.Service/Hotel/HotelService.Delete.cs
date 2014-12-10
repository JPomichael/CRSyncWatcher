using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.Hotel
{
    public partial class HotelService
    {
        /// <summary>
        /// 伪删除酒店信息-单个
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public bool Delete(int hotel_id)
        {
            bool result = false;

            var hotel = dc.hotel_info.Where(o => o.hotel_id == hotel_id).FirstOrDefault();
            hotel.h_state = false;
            hotel.CheckState = 0;
            hotel.h_utime = System.DateTime.Now;
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

        /// <summary>
        /// 多个公寓ID
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool Delete(int[] idList)
        {
            bool res = false;
            if (idList != null && idList.Count() > 0)
            {
                try
                {
                    foreach (int _idList in idList)
                    {
                        var hotel = dc.hotel_info.Where(o => o.hotel_id == _idList).FirstOrDefault();
                        hotel.h_state = false;
                        hotel.CheckState = 0;
                        hotel.h_utime = System.DateTime.Now;
                        res = true;
                    }
                    dc.SubmitChanges();
                }
                catch (Exception ex)
                {
                    res = false;
                }

            }
            return res;
        }

        /// <summary>
        /// 删除公寓信息-真实
        /// </summary>
        /// <param name=" IEnumerable<hotel_info> hotelist "></param>
        /// <returns></returns>
        public bool DeleteTrue(IEnumerable<hotel_info> hotelist)
        {
            bool result = false;
            try
            {
                //删除酒店状态
                dc.hotel_info.DeleteAllOnSubmit(hotelist);
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public bool DleteTrue(hotel_info hotel)
        {
            bool result = false;
            try
            {
                //删除酒店状态
                dc.hotel_info.DeleteOnSubmit(hotel);
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
