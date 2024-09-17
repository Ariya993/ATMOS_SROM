using ATMOS_SROM.Model.Article;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATMOS_SROM.Domain
{
    public class MS_KDBRG
    {
        public Int64 ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string BARCODE { get; set; }
        public string FART_DESC { get; set; }
        public string FCOL_DESC { get; set; }
        public string FSIZE_DESC { get; set; }
        public string FPRODUK { get; set; }
        public string FBRAND { get; set; }
        public string FGROUP { get; set; }
        public string FSEASON { get; set; }
        public string FBS { get; set; }
        public DateTime? TGL_BS { get; set; }
        public string MILIK { get; set; }
        public decimal PRICE { get; set; }
        public string KLMPK { get; set; }
        public string GENDER { get; set; }
        public decimal COGS { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; } = DateTime.UtcNow.AddHours(7);
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public string STATUS_BRG { get; set; }
        public DateTime? DATE_START { get; set; }

        /*Additional*/
        [NotMapped]
        public string ART_DESC { get; set; }
        [NotMapped]
        public Int64 ID_PROMO { get; set; }
        [NotMapped]
        public decimal SPCL_PRICE { get; set; }
        [NotMapped]
        public decimal DISCOUNT { get; set; }
        [NotMapped]
        public DateTime? START_DATE { get; set; }
        [NotMapped]
        public DateTime? END_DATE { get; set; }
        [NotMapped]
        public string FLAG { get; set; }
        [NotMapped]
        public string CATATAN { get; set; }
        [NotMapped]
        public decimal ART_PRICE { get; set; }
        [NotMapped]
        public decimal NET_PRICE { get; set; }
        [NotMapped]
        public decimal BON_PRICE { get; set; }
        [NotMapped]
        public string RETUR { get; set; }
        [NotMapped]
        public int STOCK { get; set; }
        [NotMapped]
        public string MEMBER { get; set; }

        [NotMapped]
        public Int64 ID_PROMO_SHR { get; set; }
        [NotMapped]
        public int DISC_PROMO_SHR { get; set; }

        //JENAHARA
        [NotMapped]
        public string STAT_KDBRG { get; set; }

        public MS_KDBRG()
        {

        }

        public MS_KDBRG(ArticleExcelRowModel item, string userName, bool isInsert = true, MS_KDBRG savedRecord = null)
        {
            if (isInsert)
            {
                CREATED_BY = userName;
                CREATED_DATE = DateTime.UtcNow.AddHours(7);
            }
            else
            {
                ID = savedRecord.ID;
                CREATED_BY = savedRecord.CREATED_BY;
                CREATED_DATE = savedRecord.CREATED_DATE;
                UPDATED_BY = userName;
                UPDATED_DATE = DateTime.UtcNow.AddHours(7);
            }

            ITEM_CODE = item.SKU;
            BARCODE = item.Barcode;
            FART_DESC = item.Name;
            FCOL_DESC = item.Colour;
            FSIZE_DESC = item.Size;
            FPRODUK = item.ProductType;
            FBRAND = item.Brand;
            FGROUP = item.Category;
            PRICE = item.RetailPrice;
            COGS = item.CostLocalPrice;
            CREATED_BY = userName;
            FSEASON = item.Season;
            DATE_START = item.StartDate;
        }
    }
}