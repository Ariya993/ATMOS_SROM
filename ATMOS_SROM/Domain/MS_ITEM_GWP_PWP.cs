using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_ITEM_GWP_PWP
    {
        public Int64 ID { get; set; }
        public Int64 ID_ACARA { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public string VALUE_ACARA { get; set; }
        public string ITEM_CODE { get; set; }
        public string BARCODE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public bool STATUS_ACARA { get; set; }
        public Decimal ITEM_PRICE_ACARA { get; set; }
        //Additional
        public string ITEM_DESC { get; set; }
    }
}