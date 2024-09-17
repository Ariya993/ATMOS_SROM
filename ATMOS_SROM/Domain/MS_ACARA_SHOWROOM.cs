using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_ACARA_SHOWROOM
    {
        public Int64 ID { get; set; }
        public Int64 ID_ACARA { get; set; }
        public Int64 ID_SHOWROOM { get; set; }
        public string ACARA_VALUE { get; set; }
        public string KODE { get; set; }
        public string SHOWROOM { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public bool STATUS { get; set; }
    }
}