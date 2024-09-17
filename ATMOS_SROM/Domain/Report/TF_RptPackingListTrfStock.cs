using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain.Report
{
    public class TF_RptPackingListTrfStock
    {
        public Int64 Row_Nmbr { get; set; }
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
        public string FBS { get; set; }
        public DateTime TGL_BS { get; set; }
        public string MILIK { get; set; }
        public decimal PRICE { get; set; }
        public string KLMPK { get; set; }
        public decimal COGS { get; set; }
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

        public string KODE_KE { get; set; }
        public string SHR_TUJUAN { get; set; }
        public string KODE_DARI { get; set; }
        public string SHR_ASAL { get; set; }
        public string No_Refrensi { get; set; }
        public string ALMT_TUJUAN { get; set; }
        public string ALMT_ASAL { get; set; }
        public string TELP_TUJUAN { get; set; }
        public string TELP_ASAL { get; set; }
    }
}