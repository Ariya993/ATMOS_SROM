using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain.Report
{
    public class TF_ReportPackingListGR
    {
        public Int64 Row_Nmbr { get; set; }
        public string KODE_SUPPLIER { get; set; }
        public string SUPPLIER { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE { get; set; }
        public Int64 ID { get; set; }
        public Int64 ID_HEADER { get; set; }
        public Int64 ID_DETAIL_PO { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public string NO_GR { get; set; }
        public int QTY_TIBA { get; set; }
        public string KODE { get; set; }
        public string SHOWROOM { get; set; }
        public string RECEIVED_BY { get; set; }
        public DateTime? RECEIVE_DATE { get; set; }
        public decimal COGS { get; set; }
        public decimal PRICE { get; set; }

        //Additional
        public string BARCODE { get; set; }
        public string ITEM_CODE { get; set; }
        public string FART_DESC { get; set; }
        public string FCOL_DESC { get; set; }
        public string FSIZE_DESC { get; set; }
        public string FBRAND { get; set; }
        public DateTime? TGL_TRANS { get; set; }
        public string No_Refrensi { get; set; }

    }
}