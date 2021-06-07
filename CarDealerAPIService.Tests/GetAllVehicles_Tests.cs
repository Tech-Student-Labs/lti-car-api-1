using CarDealerAPIService.App;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarDealerAPIService.Tests
{
    public class GetAllVehicles_Tests
    {
        
        [Fact]
        public void GetAllVehicles_ShouldHaveCount0_When0ItemInDatabase()
        {
            //Given we have a vehicle database
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: "CarDealer").Options;
            //When 0 item in database
            using (var context = new CarDealerContext(options))
            {
                context.Database.EnsureCreated();
                var service = new VehicleService(context);
                var result = service.GetAllVehicles();
                //Then count should be 0
                result.Count.Should().Be(0);
                context.Database.EnsureDeleted();
            }

        }
        [Fact]
        public void GetAllVehicles_ShouldHaveCount1_When1ItemInDatabase()
        {
            //Given we have a vehicle database
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: "CarDealer").Options;
            //When 1 item in database
            using (var context = new CarDealerContext(options))
            {
                context.Database.EnsureCreated();
                context.VehicleInventory.Add(new Vehicle());
                context.SaveChanges();
            }
            
            using (var context = new CarDealerContext(options))
            {
                var service = new VehicleService(context);
                var result = service.GetAllVehicles();
                //Then count should be 1
                result.Count.Should().Be(1);
                context.Database.EnsureDeleted();
            }
            
        }
        
        [Fact]
        public void GetAllVehicles_ShouldHaveCount2_When2ItemInDatabase()
        {
            //Given we have a vehicle database
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: "CarDealer").Options;
            //When 2 item in database
            using (var context = new CarDealerContext(options))
            {
                context.Database.EnsureCreated();
                context.VehicleInventory.Add(new Vehicle());
                context.VehicleInventory.Add(new Vehicle());
                context.SaveChanges();
            }
            
            using (var context = new CarDealerContext(options))
            {
                var service = new VehicleService(context);
                var result = service.GetAllVehicles();
                //Then count should be 2
                result.Count.Should().Be(2);
                context.Database.EnsureDeleted();
            }

        }
    }
}