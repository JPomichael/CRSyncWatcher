using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using CRS.Sync.Watcher.DLL;
using CRS.Sync.Watcher.Linq;

namespace CRS.Sync.Watcher.Service.RatePlan
{
    public partial class RatePlanService
    {
        public void DeleteRoomRateWS(RoomRateWS roomRate)
        {
            RoomRateWS _roomRate = new RoomRateWS();
            _roomRate = dc.RoomRateWS.Where(o => o.hotelId == roomRate.hotelId && o.rateCode == roomRate.rateCode && o.rmType == roomRate.rmType && o.rateDate.Date == roomRate.rateDate.Date).FirstOrDefault();
            if (_roomRate != null)
            {
                try
                {
                    dc.RoomRateWS.DeleteOnSubmit(_roomRate);
                    dc.SubmitChanges();
                }
                catch (Exception e)
                {
                    log.Error("删除RoomRateWS出错，出错参数 ：hotelId=" + _roomRate.hotelId + " rateCode=" + _roomRate.rateCode + " rmType=" + _roomRate.rmType + " rateDate=" + _roomRate.rateDate + "");
                    log.Debug(e);
                }

            }
        }

        public void DeleteRateCodeControl(RateCodeControl rateCodeControl)
        {
            RateCodeControl _rateCodeControl = new RateCodeControl();
            _rateCodeControl = dc.RateCodeControl.Where(o => o.hotelId == rateCodeControl.hotelId && o.rateCode == rateCodeControl.rateCode && o.rmType == rateCodeControl.rmType).FirstOrDefault();
            if (_rateCodeControl != null)
            {
                try
                {
                    dc.RateCodeControl.DeleteOnSubmit(_rateCodeControl);
                    dc.SubmitChanges();
                }
                catch (Exception e)
                {
                    log.Error("删除RateCodeControl出错，出错参数 ：hotelId=" + rateCodeControl.hotelId + " rateCode=" + rateCodeControl.rateCode + " rmType=" + rateCodeControl.rmType + " ");
                    log.Debug(e);
                }
            }
        }

        public void DeleteRateCodeInfor(RateCodeInfor rateCodeInfor)
        {
            RateCodeInfor _rateCodeInfor = new RateCodeInfor();
            _rateCodeInfor = dc.RateCodeInfor.Where(o => o.hotelId == rateCodeInfor.hotelId && o.rateCode == rateCodeInfor.rateCode).FirstOrDefault();
            if (_rateCodeInfor != null)
            {
                try
                {
                    dc.RateCodeInfor.DeleteOnSubmit(_rateCodeInfor);
                    dc.SubmitChanges();
                }
                catch (Exception e)
                {
                    log.Error("删除RateCodeControl出错，出错参数 ：hotelId=" + rateCodeInfor.hotelId + " rateCode=" + rateCodeInfor.rateCode + "");
                    log.Debug(e);
                }
            }
        }

    }
}
