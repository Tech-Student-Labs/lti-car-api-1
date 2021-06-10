using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using CarDealerWebApi.Controllers;
using CarDealerApiService.App.Data;
using CarDealerApiService.App.models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using CarDealerApiService.services;

namespace CarDealerWebApi.Tests.VehicleIntegrationTests
{
    public class VehicleServiceGetMarketValue
    {
        [Fact]
        public void Should_ReturnMarketValue_WhenPassedId()
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
            var result = vehicleInventoryService.GetMarketValues().Count;

            //Then
            result.Should().Be(1);
        }

        [Fact]
        public void Should_ReturnMarketValue_WhenPassedIdForMultipleVehicle()
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
            var vehicle2 = new Vehicle()
                {Id = 2, Make = "Tesla", Model = "XXX", Year = 2023, VinNumber = "abcxyz123", MarketValue = 23001};
            databaseContext.VehicleInventory.Add(vehicle);
            databaseContext.VehicleInventory.Add(vehicle2);
            databaseContext.SaveChanges();
            var result = vehicleInventoryService.GetMarketValues().Count;

            //Then
            result.Should().Be(2);
        }

        [Fact]
        public void Should_ReturnErrorForMarketValue_WhenInvalidIdIsPassed()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);

            //When
            var result = vehicleInventoryService.GetMarketValues().Count;

            //Then
            result.Should().Be(0);
        }
    }
}