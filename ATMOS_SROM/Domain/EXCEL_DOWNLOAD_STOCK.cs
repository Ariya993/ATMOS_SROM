using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class EXCEL_DOWNLOAD_STOCK
    {
        //KODE, SHOWROOM, BARCODE, ITEM_CODE, BRAND, ARTICLE, WARNA, UKURAN, STOCK
        public string KODE { get; set; }
        public string SHOWROOM { get; set; }
        public string BARCODE { get; set; }
        public string ITEM_CODE { get; set; }
        public string BRAND { get; set; }
        public string ARTICLE { get; set; }
        public string WARNA { get; set; }
        public string UKURAN { get; set; }
        public int STOCK { get; set; }
        public decimal Price { get; set; }
        public DateTime TANGGAL_GENERATE { get; set; }
    }
}