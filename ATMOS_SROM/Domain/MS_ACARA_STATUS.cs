using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_ACARA_STATUS
    {
        public Int64 ID { get; set; }
        public string ACARA_STATUS { get; set; }
        public string DESC_DISC { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public bool STATUS { get; set; }
    }
}