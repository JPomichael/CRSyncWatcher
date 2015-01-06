using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Domain.Dto
{
    public class PriceDTO
    {
        public int Room_rp_id { get; set; }
        public int Hotel_id { get; set; }
        public int Room_id { get; set; }
        public int RoomTypeId { get; set; }
        public int PriceID { get; set; }
        public bool Status { get; set; }
        public decimal Room_rp_price { get; set; }
        public decimal Weekend { get; set; }
        public decimal MemberCost { get; set; }
        public decimal WeekendCost { get; set; }
        public decimal Addbed { get; set; }           //加床价 无价/不支持加床则为   -1
        public DateTime Effectdate { get; set; }
        public int Ebeds { get; set; }
        public int Aviebeds { get; set; }
        public decimal Cost { get; set; }
        public decimal Commission { get; set; }
        public DateTime LastUpTime { get; set; }
    }
}
