using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class NO_SALE
    {
        public Int64 ID { get; set; }
        public Int64 ID_SHOWROOM { get; set; }
        public string KODE { get; set; }
        public string SHOWROOM { get; set; }
        public DateTime? NO_SALE_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public bool STATUS { get; set; }
    }
}