using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain.Report
{
    public class USP_SRPTTOTALPENJUALANPERPRODUCT
    {
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
        public string STATUS_BRG { get; set; }

        public int QTY { get; set; }
        public decimal NILAI_BYR { get; set; }
        public decimal DISC_R { get; set; }
        public decimal TAG_PRICE { get; set; }
        public decimal BON_PRICE { get; set; }
        public decimal DPP { get; set; }
        public decimal PPN { get; set; }
    }
}