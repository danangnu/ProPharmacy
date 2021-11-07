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
    }
}