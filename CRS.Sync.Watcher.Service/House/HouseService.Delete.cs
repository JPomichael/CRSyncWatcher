using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.House
{
    public partial class HouseService
    {
        /// <summary>
        /// 伪删除房型信息-单个
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public bool Delete(int room_id, int? hotel_id)
        {
            bool result = false;

            var house = dc.hotel_room_info.Where(o => o.room_id == room_id && o.hotel_id == hotel_id).FirstOrDefault();
            house.h_r_state = false;
            house.h_r_utime = System.DateTime.Now;
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
        /// 多个房型ID-删除
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
                        var house = dc.hotel_room_info.Where(o => o.room_id == _idList).FirstOrDefault();
                        house.h_r_state = false;
                        house.h_r_utime = System.DateTime.Now;
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
        /// 删除房型信息-真实
        /// </summary>
        /// <param name=" IEnumerable<hotel_info> hotelist "></param>
        /// <returns></returns>
        public bool DeleteTrue(IEnumerable<hotel_room_info> houselist)
        {
            bool result = false;
            try
            {
                dc.hotel_room_info.DeleteAllOnSubmit(houselist);
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public bool DleteTrue(hotel_room_info house)
        {
            bool result = false;
            try
            {
                //删除酒店状态
                dc.hotel_room_info.DeleteOnSubmit(house);
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
