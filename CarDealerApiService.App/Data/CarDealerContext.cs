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
        public DbSet<VehiclePriceRequest>  MarketValues{ get; set; }
        public CarDealerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(b=>
                {
                    b.HasKey(e => e.Id);
                    b.Property(e => e.Id).ValueGeneratedOnAdd();
                });
            
            modelBuilder.Entity<VehicleSubmissions>(b=>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VehiclePriceRequest>(b=>
            {
                b.HasKey(e => e.Vin);
            });

            modelBuilder.Entity<Prices>(b=>
            {
                b.HasKey(e => e.Average);
            });

            // modelBuilder.Entity<Vehicle>().HasData(
            //     new Vehicle { Id = 1050, Make = "Tesla", Model = "T", Year = 210, VinNumber = "1233asd", MarketValue = 51 },
            //     new Vehicle { Id = 4022, Make = "Tesla", Model = "A", Year = 2200, VinNumber = "1223a2d", MarketValue = 25 },
            //     new Vehicle { Id = 4041, Make = "Tesla", Model = "B", Year = 21200, VinNumber = "12333asd", MarketValue = 35 },
            //     new Vehicle { Id = 1045, Make = "Toyota", Model = "Camry", Year = 21010, VinNumber = "123asd", MarketValue = 45 },
            //     new Vehicle { Id = 3141, Make = "Tesla", Model = "T", Year = 52100, VinNumber = "12gg3a234sd", MarketValue = 55 },
            //     new Vehicle { Id = 2021, Make = "Car", Model = "a", Year = 21020, VinNumber = "12d3asd", MarketValue = 52 },
            //     new Vehicle { Id = 2042, Make = "Ford", Model = "T", Year = 25100, VinNumber = "123fasd", MarketValue = 15 }
            // );
        }
    }
}