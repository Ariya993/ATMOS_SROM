using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain.CustomObj
{
    public class PO_TRACER_D
    {
        public Int64 ID_PO { get; set; }
        public Int64 ID_PO_DTL { get; set; }
        public int QTY { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public string BARCODE { get; set; }
        public string ITEM_CODE { get; set; }
        public string FSize_DESC { get; set; }
        public string FART_DESC { get; set; }
        public string FCOL_DESC { get; set; }
        public string FPRODUK { get; set; }
        public string NO_GR { get; set; }
        public int QTY_TIBA { get; set; }
        public int Selisih { get; set; }
        public int STATUS_PO { get; set; }
        public string NO_PO { get; set; }

    }
}