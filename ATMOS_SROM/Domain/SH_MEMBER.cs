using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class SH_MEMBER
    {
        public Int64 ID { get; set; }
        public Int64 ID_BAYAR { get; set; }
        public Int64 ID_MEMBER { get; set; }
        public string NAMA { get; set; }
        public string PHONE { get; set; }
        public string NO_BON { get; set; }
        public decimal NET_BAYAR { get; set; }
        public decimal DISC_RATE { get; set; }
        public decimal DISC_PRICE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public bool STATUS { get; set; }
    }
}