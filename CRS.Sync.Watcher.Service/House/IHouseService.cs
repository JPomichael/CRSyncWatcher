using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.House
{
    public interface IHouseService
    {
        #region 调用CRS接口
        CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeGet GetCRSRmTypeInterface(int hotelId, string code, string cName, string eName);
        #endregion

        bool AddList(IEnumerable<hotel_room_info> roomlist);
        //bool Add(hotel_room_info room);
        int Add(hotel_room_info room);

        //bool Update(hotel_room_info house);
        int Update(hotel_room_info house);
        bool Delete(int room_id, int? hotel_id);
        bool Delete(int[] idList);
        bool DeleteTrue(IEnumerable<hotel_room_info> houselist);
        bool DleteTrue(hotel_room_info house);

        List<hotel_room_info> DTOToModel(List<CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeWS> _rmTypeList, int hotel_id);
        bool CheckIsAny(hotel_room_info room);
        CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeWS GetRmTypeInfo(List<CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeWS> RmTypeWSList, string code);
        hotel_room_info GetRoomInfo(string code, int hotel_id);


    }
}
