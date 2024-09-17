using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain.Report
{
    public class USP_SRPTSTOCKOPNAME
    {
        public string NO_BUKTI { get; set; }
        public string NO_BUKTI_DETAIL { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public string KODE { get; set; }
        public string KODE_MOVE { get; set; }
        public string SHOWROOM { get; set; }
        public string KODE_SHOWROOM { get; set; }
        public DateTime? WAKTU_SO { get; set; }
        public string STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string BARCODE { get; set; }
        public int DIFF { get; set; }
        public int STOCK_AWAL { get; set; }
        public int STOCK_AKHIR { get; set; }
        public string BARCODE_BRG { get; set; }
        public string ITEM_CODE_BRG { get; set; }
        public string FART_DESC { get; set; }
        public string FCOL_DESC { get; set; }
        public string FSIZE_DESC { get; set; }
        public string FBRAND { get; set; }
        public string FGROUP { get; set; }
        public string FPRODUK { get; set; }
        public string FSEASON { get; set; }
        public string FBS { get; set; }
        public DateTime? TGL_BS { get; set; }
        public decimal PRICE { get; set; }
        public string KLMPK { get; set; }
        public decimal COGS { get; set; }
        public string STATUS_BRG { get; set; }
    }
}