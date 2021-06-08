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
    public class VehicleServiceAddVehicleTests
    {
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
            var vehicle = new Vehicle() { Id = 1, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000 };
            vehicleInventoryService.AddVehicle(vehicle);
            var result = databaseContext.VehicleInventory.FirstOrDefault(s => s.Id == vehicle.Id);
            //Then
            result.Should().Be(vehicle);
        }
    }
}