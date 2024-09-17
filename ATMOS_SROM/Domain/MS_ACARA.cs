using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_ACARA
    {
        public Int64 ID { get; set; }
        public string NAMA_ACARA { get; set; }
        public string ACARA_DESC { get; set; }
        public string ACARA_VALUE { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public bool STATUS_ACARA { get; set; }

        //additional
        public Int64 ID_ACARA { get; set; }
        public Int64 ID_ACARA_STATUS { get; set; }
        public Int64 ID_SHOWROOM { get; set; }
        public int NO_URUT { get; set; }
        public string ACARA_STATUS { get; set; }
        public decimal DISC { get; set; }
        //public int DISC { get; set; }
        public string ARTICLE { get; set; }
        public string KODE { get; set; }
        public string SHOWROOM { get; set; }
        public string DESC_DISC { get; set; }
        public bool STATUS { get; set; }
        public decimal SPCL_PRICE { get; set; }
        public string ITEM_ACARA { get; set; }
        public decimal MIN_PURCHASE { get; set; }
    }
}