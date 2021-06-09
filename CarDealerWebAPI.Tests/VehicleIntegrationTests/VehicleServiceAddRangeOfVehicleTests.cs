using System;
using System.Linq;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarDealerWebApi.Tests
{
    public class VehicleServiceAddRangeOfVehicleTests
    {
        [Fact]
        public void AddVehicleRange_ShouldReturnThrowArgumentNullException_WhenVehicleListIsNull()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);
            //When

            Action action = () => vehicleInventoryService.AddRangeOfVehicles(null);
            //Then
            action.Should().Throw<ArgumentNullException>()
                .WithMessage("The vehicle you are trying to add is null (Parameter 'vehicles')");
        }

        [Fact]
        public void AddVehicleRange_ShouldReturnCountZero_WhenVehicleListContainsZeroVehicles()
        {
            //Action action = () => vehicleInventoryService.AddVehicle(null);
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);
            //When
            databaseContext.VehicleInventory.ToList().Count.Should().Be(0);

            var vehicle = new Vehicle[] { };
            vehicleInventoryService.AddRangeOfVehicles(vehicle);
            //Then
            databaseContext.VehicleInventory.ToList().Count.Should().Be(0);
        }

        [Fact]
        public void AddVehicleRange_ShouldReturnCountOne_WhenVehicleListContainsOneVehicles()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);
            //When
            databaseContext.VehicleInventory.ToList().Count.Should().Be(0);

            var vehicle = new Vehicle[]
            {
                new Vehicle()
                {
                    Id = 1, Make = "Tesla1", Model = "XXX1", Year = 2021, VinNumber = "abcxyz1231", MarketValue = 23001
                }
            };
            vehicleInventoryService.AddRangeOfVehicles(vehicle);
            //Then
            databaseContext.VehicleInventory.ToList().Count.Should().Be(1);
        }

        [Fact]
        public void AddVehicleRange_ShouldReturnCount2_WhenVehicleListContainsTwoVehicles()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);
            //When
            databaseContext.VehicleInventory.ToList().Count.Should().Be(0);

            var vehicle = new Vehicle[]
            {
                new Vehicle()
                {
                    Id = 1, Make = "Tesla1", Model = "XXX1", Year = 2021, VinNumber = "abcxyz1231", MarketValue = 23001
                },
                new Vehicle()
                {
                    Id = 2, Make = "Tesla2", Model = "XXX2", Year = 2022, VinNumber = "abcxyz1232", MarketValue = 23002
                }
            };
            vehicleInventoryService.AddRangeOfVehicles(vehicle);
            //Then
            databaseContext.VehicleInventory.ToList().Count.Should().Be(2);
        }
    }
}