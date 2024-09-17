using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class SH_ACARA_VALUE
    {
        public Int64 ID { get; set; }
        public Int64 ID_BAYAR { get; set; }
        public Int64 ID_ACARA { get; set; }
        public string PARAM { get; set; }
        public string VALUE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public bool STATUS { get; set; }
    }
}