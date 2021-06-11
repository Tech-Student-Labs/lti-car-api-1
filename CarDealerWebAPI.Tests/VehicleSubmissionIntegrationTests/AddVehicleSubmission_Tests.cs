using System.Collections.Generic;
using System.Linq;
using System;
using Xunit;
using FluentAssertions;
using CarDealerAPIService.services;
using Microsoft.EntityFrameworkCore;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using Moq;
using System.Threading.Tasks;

namespace CarDealerWebAPI.Tests.VehicleSubmissionIntegrationTests
{
    public class AddVehicleSubmission_Tests
    { 
        private readonly Mock<IVehicleMarketValueService> mockMarketValueService = new Mock<IVehicleMarketValueService>();
        [Fact]
        public async Task AddVehicleSubmission_ShouldIncreaseListCountTo1_WhenValidInfoIsPassed()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            mockMarketValueService.Setup(x => x.GetAverageVehiclePrice("abc123xyzz")).ReturnsAsync("123");
            var vehicleSubmissionsService = new VehicleSubmissionsService(databaseContext, mockMarketValueService.Object);
            //When
            User user = new User() {Id = "abc123"};
            Vehicle vehicle = new Vehicle() {Make = "Toyota", Model = "Camry", Year = 1994, VinNumber = "abc123xyzz"};
            databaseContext.UserTable.Add(user);
            databaseContext.VehicleInventory.Add(vehicle);
            databaseContext.SaveChanges();
            var submission = new VehicleSubmissions() {
                UserId = user.Id,
                User = user,
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = vehicle,
                VehicleId = vehicle.Id
            };
            await vehicleSubmissionsService.AddVehicleSubmission(submission);
            var result = vehicleSubmissionsService.GetAllVehicleSubmissionsByUser("abc123").Count;
            //Then
            result.Should().Be(1);
            databaseContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AddVehicleSubmission_ShouldIncreaseListCountTo3_WhenInvoked3Times()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            mockMarketValueService.Setup(x => x.GetAverageVehiclePrice("abc1213xyzz")).ReturnsAsync("123");
            mockMarketValueService.Setup(x => x.GetAverageVehiclePrice("abc2123xyzz")).ReturnsAsync("123");
            mockMarketValueService.Setup(x => x.GetAverageVehiclePrice("abc123xyz3z")).ReturnsAsync("123");
            var vehicleSubmissionsService = new VehicleSubmissionsService(databaseContext, mockMarketValueService.Object);
            //When
            User user = new User() {Id = "abc123"};
            Vehicle vehicle1 = new Vehicle() {Id = 1, Make = "Toyota", Model = "Camry", Year = 1994, VinNumber = "abc1213xyzz"};
            Vehicle vehicle2 = new Vehicle() {Id = 2, Make = "Toyota", Model = "Camry", Year = 1994, VinNumber = "abc2123xyzz"};
            Vehicle vehicle3 = new Vehicle() {Id = 3, Make = "Toyota", Model = "Camry", Year = 1994, VinNumber = "abc123xyz3z"};
            databaseContext.UserTable.Add(user);
            databaseContext.VehicleInventory.Add(vehicle1);
            databaseContext.VehicleInventory.Add(vehicle2);
            databaseContext.VehicleInventory.Add(vehicle3);
            databaseContext.SaveChanges();
            var submission1 = new VehicleSubmissions() {
                UserId = user.Id,
                User = user,
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = vehicle1,
                VehicleId = 1
            };
            var submission2 = new VehicleSubmissions() {
                UserId = user.Id,
                User = user,
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = vehicle2,
                VehicleId = 2
            };
            var submission3 = new VehicleSubmissions() {
                UserId = user.Id,
                User = user,
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = vehicle3,
                VehicleId = 3
            };
            await vehicleSubmissionsService.AddVehicleSubmission(submission1);
            await vehicleSubmissionsService.AddVehicleSubmission(submission2);
            await vehicleSubmissionsService.AddVehicleSubmission(submission3);
            var result = vehicleSubmissionsService.GetAllVehicleSubmissionsByUser("abc123").Count;
            //Then
            result.Should().Be(3);
            databaseContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task DuplicateVehicleIdShouldThrowException()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            mockMarketValueService.Setup(x => x.GetAverageVehiclePrice("abc1213xyzz")).ReturnsAsync("123");
            mockMarketValueService.Setup(x => x.GetAverageVehiclePrice("abc2123xyzz")).ReturnsAsync("123");
            var vehicleSubmissionsService = new VehicleSubmissionsService(databaseContext, mockMarketValueService.Object);
            //When
            User user = new User() {Id = "abc123"};
            Vehicle vehicle1 = new Vehicle() {Make = "Toyota", Model = "Camry", Year = 1994, VinNumber = "abc1213xyzz" };
            Vehicle vehicle2 = new Vehicle() {Make = "Toyota", Model = "Camry", Year = 1994, VinNumber = "abc2123xyzz" };
            await databaseContext.UserTable.AddAsync(user);
            await databaseContext.VehicleInventory.AddAsync(vehicle1);
            await databaseContext.VehicleInventory.AddAsync(vehicle2);
            await databaseContext.SaveChangesAsync();
            var submission1 = new VehicleSubmissions()
            {
                UserId = user.Id,
                User = user,
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = vehicle1,
                VehicleId = 1
            };
            var submission2 = new VehicleSubmissions()
            {
                UserId = user.Id,
                User = user,
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = vehicle2,
                VehicleId = 2
            };
            var submission3 = new VehicleSubmissions()
            {
                UserId = user.Id,
                User = user,
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = vehicle2,
                VehicleId = 2
            };
            await vehicleSubmissionsService.AddVehicleSubmission(submission1);
            await vehicleSubmissionsService.AddVehicleSubmission(submission2);
            Func<Task> action = async () =>
            {
                await vehicleSubmissionsService.AddVehicleSubmission(submission3);
            };
            //Then
            action.Should().Throw<System.ArgumentException>();
            databaseContext.Database.EnsureDeleted();
        }
    }
}