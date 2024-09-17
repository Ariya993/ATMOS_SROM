using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class TEMP_POS_TO_EDC
    {
        public Int64 ID { get; set; }
        public decimal CardPay { get; set; }
        public string Bank { get; set; }

        public string EDC { get; set; }
        public string KODE_CUST { get; set; }
        public string KODE_CT { get; set; }
        public DateTime? CRT_DT { get; set; }
        public string CRT_BY { get; set; }
        public string STAT_TRANS { get; set; }
    }
}