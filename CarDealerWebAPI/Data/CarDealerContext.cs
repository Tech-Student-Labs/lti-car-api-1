using Microsoft.EntityFrameworkCore;

namespace CarDealerWebAPI.Data
{
    public class CarDealerContext : DbContext
    {
        // TODO: add DbSet props here


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