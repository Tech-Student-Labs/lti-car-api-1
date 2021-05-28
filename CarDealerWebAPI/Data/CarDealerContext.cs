using Microsoft.EntityFrameworkCore;
using CarDealerAPIService.App.models;

namespace CarDealerWebAPI.Data
{
    public class CarDealerContext : DbContext
    {
        // TODO: add DbSet props here
        public DbSet<Vehicle> VehicleInventory { get; set; }

        // public CarDealerContext(DbContextOptions options) : base(options)
        // {
            
        // }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}