using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_CUST_WHOLESALE
    {
        public Int64 ID { get; set; }
        public string KD_PEMBELI { get; set; }
        public string NM_PEMBELI { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public bool BLOCK { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime UPDATED_DATE { get; set; }

    }
}