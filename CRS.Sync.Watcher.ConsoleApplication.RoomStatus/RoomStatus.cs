using CRS.Sync.Watcher.DLL;
using CRS.Sync.Watcher.Linq;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using System.Threading;
using System.Data.SqlClient;
using System.Data.Common;

namespace CRS.Sync.Watcher.ConsoleApplication.RoomStatus
{
    public class RoomStatus
    {
        CRS.Sync.Watcher.Service.RatePlan.IRatePlanService _ratePlanService = new CRS.Sync.Watcher.Service.RatePlan.RatePlanService();
        CRS.Sync.Watcher.Service.Hotel.IHotelService _hotelService = new CRS.Sync.Watcher.Service.Hotel.HotelService();
        CRS.Sync.Watcher.Service.House.IHouseService _houseService = new CRS.Sync.Watcher.Service.House.HouseService();

        //! 创建日志记录组件实例  
        public static ILog log = log4net.LogManager.GetLogger(typeof(RoomStatus));
        estay_ecsdbDataContext dc = ConnHelper.estay_ecsdb();

        public void GetHotelRoomStatus()
        {
            //! 1. 获取source_id=6的供应商数据
            List<CRS.Sync.Watcher.Linq.hotel_info> hoteList = GetCRSHotelList().Select(o => new CRS.Sync.Watcher.Linq.hotel_info
            {
                hotel_id = o.hotel_id,
                h_id = o.h_id,
                source_id = o.source_id
            }).ToList();
            DateTime Check_in = System.DateTime.Now.Date;
            DateTime Check_out = Check_in.AddMonths(3).Date;
            foreach (hotel_info _hoteList in hoteList)
            {
                //! CRS开发人员官方解释如下：
                //!  1. 根据酒店日期区间获取当前酒店下所有符合的房类代码和价格代码
                //!  2. 根据房类代码和房价代码获取具体的房类信息和房价代码信息
                #region GetPriceInfo
                //!  Channel 房价代码售卖渠道释义：
                //! 10: CRS
                //! 20: 前台
                //! 30: 官网
                CRS.Sync.Watcher.Service.WCFMobileServer.RoomRateWsGet _roomRateWsGet = _ratePlanService.GetCRSHotelRoomRateByChannel(int.Parse(_hoteList.h_id), "ALL", System.DateTime.Now.ToString("yyyy-MM-dd"), System.DateTime.Now.Date.AddMonths(3).ToString("yyyy-MM-dd"), "30");
                if (_roomRateWsGet != null)
                {
                    if (_roomRateWsGet.result == 0)
                    {
                        if (_roomRateWsGet.roomRateWS != null)
                        {
                            IEnumerable<RoomRateWS> roomRateWSList = _roomRateWsGet.roomRateWS
                                .Select(o => new RoomRateWS
                                {
                                    guid = System.Guid.NewGuid().ToString(),
                                    hotelId = _hoteList.hotel_id, //x int.Parse(_hoteList.h_id),
                                    minVacRooms = o.minVacRooms,
                                    rateCode = o.rateCode,
                                    rateDate = Convert.ToDateTime(o.rateDate),
                                    rmType = o.rmType,
                                    vacRooms = o.vacRooms
                                });
                            if (roomRateWSList != null && roomRateWSList.Count() > 0)
                            {
                                IEnumerable<CRS.Sync.Watcher.Linq.RoomStatus> RoomStatusList = roomRateWSList.Select(o => new CRS.Sync.Watcher.Linq.RoomStatus
                                {
                                    room_id = _houseService.GetRoomInfo(o.rmType, o.hotelId).room_id,
                                    r_s_time = Convert.ToDateTime(o.rateDate),
                                    //EndDate = Convert.ToDateTime(o.rateDate).AddDays(1),
                                    r_s_number = o.vacRooms,
                                    OverBooking = false,
                                    r_s_status = true,
                                    hotel_id = o.hotelId, //x int.Parse(_hoteList.h_id),
                                    eBeds = o.vacRooms,
                                    r_s_ctime = System.DateTime.Now
                                    //minVacRooms = o.minVacRooms,
                                    //vacRooms = o.vacRooms

                                });

                                foreach (CRS.Sync.Watcher.Linq.RoomStatus _RoomStatusList in RoomStatusList)
                                {
                                    try
                                    {
                                        if (_ratePlanService.CheckIsAnyRoomStatus(_RoomStatusList))
                                        {
                                            _ratePlanService.DeleteRoomStatus(_RoomStatusList);
                                        }
                                        dc.RoomStatus.InsertOnSubmit(_RoomStatusList);
                                        dc.SubmitChanges();
                                        Console.WriteLine("监测到 " + _RoomStatusList.r_s_time.Date + " 发生改变，对应的room_id=" + _RoomStatusList.room_id + " 可用房数更新为" + _RoomStatusList.r_s_number);

                                    }
                                    catch (Exception e)
                                    {
                                        log.Error(e);
                                        log.Debug("友好消息：" + e.Message + " \r\n Develoer：" + e.StackTrace + " ");
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        string messages = "GetCRSHotelRoomRateByChannel 接口返回出错！！！ 请求参数 h_id=" + _hoteList.h_id + " check_in=" + System.DateTime.Now.ToString("yyyy-MM-dd") + " check_out=" + System.DateTime.Now.ToString("yyyy-MM-dd") + "";
                        log.Warn(messages);
                        Console.WriteLine(messages);
                    }
                }
                #endregion
            }
        }




        /// <summary>
        /// 获取公寓信息-动态
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CRS.Sync.Watcher.Linq.hotel_info> GetCRSHotelList()
        {
            Expression<Func<CRS.Sync.Watcher.Linq.hotel_info, bool>> expression = PredicateBuilder.True<CRS.Sync.Watcher.Linq.hotel_info>();
            expression = expression.And(o => o.source_id == null ? false : o.source_id == 6);
            IEnumerable<CRS.Sync.Watcher.Linq.hotel_info> _result = _hotelService.GetHoteListInfo(expression).AsEnumerable();
            if (_result != null)
            {
                return _result;
            }
            else
                return null;
        }

    }
}
