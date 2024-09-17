using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain.Report
{
    public class tf_lapHarianPerthn
    {
        public String KODE_CUST { get; set; }
        public string KODE { get; set; }
        public decimal NILAI_BYR { get; set; }
        public decimal DPP { get; set; }
        public decimal PPN { get; set; }
        public decimal NET_BAYAR { get; set; }
        public decimal NET_CASH { get; set; }
        public decimal JM_CARD { get; set; }
        public decimal JM_VOUCHER { get; set; }
        public decimal NET_CARD_DEBIT { get; set; }
        public decimal NET_CARD_KREDIT { get; set; }
        public Int32 QTY { get; set; }
        public decimal DISC_R { get; set; }
        public decimal TAG_PRICE { get; set; }
        public decimal UANG_MARGIN { get; set; }
        public DateTime TGL_TRANS { get; set; }
    }
}