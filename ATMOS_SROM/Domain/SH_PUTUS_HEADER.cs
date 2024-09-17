using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class SH_PUTUS_HEADER
    {
        public Int64 ID { get; set; }
        public string KODE_CUST { get; set; }
        public string KODE { get; set; }
        public DateTime? TGL_TRANS { get; set; }
        public string NO_BON { get; set; }
        public decimal DPP { get; set; }
        public decimal PPN { get; set; }
        public int QTY { get; set; }
        public decimal BRUTTO { get; set; }
        public decimal ITDISCRP { get; set; }
        public decimal NET_BAYAR { get; set; }
        public string CARD { get; set; }
        public decimal JM_UANG { get; set; }
        public decimal KEMBALI { get; set; }
        public decimal NET_CASH { get; set; }
        public int SDH_CTK { get; set; }
        public string JAM { get; set; }
        public DateTime TGL_EDT { get; set; }
        public string BATAL { get; set; }
        public string VOUCHER { get; set; }
        public string NO_EPC { get; set; }
        public string FVER { get; set; }
        public DateTime FTGLVER { get; set; }
        public decimal DISC_P { get; set; }
        public decimal DISC_R { get; set; }
        public string NO_BONM { get; set; }
        public string FID { get; set; }
        public string FIDMESIN { get; set; }
        public string FREMARK { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string STATUS_STORE { get; set; }
        public string STATUS_HEADER { get; set; }
        public DateTime? SEND_DATE { get; set; }
        //public Int32 MARGIN { get; set; }
        public decimal MARGIN { get; set; }
        public string FRETUR { get; set; }

        //Tambahan
        public string NO_SO { get; set; }
        public string NO_SCAN { get; set; }
        public string STATUS_ORDER { get; set; }
    }
}