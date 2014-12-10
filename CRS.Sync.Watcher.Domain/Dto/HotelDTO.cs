using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Domain.Dto
{
    /// <summary>
    /// 公寓DTO
    /// </summary>
    public class HotelDTO
    {
        /// <summary>
        /// 酒店编号
        /// </summary>
        public int hotelId { get; set; }

        /// <summary>
        /// 酒店代码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 所在国家
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string cName { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string eName { get; set; }


    }
}
