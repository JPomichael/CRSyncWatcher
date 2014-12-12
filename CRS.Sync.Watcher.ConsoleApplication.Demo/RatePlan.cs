using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SQLite;
using CRS.Sync.Watcher.DLL;
using System.Data.SQLite.Linq;

namespace CRS.Sync.Watcher.ConsoleApplication.Demo
{
    public class RatePlan
    {
        CRS.Sync.Watcher.Service.RatePlan.IRatePlanService _ratePlanService = new CRS.Sync.Watcher.Service.RatePlan.RatePlanService();

        public static SQLiteConnection conn = new SQLiteConnection("Data Source=e:\\CRSInterFaceResult.db");
        public static SQLiteCommand cmd;
        public static SQLiteTransaction tran;


        public string SyncService()
        {
            string messages = "";
            //TODO:  CRS开发人员官方解释如下：
            //!  1. 根据酒店日期区间获取当前酒店下所有符合的房类代码和价格代码
            //!  2. 根据房类代码和房价代码获取具体的房类信息和房价代码信息
            CRS.Sync.Watcher.Service.WCFMobileServer.RoomRateWsGet _roomRateWsGet = _ratePlanService.GetCRSHotelRoomRateByChannel(17, "ALL", System.DateTime.Now.ToString("yyyy-MM-dd"), System.DateTime.Now.Date.AddMonths(3).ToString("yyyy-MM-dd"), "30");
            if (_roomRateWsGet != null)
            {
                if (_roomRateWsGet.result == 0)
                {
                    if (_roomRateWsGet.roomRateWS != null)
                    {
                        List<CRS.Sync.Watcher.Service.WCFMobileServer.RoomRateWS> roomRateWSList = _roomRateWsGet.roomRateWS.ToList();

                        #region SQLite SQL 稍后会调整为LINQ TO SQLite
                        conn.Open();//打开连接  
                        //实例化一个事务  
                        using (tran = conn.BeginTransaction())
                        {
                            foreach (CRS.Sync.Watcher.Service.WCFMobileServer.RoomRateWS _roomRateWSList in roomRateWSList)
                            {
                                cmd = new SQLiteCommand(conn);//实例化SQL命令  
                                cmd.Transaction = tran;
                                cmd.CommandText = "insert into RoomRateWS values(@hotelId, @rmTypeEName, @breakfastEDesc,@minVacRooms,@rateCodeCName,@rateCodeEName,@rmTypeEDesc,@ratePrice,@rateCode,@rateDate,@rmType,@rmTypeCDesc,@rmTypeCName,@vacRooms,@breakfastDesc,@needPay,@needGuarant)";//设置带参SQL语句  
                                cmd.Parameters.AddRange(new[] {
                                    //添加参数  
                                    new SQLiteParameter("@hotelId", 17),  
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
                           new SQLiteParameter("@needGuarant", _roomRateWSList.needGuarant),  
                       });
                                cmd.ExecuteNonQuery();//执行查询  
                            }
                            tran.Commit();//提交  
                        }
                        #endregion


                        CRS.Sync.Watcher.Service.WCFMobileServer.RateCodeInforGet _rateCodeInforGet = _ratePlanService.GetCRSRateCodeInfor(17, roomRateWSList[0].rateCode);

                    }
                    else
                        messages += "GetCRSHotelRoomRateByChannel 接口返回 NULL!\r\n";
                }
                else
                    messages += "GetCRSHotelRoomRateByChannel 接口返回出错！！！\r\n";
            }
            return messages;
        }

    }
}
