using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class USP_RPTTOTALPENJUALAN
    {
        public Int64 ID_KDBRG { get; set; }
        public string KODE_CUST { get; set; }
        public string KODE { get; set; }
        public DateTime? TGL_TRANS { get; set; }
        public Int32 QTY { get; set; }
        public Int64 ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string BARCODE { get; set; }
        public string FART_DESC { get; set; }
        public string FCOL_DESC { get; set; }
        public string FSIZE_DESC { get; set; }
        public string FPRODUK { get; set; }
        public string FBRAND { get; set; }
        public string FGROUP { get; set; }
        public string FSEASON { get; set; }
        public string FBS { get; set; }
        public DateTime? TGL_BS { get; set; }
        public string MILIK { get; set; }
        public decimal PRICE { get; set; }
        public string KLMPK { get; set; }
        public string GENDER { get; set; }
        public decimal COGS { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
    }
}