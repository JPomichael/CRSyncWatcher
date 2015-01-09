using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Domain.Dto
{
    public class RoomRateWSDTO
    {
        public int hotelId { get; set; }
        public string rateCode { get; set; }
        public string rmType { get; set; }
    }
}
