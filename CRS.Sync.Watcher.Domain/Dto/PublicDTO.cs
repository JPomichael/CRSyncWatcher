using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Domain.Dto
{
    /// <summary>
    /// 通用代码
    /// </summary>
    public class PublicDTO
    {
        public string id { get; set; }
        public string cnaem { get; set; }
        public string enaem { get; set; }
        public string code { get; set; }
        public string remarks { get; set; }
        public string ExtensionData { get; set; }
        public int seqid { get; set; }
        public bool status { get; set; }

    }
}
