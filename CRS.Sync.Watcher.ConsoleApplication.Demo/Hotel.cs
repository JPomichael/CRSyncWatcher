﻿using CRS.Sync.Watcher.DLL;
using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CRS.Sync.Watcher.ConsoleApplication.Demo
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
        public string SyncService(CRS.Sync.Watcher.Service.WCFMobileServer.CRSHotelParamsDTO _CRSHotelParamsDTO, string staticFolderSavePath)
        {
            string messages = "";
            string _filename = "hotel.xml";
            //!   获取完整路径
            string _fileSavePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + "\\", staticFolderSavePath + _filename);

            //! 请求通用代码
            CRS.Sync.Watcher.Service.WCFMobileServer.HotelGet hotelGetList = _hotelService.GetCRSHotelInterface(_CRSHotelParamsDTO);
            if (hotelGetList.result == 0 && hotelGetList.hotelDTOs != null)
            {
                //! 格式化酒店描述
                foreach (var _hotelDTO in hotelGetList.hotelDTOs)
                {
                    _hotelDTO.descript = StringHelper.ParseHtml(_hotelDTO.descript);
                    _hotelDTO.EDescript = StringHelper.ParseHtml(_hotelDTO.EDescript);
                }

                //! 保存/更新本机
                XmlUtil.SerializeToXml(hotelGetList, hotelGetList.GetType(), _fileSavePath, null);

                //! 加载XML 到 HotelDTO
                List<CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO> _hotelDtoList = _hotelService.GetReadXMLToObject(_fileSavePath);

                //!  模型转换
                IEnumerable<hotel_info> hoteList = _hotelService.HotelDTOToModel(_hotelDtoList);

                //! 检测/更新本机
                foreach (var _hoteList in hoteList)
                {
                    if (!_hotelService.CheckIsAny(_hoteList))
                    {
                        //!  录入公寓
                        _hotelService.Add(_hoteList);
                        messages = HotelUpperService(messages, _fileSavePath, _hoteList);
                    }
                    else
                    {
                        //!  更新
                        _hotelService.Update(_hoteList);
                        messages = HotelUpperService(messages, _fileSavePath, _hoteList);
                    }
                }
            }
            else
                messages += "GetCRSHotelInterface  接口返回问题!";
            return messages;
        }

        /// <summary>
        /// 公寓上层服务
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="_fileSavePath"></param>
        /// <param name="_hoteList"></param>
        /// <returns></returns>
        public string HotelUpperService(string messages, string _fileSavePath, hotel_info _hoteList)
        {
            hotel_info hotel = GetHotelIdByHID(_hoteList);
            if (hotel != null)
            {
                //!  公寓图片操作
                HotelPhotoUpdate(_fileSavePath, _hoteList, hotel.hotel_id);
                //TODO:  房型操作
                messages += HouseService(_hoteList, hotel.hotel_id);
            }
            return messages;
        }

        /// <summary>
        /// 房型操作
        /// </summary>
        /// <param name="_hoteList"></param>
        public string HouseService(hotel_info _hoteList, int hotel_id)
        {
            string messages = "";
            CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeGet _RmTypeGet = _houseService.GetCRSRmTypeInterface(int.Parse(_hoteList.h_id), "", "", "");
            if (_RmTypeGet.result == 0 && _RmTypeGet != null && _RmTypeGet.rmTypes != null)
            {
                List<CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeWS> rmTypes = _RmTypeGet.rmTypes.ToList();
                IEnumerable<hotel_room_info> roomList = _houseService.DTOToModel(rmTypes, hotel_id);
                foreach (var _roomList in roomList)
                {
                    //! 检验是否存在当前房型
                    if (!_houseService.CheckIsAny(_roomList))
                        _houseService.Add(_roomList);
                    else
                        //! 已存在 则更新
                        _houseService.Update(_roomList);
                    //TODO: 房型图片
                    RoomPhotoUpdate(rmTypes, _roomList);
                }
                //x _houseService.AddList(roomList);
            }
            else
                messages = "GetCRSRmTypeInterface 接口返回问题!";
            return messages;
        }

        /// <summary>
        /// 房型图片更新
        /// </summary>
        /// <param name="rmTypes"></param>
        /// <param name="_roomList"></param>
        public void RoomPhotoUpdate(List<CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeWS> rmTypes, hotel_room_info _roomList)
        {
            //! 1 获取当前房类信息
            CRS.Sync.Watcher.Service.WCFMobileServer.RmTypeWS rmType = _houseService.GetRmTypeInfo(rmTypes, _roomList.code, _roomList.hotel_id);
            hotel_room_info room = _houseService.GetRoomInfo(_roomList.code, _roomList.hotel_id);
            if (room != null)
            {
                List<hotel_room_picture_info> roomPicInfo = _photoService.GetRoomPicsDTOToModel(rmType, room.room_id);
                if (roomPicInfo != null && roomPicInfo.Count > 0)
                {
                    foreach (var _roomPicInfo in roomPicInfo)
                    {
                        if (_photoService.CheckIsAny(_roomPicInfo, room.room_id))
                            //!  存在 则从LIST中移除
                            roomPicInfo.Remove(_roomPicInfo);
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
            //TODO: 录入公寓图片
            CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO _hotelPics = _photoService.GetReadXMLPicsToObject(_hoteList.h_id, _fileSavePath);

            List<hotel_picture_info> hotelPicInfo = _photoService.GetHotelPicsDTOToModel(_hotelPics, hotel_id);
            if (hotelPicInfo != null)
            {
                foreach (var _hotelPicInfo in hotelPicInfo)
                {
                    //!  校验该图是否存在
                    if (_photoService.CheckIsAny(_hotelPicInfo, hotel_id))
                        //!  存在 则从LIST中移除
                        hotelPicInfo.Remove(_hotelPicInfo);
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
