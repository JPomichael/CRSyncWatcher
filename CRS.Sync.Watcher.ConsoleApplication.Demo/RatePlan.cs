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

namespace CRS.Sync.Watcher.ConsoleApplication.Demo
{
    public class RatePlan
    {
        CRS.Sync.Watcher.Service.RatePlan.IRatePlanService _ratePlanService = new CRS.Sync.Watcher.Service.RatePlan.RatePlanService();
        CRS.Sync.Watcher.Service.Hotel.IHotelService _hotelService = new CRS.Sync.Watcher.Service.Hotel.HotelService();

        public static SQLiteConnection conn = new SQLiteConnection("Data Source=e:\\CRSInterFaceResult.db");
        public static SQLiteCommand cmd;
        public static SQLiteTransaction tran;


        public string SyncService()
        {
            string messages = "";

            //! 1. 获取source_id=6的供应商数据
            List<hotel_info> _hoteList = GetCRSHotelList().ToList();


            #region GetPriceInfo
            //! CRS开发人员官方解释如下：
            //!  1. 根据酒店日期区间获取当前酒店下所有符合的房类代码和价格代码
            //!  2. 根据房类代码和房价代码获取具体的房类信息和房价代码信息
            CRS.Sync.Watcher.Service.WCFMobileServer.RoomRateWsGet _roomRateWsGet = _ratePlanService.GetCRSHotelRoomRateByChannel(int.Parse(_hoteList[0].h_id), "ALL", System.DateTime.Now.ToString("yyyy-MM-dd"), System.DateTime.Now.Date.AddMonths(3).ToString("yyyy-MM-dd"), "30");
            if (_roomRateWsGet != null)
            {
                if (_roomRateWsGet.result == 0)
                {
                    if (_roomRateWsGet.roomRateWS != null)
                    {
                        List<CRS.Sync.Watcher.Service.WCFMobileServer.RoomRateWS> roomRateWSList = _roomRateWsGet.roomRateWS.ToList();
                        foreach (CRS.Sync.Watcher.Service.WCFMobileServer.RoomRateWS _roomRateWSList in roomRateWSList)
                        {
                            SQLiteParameter[] parameters = new SQLiteParameter[] { 
                                         //添加参数  
                                         new SQLiteParameter("@hotelId", _hoteList[0].h_id),  
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
                            int result = SQLiteHelper.ExecuteNonQuery("insert into RoomRateWS values(@hotelId, @rmTypeEName, @breakfastEDesc,@minVacRooms,@rateCodeCName,@rateCodeEName,@rmTypeEDesc,@ratePrice,@rateCode,@rateDate,@rmType,@rmTypeCDesc,@rmTypeCName,@vacRooms,@breakfastDesc,@needPay,@needGuarant)", parameters);
                        }
                    }
                }
                else
                    messages += "GetCRSHotelRoomRateByChannel 接口返回出错！！！\r\n";
            }
            #endregion

            #region GetRateControl
            //获取Rate规则信息
            CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeControlGet _rateCodeControlGet = _ratePlanService.GetCRSRateCodeControl(17, "BAR1", "BDR", System.DateTime.Now.ToString("yyyy-MM-dd"), System.DateTime.Now.Date.AddMonths(3).ToString("yyyy-MM-dd"));
            if (_rateCodeControlGet != null)
            {
                if (_rateCodeControlGet.result == 0)
                {
                    if (_rateCodeControlGet.rateCodeControl != null && !string.IsNullOrEmpty(_rateCodeControlGet.rateCodeControl.amendStatus))
                    {
                        //! 存储接口返回数据
                        CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeControl _rateCodeControl = _rateCodeControlGet.rateCodeControl;
                        SQLiteParameter[] parameters = new SQLiteParameter[] { 
                                         //添加参数  
                                         new SQLiteParameter("@hotelId", 17),  
                                         new SQLiteParameter("@rateCode", _rateCodeControl.rateCode),
                                         new SQLiteParameter("@amendStatus", _rateCodeControl.amendStatus), 
                                         new SQLiteParameter("@amendDays", _rateCodeControl.amendDays), 
                                         new SQLiteParameter("@cancelStatus", _rateCodeControl.cancelStatus),  
                                         new SQLiteParameter("@cancelDays", _rateCodeControl.cancelDays),  
                                         new SQLiteParameter("@needPay", _rateCodeControl.needPay),  
                                         new SQLiteParameter("@minStay", _rateCodeControl.minStay),  
                                         new SQLiteParameter("@maxStay", _rateCodeControl.maxStay),  
                                         new SQLiteParameter("@booking", _rateCodeControl.booking),  
                                         new SQLiteParameter("@rmLimit", _rateCodeControl.rmLimit) 
                            };
                        int result = SQLiteHelper.ExecuteNonQuery("insert into RateCodeControl values(@hotelId, @rateCode, @amendStatus,@amendDays,@cancelStatus,@cancelDays,@needPay,@minStay,@maxStay,@booking,@rmLimit)", parameters);

                    }
                }
                else
                    messages += "GetCRSRateCodeControl 接口返回出错！！！\r\n";
            }
            #endregion

            #region GetPriceInfor
            //TODO:获取价格属性信息
            CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeInforGet _RateCodeInforGet = _ratePlanService.GetCRSRateCodeInfor(17, "BAR1");
            if (_RateCodeInforGet != null)
            {
                if (_RateCodeInforGet.result == 0)
                {
                    if (_RateCodeInforGet.rateCodeInfor != null)
                    {
                        CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeInfor _rateCodeInfor = _RateCodeInforGet.rateCodeInfor;
                        SQLiteParameter[] parameters = new SQLiteParameter[] { 
                                                     //添加参数  
                                                     new SQLiteParameter("@hotelId", 17),  
                                                     new SQLiteParameter("@rateCode", _rateCodeInfor.rateCode),
                                                     new SQLiteParameter("@cName", _rateCodeInfor.cName), 
                                                     new SQLiteParameter("@eName", _rateCodeInfor.eName), 
                                                     new SQLiteParameter("@memberType", _rateCodeInfor.memberType),  
                                                     new SQLiteParameter("@cardType", _rateCodeInfor.cardType),  
                                                     new SQLiteParameter("@isContract", _rateCodeInfor.isContract),  
                                                     new SQLiteParameter("@currency", _rateCodeInfor.currency),  
                                                     new SQLiteParameter("@rateCat", _rateCodeInfor.rateCat),  
                                                     new SQLiteParameter("@begDate", _rateCodeInfor.begDate),  
                                                     new SQLiteParameter("@endDate", _rateCodeInfor.endDate), 
                                                     new SQLiteParameter("@isdiscount", _rateCodeInfor.isdiscount), 
                                                     new SQLiteParameter("@market", _rateCodeInfor.market), 
                                                     new SQLiteParameter("@source", _rateCodeInfor.source), 
                                                     new SQLiteParameter("@weekenddays", _rateCodeInfor.weekenddays), 
                                                     new SQLiteParameter("@ptCoef", _rateCodeInfor.ptCoef), 
                                                     new SQLiteParameter("@discountOf", _rateCodeInfor.discountOf) ,
                                                     new SQLiteParameter("@discountType", _rateCodeInfor.discountType), 
                                                     new SQLiteParameter("@discountValue", _rateCodeInfor.discountValue),
                                                     new SQLiteParameter("@roundType", _rateCodeInfor.roundType),
                                                     new SQLiteParameter("@targetRound", _rateCodeInfor.targetround),
                                                     new SQLiteParameter("@status", _rateCodeInfor.status),
                                                     new SQLiteParameter("@defaultShow", _rateCodeInfor.defaultShow),
                                                     new SQLiteParameter("@sellBegDate", _rateCodeInfor.sellBegDate),
                                                     new SQLiteParameter("@sellEndDate", _rateCodeInfor.sellEndDate),
                                                     new SQLiteParameter("@bookingThrough", _rateCodeInfor.bookingThrough),
                                                     new SQLiteParameter("@aliasCn", _rateCodeInfor.aliasCn),
                                                     new SQLiteParameter("@aliasEn", _rateCodeInfor.aliasEn),
                                                     new SQLiteParameter("@contractId", _rateCodeInfor.contractId),
                                                     new SQLiteParameter("@dailyinvallocation", _rateCodeInfor.dailyinvallocation),
                                                     new SQLiteParameter("@descript", _rateCodeInfor.descript),
                                                     new SQLiteParameter("@edescript", _rateCodeInfor.edescript),
                                                     new SQLiteParameter("@frtMarket", _rateCodeInfor.frtMarket),
                                                     new SQLiteParameter("@frtSource", _rateCodeInfor.frtSource),
                                                     new SQLiteParameter("@isUpward", _rateCodeInfor.isUpward),
                                                     new SQLiteParameter("@remarks", _rateCodeInfor.remarks),
                                                     new SQLiteParameter("@syncstatus", _rateCodeInfor.syncstatus)
                                        };
                        int result = SQLiteHelper.ExecuteNonQuery("insert into RateCodeInfor values(@hotelId, @rateCode, @cName,@eName,@memberType,@cardType,@isContract,@currency,@rateCat,@begDate,@endDate,@isdiscount,@market,@source,@weekenddays,@ptCoef,@discountOf,@discountType,@discountValue,@roundType,@targetRound,@status,@defaultShow,@sellBegDate,@sellEndDate,@bookingThrough,@aliasCn,@aliasEn,@contractId,@dailyinvallocation,@descript,@edescript,@frtMarket,@frtSource,@isUpward,@remarks,@syncstatus)", parameters);
                    }
                }
                else
                    messages += "GetCRSRateCodeInfor 接口返回出错！！！\r\n";
            }
            #endregion

            return messages;
        }
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
