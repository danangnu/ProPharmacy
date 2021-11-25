using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {         
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<FilesVersion> FilesVersion { get; set; }
        public DbSet<Docs> Documents { get; set; }
        public DbSet<Prescriptions> Prescriptions { get; set; }
        public DbSet<ScheduleOfPayment> ScheduleOfPayments { get; set; }
        public DbSet<PriceListHistory> PriceListHistory { get; set; }
        public DbSet<PrescriptionSummary> PrescriptionSummary { get; set; }
        public DbSet<Mur> Mur { get; set; }
         public DbSet<SalesSummary> SalesSummary { get; set; }
          public DbSet<ExpenseSummary> ExpenseSummary { get; set; }
    }
}