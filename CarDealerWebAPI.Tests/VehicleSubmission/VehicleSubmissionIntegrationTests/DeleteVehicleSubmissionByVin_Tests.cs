using System;
using System.Linq;
using System.Threading.Tasks;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleSubmissionIntegrationTests
{
    public class DeleteVehicleSubmissionByVin_Tests
    {
        private readonly Mock<IVehicleMarketValueService> mockMarketValueService =
            new Mock<IVehicleMarketValueService>();

        [Fact]
        public void DeleteVehicleSubmissionByVIN_ShouldThrowException_WhenCalledWithoutVIN()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            //When
            var service = new VehicleSubmissionsService(databaseContext,mockMarketValueService.Object);
            Action action = () => service.DeleteVehicleSubmissionByVIN(null);
            //Then
            action.Should().Throw<ArgumentNullException>();
        }
        [Fact]
        public void DeleteVehicleSubmissionByVIN_ShouldThrowException_WhenCalledWithAVINThatDoesntExist()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            //When
            var service = new VehicleSubmissionsService(databaseContext,mockMarketValueService.Object);
            Action action = () => service.DeleteVehicleSubmissionByVIN("4T1BF3EK5BU638805");
            //Then
            action.Should().Throw<InvalidOperationException>().WithMessage("delete vehicle submission is null ");
        }
        
        [Fact]
        public async Task DeleteVehicleSubmissionByVIN_ShouldDeleteVehicleWithVin_WhenCalledWithAVINThatDoesExists()
        {
               //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            mockMarketValueService.Setup(x => x.GetAverageVehiclePrice("abc1213xyzz")).ReturnsAsync("123");
            mockMarketValueService.Setup(x => x.GetAverageVehiclePrice("abc2123xyzz")).ReturnsAsync("123");
            mockMarketValueService.Setup(x => x.GetAverageVehiclePrice("abc123xyz3z")).ReturnsAsync("123");
            var vehicleSubmissionsService =
                new VehicleSubmissionsService(databaseContext, mockMarketValueService.Object);
            //When
            User user = new User() {Id = "abc123"};
            Vehicle vehicle1 = new Vehicle()
                {Id = 1, Make = "Toyota", Model = "Camry", Year = 1994, VinNumber = "abc1213xyzz"};
            Vehicle vehicle2 = new Vehicle()
                {Id = 2, Make = "Toyota", Model = "Camry", Year = 1994, VinNumber = "abc2123xyzz"};
            Vehicle vehicle3 = new Vehicle()
                {Id = 3, Make = "Toyota", Model = "Camry", Year = 1994, VinNumber = "abc123xyz3z"};
            databaseContext.UserTable.Add(user);
            databaseContext.VehicleInventory.Add(vehicle1);
            databaseContext.VehicleInventory.Add(vehicle2);
            databaseContext.VehicleInventory.Add(vehicle3);
            databaseContext.SaveChanges();
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
                Vehicle = vehicle3,
                VehicleId = 3
            };
            await vehicleSubmissionsService.AddVehicleSubmission(submission1,74524);
            await vehicleSubmissionsService.AddVehicleSubmission(submission2,12345);
            await vehicleSubmissionsService.AddVehicleSubmission(submission3,75465);
            databaseContext.VehicleSubmissions.Count().Should().Be(3);
            vehicleSubmissionsService.DeleteVehicleSubmissionByVIN("abc1213xyzz");
            //then
            databaseContext.VehicleSubmissions.Count().Should().Be(2);
        }
    }
}