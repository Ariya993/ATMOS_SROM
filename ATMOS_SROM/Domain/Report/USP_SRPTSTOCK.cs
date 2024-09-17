using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain.Report
{
    public class USP_SRPTSTOCK
    {
        public Int64 ID { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public string ITEM_CODE { get; set; }
        public string BARCODE { get; set; }
        public string WAREHOUSE { get; set; }
        public string KODE { get; set; }
        public int STOCK { get; set; }
        public string BARCODE_BRG { get; set; }
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
        public string STATUS_BRG { get; set; }
        public string KODE_PT { get; set; }
    }
}