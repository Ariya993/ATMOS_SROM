using ATMOS_SROM.Model.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleDbTransactionService _articleDatabaseTransactionService;

        public ArticleService(IArticleDbTransactionService articleDatabaseTransactionService)
        {
            _articleDatabaseTransactionService = articleDatabaseTransactionService;
        }

        public int ProcessExcelArticleRow(List<ArticleExcelRowModel> excelArticles, string userName, ref int dataDuplicate, ref int dataHasSpclChar)
        {
            String doubleQuotmark = @"""";
            string[] specialChar = { "&", "%", "'", "#", doubleQuotmark };
            
            List<ArticleExcelRowModel> validInsertOrUpdateBarang = new List<ArticleExcelRowModel>();

            foreach (ArticleExcelRowModel item in excelArticles)
            {
                if (ValidMasterArticleRow(item))
                {
                    if (validInsertOrUpdateBarang.Where(x => x.Barcode.Equals(item.Barcode, StringComparison.OrdinalIgnoreCase)).Any())
                    {
                        dataDuplicate++;
                    }
                    else
                    {
                        if (specialChar.Any(item.SKU.Contains) || specialChar.Any(item.Name.Contains))
                        {
                            dataHasSpclChar++;
                        }
                        else
                        {
                            validInsertOrUpdateBarang.Add(item);
                        }
                    }
                }
            }

            if (validInsertOrUpdateBarang.Any())
            {
                return _articleDatabaseTransactionService.BulkInsertOrUpdate(validInsertOrUpdateBarang, userName);
            }

            return 0;
        }

        private bool ValidMasterArticleRow(ArticleExcelRowModel item)
        {
            return !(string.IsNullOrEmpty(item.SKU) ||
                        string.IsNullOrEmpty(item.Barcode) ||
                        string.IsNullOrEmpty(item.Colour) ||
                        string.IsNullOrEmpty(item.Size));
        }
    }
}