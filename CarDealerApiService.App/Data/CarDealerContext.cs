using CarDealerAPIService.App.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarDealerAPIService.App.Data
{
    public class CarDealerContext : IdentityDbContext<User>
    {
        // TODO: add DbSet props here
        public DbSet<Vehicle> VehicleInventory { get; set; }
        public DbSet<User> UserTable { get; set; }
        public DbSet<VehicleSubmissions> VehicleSubmissions { get; set; }
        public DbSet<VehicleListing> VehicleListings { get; set; }

        public CarDealerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
            });



            modelBuilder.Entity<VehicleSubmissions>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VehicleListing>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
            });
        }
    }
}