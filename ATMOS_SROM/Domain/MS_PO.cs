using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_PO
    {
        public Int64 ID { get; set; }
        public string NO_PO { get; set; }
        public string PO_REFF { get; set; }
        public DateTime? DATE { get; set; }
        public string BRAND { get; set; }
        public string CONTACT { get; set; }
        public string POSITION { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE { get; set; }
        public string SUPPLIER { get; set; }
        public string KODE_SUPPLIER { get; set; }
        public int QTY { get; set; }
        public string STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATE_BY { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
        public bool STATUS_PO { get; set; }

        //Additional
        public string CREATED_BY_TEMP { get; set; }
        public int QTY_NOW { get; set; }
        public decimal FPPN_PCT { get; set; }
        public decimal FPPN_RP { get; set; }

    }
}