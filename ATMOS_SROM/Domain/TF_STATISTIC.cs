using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class TF_STATISTIC
    {
        public int STOCK_AWAL { get; set; }
        public string KODE { get; set; }
        public int PENJUALAN { get; set; }
        public int PUTUS { get; set; }
        public int PUTUS_RETUR { get; set; }
        public int PO { get; set; }
        public int TERIMA_BRG { get; set; }
        public int TERIMA_PINJAM { get; set; }
        public int KELUAR_BRG { get; set; }
        public int KELUAR_PINJAM { get; set; }
        public int ADJ_MANUAL { get; set; }
        public int ADJUSTMENT_SO { get; set; }
        public int STOCK_AKHIR { get; set; }
        public int DIFF { get; set; }
    }
}