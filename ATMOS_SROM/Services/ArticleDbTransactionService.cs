using ATMOS_SROM.DataAccess;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model.Article;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATMOS_SROM.Services
{
    public class ArticleDbTransactionService : IArticleDbTransactionService
    {
        private ILog _log;
        private readonly ApplicationDbContext _dbContext;

        public ArticleDbTransactionService(ILog log,
            ApplicationDbContext dbContext)
        {
            _log = log;
            _dbContext = dbContext;
        }

        public int BulkInsertOrUpdate(List<ArticleExcelRowModel> items, string userName)
        {
            int dataCreated = 0;

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var brgItem in items)
                    {
                        MS_KDBRG kdbrg = FindKdBrg(brgItem, userName);
                        _dbContext.MS_KDBRG.AddOrUpdateExtension(kdbrg);

                        dataCreated++;
                    }
                    
                    _dbContext.SaveChanges();

                    foreach (var priceItem in items)
                    {
                        InsertPrice(priceItem, userName);
                    }

                    _dbContext.SaveChanges();
                    
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _log.Error($"Error: {ex.Message}. Inner exception: {ex.InnerException}. Stack trace: {ex.StackTrace}");

                    transaction.Rollback();
                    throw;
                }
            }

            return dataCreated;
        }

        private MS_KDBRG FindKdBrg(ArticleExcelRowModel articleExcelRowModel, string userName)
        {
            var barang = _dbContext.MS_KDBRG
                .FirstOrDefault(x => x.BARCODE.Equals(articleExcelRowModel.Barcode, StringComparison.OrdinalIgnoreCase));

            if (barang is null)
            {
                return new MS_KDBRG(articleExcelRowModel, userName);
            }

            return new MS_KDBRG(articleExcelRowModel, userName, false, barang);
        }

        private void InsertPrice(ArticleExcelRowModel priceItem, string userName)
        {
            long idBarang = _dbContext.MS_KDBRG
                .FirstOrDefault(x => x.BARCODE.Equals(priceItem.Barcode, StringComparison.OrdinalIgnoreCase)).ID;

            MS_PRICE lastPrice = _dbContext.MS_PRICE
                .Where(x => x.ID_KDBRG == idBarang)
                .OrderByDescending(x => x.CREATED_DATE)
                .FirstOrDefault();

            MS_PRICE newPrice = new MS_PRICE();

            if (lastPrice is null)
            {
                newPrice = new MS_PRICE(priceItem, idBarang, userName);

                _dbContext.MS_PRICE.Add(newPrice);
            }
            else
            {
                if (lastPrice.PRICE != priceItem.RetailPrice)
                {
                    lastPrice.END_DATE = priceItem.StartDate;

                    newPrice = new MS_PRICE(priceItem, idBarang, userName);

                    _dbContext.MS_PRICE.Add(newPrice);
                }
            }
        }
    }
}