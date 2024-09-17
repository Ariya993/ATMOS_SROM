using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class USP_TOTALMARGIN
    {
        public string SHOWROOM { get; set; }
        public string KODE_CUST { get; set; }
        public string BRAND { get; set; }
        public string STORE { get; set; }
        public string STATUS_MARGIN { get; set; }
        public decimal MARGIN { get; set; }
        public int TOTAL_SALES { get; set; }
        public decimal UANG_STORE { get; set; }
        public decimal UANG_TERIMA { get; set; }
        public decimal DPP_TERIMA { get; set; }
        public decimal PPN_TERIMA { get; set; }
    }
}