using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain.CustomObj
{
    public class VW_REPRINTBON
    {
        public string NO_BON { get; set; }
        public string ARTICLE { get; set; }
        public Decimal BON_PRICE { get; set; }
        public Decimal DISC_P { get; set; }
        public Decimal DISC_R { get; set; }
        public Decimal NET_PRICE { get; set; }
        public Decimal TOTAL_NET { get; set; }
        public Decimal JM_VOUCHER { get; set; }
        public Decimal JM_CARD { get; set; }
        public Decimal JM_UANG { get; set; }
        public Decimal KEMBALI { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public String CREATED_BY { get; set; }
        public DateTime TGL_TRANS { get; set; }
        public string KODE { get; set; }
        public string SHOWROOM { get; set; }
        public string LOGO_IMG { get; set; }
        public string ALAMAT { get; set; }
        public string PHONE { get; set; }
        public int QTY { get; set; }
        public String BRAND { get; set; }
        public string VOUCHER { get; set; }
        public string CARD { get; set; }
        public Decimal DPP { get; set; }
        public Decimal PPN { get; set; }
        public string ONGKIR { get; set; }
        public decimal JM_ONGKIR { get; set; }
        public decimal JM_FREE_ONGKIR { get; set; }
    }
}