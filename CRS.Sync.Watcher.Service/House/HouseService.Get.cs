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
        /// CRS接口
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="code"></param>
        /// <param name="cName"></param>
        /// <param name="eName"></param>
        /// <returns></returns>
        public CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeGet GetCRSRmTypeInterface(int hotelId, string code, string cName, string eName)
        {
            CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeGet _result = null;
            try
            {
                _result = _WCFClient.GetCRSRmTypeInterface(hotelId, code, cName, eName);
            }
            catch (Exception)
            {
            }
            return _result;
        }

        /// <summary>
        /// 模型转换
        /// </summary>
        /// <param name="_rmTypeList"></param>
        /// <returns></returns>
        public List<hotel_room_info> DTOToModel(List<CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeWS> _rmTypeList, int hotel_id)
        {
            List<hotel_room_info> result = _rmTypeList.Select(o => new hotel_room_info
            {
                h_r_id = "0116",
                code = o.code,
                hotel_id = hotel_id,
                h_r_name_cn = o.CName,
                h_r_name_en = o.EName,
                h_r_description_cn = o.descript,
                h_r_description_en = o.edescript,
                //x h_r_acreage=null,
                category = o.category,
                house_service = o.features,
                h_r_bed_number = o.stdPax.ToString(),
                h_r_house_number = o.qty.ToString(),
                Comments = o.remarks,
                h_r_state = !string.IsNullOrEmpty(o.status) ? (o.status == "1" ? true : false) : false,
                h_r_sort = Convert.ToInt32(o.seqId),
                h_r_ctime = System.DateTime.Now,
                DefaultPrice = Convert.ToDecimal(o.stdRate),
                h_r_acreage = "1",
                h_r_floor = "1",
                h_r_bed_type = o.CName,

            }).ToList();
            return result;
        }

        /// <summary>
        /// 校验是否存在
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool CheckIsAny(hotel_room_info room)
        {
            return dc.hotel_room_info.Where(o => o.code == room.code && o.hotel_id == room.hotel_id).Any();
        }

        /// <summary>
        /// 获取指定房类信息 by code ,hotel_id
        /// </summary>
        /// <param name="_RmTypeWS"></param>
        ///  <param name="code"></param>
        ///   <param name="hotel_id"></param>
        /// <returns></returns>
        public CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeWS GetRmTypeInfo(List<CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeWS> RmTypeWSList, string code)
        {
            return RmTypeWSList.Where(o => o.code == code).FirstOrDefault();
        }

        /// <summary>
        /// 获取房型信息  by‘ code hotel_id
        /// </summary>
        /// <param name="code"></param>
        /// <param name="hotel_id"></param>
        /// <returns></returns>
        public hotel_room_info GetRoomInfo(string code, int hotel_id)
        {
            return dc.hotel_room_info.Where(o => o.code == code && o.hotel_id == hotel_id).FirstOrDefault();
        }

    }
}
