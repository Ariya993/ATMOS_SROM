using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_DISCOUNT_SHR
    {
        public Int64 ID { get; set; }
        public Int64 ID_SHR { get; set; }
        public string KODE { get; set; }
        public string SHOWROOM { get; set; }
        public int DISCOUNT { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public bool STATUS { get; set; }
        public string TIPE { get; set; }
        public string TIPE_DISCOUNT { get; set; }
    }
}