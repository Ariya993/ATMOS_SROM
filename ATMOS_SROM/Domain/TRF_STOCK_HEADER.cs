using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class TRF_STOCK_HEADER
    {
        public Int64 ID { get; set; }
        public string NO_BUKTI { get; set; }
        public string DARI { get; set; }
        public string KE { get; set; }
        public string KODE_DARI { get; set; }
        public string KODE_KE { get; set; }
        public DateTime? WAKTU_KIRIM { get; set; }
        public DateTime? WAKTU_TERIMA { get; set; }
        public DateTime? DONE_TIME { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public string STATUS { get; set; }
        public bool STATUS_TRF { get; set; }
    }
}