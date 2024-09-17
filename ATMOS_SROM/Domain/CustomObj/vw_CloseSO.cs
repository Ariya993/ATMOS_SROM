using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain.CustomObj
{
    public class vw_CloseSO
    {
        public string NO_PO { get; set; }
        public string NO_SO { get; set; }
        public string KODE_CUST { get; set; }
        public DateTime? TGL_TRANS { get; set; }
        public DateTime? SEND_DATE { get; set; }
        public int QTY { get; set; }
        public int QTY_REAL_1 { get; set; }
        public DateTime? TGL_REAL_1 { get; set; }
        public DateTime? TGL_KIRIM_1 { get; set; }
        public int QTY_REAL_2 { get; set; }
        public DateTime? TGL_REAL_2 { get; set; }
        public DateTime? TGL_KIRIM_2 { get; set; }
        public string FRETUR { get; set; }
        public string NO_BON { get; set; }
        public string STATUS_HEADER { get; set; }
    }
}