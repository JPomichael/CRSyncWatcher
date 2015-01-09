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
using CRS.Sync.Watcher.Domain.Dto;
using System.Threading;
using System.Data.SqlClient;
using System.Data.Common;

namespace CRS.Sync.Watcher.ConsoleApplication.RatePlan
{
    public class RatePlan
    {
        CRS.Sync.Watcher.Service.RatePlan.IRatePlanService _ratePlanService = new CRS.Sync.Watcher.Service.RatePlan.RatePlanService();
        CRS.Sync.Watcher.Service.Hotel.IHotelService _hotelService = new CRS.Sync.Watcher.Service.Hotel.HotelService();
        CRS.Sync.Watcher.Service.House.IHouseService _houseService = new CRS.Sync.Watcher.Service.House.HouseService();

        //! 创建日志记录组件实例  
        public static ILog log = log4net.LogManager.GetLogger(typeof(RatePlan));
        estay_ecsdbDataContext dc = ConnHelper.estay_ecsdb();

        /// <summary>
        /// 价格
        /// </summary>
        /// <returns></returns>
        public void PriceService()
        {
            //! 1. 获取source_id=6的供应商数据
            List<hotel_info> hoteList = GetCRSHotelList().Select(o => new hotel_info
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
                                    hotelId = int.Parse(_hoteList.h_id),
                                    rmTypeEName = o.rmTypeEName,
                                    breakfastEDesc = o.breakfastEDesc,
                                    minVacRooms = o.minVacRooms,
                                    rateCodeCName = o.rateCodeCName,
                                    rateCodeEName = o.rateCodeEName,
                                    rmTypeEDesc = o.rmTypeEDesc,
                                    ratePrice = Convert.ToDecimal(o.ratePrice),
                                    rateCode = o.rateCode,
                                    rateDate = Convert.ToDateTime(o.rateDate),
                                    rmType = o.rmType,
                                    rmTypeCDesc = o.rmTypeCDesc,
                                    rmTypeCName = o.rmTypeCName,
                                    vacRooms = o.vacRooms,
                                    breakfastDesc = o.breakfastDesc,
                                    needPay = o.needPay,
                                    needGuarant = o.needGuarant
                                    //x intro = o.intro
                                });
                            if (roomRateWSList != null && roomRateWSList.Count() > 0)
                            {
                                try
                                {

                                    //List<RoomRateWS> deList =
                                    //dc.RoomRateWS.Where(o => o.hotelId ==
                                    //Convert.ToInt32(_hoteList.h_id) &&
                                    //o.rateDate.Value.Date >=
                                    //System.DateTime.Now.Date &&
                                    //o.rateDate.Value.Date <=
                                    //System.DateTime.Now.Date.AddMonths(3).Date).ToList();
                                    IEnumerable<RoomRateWS> deList = dc.ExecuteQuery<RoomRateWS>(@"select id,guid,hotelId,rmTypeEName,breakfastEDesc,minVacRooms,rateCodeCName,rateCodeEName,rmTypeEDesc,ratePrice,rateCode,rateDate,rmType,rmTypeCDesc,rmTypeCName,vacRooms,breakfastDesc,needPay,needGuarant from RoomRateWS where hotelId={0} and rateDate>={1} and rateDate<={2}", int.Parse(_hoteList.h_id), Check_in, Check_out);
                                    if (deList != null && deList.Count() > 0)
                                    {
                                        dc.ExecuteCommand(@"delete from RoomRateWS where id in (select id from RoomRateWS  where hotelId= {0} and rateDate>= {1} and rateDate<= {2})", int.Parse(_hoteList.h_id), Check_in, Check_out);
                                        // LinqHelper _linqHelper = new   
                                        // LinqHelper();
                                        //_linqHelper.DeletesEntity<RoomRateWS>(deList.ToList());
                                        //    foreach (RoomRateWS _deList in
                                        //    deList) {
                                        //        dc.RoomRateWS.DeleteOnSubmit(_deList);
                                        //        //x dc.ExecuteCommand("delete * from RoomRateWS where hotelId={0} and rateDate>={1} and rateDate<={2}", _hoteList.h_id, System.DateTime.Now.Date, System.DateTime.Now.Date.AddMonths(3).Date);
                                        //        dc.SubmitChanges();
                                        //    }
                                    }
                                    foreach (RoomRateWS _roomRateWSList in roomRateWSList)
                                    {
                                        dc.RoomRateWS.InsertOnSubmit(_roomRateWSList);
                                    }

                                    dc.SubmitChanges();
                                    Console.WriteLine("更新 h_id=" + _hoteList.h_id + "酒店价格" + roomRateWSList.Count() + " 条");
                                }
                                catch (Exception e)
                                {
                                    log.Error(e);
                                    log.Debug("友好消息：" + e.Message + " \r\n Develoer：" + e.StackTrace + " ");

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
        /// 价格描述
        /// </summary>
        /// <returns></returns>
        public void PriceDescriptService()
        {
            IEnumerable<RoomRateWSDTO> RoomRateWSDTO = dc.RoomRateWS
                .GroupBy(o => new { o.hotelId, o.rateCode, o.rmType })
                .Select(o => new CRS.Sync.Watcher.Domain.Dto.RoomRateWSDTO
                {
                    hotelId = o.Key.hotelId,
                    rmType = o.Key.rmType,
                    rateCode = o.Key.rateCode
                });
            if (RoomRateWSDTO != null)
            {
                foreach (var _RoomRateWSDTO in RoomRateWSDTO)
                {
                    PriceDescriptThreadPoolService(_RoomRateWSDTO);
                    //ThreadPool.QueueUserWorkItem(PriceDescriptThreadPoolService, _RoomRateWSDTO);
                }

            }
        }

        /// <summary>
        /// 临时数据入库处理服务
        /// </summary>
        public void SyncDataBaseService()
        {
            //TODO: RatePlan
            #region RatePlan

            List<RatePlanDTO> ConvertRatePlan = dc.RoomRateWS.GroupBy(o => new { o.hotelId, o.rateCode, o.rateCodeCName, o.needGuarant })
                .Select(o => new CRS.Sync.Watcher.Domain.Dto.RatePlanDTO
                {
                    ratePlanId = o.Key.rateCode, //! 同rateCode 下皆同RatePlan
                    Hotel_id = _hotelService.GetHotelInfoByHid(o.Key.hotelId.ToString()).hotel_id,
                    H_room_rp_state = true,
                    H_room_rp_name_cn = o.Key.rateCodeCName,
                    H_room_rp_name_en = "",
                    H_room_rp_is_pay_online = true,
                    H_room_rp_check_in = "00:00:00",
                    H_room_rp_check_out = "23:59:00",
                    H_room_rp_least_day = 1, //!string.IsNullOrEmpty(control.minStay) ? (Convert.ToInt32(!string.IsNullOrEmpty(control.minStay)) > 0 ? Convert.ToInt32(!string.IsNullOrEmpty(control.minStay)) : 1) : 1,
                    H_room_rp_longest_day = 365, //!string.IsNullOrEmpty(control.maxStay) ? (Convert.ToInt32(!string.IsNullOrEmpty(control.maxStay)) > 0 ? Convert.ToInt32(!string.IsNullOrEmpty(control.minStay)) : 365) : 365,
                    H_room_rp_invoice = true,
                    H_room_rp_breakfast_title = "",
                    //H_room_rp_ctime = System.DateTime.Now,
                    paymentType = null, //control.needPay == "1" ? "Prepay" : null
                    guaranteeRuleIds = o.Key.needGuarant != null ? Convert.ToInt32(o.Key.needGuarant).ToString() : null,

                }).ToList();
            if (ConvertRatePlan != null)
            {
                foreach (RatePlanDTO _ConvertRatePlan in ConvertRatePlan)
                {
                    int h_room_rp_id = 0;
                    hotel_room_RP_info rp_info = _ratePlanService.GetRatePlanInfo(_ConvertRatePlan.Hotel_id, _ConvertRatePlan.ratePlanId);
                    if (rp_info != null)
                    {
                        #region edit
                        using (estay_ecsdbDataContext db = ConnHelper.estay_ecsdb())
                        {
                            hotel_room_RP_info editRP = dc.hotel_room_RP_info.Where(o => o.h_room_rp_id == rp_info.h_room_rp_id).FirstOrDefault();
                            //editRP.RatePlanId = _ConvertRatePlan.ratePlanId;
                            //editRP.hotel_id = _ConvertRatePlan.Hotel_id;
                            editRP.h_room_rp_state = _ConvertRatePlan.H_room_rp_state;
                            editRP.h_room_rp_name_cn = _ConvertRatePlan.H_room_rp_name_cn;
                            editRP.h_room_rp_name_en = _ConvertRatePlan.H_room_rp_name_en;
                            editRP.h_room_rp_is_pay_online = _ConvertRatePlan.H_room_rp_is_pay_online;
                            editRP.h_room_rp_check_in = _ConvertRatePlan.H_room_rp_check_in;
                            editRP.h_room_rp_check_out = _ConvertRatePlan.H_room_rp_check_out;
                            editRP.h_room_rp_least_day = _ConvertRatePlan.H_room_rp_least_day; //!string.IsNullOrEmpty(control.minStay) ? (Convert.ToInt32(!string.IsNullOrEmpty(control.minStay)) > 0 ? Convert.ToInt32(!string.IsNullOrEmpty(control.minStay)) : 1) : 1,
                            editRP.h_room_rp_longest_day = _ConvertRatePlan.H_room_rp_longest_day; //!string.IsNullOrEmpty(control.maxStay) ? (Convert.ToInt32(!string.IsNullOrEmpty(control.maxStay)) > 0 ? Convert.ToInt32(!string.IsNullOrEmpty(control.minStay)) : 365) : 365,
                            editRP.h_room_rp_invoice = _ConvertRatePlan.H_room_rp_invoice;
                            editRP.h_room_rp_breakfast_title = _ConvertRatePlan.H_room_rp_breakfast_title;
                            editRP.h_room_rp_utime = System.DateTime.Now;
                            editRP.CustomerType = _ConvertRatePlan.paymentType; //control.needPay == "1" ? "Prepay" : null
                            editRP.GuaranteeRuleIds = _ConvertRatePlan.guaranteeRuleIds;
                            //x db.hotel_room_RP_info.DeleteOnSubmit(db.hotel_room_RP_info.Where(o => o.h_room_rp_id == rp_info.h_room_rp_id).FirstOrDefault());
                            List<hotel_room_RP_price> price = db.hotel_room_RP_price.Where(o => o.Room_rp_id == rp_info.h_room_rp_id).ToList();
                            if (price != null && price.Count() > 0)
                            {
                                foreach (hotel_room_RP_price _price in price)
                                    db.hotel_room_RP_price.DeleteOnSubmit(_price);
                            }
                            db.SubmitChanges();
                            h_room_rp_id = editRP.h_room_rp_id;
                        }
                        #endregion
                    }
                    else
                    {
                        #region add
                        hotel_room_RP_info RP = new hotel_room_RP_info();
                        RP.RatePlanId = _ConvertRatePlan.ratePlanId;
                        RP.hotel_id = _ConvertRatePlan.Hotel_id;
                        RP.h_room_rp_state = _ConvertRatePlan.H_room_rp_state;
                        RP.h_room_rp_name_cn = _ConvertRatePlan.H_room_rp_name_cn;
                        RP.h_room_rp_name_en = _ConvertRatePlan.H_room_rp_name_en;
                        RP.h_room_rp_is_pay_online = _ConvertRatePlan.H_room_rp_is_pay_online;
                        RP.h_room_rp_check_in = _ConvertRatePlan.H_room_rp_check_in;
                        RP.h_room_rp_check_out = _ConvertRatePlan.H_room_rp_check_out;
                        RP.h_room_rp_least_day = _ConvertRatePlan.H_room_rp_least_day; //!string.IsNullOrEmpty(control.minStay) ? (Convert.ToInt32(!string.IsNullOrEmpty(control.minStay)) > 0 ? Convert.ToInt32(!string.IsNullOrEmpty(control.minStay)) : 1) : 1,
                        RP.h_room_rp_longest_day = _ConvertRatePlan.H_room_rp_longest_day; //!string.IsNullOrEmpty(control.maxStay) ? (Convert.ToInt32(!string.IsNullOrEmpty(control.maxStay)) > 0 ? Convert.ToInt32(!string.IsNullOrEmpty(control.minStay)) : 365) : 365,
                        RP.h_room_rp_invoice = _ConvertRatePlan.H_room_rp_invoice;
                        RP.h_room_rp_breakfast_title = _ConvertRatePlan.H_room_rp_breakfast_title;
                        RP.h_room_rp_ctime = System.DateTime.Now;
                        RP.CustomerType = _ConvertRatePlan.paymentType; //control.needPay == "1" ? "Prepay" : null
                        RP.GuaranteeRuleIds = _ConvertRatePlan.guaranteeRuleIds;
                        using (estay_ecsdbDataContext db = ConnHelper.estay_ecsdb())
                        {
                            db.hotel_room_RP_info.InsertOnSubmit(RP);
                            db.SubmitChanges();
                            h_room_rp_id = RP.h_room_rp_id;
                        }
                        #endregion
                    }

                    Console.WriteLine(" h_room_rp_id= " + h_room_rp_id + " 产品数据有变动");

                    //x  若不及时补充RatePlan 下Price 会造成有RP 无Price
                    #region Price
                    List<PriceDTO> ConvertPriceBatch = dc.RoomRateWS.Where(o => o.rateCode == _ConvertRatePlan.ratePlanId)
                        .Select(o => new PriceDTO
                        {
                            Hotel_id = _ConvertRatePlan.Hotel_id,
                            Room_id = _houseService.GetRoomInfo(o.rmType, _ConvertRatePlan.Hotel_id).room_id,
                            //!  这里会出现找不到 rpid
                            Room_rp_id = _ratePlanService.GetRatePlanInfo(_ConvertRatePlan.Hotel_id, o.rateCode).h_room_rp_id,
                            RoomTypeId = 0,
                            PriceID = 0,
                            Room_rp_price = Convert.ToDecimal(o.ratePrice),
                            Status = Convert.ToDecimal(o.ratePrice) == Convert.ToDecimal(-1.00) ? false : true,    //默认值1 ，1有效，-1无效
                            Cost = 0,
                            Weekend = 0,
                            MemberCost = 0,
                            WeekendCost = 0,
                            Addbed = -1,
                            Effectdate = Convert.ToDateTime(o.rateDate),
                            Ebeds = -1,
                            Aviebeds = -1,
                            Commission = 0,
                            //LastUpTime = System.DateTime.Now
                        }).ToList();

                    if (ConvertPriceBatch != null && ConvertPriceBatch.Count() > 0)
                    {
                        foreach (PriceDTO _ConvertPriceBatch in ConvertPriceBatch)
                        {
                            hotel_room_RP_price price = new hotel_room_RP_price();
                            price.Hotel_id = _ConvertPriceBatch.Hotel_id;
                            price.Room_id = _ConvertPriceBatch.Room_id;
                            price.Room_rp_id = _ConvertPriceBatch.Room_rp_id;
                            price.RoomTypeId = _ConvertPriceBatch.RoomTypeId;
                            price.PriceID = _ConvertPriceBatch.PriceID;
                            price.Room_rp_price = _ConvertPriceBatch.Room_rp_price;
                            price.Cost = _ConvertPriceBatch.Cost;
                            price.Weekend = _ConvertPriceBatch.Weekend;
                            price.MemberCost = _ConvertPriceBatch.MemberCost;
                            price.WeekendCost = _ConvertPriceBatch.WeekendCost;
                            price.Addbed = _ConvertPriceBatch.Addbed;
                            price.Effectdate = _ConvertPriceBatch.Effectdate;
                            price.Ebeds = _ConvertPriceBatch.Ebeds;
                            price.Aviebeds = _ConvertPriceBatch.Aviebeds;
                            price.Status = _ConvertPriceBatch.Status;
                            price.Commission = _ConvertPriceBatch.Commission;
                            price.LastUpTime = System.DateTime.Now;
                            using (estay_ecsdbDataContext db = ConnHelper.estay_ecsdb())
                            {
                                try
                                {
                                    db.hotel_room_RP_price.InsertOnSubmit(price);
                                    db.SubmitChanges();
                                    Console.WriteLine(" Rp_price_id= " + price.Rp_price_id + " 价格数据有变动");
                                }
                                catch (Exception e)
                                {
                                    log.Error(e);
                                    log.Debug("友好消息：" + e.Message + " \r\n Develoer：" + e.StackTrace + " ");
                                }

                            }

                        }

                        //x dc.SubmitChanges();
                    }
                    #endregion

                }

            }

            #endregion



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_RoomRateWSDTO"></param>
        public void PriceDescriptThreadPoolService(RoomRateWSDTO CodeDTO)
        {
            //x var CodeDTO = _RoomRateWSDTO as RoomRateWSDTO;

            #region GetRateControl
            CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeControlGet _rateCodeControlGet = _ratePlanService.GetCRSRateCodeControl(CodeDTO.hotelId, CodeDTO.rateCode, CodeDTO.rmType, System.DateTime.Now.ToString("yyyy-MM-dd"), System.DateTime.Now.Date.AddMonths(3).ToString("yyyy-MM-dd"));
            if (_rateCodeControlGet != null)
            {
                if (_rateCodeControlGet.result == 0)
                {
                    if (_rateCodeControlGet.rateCodeControl != null && !string.IsNullOrEmpty(_rateCodeControlGet.rateCodeControl.amendStatus))
                    {
                        RateCodeControl RateCodeControlModel = new RateCodeControl();
                        RateCodeControlModel.guid = System.Guid.NewGuid().ToString();
                        RateCodeControlModel.hotelId = CodeDTO.hotelId;
                        RateCodeControlModel.rateCode = CodeDTO.rateCode;
                        RateCodeControlModel.rmType = CodeDTO.rmType;
                        RateCodeControlModel.amendStatus = _rateCodeControlGet.rateCodeControl.amendStatus;
                        RateCodeControlModel.amendDays = _rateCodeControlGet.rateCodeControl.amendDays;
                        RateCodeControlModel.cancelStatus = _rateCodeControlGet.rateCodeControl.cancelStatus;
                        RateCodeControlModel.cancelDays = _rateCodeControlGet.rateCodeControl.cancelDays;
                        RateCodeControlModel.needPay = _rateCodeControlGet.rateCodeControl.needPay;
                        RateCodeControlModel.minStay = _rateCodeControlGet.rateCodeControl.minStay;
                        RateCodeControlModel.maxStay = _rateCodeControlGet.rateCodeControl.maxStay;
                        RateCodeControlModel.booking = _rateCodeControlGet.rateCodeControl.booking;
                        RateCodeControlModel.rmLimit = _rateCodeControlGet.rateCodeControl.rmLimit;
                        if (_ratePlanService.CheckIsAnyRateCodeControl(RateCodeControlModel))
                        {
                            //! 存在则删除!!!
                            _ratePlanService.DeleteRateCodeControl(RateCodeControlModel);
                        }

                        try
                        {
                            dc.RateCodeControl.InsertOnSubmit(RateCodeControlModel);
                            dc.SubmitChanges();
                            Console.WriteLine("更新 h_id=" + CodeDTO.hotelId + " RateCodeControl数据");
                        }
                        catch (Exception e)
                        {
                            log.Error(e);
                            log.Debug("友好消息：" + e.Message + " \r\n Develoer：" + e.StackTrace + " ");
                        }

                    }
                }
                else
                {
                    string messages = "GetCRSRateCodeControl 接口返回出错！！！ 请求参数 h_id=" + CodeDTO.hotelId + " rateCode=" + CodeDTO.rateCode + " rmType=" + CodeDTO.rmType + " check_in=" + System.DateTime.Now.ToString("yyyy-MM-dd") + " check_out=" + System.DateTime.Now.ToString("yyyy-MM-dd") + "";
                    log.Warn(messages);
                    Console.WriteLine(messages);
                }
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
                        RateCodeInforModel.guid = System.Guid.NewGuid().ToString();
                        RateCodeInforModel.hotelId = CodeDTO.hotelId;
                        RateCodeInforModel.rateCode = CodeDTO.rateCode;
                        RateCodeInforModel.cName = _rateCodeInforGet.rateCodeInfor.cName;
                        RateCodeInforModel.eName = _rateCodeInforGet.rateCodeInfor.eName;
                        RateCodeInforModel.memberType = _rateCodeInforGet.rateCodeInfor.memberType;
                        RateCodeInforModel.cardType = _rateCodeInforGet.rateCodeInfor.cardType;
                        RateCodeInforModel.isContract = _rateCodeInforGet.rateCodeInfor.isContract;
                        RateCodeInforModel.currency = _rateCodeInforGet.rateCodeInfor.currency;
                        RateCodeInforModel.rateCat = _rateCodeInforGet.rateCodeInfor.rateCat;
                        RateCodeInforModel.begDate = _rateCodeInforGet.rateCodeInfor.begDate;
                        RateCodeInforModel.endDate = _rateCodeInforGet.rateCodeInfor.endDate;
                        RateCodeInforModel.isdiscount = _rateCodeInforGet.rateCodeInfor.isdiscount;
                        RateCodeInforModel.market = _rateCodeInforGet.rateCodeInfor.market;
                        RateCodeInforModel.source = _rateCodeInforGet.rateCodeInfor.source;
                        RateCodeInforModel.weekenddays = _rateCodeInforGet.rateCodeInfor.weekenddays;
                        RateCodeInforModel.ptCoef = Convert.ToInt32(_rateCodeInforGet.rateCodeInfor.ptCoef);
                        RateCodeInforModel.discountOf = _rateCodeInforGet.rateCodeInfor.discountOf;
                        RateCodeInforModel.discountType = _rateCodeInforGet.rateCodeInfor.discountType;
                        RateCodeInforModel.discountValue = Convert.ToInt32(_rateCodeInforGet.rateCodeInfor.discountValue);
                        RateCodeInforModel.roundType = _rateCodeInforGet.rateCodeInfor.roundType;
                        RateCodeInforModel.targetRound = _rateCodeInforGet.rateCodeInfor.targetround;
                        RateCodeInforModel.status = _rateCodeInforGet.rateCodeInfor.status;
                        RateCodeInforModel.defaultShow = _rateCodeInforGet.rateCodeInfor.defaultShow;
                        RateCodeInforModel.sellBegDate = _rateCodeInforGet.rateCodeInfor.sellBegDate;
                        RateCodeInforModel.sellEndDate = _rateCodeInforGet.rateCodeInfor.sellEndDate;
                        RateCodeInforModel.bookingThrough = _rateCodeInforGet.rateCodeInfor.bookingThrough;
                        RateCodeInforModel.aliasCn = _rateCodeInforGet.rateCodeInfor.aliasCn;
                        RateCodeInforModel.aliasEn = _rateCodeInforGet.rateCodeInfor.aliasEn;
                        RateCodeInforModel.contractId = Convert.ToInt32(_rateCodeInforGet.rateCodeInfor.contractId);
                        RateCodeInforModel.dailyinvallocation = Convert.ToInt32(_rateCodeInforGet.rateCodeInfor.dailyinvallocation);
                        RateCodeInforModel.descript = _rateCodeInforGet.rateCodeInfor.descript;
                        RateCodeInforModel.edescript = _rateCodeInforGet.rateCodeInfor.edescript;
                        RateCodeInforModel.frtMarket = _rateCodeInforGet.rateCodeInfor.frtMarket;
                        RateCodeInforModel.frtSource = _rateCodeInforGet.rateCodeInfor.frtSource;
                        RateCodeInforModel.isUpward = _rateCodeInforGet.rateCodeInfor.isUpward;
                        RateCodeInforModel.remarks = _rateCodeInforGet.rateCodeInfor.remarks;
                        RateCodeInforModel.syncstatus = _rateCodeInforGet.rateCodeInfor.syncstatus;

                        if (_ratePlanService.CheckIsAnyRateCodeInfor(RateCodeInforModel))
                        {
                            _ratePlanService.DeleteRateCodeInfor(RateCodeInforModel);
                        }

                        try
                        {
                            dc.RateCodeInfor.InsertOnSubmit(RateCodeInforModel);
                            dc.SubmitChanges();
                            Console.WriteLine("更新 h_id=" + CodeDTO.hotelId + " RateCodeInfor数据");
                        }
                        catch (Exception e)
                        {
                            log.Error(e);
                            log.Debug("友好消息：" + e.Message + " \r\n Develoer：" + e.StackTrace + " ");
                        }

                    }
                }
                else
                {
                    string messages = "GetCRSRateCodeInfor 接口返回出错！！！ 请求参数 h_id=" + CodeDTO.hotelId + " rateCode=" + CodeDTO.rateCode + "";
                    log.Warn(messages);
                    Console.WriteLine(messages);
                }

            }
            #endregion
        }


        /// <summary>
        /// 获取公寓信息-动态
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
