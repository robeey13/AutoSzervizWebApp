using Microsoft.EntityFrameworkCore;

namespace AutoSzervizWebApp.Data
{
    public class AutoszervizDbContext : DbContext
    {
        public AutoszervizDbContext(DbContextOptions<AutoszervizDbContext> options) : base(options)
        {
        }

        public DbSet<AutoSzervizWebApp.Models.Ugyfel> Ugyfelek { get; set; }
        public DbSet<AutoSzervizWebApp.Models.Javitas> Javitasok { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AutoSzervizWebApp.Models.Ugyfel>()
                .HasMany(u => u.Javitasok)
                .WithOne(j => j.Ugyfel)
                .HasForeignKey(j => j.UgyfelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
