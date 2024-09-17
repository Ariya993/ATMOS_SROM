using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class LAPORAN_HARIAN
    {
        public string KODE_CUST { get; set; }
        public string KODE { get; set; }
        public decimal NILAI_BYR { get; set; }
        public decimal DPP { get; set; }
        public decimal PPN { get; set; }
        public decimal NET_BAYAR { get; set; }
        public decimal NET_CASH { get; set; }
        public decimal JM_CARD { get; set; }
        public decimal JM_VOUCHER { get; set; }
        public string KD_CARD_DEBIT { get; set; }
        public decimal NET_CARD_DEBIT { get; set; }
        public string KD_CARD_KREDIT { get; set; }
        public decimal NET_CARD_KREDIT { get; set; }
        public int QTY { get; set; }
        public decimal DISC_R { get; set; }
        public decimal TAG_PRICE { get; set; }
        public decimal MARGIN { get; set; }
        public decimal OTHERS { get; set; }
    }
}