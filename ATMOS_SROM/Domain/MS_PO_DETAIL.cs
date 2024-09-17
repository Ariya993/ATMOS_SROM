using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_PO_DETAIL
    {
        public Int64 ID { get; set; }
        public Int64 ID_PO { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public string BARCODE { get; set; }
        public string ITEM_CODE { get; set; }
        public decimal COGS { get; set; }
        public decimal PRICE { get; set; }
        public int QTY { get; set; }
        public int QTY_REAL { get; set; }
        public string STATUS { get; set; }
        public bool STATUS_PO { get; set; }

        //Additional
        public int QTY_TIBA { get; set; }
        public int QTY_WAIT { get; set; }
        public string NO_PO { get; set; }
        public string FART_DESC { get; set; }
        public string FCOL_DESC { get; set; }
        public string FSIZE_DESC { get; set; }
        public decimal FPPN_PCT { get; set; }
        public decimal FPPN_RP { get; set; }
        public decimal FJUMLAH { get; set; }
    }
}