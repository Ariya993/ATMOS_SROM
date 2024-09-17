using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class OP_DETAIL
    {
        public Int64 ID { get; set; }
        public Int64 ID_HEADER { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public Int64 ID_STOCK { get; set; }
        public string RAK { get; set; }
        public string NO_BUKTI { get; set; }
        public string ITEM_CODE { get; set; }
        public string BARCODE { get; set; }
        public int DIFF_STOCK { get; set; }
        public int STOCK { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }

        //ADDITIONAL
        public Int64 IDSTOCK { get; set; }
        public string ITEM_CODE_STOCK { get; set; }
        public string BARCODE_STOCK { get; set; }
        public string WAREHOUSE { get; set; }
        public string KODE { get; set; }
        public int QTY_STOCK { get; set; }
        public string STOCK_RAK { get; set; }
    }
}