using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class TEMP_KDBRG
    {
        public Int64 ID { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public Int64 ID_HEADER { get; set; }
        public Int64 ID_SHOWROOM { get; set; }
        public string KODE { get; set; }
        public string SHOWROOM { get; set; }
        public string BARCODE { get; set; }
        public string ITEM_CODE { get; set; }
        public int QTY { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string FLAG { get; set; }
        public string STAT { get; set; }
        public string BRAND { get; set; }
        public string ART_DESC { get; set; }
        public string FCOLOR { get; set; }
        public string FSIZE { get; set; }
        public decimal PRICE { get; set; }
        public decimal DISCOUNT { get; set; }
        public decimal PRICE_AKHIR { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
    }
}