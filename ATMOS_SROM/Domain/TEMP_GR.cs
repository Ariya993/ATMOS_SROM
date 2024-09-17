using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class TEMP_GR
    {
        public Int64 ID { get; set; }
        public Int64 ID_PO { get; set; }
        public string KODE_SUPPLIER { get; set; }
        public string SUPPLIER { get; set; }
        public DateTime TGL_TRANS { get; set; }
        public string CREATED_BY { get; set; }
    }
}