using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class SH_VOUCHER
    {
        public Int64 ID { get; set; }
        public Int64 ID_BAYAR { get; set; }
        public string KODE_CUST { get; set; }
        public string NO_VCR { get; set; }
        public decimal NILAI { get; set; }
        public string NO_CARD { get; set; }
        public string NAMA { get; set; }
        public string NO_BON { get; set; }
        public string NAMA_SHR { get; set; }
        public int FLAG { get; set; }
        public string BATAL { get; set; }
        public string FNOUNIK { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public bool STATUS { get; set; }
    }
}