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
    public class GetVehicleSubmission_Tests
    {
        [Fact]
        public void GetAllSubmissions_ShouldReturn0_WhenListIsEmpty()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleSubmissionsService = new VehicleSubmissionsService(databaseContext);
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
            var vehicleSubmissionsService = new VehicleSubmissionsService(databaseContext);
            //When
            var submission = new VehicleSubmissions() {
                UserId = "abc123",
                TimeStamp = new DateTime(12, 12, 12),
                VehicleId = 1
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
            var vehicleSubmissionsService = new VehicleSubmissionsService(databaseContext);
            //When
            User MyUser = new User() {Id="abc123"};
            var submission1 = new VehicleSubmissions() {
                UserId = MyUser.Id,
                TimeStamp = new DateTime(12, 12, 12),
                VehicleId = 1
            };
            var submission2 = new VehicleSubmissions() {
                UserId = MyUser.Id,
                TimeStamp = new DateTime(12, 12, 12),
                VehicleId = 2
            };
            var submission3 = new VehicleSubmissions() {
                UserId = MyUser.Id,
                TimeStamp = new DateTime(12, 12, 12),
                VehicleId = 3
            };
            databaseContext.VehicleSubmissions.Add(submission1);
            databaseContext.VehicleSubmissions.Add(submission2);
            databaseContext.VehicleSubmissions.Add(submission3);
            databaseContext.SaveChanges();
            var result = vehicleSubmissionsService.GetAllVehicleSubmissionsByUser("abc123");
            //Then
            result.Count.Should().Be(3);
            result[0].Vehicle.Make.Should().Be("Toyota");
        }
    }
}