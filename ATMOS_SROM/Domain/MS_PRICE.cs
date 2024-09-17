using ATMOS_SROM.Model.Article;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_PRICE
    {
        public Int64 ID { get; set; }
        public Int64 ID_KDBRG { get; set; }
        public string ITEM_CODE { get; set; }
        public decimal PRICE { get; set; }
        public DateTime START_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; } = DateTime.UtcNow.AddHours(7);

        public DateTime? END_DATE { get; set; }
        [NotMapped]
        public string Info { get; set; }

        public MS_PRICE()
        {

        }

        public MS_PRICE(ArticleExcelRowModel item, long idKdbrg, string userName)
        {
            CREATED_BY = userName;
            ID_KDBRG = idKdbrg;
            ITEM_CODE = item.SKU;
            PRICE = item.RetailPrice;
            START_DATE = item.StartDate;
        }
    }
}