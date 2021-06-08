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
    public class VehicleServiceTests
    {
        [Fact]
        public void VehicleInventory_ShouldReturn0_When0ItemsInDatabase()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            //When
            var result = databaseContext.VehicleInventory.ToList().Count;
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
            vehicleInventoryService.AddVehicle(new Vehicle() { Id = 1, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000 });
            databaseContext.SaveChanges();
            var result = databaseContext.VehicleInventory.ToList().Count;
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
            var vehicles = new Vehicle[] {
                new Vehicle() { Id = 1, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000 },
                new Vehicle() { Id = 2, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000 },
                new Vehicle() { Id = 3, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000 },
                new Vehicle() { Id = 4, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000 },
                new Vehicle() { Id = 5, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000 }
            };
            vehicleInventoryService.AddRangeOfVehicles(vehicles);
            var result = databaseContext.VehicleInventory.ToList().Count;
            //Then
            result.Should().Be(5);
        }

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
            vehicleInventoryService.AddVehicle(vehicle);
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
            vehicleInventoryService.AddVehicle(vehicle);
            databaseContext.SaveChanges();
            Action action = () => vehicleInventoryService.GetVehicle(-1);
            //Then
            action.Should().Throw<System.Exception>();
        }

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
            action.Should().Throw<System.Exception>();
        }

        [Fact]
        public void Database_Can_Be_Destroyed()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);
            //When
            var vehicles = new Vehicle[] {
                new Vehicle() { Id = 1, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000 }
            };
            vehicleInventoryService.AddRangeOfVehicles(vehicles);
            vehicleInventoryService.DestroyDatabase();
            var result = databaseContext.VehicleInventory.ToList().Count;
            //Then
            result.Should().Be(0);
        }

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
            var vehicle = new Vehicle() { Id = 1, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000 };
            vehicleInventoryService.AddVehicle(vehicle);
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
            var vehicle = new Vehicle() { Id = 1, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000 };
            vehicleInventoryService.AddVehicle(vehicle);
            vehicle.Year = 2023;
            Action action = () => vehicleInventoryService.UpdateVehicle(null);
            //Then
            action.Should().Throw<System.Exception>();
        }

        [Fact]
        public void Vehicle_Can_Be_Deleted()
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
            var result = databaseContext.VehicleInventory.ToList().Count;
            result.Should().Be(1);
            vehicleInventoryService.DeleteVehicle(vehicle);
            result = databaseContext.VehicleInventory.ToList().Count;
            //Then
            result.Should().Be(0);
        }

        [Fact]
        public void DeleteVehicle_Should_Throw_Exception_When_Null_Is_Passed()
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
            Action action = () => vehicleInventoryService.DeleteVehicle(null);
            //Then
            action.Should().Throw<System.Exception>();
        }
    }
} 