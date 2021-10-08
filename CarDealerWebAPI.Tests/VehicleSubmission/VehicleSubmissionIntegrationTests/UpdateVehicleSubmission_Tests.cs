using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleSubmissionIntegrationTests
{
    public class UpdateVehicleSubmission_Tests
    {
        private readonly Mock<IVehicleMarketValueService> mockMarketValueService =
            new Mock<IVehicleMarketValueService>();

        [Fact]
        public void UpdateVehicleSubmission_ShouldUpdateVehicle()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleSubmissionsService =
                new VehicleSubmissionsService(databaseContext, mockMarketValueService.Object);
            //When
            var submission = new VehicleSubmissions()
            {
                UserId = "abc123",
                TimeStamp = new DateTime(12, 12, 12),
                VehicleId = 1
            };
            databaseContext.VehicleSubmissions.Add(submission);
            databaseContext.SaveChanges();
            submission.TimeStamp = new DateTime(1999, 1, 13, 3, 57, 32, 11);
            vehicleSubmissionsService.UpdateVehicleSubmission(submission);
            //Then
            var result = databaseContext.VehicleSubmissions.ToList()[0].TimeStamp;
            result.Should().Be(new DateTime(1999, 1, 13, 3, 57, 32, 11));
        }

        [Fact]
        public void UpdateVehicleSubmission_ShouldThrowException_WhenPassedNull()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleSubmissionsService =
                new VehicleSubmissionsService(databaseContext, mockMarketValueService.Object);
            //When
            Action action = () => vehicleSubmissionsService.UpdateVehicleSubmission(null);
            //Then
            action.Should().Throw<System.ArgumentNullException>();
        }
    }
}