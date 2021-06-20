using System;
using System.Linq;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleListingIntegrationTests
{
    public class VehicleListingAddToVehicleListingIntegrationTests
    {
        [Fact]
        public void AddToVehicleListings_ShouldThrowArgumentNullException_WhenPassedANullValue()
        {

            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new CarDealerContext(options);
            var vehicleListingService = new VehicleListingService(dbContext);
            //When
            Action action = () => vehicleListingService.AddToVehicleListing(null);
            //Then
            action.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void AddToVehicleListings_ShouldAddTodbContext_WhenPassedVehicleListing()
        {

            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new CarDealerContext(options);
            var vehicleListingService = new VehicleListingService(dbContext);
            var vehicle = new Vehicle {Make = "toyoya",MarketValue = 20000,Model = "camry",VinNumber = "jksndfjsnfjnui32",Year = 2020};
            dbContext.VehicleInventory.Add(vehicle);
            dbContext.SaveChanges();
            var vehicleId = dbContext.VehicleInventory.FirstOrDefault(x => x.VinNumber == "jksndfjsnfjnui32").Id;
            var vehicleListing = new VehicleListing() { VehicleId = vehicleId,Vehicle = vehicle};
            //When
            vehicleListingService.AddToVehicleListing(vehicleListing);
            //Then
            dbContext.VehicleListings.Count().Should().Be(1);
        }
        
    }
}