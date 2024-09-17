using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class SH_CARD
    {
        public Int64 ID { get; set; }
        public Int64 ID_BAYAR { get; set; }
        public decimal NET_CARD { get; set; }
        public string KD_CCARD { get; set; }
        public string NO_CCARD { get; set; }
        public string VL_CCARD { get; set; }
        public string BANK { get; set; }
        public string EDC { get; set; }
    }
}