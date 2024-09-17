using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_PROMO
    {
        public Int64 ID { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public string ITEM_CODE { get; set; }
        public string BARCODE { get; set; }
        public decimal SPCL_PRICE { get; set; }
        public int? DISCOUNT { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public string FLAG { get; set; }
        public string CATATAN { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public bool STATUS { get; set; }

        //Additional
        public decimal PRICE { get; set; }
        public decimal PRICE_AKHIR { get; set; }
        public string BARCODE_BRG { get; set; }
        public string ITEM_CODE_BRG { get; set; }
        public string FBRAND { get; set; }
        public string FART_DESC { get; set; }
        public string FCOL_DESC { get; set; }
        public string FSIZE_DESC { get; set; }
    }
}