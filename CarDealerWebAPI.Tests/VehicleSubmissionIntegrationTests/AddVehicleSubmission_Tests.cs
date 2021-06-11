using System.Collections.Generic;
using System.Linq;
using System;
using Xunit;
using FluentAssertions;
using CarDealerAPIService.services;
using Microsoft.EntityFrameworkCore;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;

namespace CarDealerWebAPI.Tests.VehicleSubmissionIntegrationTests
{
    public class AddVehicleSubmission_Tests
    {
        [Fact]
        public void AddVehicleSubmission_ShouldIncreaseListCountTo1_WhenValidInfoIsPassed()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleSubmissionsService = new VehicleSubmissionsService(databaseContext);
            //When
            User user = new User() {Id = "abc123"};
            Vehicle vehicle = new Vehicle() {Make = "Toyota", Model = "Camry", Year = 1994, VinNumber = "abc123xyzz"};
            databaseContext.UserTable.Add(user);
            databaseContext.VehicleInventory.Add(vehicle);
            databaseContext.SaveChanges();
            var submission = new VehicleSubmissions() {
                UserId = user.Id,
                TimeStamp = new DateTime(12, 12, 12),
                VehicleId = 1
            };
            vehicleSubmissionsService.AddVehicleSubmission(submission);
            var result = vehicleSubmissionsService.GetAllVehicleSubmissionsByUser("abc123").Count;
            //Then
            result.Should().Be(1);
        }

        [Fact]
        public void AddVehicleSubmission_ShouldIncreaseListCountTo3_WhenInvoked3Times()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleSubmissionsService = new VehicleSubmissionsService(databaseContext);
            //When
            User user = new User() {Id = "abc123"};
            Vehicle vehicle1 = new Vehicle() {Make = "Toyota", Model = "Camry", Year = 1994, VinNumber = "abc123xyzz"};
            Vehicle vehicle2 = new Vehicle() {Make = "Toyota", Model = "Camry", Year = 1994, VinNumber = "abc123xyzz"};
            Vehicle vehicle3 = new Vehicle() {Make = "Toyota", Model = "Camry", Year = 1994, VinNumber = "abc123xyzz"};
            databaseContext.UserTable.Add(user);
            databaseContext.VehicleInventory.Add(vehicle1);
            databaseContext.VehicleInventory.Add(vehicle2);
            databaseContext.VehicleInventory.Add(vehicle3);
            databaseContext.SaveChanges();
            var submission1 = new VehicleSubmissions() {
                UserId = user.Id,
                TimeStamp = new DateTime(12, 12, 12),
                VehicleId = 1
            };
            var submission2 = new VehicleSubmissions() {
                UserId = user.Id,
                TimeStamp = new DateTime(12, 12, 12),
                VehicleId = 2
            };
            var submission3 = new VehicleSubmissions() {
                UserId = user.Id,
                TimeStamp = new DateTime(12, 12, 12),
                VehicleId = 3
            };
            vehicleSubmissionsService.AddVehicleSubmission(submission1);
            vehicleSubmissionsService.AddVehicleSubmission(submission2);
            vehicleSubmissionsService.AddVehicleSubmission(submission3);
            var result = vehicleSubmissionsService.GetAllVehicleSubmissionsByUser("abc123").Count;
            //Then
            result.Should().Be(3);
        }
    }
}