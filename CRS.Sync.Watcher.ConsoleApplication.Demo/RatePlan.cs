using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using CRS.Sync.Watcher.DLL;
using CRS.Sync.Watcher.Linq;
using System.Linq.Expressions;
using System.Collections;
using log4net;
using MainContext;
using Devart.Data.SQLite;
using Devart.Data.Linq;
using CRS.Sync.Watcher.Domain.Dto;
using System.Threading;

namespace CRS.Sync.Watcher.ConsoleApplication.Demo
{
    public class RatePlan
    {
        CRS.Sync.Watcher.Service.RatePlan.IRatePlanService _ratePlanService = new CRS.Sync.Watcher.Service.RatePlan.RatePlanService();
        CRS.Sync.Watcher.Service.Hotel.IHotelService _hotelService = new CRS.Sync.Watcher.Service.Hotel.HotelService();

        //! 创建日志记录组件实例  
        public static ILog log = log4net.LogManager.GetLogger(typeof(RatePlan));
        public static MainDataContext SQLifeDC = new MainDataContext();

        /// <summary>
        /// 价格
        /// </summary>
        /// <returns></returns>
        public string PriceService()
        {
            string messages = "";

            //! 1. 获取source_id=6的供应商数据
            List<hotel_info> hoteList = GetCRSHotelList().ToList();
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
                            IEnumerable<RoomRateW> roomRateWSList = _roomRateWsGet.roomRateWS
                                .Select(o => new RoomRateW
                                {
                                    HotelId = int.Parse(_hoteList.h_id),
                                    RmTypeEName = o.rmTypeEName,
                                    BreakfastEDesc = o.breakfastEDesc,
                                    MinVacRooms = o.minVacRooms,
                                    RateCodeCName = o.rateCodeCName,
                                    RateCodeEName = o.rateCodeEName,
                                    RmTypeEDesc = o.rmTypeEDesc,
                                    RatePrice = o.ratePrice.ToString(),
                                    RateCode = o.rateCode,
                                    RateDate = o.rateDate,
                                    RmType = o.rmType,
                                    RmTypeCDesc = o.rmTypeCDesc,
                                    RmTypeCName = o.rmTypeCName,
                                    VacRooms = o.vacRooms,
                                    BreakfastDesc = o.breakfastDesc,
                                    NeedPay = o.needPay,
                                    NeedGuarant = o.needGuarant
                                }).AsEnumerable();
                            try
                            {
                                SQLifeDC.RoomRateWs.InsertAllOnSubmit(roomRateWSList);
                                SQLifeDC.SubmitChanges();
                            }
                            catch (Exception e)
                            {
                                log.Error(e);
                                log.Debug("友好消息：" + e.Message + " \r\n Develoer：" + e.StackTrace + " ");
                            }
                        }
                    }
                    else
                        messages += "GetCRSHotelRoomRateByChannel 接口返回出错！！！\r\n";
                }
                #endregion
            }
            return messages;

        }

        /// <summary>
        /// 价格描述
        /// </summary>
        /// <returns></returns>
        public string PriceDescriptService()
        {
            string messages = "";
            IEnumerable<RoomRateWSDTO> RoomRateWSDTO = SQLifeDC.RoomRateWs
                .GroupBy(o => new { o.HotelId, o.RateCode, o.RmType })
                .Select(o => new CRS.Sync.Watcher.Domain.Dto.RoomRateWSDTO
                {
                    hotelId = o.Key.HotelId,
                    rmType = o.Key.RmType,
                    rateCode = o.Key.RateCode
                });
            if (RoomRateWSDTO != null)
            {
                foreach (var _RoomRateWSDTO in RoomRateWSDTO)
                {
                    ThreadPool.QueueUserWorkItem(PriceDescriptThreadPoolService, _RoomRateWSDTO);
                }

            }
            return messages;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="_RoomRateWSDTO"></param>
        public void PriceDescriptThreadPoolService(object _RoomRateWSDTO)
        {
            var CodeDTO = _RoomRateWSDTO as RoomRateWSDTO;

            #region GetRateControl
            CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeControlGet _rateCodeControlGet = _ratePlanService.GetCRSRateCodeControl(CodeDTO.hotelId, CodeDTO.rateCode, CodeDTO.rmType, System.DateTime.Now.ToString("yyyy-MM-dd"), System.DateTime.Now.Date.AddMonths(3).ToString("yyyy-MM-dd"));
            if (_rateCodeControlGet != null)
            {
                if (_rateCodeControlGet.result == 0)
                {
                    if (_rateCodeControlGet.rateCodeControl != null && !string.IsNullOrEmpty(_rateCodeControlGet.rateCodeControl.amendStatus))
                    {
                        RateCodeControl RateCodeControlModel = new RateCodeControl();
                        RateCodeControlModel.HotelId = CodeDTO.hotelId;
                        RateCodeControlModel.RateCode = CodeDTO.rateCode;
                        RateCodeControlModel.RmType = CodeDTO.rmType;
                        RateCodeControlModel.AmendStatus = _rateCodeControlGet.rateCodeControl.amendStatus;
                        RateCodeControlModel.AmendDays = _rateCodeControlGet.rateCodeControl.amendDays;
                        RateCodeControlModel.CancelStatus = _rateCodeControlGet.rateCodeControl.cancelStatus;
                        RateCodeControlModel.CancelDays = _rateCodeControlGet.rateCodeControl.cancelDays;
                        RateCodeControlModel.NeedPay = _rateCodeControlGet.rateCodeControl.needPay;
                        RateCodeControlModel.MinStay = _rateCodeControlGet.rateCodeControl.minStay;
                        RateCodeControlModel.MaxStay = _rateCodeControlGet.rateCodeControl.maxStay;
                        RateCodeControlModel.Booking = _rateCodeControlGet.rateCodeControl.booking;
                        RateCodeControlModel.RmLimit = _rateCodeControlGet.rateCodeControl.rmLimit;
                        try
                        {
                            SQLifeDC.RateCodeControls.InsertOnSubmit(RateCodeControlModel);
                            SQLifeDC.SubmitChanges();
                        }
                        catch (Exception e)
                        {
                            log.Error(e);
                            log.Debug("友好消息：" + e.Message + " \r\n Develoer：" + e.StackTrace + " ");
                        }

                    }
                }
                //else
                //    messages += "GetCRSRateCodeControl 接口返回出错！！！\r\n";
            }
            #endregion

            #region GetPriceInfor
            CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeInforGet _rateCodeInforGet = _ratePlanService.GetCRSRateCodeInfor(CodeDTO.hotelId, CodeDTO.rateCode);
            if (_rateCodeInforGet != null)
            {
                if (_rateCodeInforGet.result == 0)
                {
                    if (_rateCodeInforGet.rateCodeInfor != null)
                    {
                        RateCodeInfor RateCodeInforModel = new RateCodeInfor();
                        RateCodeInforModel.HotelId = CodeDTO.hotelId;
                        RateCodeInforModel.RateCode = CodeDTO.rateCode;
                        RateCodeInforModel.CName = _rateCodeInforGet.rateCodeInfor.cName;
                        RateCodeInforModel.EName = _rateCodeInforGet.rateCodeInfor.eName;
                        RateCodeInforModel.MemberType = _rateCodeInforGet.rateCodeInfor.memberType;
                        RateCodeInforModel.CardType = _rateCodeInforGet.rateCodeInfor.cardType;
                        RateCodeInforModel.IsContract = _rateCodeInforGet.rateCodeInfor.isContract;
                        RateCodeInforModel.Currency = _rateCodeInforGet.rateCodeInfor.currency;
                        RateCodeInforModel.RateCat = _rateCodeInforGet.rateCodeInfor.rateCat;
                        RateCodeInforModel.BegDate = _rateCodeInforGet.rateCodeInfor.begDate;
                        RateCodeInforModel.EndDate = _rateCodeInforGet.rateCodeInfor.endDate;
                        RateCodeInforModel.Isdiscount = _rateCodeInforGet.rateCodeInfor.isdiscount;
                        RateCodeInforModel.Market = _rateCodeInforGet.rateCodeInfor.market;
                        RateCodeInforModel.Source = _rateCodeInforGet.rateCodeInfor.source;
                        RateCodeInforModel.Weekenddays = _rateCodeInforGet.rateCodeInfor.weekenddays;
                        RateCodeInforModel.PtCoef = Convert.ToInt32(_rateCodeInforGet.rateCodeInfor.ptCoef);
                        RateCodeInforModel.DiscountOf = _rateCodeInforGet.rateCodeInfor.discountOf;
                        RateCodeInforModel.DiscountType = _rateCodeInforGet.rateCodeInfor.discountType;
                        RateCodeInforModel.DiscountValue = Convert.ToInt32(_rateCodeInforGet.rateCodeInfor.discountValue);
                        RateCodeInforModel.RoundType = _rateCodeInforGet.rateCodeInfor.roundType;
                        RateCodeInforModel.TargetRound = _rateCodeInforGet.rateCodeInfor.targetround;
                        RateCodeInforModel.Status = _rateCodeInforGet.rateCodeInfor.status;
                        RateCodeInforModel.DefaultShow = _rateCodeInforGet.rateCodeInfor.defaultShow;
                        RateCodeInforModel.SellBegDate = _rateCodeInforGet.rateCodeInfor.sellBegDate;
                        RateCodeInforModel.SellEndDate = _rateCodeInforGet.rateCodeInfor.sellEndDate;
                        RateCodeInforModel.BookingThrough = _rateCodeInforGet.rateCodeInfor.bookingThrough;
                        RateCodeInforModel.AliasCn = _rateCodeInforGet.rateCodeInfor.aliasCn;
                        RateCodeInforModel.AliasEn = _rateCodeInforGet.rateCodeInfor.aliasEn;
                        RateCodeInforModel.ContractId = Convert.ToInt32(_rateCodeInforGet.rateCodeInfor.contractId);
                        RateCodeInforModel.Dailyinvallocation = Convert.ToInt32(_rateCodeInforGet.rateCodeInfor.dailyinvallocation);
                        RateCodeInforModel.Descript = _rateCodeInforGet.rateCodeInfor.descript;
                        RateCodeInforModel.Edescript = _rateCodeInforGet.rateCodeInfor.edescript;
                        RateCodeInforModel.FrtMarket = _rateCodeInforGet.rateCodeInfor.frtMarket;
                        RateCodeInforModel.FrtSource = _rateCodeInforGet.rateCodeInfor.frtSource;
                        RateCodeInforModel.IsUpward = _rateCodeInforGet.rateCodeInfor.isUpward;
                        RateCodeInforModel.Remarks = _rateCodeInforGet.rateCodeInfor.remarks;
                        RateCodeInforModel.Syncstatus = _rateCodeInforGet.rateCodeInfor.syncstatus;
                        try
                        {
                            SQLifeDC.RateCodeInfors.InsertOnSubmit(RateCodeInforModel);
                            SQLifeDC.SubmitChanges();
                        }
                        catch (Exception e)
                        {
                            log.Error(e);
                            log.Debug("友好消息：" + e.Message + " \r\n Develoer：" + e.StackTrace + " ");
                        }

                    }
                }

            }
            #endregion
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
