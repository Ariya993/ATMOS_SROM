using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class TEMP_WHOLESALE
    {
        public Int64 ID { get; set; }
        public DateTime TGL_PENJUALAN { get; set; }
        public DateTime TGL_PENGIRIMAN { get; set; }
        public string NAMA_PEMBELI { get; set; }
        public string KODE_PEMBELI { get; set; }
        //public int MARGIN { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public decimal MARGIN { get; set; }
    }
}