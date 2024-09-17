using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class TEMP_ACARA
    {
        public Int64 ID { get; set; }
        public Int64 ID_TEMP { get; set; }
        public Int64 ID_ACARA { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public string ITEM_CODE { get; set; }
        public string VALUE_ACARA { get; set; }
        public decimal NET_PRICE { get; set; }
        public decimal SPCL_PRICE { get; set; }
        public decimal DISC { get; set; }
        //public Int32 DISC { get; set; }
        public decimal DISC_PRICE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
    }
}