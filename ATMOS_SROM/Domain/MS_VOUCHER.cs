using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_VOUCHER
    {
        public Int64 ID { get; set; }
        public string NO_VOUCHER { get; set; }
        public decimal NILAI { get; set; }
        public string JENIS { get; set; }
        public DateTime VALID_FROM { get; set; }
        public DateTime? VALID_UNTIL { get; set; }
        public string STATUS_VOUCHER { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public bool STATUS { get; set; }
        public string KODE { get; set; }
        public DateTime? KODE_CREATED { get; set; }

        public DateTime TGL_TRANS { get; set; }
    }
}