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

        public bool Update(hotel_picture_info pic)
        {
            bool result = false;
            //hotel_picture_info pic = dc.hotel_picture_info.Where(o => o. == hotel.hotel_id).FirstOrDefault();
            //_hotel = hotel;

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

        #endregion

        #region RoomPic

        //!  具体实现

        #endregion

    }
}
