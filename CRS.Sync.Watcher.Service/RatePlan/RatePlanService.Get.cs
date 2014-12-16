using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.RatePlan
{
    public partial class RatePlanService
    {
        #region CRS InterFace
        /// <summary>
        /// 获取对价格代码的描述
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="rateCode"></param>
        /// <returns></returns>
        public CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeDescriptGet GetCRSRateCodeDescript(int hotelId, string rateCode)
        {
            CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeDescriptGet _result = null;
            try
            {
                _result = _WCFClient.GetCRSRateCodeDescript(hotelId, rateCode);
            }
            catch (Exception)
            {
            }
            return _result;

        }

        public CRS.Sync.Watcher.Service.WCFMobileServer.RoomRateWsGet GetCRSHotelRoomRateByChannel(int hotelId, string rateCode, string start, string end, string Channel)
        {
            CRS.Sync.Watcher.Service.WCFMobileServer.RoomRateWsGet result = null;
            try
            {
                result = _WCFClient.GetCRSHotelRoomRateByChannel(hotelId, rateCode, start, end, Channel);
            }
            catch (Exception)
            {
            }

            return result;
        }

        public CRS.Sync.Watcher.Service.WCFMobileServer.RoomRateWsGet GetCRSHotelRoomRateComCodeByChannel(int hotelId, string rateCode, DateTime start, DateTime end, string comCode, string Channel)
        {
            CRS.Sync.Watcher.Service.WCFMobileServer.RoomRateWsGet result = null;
            try
            {
                result = _WCFClient.GetCRSHotelRoomRateComCodeByChannel(hotelId, rateCode, start, end, comCode, Channel);
            }
            catch (Exception)
            {
            }

            return result;
        }

        public CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeControlGet GetCRSRateCodeControl(int hotelId, string rateCode, string rmType, string start, string end)
        {
            CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeControlGet result = null;
            try
            {
                result = _WCFClient.GetCRSRateCodeControl(hotelId, rateCode, rmType, start, end);
            }
            catch (Exception)
            {
            }

            return result;
        }

        public CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeInforGet GetCRSRateCodeInfor(int hotelId, string rateCode)
        {
            CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeInforGet result = null;
            try
            {
                result = _WCFClient.GetCRSRateCodeInfor(hotelId, rateCode);
            }
            catch (Exception)
            {
            }

            return result;
        }

        public CRS.Sync.Watcher.Service.WCFMobileServer.LowPriceGet GetCRSHotelLowPrice(int hotelId, string rateCode, DateTime start, DateTime end, string channel)
        {
            CRS.Sync.Watcher.Service.WCFMobileServer.LowPriceGet result = null;
            try
            {
                result = _WCFClient.GetCRSHotelLowPrice(hotelId, rateCode, start, end, channel);
            }
            catch (Exception)
            {
            }

            return result;
        }

        public CRS.Sync.Watcher.Service.WCFMobileServer.LowPriceGet GetCRSHotelHotelLowComCode(int hotelId, string rateCode, DateTime start, DateTime end, string comCode, string channel)
        {
            CRS.Sync.Watcher.Service.WCFMobileServer.LowPriceGet result = null;
            try
            {
                result = _WCFClient.GetCRSHotelHotelLowComCode(hotelId, rateCode, start, end, comCode, channel);
            }
            catch (Exception)
            {
            }

            return result;
        }
        #endregion
    }
}
