using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class EXCELL_RAK_TEMP_SO
    {
        //RAK, ITEM_CODE, BARCODE, ART_DESC, WARNA, SIZE, STOCK
        public string RAK { get; set; }
        public string ITEM_CODE { get; set; }
        public string BARCODE { get; set; }
        public string ART_DESC { get; set; }
        public string WARNA { get; set; }
        public string SIZE { get; set; }
        public int STOCK { get; set; }
    }
}