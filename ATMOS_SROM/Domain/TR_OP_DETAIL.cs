using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class TR_OP_DETAIL
    {
        public Int64 ID { get; set; }
        public Int64 ID_HEADER { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public string NO_BUKTI { get; set; }
        public string ITEM_CODE { get; set; }
        public string DARI { get; set; }
        public string KE { get; set; }
        public DateTime? WAKTU_KIRIM { get; set; }
        public DateTime? WAKTU_TERIMA { get; set; }
        public int QTY_KIRIM { get; set; }
        public int QTY_TERIMA { get; set; }
        public int STOCK_AKHIR_KIRIM { get; set; }
        public int STOCK_AKHIR_TERIMA { get; set; }
        public string USER_KIRIM { get; set; }
        public string USER_TERIMA { get; set; }
        public string ALASAN { get; set; }
        public string REFF { get; set; }

        //Additional
        public string STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public bool STATUS_TRF { get; set; }

        public string BARCODE { get; set; }
        public string FART_DESC { get; set; }
        public string FCOL_DESC { get; set; }
        public string FSIZE_DESC { get; set; }
        public string FPRODUK { get; set; }
        public string FBRAND { get; set; }
        public string FGROUP { get; set; }
        public string FSEASON { get; set; }
        public string GENDER { get; set; }
    }
}