using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CRS.Sync.Watcher.Service.Photo
{
    public partial class PhotoService
    {
        #region HotelPic

        /// <summary>
        /// 加载本机公寓图片
        /// </summary>
        /// <param name="h_id"></param>
        /// <param name="_fileSavePath"></param>
        /// <returns></returns>
        public CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO GetReadXMLPicsToObject(string h_id, string _fileSavePath)
        {
            XElement xmlinq = XElement.Load(_fileSavePath);
            var hotelDTOs = xmlinq.Element("hotelDTOs");
            CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO result = hotelDTOs.Descendants("HotelDTO")
                .Where(o => o.Element("hotelId").Value == h_id)
                .Select(o => new CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO
                {
                    hotelId = Convert.ToInt32(o.Element("hotelId").Value),
                    code = o.Element("code").Value,
                    photo1 = o.Element("photo1").Value,
                    photo2 = o.Element("photo2").Value,
                    photo3 = o.Element("photo3").Value,
                }).Take(1).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// 公寓图片DTO转换模型
        /// </summary>
        /// <param name="_hotelPics"></param>
        /// <param name="hotel_id"> 已转化后</param>
        /// <returns></returns>
        public List<hotel_picture_info> GetHotelPicsDTOToModel(CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO _hotelPics, int hotel_id)
        {
            List<hotel_picture_info> hotelpic = new List<hotel_picture_info>();
            hotel_picture_info p = null;

            if (!string.IsNullOrEmpty(_hotelPics.photo1))
            {
                p = new hotel_picture_info();
                p.hotel_id = hotel_id;
                p.h_p_time = System.DateTime.Now;
                p.h_p_sort = 1000;
                p.h_p_pic_original_url = _hotelPics.photo1;
                p.h_p_pic_thumb_url = _hotelPics.photo1;
                hotelpic.Add(p);
            }
            if (!string.IsNullOrEmpty(_hotelPics.photo2))
            {
                p = new hotel_picture_info();
                p.hotel_id = hotel_id;
                p.h_p_time = System.DateTime.Now;
                p.h_p_sort = 1000;
                p.h_p_pic_original_url = _hotelPics.photo2;
                p.h_p_pic_thumb_url = _hotelPics.photo2;
                hotelpic.Add(p);
            }
            if (!string.IsNullOrEmpty(_hotelPics.photo3))
            {
                p = new hotel_picture_info();
                p.hotel_id = hotel_id;
                p.h_p_time = System.DateTime.Now;
                p.h_p_sort = 1000;
                p.h_p_pic_original_url = _hotelPics.photo3;
                p.h_p_pic_thumb_url = _hotelPics.photo3;
                hotelpic.Add(p);
            }
            return hotelpic;
        }


        /// <summary>
        /// 校验是否已存在
        /// </summary>
        /// <param name="pic"></param>
        /// <param name="hotel_id"></param>
        /// <returns></returns>
        public bool CheckIsAny(hotel_picture_info pic, int hotel_id)
        {
            return dc.hotel_picture_info.Where(o => o.hotel_id == hotel_id && o.h_p_pic_original_url == pic.h_p_pic_original_url || o.h_p_pic_thumb_url == pic.h_p_pic_thumb_url).Any();
        }

        #endregion

        #region RoomPic

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="rmType"></param>
        /// <param name="hotel_id"></param>
        /// <returns></returns>
        public List<hotel_room_picture_info> GetRoomPicsDTOToModel(CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeWS rmType, int room_id)
        {
            List<hotel_room_picture_info> pics = new List<hotel_room_picture_info>();
            hotel_room_picture_info p = null;

            if (!string.IsNullOrEmpty(rmType.photo1))
            {
                p = new hotel_room_picture_info();
                p.room_id = room_id;
                p.h_r_p_time = System.DateTime.Now;
                p.h_r_p_sort = 1000;
                p.h_r_p_pic_original_url = rmType.photo1;
                p.h_r_p_pic_thumb_url = rmType.photo1;
                pics.Add(p);
            }
    
            if (!string.IsNullOrEmpty(rmType.photo2))
            {
                p = new hotel_room_picture_info();
                p.room_id = room_id;
                p.h_r_p_time = System.DateTime.Now;
                p.h_r_p_sort = 1000;
                p.h_r_p_pic_original_url = rmType.photo1;
                p.h_r_p_pic_thumb_url = rmType.photo1;
                pics.Add(p);
            }
            return pics;
        }

        /// <summary>
        /// 检验房型图片-  by  room_id
        /// </summary>
        /// <param name="pic"></param>
        /// <param name="room_id"></param>
        /// <returns></returns>
        public bool CheckIsAny(hotel_room_picture_info pic, int room_id)
        {
            return dc.hotel_room_picture_info.Where(o => o.room_id == room_id && o.h_r_p_pic_original_url == pic.h_r_p_pic_original_url || o.h_r_p_pic_thumb_url == pic.h_r_p_pic_thumb_url).Any();
        }

        #endregion

    }
}
