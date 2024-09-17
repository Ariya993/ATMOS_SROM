using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class TR_ADJUSTMENT
    {
        public Int64 ID { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public Int64 ID_WAREHOUSE { get; set; }
        public string KODE { get; set; }
        public string SHOWROOM { get; set; }
        public string BARCODE { get; set; }
        public string ITEM_CODE { get; set; }
        public string BRAND { get; set; }
        public string ART_DESC { get; set; }
        public string COLOR { get; set; }
        public string SIZE { get; set; }
        public Int32 STOCK_AWAL { get; set; }
        public int ADJUSTMENT { get; set; }
        public int STOCK_AKHIR { get; set; }
        public string ALASAN { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
    }
}