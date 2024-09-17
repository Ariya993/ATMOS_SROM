using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_SKU
    {
        public decimal MS_SKU_ID { get; set; }
        public string KD_SKU { get; set; }
        public string KD_BRAND { get; set; }
        public string GRUP { get; set; }
        //public string KD_SHOWROOM { get; set; }
        public decimal DISC_P { get; set; }
        public decimal MARGIN { get; set; }
        public decimal? BBN_DEPT { get; set; }
        public string KETERANGAN { get; set; }
        public DateTime FADD { get; set; }
        public DateTime FUPDT { get; set; }
        public string USR_ADD { get; set; }
        public string USR_UPD { get; set; }
        public string FILE_UP { get; set; }
    }
}