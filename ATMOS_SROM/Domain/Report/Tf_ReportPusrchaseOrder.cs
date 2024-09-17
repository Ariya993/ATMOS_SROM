using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain.Report
{
    public class Tf_ReportPusrchaseOrder
    {
        public Int64 Row_Nmbr { get; set; }
        public Int64 ID { get; set; }
        public string NO_PO { get; set; }
        public string PO_REFF { get; set; }
        public DateTime DATE { get; set; }
        public string BRAND { get; set; }
        public string CONTACT { get; set; }
        public string POSITION { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE { get; set; }
        public string KODE_SUPPLIER { get; set; }
        public string SUPPLIER { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public string BARCODE { get; set; }
        public string FART_DESC { get; set; }
        public string FCOL_DESC { get; set; }
        public string FSIZE_DESC { get; set; }
        public Int32 QTY { get; set; }
        public decimal COGS { get; set; }
        public decimal ttl_amt { get; set; }
        public Boolean STATUS_PO { get; set; }

    }
}