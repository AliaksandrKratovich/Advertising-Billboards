using Advertising_Billboards.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBaseAccess.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<DeviceGroup> DeviceGroups { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<AdvertisementStatistics> AdvertisementStatistics { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.DeviceGroups).WithOne(d => d.User);
            modelBuilder.Entity<User>().HasMany(u => u.Devices).WithOne(d => d.User);
            modelBuilder.Entity<DeviceGroup>().HasMany(d => d.Devices).WithOne(u => u.DeviceGroup);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            base.OnConfiguring(optionsBuilder);
        }
    }
}
