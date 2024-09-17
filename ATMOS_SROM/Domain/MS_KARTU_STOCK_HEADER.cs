using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_KARTU_STOCK_HEADER
    {
        public string KODE { get; set; }
        public string SHOWROOM { get; set; }
        public Int32 SALDO_AWAL { get; set; }
        public Int32 SALE { get; set; }
        public Int32 BELI { get; set; }
        public Int32 TERIMA { get; set; }
        public Int32 KIRIM { get; set; }
        public Int32 ADJUSTMENT { get; set; }
        public Int32 SALDO_AKHIR { get; set; }
    }
}