using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_PROMO_SHR
    {
        public Int64 ID { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public Int64 ID_STORE { get; set; }
        public string BARCODE { get; set; }
        public string SHOWROOM { get; set; }
        public string KODE { get; set; }
        public int DISCOUNT { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string KET { get; set; }
        public decimal PRICE { get; set; }
        public string STAT { get; set; }
        
        //Additional
        public string BARCODE_BRG { get; set; }
        public string ITEM_CODE { get; set; }
        public string FBRAND { get; set; }
        public string FART_DESC { get; set; }
        public string FCOL_DESC { get; set; }
        public string FSIZE_DESC { get; set; }
        public decimal PRICE_BRG { get; set; }
        public string ART_DESC { get; set; }
    }
}