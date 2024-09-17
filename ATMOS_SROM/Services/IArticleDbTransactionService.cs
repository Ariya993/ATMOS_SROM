using ATMOS_SROM.Model.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATMOS_SROM.Services
{
    public interface IArticleDbTransactionService
    {
        int BulkInsertOrUpdate(List<ArticleExcelRowModel> items, string userName);
    }
}
