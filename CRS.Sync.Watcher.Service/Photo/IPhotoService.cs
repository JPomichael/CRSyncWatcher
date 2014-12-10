using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.Photo
{
    public interface IPhotoService
    {
        #region HotelPic
        bool AddList(IEnumerable<hotel_picture_info> piclist);

        bool Add(hotel_picture_info pic);

        bool Update(hotel_picture_info pic);

        CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO GetReadXMLPicsToObject(string h_id, string _fileSavePath);

        List<hotel_picture_info> GetHotelPicsDTOToModel(CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO _hotelPics, int hotel_id);

        bool CheckIsAny(hotel_picture_info pic, int hotel_id);
        #endregion

        #region RoomPic
        bool AddList(IEnumerable<hotel_room_picture_info> piclist);

        bool Add(hotel_room_picture_info pic);

        List<hotel_room_picture_info> GetRoomPicsDTOToModel(CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeWS rmType, int room_id);

        bool CheckIsAny(hotel_room_picture_info pic, int room_id);
        #endregion

    }
}
