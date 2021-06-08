using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using CarDealerWebAPI.Controllers;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CarDealerWebAPI.services;
using System;

namespace CarDealerWebAPI.Tests.UnitTests
{
    public class VehicleServiceGetTests
    {
        [Fact]
        public void GetVehicle_ShouldReturnVehicle1_WhenId1IsPassed()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);
            //When
            var vehicle = new Vehicle() { Id = 1, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000 };
            databaseContext.VehicleInventory.Add(vehicle);
            databaseContext.SaveChanges();
            var result = vehicleInventoryService.GetVehicle(1);
            //Then
            result.Should().Be(vehicle);
        }

        [Fact]
        public void GetVehicle_ShouldThrowException_WhenAnIdNotInDatabaseIsPassed()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);
            //When
            var vehicle = new Vehicle() { Id = 1, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000 };
            databaseContext.VehicleInventory.Add(vehicle);
            databaseContext.SaveChanges();
            Action action = () => vehicleInventoryService.GetVehicle(-1);
            //Then
            action.Should().Throw<System.ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'Id not found in database.')");
        }
    }
}