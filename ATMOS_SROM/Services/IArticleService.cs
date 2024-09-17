using ATMOS_SROM.Model.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATMOS_SROM.Services
{
    public interface IArticleService
    {
        int ProcessExcelArticleRow(List<ArticleExcelRowModel> excelArticles, string userName, ref int dataDuplicate, ref int dataHasSpclChar);
    }
}
