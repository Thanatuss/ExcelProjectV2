using Microsoft.EntityFrameworkCore;
using SSO.Share.Domain.Sql.Admin.Area;

namespace Import_Excel.Infrastructore.DbContext
{
    public class ProgramDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ProgramDbContext(DbContextOptions<ProgramDbContext> options) : base(options)
        {
        }

        // 👇 سازنده بدون پارامتر (مورد نیاز برای Migration)
        public ProgramDbContext()
        {
        }

        public DbSet<AreaType> AreaTypes { get; set; }
        public DbSet<Area> Areas { get; set; } 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AreaType>().ToTable("AreaTypes");
            modelBuilder.Entity<AreaType>().HasKey(a => a.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // مقداردهی ConnectionString برای Migration
                optionsBuilder.UseSqlServer("Server=.;Database=Excel;TrustServerCertificate=true;Integrated Security=True");
            }
        }
    }
}