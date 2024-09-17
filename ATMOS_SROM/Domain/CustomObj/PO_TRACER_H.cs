using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain.CustomObj
{
    public class PO_TRACER_H
    {
        public string NO_PO { get; set; }
        public string NO_GR { get; set; }
        public string PO_REFF { get; set; }
        public string SUPPLIER { get; set; }
        public DateTime? PO_DATE { get; set; }
        public string BRAND { get; set; }
        public string CONTACT { get; set; }
        public string EMAIL { get; set; }
        public string PHONE { get; set; }
        public string Addr { get; set; }
        public string POSITION { get; set; }
        public int TotalPOQty { get; set; }
        public string Kode { get; set; }
        public string Status_GR { get; set; }
        public Int64 ID { get; set; }
        public DateTime? GR_DATE { get; set; }
    }
}