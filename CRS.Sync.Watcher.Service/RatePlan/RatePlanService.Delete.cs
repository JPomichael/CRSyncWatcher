using CRS.Sync.Watcher.DLL;
using CRS.Sync.Watcher.Linq;
using MainContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CRS.Sync.Watcher.Service.RatePlan
{
    public partial class RatePlanService
    {
        public void DeleteRoomRateW(RoomRateW roomRate)
        {
            RoomRateW _roomRate = new RoomRateW();
            _roomRate = SQLifeDC.RoomRateWs.Where(o => o.HotelId == roomRate.HotelId && o.RateCode == roomRate.RateCode && o.RmType == roomRate.RmType && o.RateDate.Value.Date == roomRate.RateDate.Value.Date).FirstOrDefault();
            if (_roomRate != null)
            {
                SQLifeDC.RoomRateWs.DeleteOnSubmit(_roomRate);
                SQLifeDC.SubmitChanges();
                SQLifeDC.Connection.Close();
            }
        }

        public void DeleteRateCodeControl(RateCodeControl rateCodeControl)
        {
            RateCodeControl _rateCodeControl = new RateCodeControl();
            _rateCodeControl = SQLifeDC.RateCodeControls.Where(o => o.HotelId == rateCodeControl.HotelId && o.RateCode == rateCodeControl.RateCode && o.RmType == rateCodeControl.RmType).FirstOrDefault();
            if (_rateCodeControl != null)
            {
                SQLifeDC.RateCodeControls.DeleteOnSubmit(_rateCodeControl);
                SQLifeDC.SubmitChanges();
                SQLifeDC.Connection.Close();
            }
        }

        public void DeleteRateCodeInfor(RateCodeInfor rateCodeInfor)
        {
            RateCodeInfor _rateCodeInfor = new RateCodeInfor();
            _rateCodeInfor = SQLifeDC.RateCodeInfors.Where(o => o.HotelId == rateCodeInfor.HotelId && o.RateCode == rateCodeInfor.RateCode).FirstOrDefault();
            if (_rateCodeInfor != null)
            {
                SQLifeDC.RateCodeInfors.DeleteOnSubmit(_rateCodeInfor);
                SQLifeDC.SubmitChanges();
            }
        }

    }
}
