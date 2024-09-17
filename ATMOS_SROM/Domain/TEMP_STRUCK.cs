using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class TEMP_STRUCK
    {
        public Int64 ID { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public string BARCODE { get; set; }
        public string ITEM_CODE { get; set; }
        public string ART_DESC { get; set; }
        public string WARNA { get; set; }
        public string SIZE { get; set; }
        public string BRAND { get; set; }
        public decimal PRICE { get; set; }
        public decimal SPCL_PRICE { get; set; }
        public decimal BON_PRICE { get; set; }
        public decimal ART_PRICE { get; set; }
        public decimal NET_PRICE { get; set; }
        public int QTY { get; set; }
        public string SA_NAME { get; set; }
        public Int64 ID_ACARA { get; set; }
        public decimal NET_ACARA { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string RETUR { get; set; }
        public string MEMBER { get; set; }
        public string EPC { get; set; }

        /*Additional*/
        public decimal DISCOUNT { get; set; }
        //public int DISCOUNT { get; set; }
        public string JENIS_DISCOUNT { get; set; }
        public string ALL_DETAIL { get; set; }
        public decimal TOTAL_PRICE { get; set; }
        public decimal NET_DISCOUNT { get; set; }
        public int DISC_TEMP { get; set; }
    }
}