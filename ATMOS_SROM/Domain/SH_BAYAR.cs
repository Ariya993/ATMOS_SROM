using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class SH_BAYAR
    {
        public Int64 ID { get; set; }
        public string KODE_CUST { get; set; }
        public string KODE { get; set; }
        public DateTime TGL_TRANS { get; set; }
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
        public string EPC { get; set; }
        public string FVER { get; set; }
        public DateTime FTGLVER { get; set; }
        public decimal DISC_P { get; set; }
        public decimal DISC_R { get; set; }
        public string NO_BONM { get; set; }
        public string FID { get; set; }
        public string FIDMESIN { get; set; }
        public string FREMARK { get; set; }
        public string CREATED_BY { get; set; }
        public string STATUS_STORE { get; set; }
        public decimal JM_CARD { get; set; }
        public decimal JM_VOUCHER { get; set; }
        public string MEMBER { get; set; }
        public string VOID_BY { get; set; }
        public DateTime? VOID_DATE { get; set; }
        public string NO_MEMBER { get; set; }

        //Additional
        public decimal NET_CARD { get; set; }
        public decimal OTHER { get; set; }
        public string ONGKIR { get; set; }
        public decimal JM_ONGKIR { get; set; }
        public decimal JM_FREE_ONGKIR { get; set; }
    }
}