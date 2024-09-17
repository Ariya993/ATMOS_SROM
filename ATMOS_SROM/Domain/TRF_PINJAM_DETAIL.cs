using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class TRF_PINJAM_DETAIL
    {
        public Int64 ID { get; set; }
        public Int64 ID_HEADER { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public string NO_BUKTI { get; set; }
        public string BARCODE { get; set; }
        public string ITEM_CODE { get; set; }
        public Int32 QTY_KIRIM { get; set; }
        public Int32 QTY_TERIMA { get; set; }
        public int STOCK_AKHIR_KIRIM { get; set; }
        public int STOCK_AKHIR_TERIMA { get; set; }
        public string USER_KIRIM { get; set; }
        public string USER_TERIMA { get; set; }
        public string ALASAN { get; set; }
        public string REFF { get; set; }
        public string FLAG { get; set; }
        public string BRAND { get; set; }
        public string FART_DESC { get; set; }
        public string COLOR { get; set; }
        public string SIZE { get; set; }
    }
}