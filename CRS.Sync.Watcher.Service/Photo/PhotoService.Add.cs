using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.Photo
{
    public partial class PhotoService
    {
        #region HotelPic
        public bool AddList(IEnumerable<hotel_picture_info> piclist)
        {
            bool result = false;
            try
            {
                dc.hotel_picture_info.InsertAllOnSubmit(piclist);
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool Add(hotel_picture_info pic)
        {
            bool result = false;
            try
            {

                dc.hotel_picture_info.InsertOnSubmit(pic);
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion

        #region RoomPic
        public bool AddList(IEnumerable<hotel_room_picture_info> piclist)
        {
            bool result = false;
            try
            {
                dc.hotel_room_picture_info.InsertAllOnSubmit(piclist);
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool Add(hotel_room_picture_info pic)
        {
            bool result = false;
            try
            {

                dc.hotel_room_picture_info.InsertOnSubmit(pic);
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion

    }
}
