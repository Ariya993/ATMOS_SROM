using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_STAGING_WHOLESALE
    {
        public Int64 ID { get; set; }
        public Int64 ID_KDBRG{ get; set; }
        public string BARCODE { get; set; }
        public int QTY { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public bool STATUS { get; set; }
        public decimal NET_PRICE { get; set; }
    }
}