using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_ACARA_DISC
    {
        public Int64 ID { get; set; }
        public Int64 ID_ACARA { get; set; }
        public Int64 ID_ACARA_STATUS { get; set; }
        public Int32 NO_URUT { get; set; }
        public string ACARA_VALUE { get; set; }
        public string ACARA_STATUS { get; set; }
        public decimal? DISC { get; set; }
        //public int? DISC { get; set; }
        public decimal? SPCL_PRICE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public bool STATUS { get; set; }

        //Tambahan
        public decimal? MIN_PURCHASE { get; set; }
    }
}