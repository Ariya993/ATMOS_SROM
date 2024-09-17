using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class SO_WHOLESALES_HEADER
    {
        public Int64 ID { get; set; }
        public string NO_PO { get; set; }
        public string NO_SO { get; set; }
        public string KODE_CUST { get; set; }
        public string KODE { get; set; }
        public DateTime? TGL_TRANS { get; set; }
        public DateTime? SEND_DATE { get; set; }
        public int QTY { get; set; }
        //public Int32 MARGIN { get; set; }
        public string STATUS_HEADER { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string FRETUR { get; set; }
        public decimal MARGIN { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime UPDATED_DATE { get; set; }
    }
}