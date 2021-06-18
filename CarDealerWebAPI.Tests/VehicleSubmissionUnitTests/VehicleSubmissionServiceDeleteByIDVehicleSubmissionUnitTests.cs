using System;
using System.Linq;
using System.Threading.Tasks;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Moq;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleSubmissionUnitTests
{
    public class VehicleSubmissionServiceDeleteByIDVehicleSubmissionUnitTests
    {
        private Mock<IVehicleMarketValueService> mockMarketValueService = new Mock<IVehicleMarketValueService>();
        [Fact]
        public async Task VehicleSubmissionDeleteByID_ShouldDeleteSubmissionAndVehicle_WhenCalled()
        {
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleSubmissionsService =
                new VehicleSubmissionsService(databaseContext, mockMarketValueService.Object);
            //When
            var vehicle = new Vehicle() { Id = 1,Make = "Toyota",Model = "camry",VinNumber = "WDBUF83J75X173935",Year = 1997};
            var user = new User {Id = "abc123", Email = "jnskjdfnsf@yahoo.com", UserName = "jksndfjsndf",FirstName = "kevin",LastName = "huynh",PasswordHash = "sjkdfsf"};
            var submission = new VehicleSubmissions()
            {
                UserId = "abc123",
                TimeStamp = new DateTime(12, 12, 12),
                VehicleId = 1,
                Vehicle = vehicle
            };
            await databaseContext.VehicleInventory.AddAsync(vehicle);
            await databaseContext.UserTable.AddAsync(user);
            await databaseContext.SaveChangesAsync();
            await vehicleSubmissionsService.AddVehicleSubmission(submission,51519);
            databaseContext.VehicleSubmissions.Count().Should().Be(1);
            
            var subId = databaseContext.VehicleSubmissions.FirstOrDefault(x => true).Id;
            vehicleSubmissionsService.DeleteVehicleSubmissionById(subId);
            //Then
            databaseContext.VehicleSubmissions.ToList().Count.Should().Be(0);
        }
        
        [Fact]
        public async Task VehicleSubmissionDeleteByID_ShouldThrowError_WhenCalledWithNoUser()
        {
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleSubmissionsService =
                new VehicleSubmissionsService(databaseContext, mockMarketValueService.Object);
            //When
            var vehicle = new Vehicle() { Id = 1,Make = "Toyota",Model = "camry",VinNumber = "WDBUF83J75X173935",Year = 1997};
            var user = new User {Id = "abc123", Email = "jnskjdfnsf@yahoo.com", UserName = "jksndfjsndf",FirstName = "kevin",LastName = "huynh",PasswordHash = "sjkdfsf"};
            var submission = new VehicleSubmissions()
            {
                UserId = "abc123",
                TimeStamp = new DateTime(12, 12, 12),
                VehicleId = 1,
                Vehicle = vehicle
            };
            await databaseContext.VehicleInventory.AddAsync(vehicle);
            await databaseContext.UserTable.AddAsync(user);
            await databaseContext.SaveChangesAsync();
            await vehicleSubmissionsService.AddVehicleSubmission(submission,51519);
            databaseContext.VehicleSubmissions.Count().Should().Be(1);
            var deleteUser = databaseContext.UserTable.FirstOrDefault(x => true);
            var subId = databaseContext.VehicleSubmissions.FirstOrDefault(x => true).Id;
            Action action = () => vehicleSubmissionsService.DeleteVehicleSubmissionById("1sdf");
            //Then
            action.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}