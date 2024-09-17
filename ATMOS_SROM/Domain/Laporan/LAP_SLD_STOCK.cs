using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain.Laporan
{
    public class LAP_SLD_STOCK
    {
        public Int64 ID { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public Int64 ID_SHOWROOM { get; set; }
        public string SHOWROOM { get; set; }
        public string KODE { get; set; }
        public string STATUS_SHOWROOM { get; set; }
        public string BARCODE { get; set; }
        public int SALDO_AWAL { get; set; }
        public int PENJUALAN { get; set; }
        public int PEMBELIAN { get; set; }
        public int TERIMA_BARANG { get; set; }
        public int KELUAR_BARANG { get; set; }
        public int ADJUSTMENT { get; set; }
        public int SALDO_AKHIR { get; set; }
        public DateTime? TGL_CUT_OFF { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string FBULAN { get; set; }
        public bool STATUS { get; set; }
        public string ITEM_CODE { get; set; }
        public string FBRAND { get; set; }
        public string FART_DESC { get; set; }
        public string FCOL_DESC { get; set; }
        public string FSIZE_DESC { get; set; }
        public string FPRODUK { get; set; }
        public string FGROUP { get; set; }
        public string FSEASON { get; set; }
    }
}