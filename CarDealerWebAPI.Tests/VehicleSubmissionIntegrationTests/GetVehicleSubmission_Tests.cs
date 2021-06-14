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

namespace CarDealerWebAPI.Tests.VehicleSubmissionIntegrationTests
{
    public class GetVehicleSubmission_Tests
    {
        private readonly Mock<IVehicleMarketValueService> mockMarketValueService =
            new Mock<IVehicleMarketValueService>();

        [Fact]
        public void GetAllSubmissions_ShouldReturn0_WhenListIsEmpty()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleSubmissionsService =
                new VehicleSubmissionsService(databaseContext, mockMarketValueService.Object);
            //When
            var result = vehicleSubmissionsService.GetAllVehicleSubmissionsByUser("abc123").Count;
            //Then
            result.Should().Be(0);
        }

        [Fact]
        public void GetAllSubmissions_ShouldReturn1_WhenListHas1Element()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleSubmissionsService =
                new VehicleSubmissionsService(databaseContext, mockMarketValueService.Object);
            //When
            User MyUser = new User() {Id = "abc123", Email = "string@string.com", PasswordHash = "xxxxxx"};
            Vehicle vehicle = new Vehicle() {Id = 1};
            databaseContext.UserTable.Add(MyUser);
            databaseContext.VehicleInventory.Add(vehicle);
            var submission = new VehicleSubmissions()
            {
                User = MyUser,
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = vehicle
            };
            databaseContext.VehicleSubmissions.Add(submission);
            databaseContext.SaveChanges();
            var result = vehicleSubmissionsService.GetAllVehicleSubmissionsByUser("abc123").Count;
            //Then
            result.Should().Be(1);
        }

        [Fact]
        public void GetAllSubmissions_ShouldReturn3_WhenListHas3Elements()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleSubmissionsService =
                new VehicleSubmissionsService(databaseContext, mockMarketValueService.Object);
            //When
            User MyUser = new User() {Id = "abc123"};
            Vehicle vehicle1 = new Vehicle() {Id = 1, Make = "Toyota"};
            Vehicle vehicle2 = new Vehicle() {Id = 2, Make = "Chevy"};
            Vehicle vehicle3 = new Vehicle() {Id = 3, Make = "SUV"};
            var submission1 = new VehicleSubmissions()
            {
                User = MyUser,
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = vehicle1
            };
            var submission2 = new VehicleSubmissions()
            {
                User = MyUser,
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = vehicle2
            };
            var submission3 = new VehicleSubmissions()
            {
                User = MyUser,
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = vehicle3
            };
            databaseContext.VehicleSubmissions.Add(submission1);
            databaseContext.VehicleSubmissions.Add(submission2);
            databaseContext.VehicleSubmissions.Add(submission3);
            databaseContext.SaveChanges();
            var result = vehicleSubmissionsService.GetAllVehicleSubmissionsByUser("abc123");
            //Then
            result.Count.Should().Be(3);
        }
    }
}