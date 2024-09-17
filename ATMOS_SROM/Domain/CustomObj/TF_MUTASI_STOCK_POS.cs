using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain.CustomObj
{
    public class TF_MUTASI_STOCK_POS
    {
        public string KODE { get; set; }
        public string BARCODE { get; set; }
        public int SLD_AWAl { get; set; }
        public int QTY_BELI { get; set; }
        public int QTY_TERIMA { get; set; }
        public int QTY_RTR_PTS { get; set; }
        public int QTY_IN_PINJAM { get; set; }
        public int QTY_KIRIM { get; set; }
        public int QTY_JUAL { get; set; }
        public int QTY_JUAL_PTS { get; set; }
        public int QTY_OUT_PINJAM { get; set; }
        public int QTY_ADJ { get; set; }
        public int QTY_OPNM { get; set; }
        public int SLD_AKHIR { get; set; }
        public int ADJ_GIT { get; set; }
    }
}