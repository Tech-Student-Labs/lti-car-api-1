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
            var submission = new VehicleSubmissions() {
                User = new User() {Id="abc123"},
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = new Vehicle() {Make="Toyota", Model="Highlander", Year=1994, VinNumber="abc"}
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
            User MyUser = new User() {Id="abc123"};
            var submission1 = new VehicleSubmissions() {
                User = MyUser,
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = new Vehicle() {Make="Toyota", Model="Highlander", Year=1994, VinNumber="abc"}
            };
            var submission2 = new VehicleSubmissions() {
                User = MyUser,
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = new Vehicle() {Make="Toyota", Model="Highlander", Year=1994, VinNumber="abc"}
            };
            var submission3 = new VehicleSubmissions() {
                User = MyUser,
                TimeStamp = new DateTime(12, 12, 12),
                Vehicle = new Vehicle() {Make="Toyota", Model="Highlander", Year=1994, VinNumber="abc"}
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