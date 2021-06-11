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
    public class DeleteVehicleSubmission_Tests
    {
        [Fact]
        public void DeleteVehicleSubmission_ShouldDeleteVehicle()
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
            submission.TimeStamp = new DateTime(1999, 1, 13, 3, 57, 32, 11);
            vehicleSubmissionsService.DeleteVehicleSubmission(submission);
            //Then
            var result = databaseContext.VehicleSubmissions.ToList().Count;
            result.Should().Be(0);
        }

        [Fact]
        public void DeleteVehicleSubmission_ShouldThrowException_WhenPassedNull()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleSubmissionsService = new VehicleSubmissionsService(databaseContext);
            //When
            Action action = () => vehicleSubmissionsService.DeleteVehicleSubmission(null);
            //Then
            action.Should().Throw<System.ArgumentNullException>();
        }
    }
}