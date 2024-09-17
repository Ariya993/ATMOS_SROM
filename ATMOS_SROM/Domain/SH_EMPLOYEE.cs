using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class SH_EMPLOYEE
    {
        public Int64 ID { get; set; }
        public Int64 ID_BAYAR { get; set; }
        public string NAME { get; set; }
        public string POSITION { get; set; }
        public string NIK { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
    }
}