using ATMOS_SROM.Domain;
using System.Data.Entity;

namespace ATMOS_SROM.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<MS_KDBRG> MS_KDBRG { get; set; }
        public DbSet<MS_PRICE> MS_PRICE { get; set; }

        public ApplicationDbContext() : base("name=ConnectionString")
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }
    }
}