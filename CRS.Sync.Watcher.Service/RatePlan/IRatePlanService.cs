using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CRS.Sync.Watcher.Service.RatePlan
{
    public interface IRatePlanService
    {
        #region CRS InterFace
        CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeDescriptGet GetCRSRateCodeDescript(int hotelId, string rateCode);
        CRS.Sync.Watcher.Service.WCFMobileServer.RoomRateWsGet GetCRSHotelRoomRateByChannel(int hotelId, string rateCode, string start, string end, string Channel);
        CRS.Sync.Watcher.Service.WCFMobileServer.RoomRateWsGet GetCRSHotelRoomRateComCodeByChannel(int hotelId, string rateCode, DateTime start, DateTime end, string comCode, string Channel);
        CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeControlGet GetCRSRateCodeControl(int hotelId, string rateCode, string rmType, string start, string end);
        CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeInforGet GetCRSRateCodeInfor(int hotelId, string rateCode);
        CRS.Sync.Watcher.Service.WCFMobileServer.LowPriceGet GetCRSHotelLowPrice(int hotelId, string rateCode, DateTime start, DateTime end, string channel);
        CRS.Sync.Watcher.Service.WCFMobileServer.LowPriceGet GetCRSHotelHotelLowComCode(int hotelId, string rateCode, DateTime start, DateTime end, string comCode, string channel);
        #endregion

        #region Check
        bool CheckIsAnyRateCodeControl(RateCodeControl rateCodeControl);
        bool CheckIsAnyRateCodeInfor(RateCodeInfor rateCodeInfor);
        bool CheckIsAnyRoomRateWS(RoomRateWS roomRateWS);
        #endregion

        #region Delete
        void DeleteRoomRateWS(RoomRateWS roomRate);
        void DeleteRateCodeControl(RateCodeControl rateCodeControl);
        void DeleteRateCodeInfor(RateCodeInfor rateCodeInfor);
        #endregion

        IEnumerable<RoomRateWS> GetRoomRateWSList(Expression<Func<RoomRateWS, bool>> expression);
    }
}
