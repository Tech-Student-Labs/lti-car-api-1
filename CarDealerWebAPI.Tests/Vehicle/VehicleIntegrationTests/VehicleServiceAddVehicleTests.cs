using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace CarDealerWebAPI.Tests
{
    public class VehicleServiceAddVehicleTests
    {
        [Fact]
        public void AddMethod_ShouldThrowError_WhenNullIsPassedAsAParameter()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);
            //When

            //Then
            Action action = () => vehicleInventoryService.AddVehicle(null);
            action.Should().Throw<System.ArgumentNullException>()
                .WithMessage("The vehicle you are trying to add is null (Parameter 'vehicle')");
        }

        [Fact]
        public void AddVehicle_ShouldAddVehicleToDatabase()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);
            //When
            var vehicle = new Vehicle()
            { Id = 1, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000 };
            vehicleInventoryService.AddVehicle(vehicle);
            var result = databaseContext.VehicleInventory.FirstOrDefault(s => s.Id == vehicle.Id);
            //Then
            result.Should().Be(vehicle);
        }
    }
}