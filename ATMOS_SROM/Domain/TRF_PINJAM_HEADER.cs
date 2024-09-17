using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class TRF_PINJAM_HEADER
    {
        public Int64 ID { get; set; }
        public string NO_BUKTI { get; set; }
        public string DARI { get; set; }
        public string KE { get; set; }
        public string KODE_DARI { get; set; }
        public string KODE_KE { get; set; }
        public string NAMA { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public DateTime? WAKTU_KIRIM { get; set; }
        public DateTime? WAKTU_KEMBALI { get; set; }
        public DateTime? WAKTU_SELESAI { get; set; }
        public string STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public bool STATUS_PINJAM { get; set; }
        public int TOTAL_KIRIM { get; set; }
        public int TOTAL_KEMBALI { get; set; }
    }
}