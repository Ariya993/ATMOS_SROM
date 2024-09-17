using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class USP_RPTTOTALSALES
    {
        public string KODE_CUST { get; set; }
        public string KODE_CT { get; set; }
        public string STATUS_STORE { get; set; }
        public string CARD { get; set; }
        public int TOTAL_SALES { get; set; }
    }
}