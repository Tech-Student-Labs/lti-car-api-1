using CarDealerAPIService.App.models;
using Microsoft.EntityFrameworkCore;

namespace CarDealerAPIService.App.Data
{
    public class CarDealerContext : DbContext
    {
        // TODO: add DbSet props here
        public DbSet<Vehicle> VehicleInventory { get; set; }

        public CarDealerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}