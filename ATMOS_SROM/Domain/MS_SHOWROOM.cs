using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_SHOWROOM
    {
        public Int64 ID { get; set; }
        public string KODE { get; set; }
        public string SHOWROOM { get; set; }
        public string STORE { get; set; }
        public string BRAND { get; set; }
        public string ALAMAT { get; set; }
        public string PHONE { get; set; }
        public string STATUS { get; set; }
        public string STATUS_SHOWROOM { get; set; }
        public string VALUE { get; set; }

        #region "Tambahan"
        public decimal LUAS { get; set; }
        public int JUM_SPG { get; set; }
        public decimal SEWA { get; set; }
        public decimal SERVICE { get; set; }
        public decimal SALARY { get; set; }
        public decimal INTERNET { get; set; }
        public decimal LISTRIK { get; set; }
        public decimal TELEPON { get; set; }
        public decimal SUSUT { get; set; }
        public decimal BY_LAIN2 { get; set; }
        public string STATUS_AWAL { get; set; }
        public string KODE_PT { get; set; }
        public string BRAND_JUAL { get; set; }
        public string VENDOR { get; set; }
        public string LOGO_IMG { get; set; }
        #endregion
    }
}