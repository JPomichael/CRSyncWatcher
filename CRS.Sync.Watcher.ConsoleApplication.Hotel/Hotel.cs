using CRS.Sync.Watcher.DLL;
using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CRS.Sync.Watcher.ConsoleApplication.Hotel
{
    public class Hotel
    {
        #region 服务引用
        CRS.Sync.Watcher.Service.Hotel.IHotelService _hotelService = new CRS.Sync.Watcher.Service.Hotel.HotelService();
        CRS.Sync.Watcher.Service.House.IHouseService _houseService = new CRS.Sync.Watcher.Service.House.HouseService();
        CRS.Sync.Watcher.Service.Photo.IPhotoService _photoService = new CRS.Sync.Watcher.Service.Photo.PhotoService();
        CRS.Sync.Watcher.Service.City.ICityService _cityService = new CRS.Sync.Watcher.Service.City.CityService();
        #endregion

        /// <summary>
        /// 公寓同步
        /// </summary>
        /// <param name="_CRSHotelParamsDTO">请求对象</param>
        /// <param name="staticFolderSavePath">保存目录</param>
        /// <returns></returns>
        public void SyncService(CRS.Sync.Watcher.Service.WCFMobileServer.CRSHotelParamsDTO _CRSHotelParamsDTO, string staticFolderSavePath)
        {
            string _filename = "hotel.xml";
            //!   获取完整路径
            string _fileSavePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + "\\", staticFolderSavePath + _filename);

            //! 请求通用代码
            CRS.Sync.Watcher.Service.WCFMobileServer.HotelGet hotelGetList = _hotelService.GetCRSHotelInterface(_CRSHotelParamsDTO);
            if (hotelGetList != null)
            {
                if (hotelGetList.result == 0)
                {
                    if (hotelGetList.hotelDTOs != null)
                    {
                        //! 格式化酒店描述
                        foreach (var _hotelDTO in hotelGetList.hotelDTOs)
                        {
                            _hotelDTO.descript = StringHelper.ParseHtml(_hotelDTO.descript);
                            _hotelDTO.EDescript = StringHelper.ParseHtml(_hotelDTO.EDescript);
                        }

                        //! 保存/更新本机
                        XmlUtil.SerializeToXml(hotelGetList, hotelGetList.GetType(), _fileSavePath, null);

                        //! 加载本地XML 到 HotelDTO
                        List<CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO> _hotelDtoList = _hotelService.GetReadXMLToObject(_fileSavePath);

                        //!  转换 Table Model
                        IEnumerable<hotel_info> hoteList = _hotelService.HotelDTOToModel(_hotelDtoList);

                        //! 检测/更新本机
                        foreach (var _hoteList in hoteList)
                        {
                            int hotelid = 0;
                            if (!_hotelService.CheckIsAny(_hoteList))
                            {
                                hotelid = _hotelService.Add(_hoteList);
                            }
                            else
                            {
                                _hoteList.h_utime = System.DateTime.Now;
                                hotelid = _hotelService.Update(_hoteList);
                            }
                            Console.WriteLine(" hotel_id= " + hotelid + " 的公寓数据已更新.");
                            HotelUpperService(_fileSavePath, _hoteList);

                        }
                    }
                }
                else
                    Console.WriteLine("GetCRSHotelInterface 接口返回 hotelGetList.result=" + hotelGetList.result + "!");
            }
        }

        /// <summary>
        /// 公寓上层服务
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="_fileSavePath"></param>
        /// <param name="_hoteList"></param>
        /// <returns></returns>
        public void HotelUpperService(string _fileSavePath, hotel_info _hoteList)
        {
            hotel_info hotel = GetHotelIdByHID(_hoteList);
            if (hotel != null)
            {
                //!  公寓图片操作
                HotelPhotoUpdate(_fileSavePath, _hoteList, hotel.hotel_id);
                //! 房型操作
                HouseService(_hoteList, hotel.hotel_id);
            }
        }

        /// <summary>
        /// 房型操作
        /// </summary>
        /// <param name="_hoteList"></param>
        public void HouseService(hotel_info _hoteList, int hotel_id)
        {
            CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeGet _RmTypeGet = _houseService.GetCRSRmTypeInterface(int.Parse(_hoteList.h_id), "", "", "");
            if (_RmTypeGet != null)
            {
                if (_RmTypeGet.result == 0)
                {
                    if (_RmTypeGet.rmTypes != null)
                    {
                        List<CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeWS> rmTypes = _RmTypeGet.rmTypes.ToList();
                        IEnumerable<hotel_room_info> roomList = _houseService.DTOToModel(rmTypes, hotel_id);
                        foreach (var _roomList in roomList)
                        {
                            int roomid = 0;
                            //! 检验是否存在当前房型
                            if (!_houseService.CheckIsAny(_roomList))
                            {
                                roomid = _houseService.Add(_roomList);
                            }
                            else
                            {       //! 已存在 则更新
                                _roomList.h_r_utime = System.DateTime.Now;
                                roomid = _houseService.Update(_roomList);
                            }
                            //! 房型图片
                            RoomPhotoUpdate(rmTypes, _roomList);
                            Console.WriteLine(" room_id= " + roomid + " 的房型数据已更新. ");

                        }
                    }
                }
                else
                    Console.WriteLine("  hotel_id= " + hotel_id + " GetCRSRmTypeInterface 接口返回 RmTypeGet.result == " + _RmTypeGet.result + "");
            }
        }

        /// <summary>
        /// 房型图片更新
        /// </summary>
        /// <param name="rmTypes"></param>
        /// <param name="_roomList"></param>
        public void RoomPhotoUpdate(List<CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeWS> rmTypes, hotel_room_info _roomList)
        {
            //! 1 获取当前房类信息
            CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeWS rmType = _houseService.GetRmTypeInfo(rmTypes, _roomList.code);
            hotel_room_info room = _houseService.GetRoomInfo(_roomList.code, _roomList.hotel_id);
            if (room != null)
            {
                List<hotel_room_picture_info> roomPicInfo = _photoService.GetRoomPicsDTOToModel(rmType, room.room_id);
                if (roomPicInfo != null && roomPicInfo.Count > 0)
                {
                    for (int i = 0; i < roomPicInfo.Count; i++)
                    {
                        if (_photoService.CheckIsAny(roomPicInfo[i], room.room_id))
                            //!  存在 则从LIST中移除
                            roomPicInfo.Remove(roomPicInfo[i]);
                    }
                    _photoService.AddList(roomPicInfo.AsEnumerable());
                }
            }
        }

        /// <summary>
        /// 公寓图片更新
        /// </summary>
        /// <param name="_fileSavePath"></param>
        /// <param name="_hoteList"></param>
        public void HotelPhotoUpdate(string _fileSavePath, hotel_info _hoteList, int hotel_id)
        {
            //! 录入公寓图片
            CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO _hotelPics = _photoService.GetReadXMLPicsToObject(_hoteList.h_id, _fileSavePath);

            List<hotel_picture_info> hotelPicInfo = _photoService.GetHotelPicsDTOToModel(_hotelPics, hotel_id);
            if (hotelPicInfo != null && hotelPicInfo.Count > 0)
            {
                for (int i = 0; i < hotelPicInfo.Count; i++)
                {
                    //!  校验该图是否存在
                    if (_photoService.CheckIsAny(hotelPicInfo[i], hotel_id))
                        //!  存在 则从LIST中移除
                        hotelPicInfo.Remove(hotelPicInfo[i]);
                }
                //! ！null  录入
                _photoService.AddList(hotelPicInfo.AsEnumerable());

            }
        }

        /// <summary>
        /// 获取公寓指定公寓
        /// </summary>
        /// <param name="_hoteList"></param>
        /// <returns></returns>
        public hotel_info GetHotelIdByHID(hotel_info _hoteList)
        {
            #region 查找hotel_id筛选条件
            Expression<Func<hotel_info, bool>> expression = PredicateBuilder.True<hotel_info>();
            expression = expression.And(o => o.h_id == null ? false : o.h_id == _hoteList.h_id);
            //x expression = expression.And(o => o == null ? false : o.h_id == _hoteList.h_id);
            expression = expression.And(o => o.source_id == null ? false : o.source_id == _hoteList.source_id);
            #endregion
            //!  得到相关hotel_id
            hotel_info _hotelinfo = _hotelService.GetHoteListInfo(expression).Take(1).FirstOrDefault();
            return _hotelinfo;
        }

    }
}
