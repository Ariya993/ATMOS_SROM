using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_STOCK
    {
        public Int64 ID { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public string ITEM_CODE { get; set; }
        public string BARCODE { get; set; }
        public string WAREHOUSE { get; set; }
        public string KODE { get; set; }
        public int STOCK { get; set; }
        public string RAK { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public string ART_DESC { get; set; }
        public string PRODUK { get; set; }
        public string FGROUP { get; set; }
        public string WARNA { get; set; }
        public string SIZE { get; set; }
        public string BRAND { get; set; }
    }
}