using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class VW_LAPORAN_HARIAN
    {
        public DateTime TGL_TRANS { get; set; }
        public int QTY { get; set; }
        public decimal TAG_PRICE { get; set; }
        public decimal BON_PRICE { get; set; }
        public decimal DISC_ACARA { get; set; }
        public decimal CUST_BAYAR { get; set; }
        public decimal UANG_TERIMA { get; set; }
        public decimal POTONGAN { get; set; }
        public decimal MARGIN { get; set; }
        public string SHOWROOM { get; set; }
        public string KODE { get; set; }
    }
}