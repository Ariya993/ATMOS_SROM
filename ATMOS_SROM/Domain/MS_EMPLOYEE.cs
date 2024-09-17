using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_EMPLOYEE
    {
        public Int64 ID { get; set; }
        public string NIK { get; set; }
        public string NAMA { get; set; }
        public string JABATAN { get; set; }
        public DateTime? JOIN_DATE { get; set; }
        public decimal LIMIT { get; set; }
        public string STATUS_EMPLOYEE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public bool STATUS { get; set; }
        public int DAY_JOIN { get; set; }
        public int STATUS_CARD { get; set; }
        public string TIPE { get; set; }
        public decimal LIMIT_DELAMI { get; set; }
        public string STATUS_EPC { get; set; }
        public decimal SISA_LIMIT { get; set; }
    }
}