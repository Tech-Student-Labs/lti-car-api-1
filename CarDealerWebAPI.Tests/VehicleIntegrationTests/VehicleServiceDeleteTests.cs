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
    public class VehicleServiceDeleteTests
    {
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
            var vehicles = new Vehicle[]
            {
                new Vehicle()
                    {Id = 1, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000}
            };
            databaseContext.VehicleInventory.AddRange(vehicles);
            databaseContext.SaveChanges();
            vehicleInventoryService.DestroyDatabase();
            var result = databaseContext.VehicleInventory.ToList().Count;
            //Then
            result.Should().Be(0);
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
            var vehicle = new Vehicle()
                {Id = 1, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000};
            databaseContext.VehicleInventory.Add(vehicle);
            databaseContext.SaveChanges();
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
            var vehicle = new Vehicle()
                {Id = 1, Make = "Tesla", Model = "XXX", Year = 2022, VinNumber = "abcxyz123", MarketValue = 23000};
            databaseContext.VehicleInventory.Add(vehicle);
            databaseContext.SaveChanges();
            Action action = () => vehicleInventoryService.DeleteVehicle(null);
            //Then
            action.Should().Throw<System.ArgumentNullException>()
                .WithMessage("The vehicle you are trying to delete is null (Parameter 'vehicle')");
        }

        [Fact]
        public void DeleteVehicleById_CanDeleteAVehicle()
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
            vehicleInventoryService.DeleteVehicleById(1);
            //Then
            databaseContext.VehicleInventory.ToList().Count.Should().Be(0);
        }

        [Fact]
        public void DeleteVehicleById_ShouldThrowException_WhenPassedOutOfRangeValue()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleInventoryService = new VehicleService(databaseContext);
            //When
            Action action = () => vehicleInventoryService.DeleteVehicleById(-1);
            //Then
            action.Should().Throw<System.ArgumentOutOfRangeException>();
        }
    }
}