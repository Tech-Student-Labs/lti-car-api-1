using System;
using System.Linq;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarDealerWebAPI.Tests
{
    public class VehicleServiceGetAllTests
    {
        [Fact]
        public void VehicleInventory_ShouldReturn0_When0ItemsInDatabase()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);
            //When
            var result = vehicleInventoryService.GetAllVehicles().Count;
            //Then
            result.Should().Be(0);
        }

        [Fact]
        public void VehicleInventory_ShouldReturn1_When1ItemInDatabase()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);
            //When
            databaseContext.VehicleInventory.Add(new Vehicle()
                {Id = 1, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000});
            databaseContext.SaveChanges();
            var result = vehicleInventoryService.GetAllVehicles().Count;
            //Then
            result.Should().Be(1);
        }

        [Fact]
        public void VehicleInventory_ShouldReturn5_When5ItemsExist()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);
            //When
            var vehicles = new Vehicle[]
            {
                new Vehicle()
                    {Id = 1, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000},
                new Vehicle()
                    {Id = 2, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000},
                new Vehicle()
                    {Id = 3, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000},
                new Vehicle()
                    {Id = 4, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000},
                new Vehicle()
                    {Id = 5, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000}
            };
            databaseContext.VehicleInventory.AddRange(vehicles);
            databaseContext.SaveChanges();
            var result = vehicleInventoryService.GetAllVehicles().Count;
            //Then
            result.Should().Be(5);
        }
    }
}