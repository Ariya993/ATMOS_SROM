using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain.Report
{
    public class USP_SRPTTRANSFERSTOCK
    {
        public string NO_BUKTI { get; set; }
        public string KODE_MOVE { get; set; }
        public string DARI { get; set; }
        public string KE { get; set; }
        public string KODE_DARI { get; set; }
        public string KODE_KE { get; set; }
        public DateTime? WAKTU_KIRIM { get; set; }
        public DateTime? WAKTU_TERIMA { get; set; }
        public DateTime? DONE_TIME { get; set; }
        public string STATUS { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public string NO_BUKTI_DETAIL { get; set; }
        public string BARCODE { get; set; }
        public int QTY_KIRIM { get; set; }
        public int QTY_TERIMA { get; set; }
        public int STOCK_AKHIR_KIRIM { get; set; }
        public int STOCK_AKHIR_TERIMA { get; set; }
        public string USER_KIRIM { get; set; }
        public string USER_TERIMA { get; set; }
        public string ALASAN { get; set; }
        public string REFF { get; set; }
        public string BARCODE_BRG { get; set; }
        public string ITEM_CODE { get; set; }
        public string FBRAND { get; set; }
        public string FGROUP { get; set; }
        public string FPRODUK { get; set; }
        public string FSEASON { get; set; }
        public string FART_DESC { get; set; }
        public string FCOL_DESC { get; set; }
        public string FSIZE_DESC { get; set; }
        public string FBS { get; set; }
        public DateTime? TGL_BS { get; set; }
        public string MILIK { get; set; }
        public string KLMPK { get; set; }
        public decimal PRICE { get; set; }
        public decimal COGS { get; set; }
        public string STATUS_BRG { get; set; }
        public string KODE_PT { get; set; }
    }
}