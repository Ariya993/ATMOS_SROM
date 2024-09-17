using NPOI.SS.UserModel;
using System;

namespace ATMOS_SROM.Model.Article
{
    public class ArticleExcelRowModel
    {
        public string Brand { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public string Size { get; set; }
        public string ProductType { get; set; }
        public decimal CostLocalPrice { get; set; }
        public decimal RetailPrice { get; set; }
        public string Category { get; set; }
        public string Season { get; set; }
        public DateTime StartDate { get; set; }

        public ArticleExcelRowModel()
        {

        }

        public ArticleExcelRowModel(IRow row)
        {
            DateTime dt = new DateTime();
            decimal outRetailPrice = 0;
            decimal outCostLocalPrice = 0;

            decimal.TryParse(row.GetCell(7).ToString().Trim(), out outCostLocalPrice);
            decimal.TryParse(row.GetCell(8).ToString().Trim(), out outRetailPrice);

            Brand = row.GetCell(0).ToString().Trim();
            SKU = row.GetCell(1).ToString().Trim();
            Barcode = row.GetCell(2).ToString().Trim();
            Barcode = string.IsNullOrEmpty(Barcode) ? String.Format("{0:yMMddHHmmssff}", dt) : Barcode;
            Name = row.GetCell(3).ToString().Trim();
            Colour = row.GetCell(4).ToString().Trim();
            Size = row.GetCell(5).ToString().Trim();
            ProductType = row.GetCell(6).ToString().Trim();
            CostLocalPrice = outCostLocalPrice;
            RetailPrice = outRetailPrice;
            Category = row.GetCell(9).ToString().Trim();
            Season = row.GetCell(10).ToString().Trim();
            StartDate = Convert.ToDateTime(row.GetCell(11).ToString());
        }
    }
}