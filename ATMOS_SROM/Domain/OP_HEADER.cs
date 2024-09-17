using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class OP_HEADER
    {
        public Int64 ID { get; set; }
        public string NO_BUKTI { get; set; }
        public string KE { get; set; }
        public string KODE_KE { get; set; }
        public DateTime UPLOAD_TIME { get; set; }
        public string STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
    }
}