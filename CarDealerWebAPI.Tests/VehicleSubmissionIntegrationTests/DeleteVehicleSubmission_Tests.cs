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
        public void UpdateVehicleSubmission_ShouldUpdateVehicle()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleSubmissionsService = new VehicleSubmissionsService(databaseContext);
            //When
            var submission = new VehicleSubmissions() {
                User = new User() {Id="abc123"},
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = new Vehicle() {Make="Toyota", Model="Highlander", Year=1994, VinNumber="abc"}
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
        public void TestName()
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