using System;
using System.Linq;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerWebAPI.services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarDealerWebApi.Tests
{
    public class VehicleServiceUpdateTests
    {
        [Fact]
        public void Vehicle_Can_Be_Updated()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);
            //When
            var vehicle = new Vehicle()
                {Id = 1, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000};
            databaseContext.VehicleInventory.Add(vehicle);
            databaseContext.SaveChanges();
            vehicle.Year = 2023;
            vehicleInventoryService.UpdateVehicle(vehicle);
            var result = databaseContext.VehicleInventory.ToList()[0].Year;
            //Then
            result.Should().Be(2023);
        }

        [Fact]
        public void Update_Throws_Exception_When_Null_Is_Passed()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);
            //When
            Action action = () => vehicleInventoryService.UpdateVehicle(null);
            //Then
            action.Should().Throw<System.ArgumentNullException>()
                .WithMessage("The vehicle you are trying to update is null (Parameter 'vehicle')");
        }
    }
}