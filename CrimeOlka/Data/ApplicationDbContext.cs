// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using CrimeAnalysisSystem.Models;

namespace CrimeAnalysisSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CrimeRecord> CrimeRecords { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<CrimeCategory> CrimeCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrimeRecord>()
                .HasOne(c => c.Location)
                .WithMany()
                .HasForeignKey(c => c.LocationId);
        }
    }
}
