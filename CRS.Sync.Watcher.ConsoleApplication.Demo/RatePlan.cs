using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SQLite;
using CRS.Sync.Watcher.DLL;
using System.Data.SQLite.Linq;
using CRS.Sync.Watcher.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Data;
using log4net;

namespace CRS.Sync.Watcher.ConsoleApplication.Demo
{
    public class RatePlan
    {
        CRS.Sync.Watcher.Service.RatePlan.IRatePlanService _ratePlanService = new CRS.Sync.Watcher.Service.RatePlan.RatePlanService();
        CRS.Sync.Watcher.Service.Hotel.IHotelService _hotelService = new CRS.Sync.Watcher.Service.Hotel.HotelService();

        //! 创建日志记录组件实例  
        public static ILog log = log4net.LogManager.GetLogger(typeof(RatePlan));

        /// <summary>
        /// 同步服务应用
        /// </summary>
        /// <returns></returns>
        public string SyncService()
        {
            string messages = "";
            //! 1. 获取source_id=6的供应商数据
            List<hotel_info> hoteList = GetCRSHotelList().ToList();
            string sql = "insert into RoomRateWS values(@hotelId, @rmTypeEName, @breakfastEDesc,@minVacRooms,@rateCodeCName,@rateCodeEName,@rmTypeEDesc,@ratePrice,@rateCode,@rateDate,@rmType,@rmTypeCDesc,@rmTypeCName,@vacRooms,@breakfastDesc,@needPay,@needGuarant)";
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
                            #region T-SQL
                            List<CRS.Sync.Watcher.Service.WCFMobileServer.RoomRateWS> roomRateWSList = _roomRateWsGet.roomRateWS.ToList();

                            //TODO: 这里减少循环 =》 减少事物的提交
                            foreach (CRS.Sync.Watcher.Service.WCFMobileServer.RoomRateWS _roomRateWSList in roomRateWSList)
                            {
                                SQLiteParameter[] parameters = new SQLiteParameter[] { 
                                         //添加参数  
                                         new SQLiteParameter("@hotelId",_hoteList.h_id),  
                                         new SQLiteParameter("@rmTypeEName", _roomRateWSList.rmTypeEName),
                                         new SQLiteParameter("@breakfastEDesc", _roomRateWSList.breakfastEDesc), 
                                         new SQLiteParameter("@minVacRooms", _roomRateWSList.minVacRooms), 
                                         new SQLiteParameter("@rateCodeCName", _roomRateWSList.rateCodeCName),  
                                         new SQLiteParameter("@rateCodeEName", _roomRateWSList.rateCodeEName),  
                                         new SQLiteParameter("@rmTypeEDesc", _roomRateWSList.rmTypeEDesc),  
                                         new SQLiteParameter("@ratePrice", _roomRateWSList.ratePrice),  
                                         new SQLiteParameter("@rateCode", _roomRateWSList.rateCode),  
                                         new SQLiteParameter("@rateDate", _roomRateWSList.rateDate),  
                                         new SQLiteParameter("@rmType", _roomRateWSList.rmType),  
                                         new SQLiteParameter("@rmTypeCDesc", _roomRateWSList.rmTypeCDesc),  
                                         new SQLiteParameter("@rmTypeCName", _roomRateWSList.rmTypeCName),  
                                         new SQLiteParameter("@vacRooms", _roomRateWSList.vacRooms),  
                                         new SQLiteParameter("@breakfastDesc", _roomRateWSList.breakfastDesc),  
                                         new SQLiteParameter("@needPay", _roomRateWSList.needPay),  
                                         new SQLiteParameter("@needGuarant", _roomRateWSList.needGuarant)
                            };
                                try
                                {
                                    SQLiteHelper.ExecuteNonQuery(sql, parameters);
                                }
                                catch (Exception e)
                                {
                                    log.Error(e);
                                    throw new Exception("insert into RoomRateWS unsuccessfully!!! : " + e.Message + "");
                                }

                            }
                            #endregion

                        }
                    }
                    else
                        messages += "GetCRSHotelRoomRateByChannel 接口返回出错！！！\r\n";
                }
                #endregion
            }

            //! 获取并过滤得 hotelId ,  rateCode ,  rmType
            //SQLiteDataReader reader = SQLiteHelper.ExecuteReader("select hotelId,rateCode,rmType from RoomRateWS  where hotelId=" + int.Parse(_hoteList.h_id) + " group by rmType,rateCode", null);
            //List<CRS.Sync.Watcher.Domain.Dto.RoomRateWSDTO> RoomRateWSDTOList = new List<Domain.Dto.RoomRateWSDTO>();
            //if (reader.HasRows)
            //{
            //    #region 1. 根据酒店日期区间获取当前酒店下所有符合的房类代码和价格代码
            //    // Call Read before accessing data.
            //    while (reader.Read())
            //    {
            //        CRS.Sync.Watcher.Domain.Dto.RoomRateWSDTO roomRateWSDTO = new Domain.Dto.RoomRateWSDTO();
            //        roomRateWSDTO.hotelId = Convert.ToInt32(reader.GetValue(0));
            //        roomRateWSDTO.rateCode = reader.GetValue(1).ToString();
            //        roomRateWSDTO.rmType = reader.GetValue(2).ToString();
            //        RoomRateWSDTOList.Add(roomRateWSDTO);

            //    }
            //    // Call Close when done reading.
            //    reader.Close();
            //    #endregion

            //}

            //if (RoomRateWSDTOList != null && RoomRateWSDTOList.Count() >= 0)
            //{
            //    #region  2. 根据房类代码和房价代码获取具体的房类信息和房价代码信息
            //    foreach (CRS.Sync.Watcher.Domain.Dto.RoomRateWSDTO _RoomRateWSDTOList in RoomRateWSDTOList)
            //    {
            //        #region GetRateControl
            //        //获取Rate规则信息
            //        CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeControlGet _rateCodeControlGet = _ratePlanService.GetCRSRateCodeControl(int.Parse(_hoteList.h_id), _RoomRateWSDTOList.rateCode, _RoomRateWSDTOList.rmType, System.DateTime.Now.ToString("yyyy-MM-dd"), System.DateTime.Now.Date.AddMonths(3).ToString("yyyy-MM-dd"));
            //        if (_rateCodeControlGet != null)
            //        {
            //            if (_rateCodeControlGet.result == 0)
            //            {
            //                if (_rateCodeControlGet.rateCodeControl != null && !string.IsNullOrEmpty(_rateCodeControlGet.rateCodeControl.amendStatus))
            //                {
            //                    //! 存储接口返回数据
            //                    CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeControl _rateCodeControl = _rateCodeControlGet.rateCodeControl;
            //                    SQLiteParameter[] parameters = new SQLiteParameter[] { 
            //                                 //添加参数  
            //                                 new SQLiteParameter("@hotelId", int.Parse(_hoteList.h_id)),  
            //                                 new SQLiteParameter("@rateCode", _rateCodeControl.rateCode),
            //                                 new SQLiteParameter("@amendStatus", _rateCodeControl.amendStatus), 
            //                                 new SQLiteParameter("@amendDays", _rateCodeControl.amendDays), 
            //                                 new SQLiteParameter("@cancelStatus", _rateCodeControl.cancelStatus),  
            //                                 new SQLiteParameter("@cancelDays", _rateCodeControl.cancelDays),  
            //                                 new SQLiteParameter("@needPay", _rateCodeControl.needPay),  
            //                                 new SQLiteParameter("@minStay", _rateCodeControl.minStay),  
            //                                 new SQLiteParameter("@maxStay", _rateCodeControl.maxStay),  
            //                                 new SQLiteParameter("@booking", _rateCodeControl.booking),  
            //                                 new SQLiteParameter("@rmLimit", _rateCodeControl.rmLimit) 
            //                    };
            //                    try
            //                    {
            //                        SQLiteHelper.ExecuteNonQuery("insert into RateCodeControl values(@hotelId, @rateCode, @amendStatus,@amendDays,@cancelStatus,@cancelDays,@needPay,@minStay,@maxStay,@booking,@rmLimit)", parameters);
            //                    }
            //                    catch (Exception e)
            //                    {
            //                        log.Error(e);
            //                        throw new Exception("insert into RateCodeControl unsuccessfully!!! : " + e.Message + "");
            //                    }
            //                }
            //            }
            //            else
            //                messages += "GetCRSRateCodeControl 接口返回出错！！！\r\n";
            //        }
            //        #endregion

            //        #region GetPriceInfor
            //        //! 获取价格属性信息
            //        CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeInforGet _RateCodeInforGet = _ratePlanService.GetCRSRateCodeInfor(int.Parse(_hoteList.h_id), _RoomRateWSDTOList.rateCode);
            //        if (_RateCodeInforGet != null)
            //        {
            //            if (_RateCodeInforGet.result == 0)
            //            {
            //                if (_RateCodeInforGet.rateCodeInfor != null)
            //                {
            //                    CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeInfor _rateCodeInfor = _RateCodeInforGet.rateCodeInfor;
            //                    SQLiteParameter[] parameters = new SQLiteParameter[] { 
            //                                             //添加参数  
            //                                             new SQLiteParameter("@hotelId", int.Parse(_hoteList.h_id)),  
            //                                             new SQLiteParameter("@rateCode", _rateCodeInfor.rateCode),
            //                                             new SQLiteParameter("@cName", _rateCodeInfor.cName), 
            //                                             new SQLiteParameter("@eName", _rateCodeInfor.eName), 
            //                                             new SQLiteParameter("@memberType", _rateCodeInfor.memberType),  
            //                                             new SQLiteParameter("@cardType", _rateCodeInfor.cardType),  
            //                                             new SQLiteParameter("@isContract", _rateCodeInfor.isContract),  
            //                                             new SQLiteParameter("@currency", _rateCodeInfor.currency),  
            //                                             new SQLiteParameter("@rateCat", _rateCodeInfor.rateCat),  
            //                                             new SQLiteParameter("@begDate", _rateCodeInfor.begDate),  
            //                                             new SQLiteParameter("@endDate", _rateCodeInfor.endDate), 
            //                                             new SQLiteParameter("@isdiscount", _rateCodeInfor.isdiscount), 
            //                                             new SQLiteParameter("@market", _rateCodeInfor.market), 
            //                                             new SQLiteParameter("@source", _rateCodeInfor.source), 
            //                                             new SQLiteParameter("@weekenddays", _rateCodeInfor.weekenddays), 
            //                                             new SQLiteParameter("@ptCoef", _rateCodeInfor.ptCoef), 
            //                                             new SQLiteParameter("@discountOf", _rateCodeInfor.discountOf) ,
            //                                             new SQLiteParameter("@discountType", _rateCodeInfor.discountType), 
            //                                             new SQLiteParameter("@discountValue", _rateCodeInfor.discountValue),
            //                                             new SQLiteParameter("@roundType", _rateCodeInfor.roundType),
            //                                             new SQLiteParameter("@targetRound", _rateCodeInfor.targetround),
            //                                             new SQLiteParameter("@status", _rateCodeInfor.status),
            //                                             new SQLiteParameter("@defaultShow", _rateCodeInfor.defaultShow),
            //                                             new SQLiteParameter("@sellBegDate", _rateCodeInfor.sellBegDate),
            //                                             new SQLiteParameter("@sellEndDate", _rateCodeInfor.sellEndDate),
            //                                             new SQLiteParameter("@bookingThrough", _rateCodeInfor.bookingThrough),
            //                                             new SQLiteParameter("@aliasCn", _rateCodeInfor.aliasCn),
            //                                             new SQLiteParameter("@aliasEn", _rateCodeInfor.aliasEn),
            //                                             new SQLiteParameter("@contractId", _rateCodeInfor.contractId),
            //                                             new SQLiteParameter("@dailyinvallocation", _rateCodeInfor.dailyinvallocation),
            //                                             new SQLiteParameter("@descript", _rateCodeInfor.descript),
            //                                             new SQLiteParameter("@edescript", _rateCodeInfor.edescript),
            //                                             new SQLiteParameter("@frtMarket", _rateCodeInfor.frtMarket),
            //                                             new SQLiteParameter("@frtSource", _rateCodeInfor.frtSource),
            //                                             new SQLiteParameter("@isUpward", _rateCodeInfor.isUpward),
            //                                             new SQLiteParameter("@remarks", _rateCodeInfor.remarks),
            //                                             new SQLiteParameter("@syncstatus", _rateCodeInfor.syncstatus)
            //                                };
            //                    try
            //                    {
            //                        SQLiteHelper.ExecuteNonQuery("insert into RateCodeInfor values(@hotelId, @rateCode, @cName,@eName,@memberType,@cardType,@isContract,@currency,@rateCat,@begDate,@endDate,@isdiscount,@market,@source,@weekenddays,@ptCoef,@discountOf,@discountType,@discountValue,@roundType,@targetRound,@status,@defaultShow,@sellBegDate,@sellEndDate,@bookingThrough,@aliasCn,@aliasEn,@contractId,@dailyinvallocation,@descript,@edescript,@frtMarket,@frtSource,@isUpward,@remarks,@syncstatus)", parameters);
            //                    }
            //                    catch (Exception e)
            //                    {
            //                        log.Error(e);
            //                        throw new Exception("insert into RateCodeInfor unsuccessfully!!! : " + e.Message + "");
            //                    }
            //                }
            //            }
            //            else
            //                messages += "GetCRSRateCodeInfor 接口返回出错！！！\r\n";
            //        }
            //        #endregion
            //    }
            //    #endregion
            //}

            return messages;
        }

        /// <summary>
        /// 获取公寓信息-构建动态
        /// </summary>
        /// <returns></returns>
        public IEnumerable<hotel_info> GetCRSHotelList()
        {
            Expression<Func<hotel_info, bool>> expression = PredicateBuilder.True<hotel_info>();
            expression = expression.And(o => o.source_id == null ? false : o.source_id == 6);
            IEnumerable<hotel_info> _result = _hotelService.GetHoteListInfo(expression).AsEnumerable();
            if (_result != null)
            {
                return _result;
            }
            else
                return null;
        }


    }
}
