using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain.Report
{
    public class TF_DeisBIF
    {
        public DateTime DT_FIRST { get; set; }
        public DateTime DT_NOW { get; set; }
        public DateTime DT_FIRST_LY { get; set; }
        public DateTime DT_NOW_LY { get; set; }
        public string KODE { get; set; }
        public string BRAND { get; set; }
        public string STATUS_SHOWROOM { get; set; }
        public string SHOWROOM { get; set; }
        public string ALAMAT { get; set; }
        public Int32 QTY_TY { get; set; }
        public decimal NILAI_BYR_TY { get; set; }
        public Int32 QTY_BON_TY { get; set; }
        public decimal ATV { get; set; }
        public Int32 UPT { get; set; }
        public decimal ASP { get; set; }
        public Int32 QTY_LY { get; set; }
        public decimal NILAI_BYR_LY { get; set; }
        public Int32 QTY_BON_LY { get; set; }
        public Int32 QTY_TY_DtToMonth { get; set; }
        public decimal NILAI_BYR_TY_DtToMonth { get; set; }
        public Int32 QTY_BON_TY_DtToMonth { get; set; }
        public decimal ATV_DtToMonth { get; set; }
        public Int32 UPT_DtToMonth { get; set; }
        public decimal ASP_DtToMonth { get; set; }
        public Int32 QTY_LY_DtToMonth { get; set; }
        public decimal NILAI_BYR_LY_DtToMonth { get; set; }
        public Int32 QTY_BON_LY_DtToMonth { get; set; }
    }
}