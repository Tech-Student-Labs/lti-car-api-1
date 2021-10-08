using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleSubmissionUnitTests
{
    public class VehicleSubmissionServiceAddVehicleSubmissionUnitTests
    {

        private Mock<IVehicleMarketValueService> marketMock = new Mock<IVehicleMarketValueService>();
        [Fact]
        public void AddVehicleSubmissionUnitTest_ShouldThrowArgumentException_WhenThereIsNoUser()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);


            marketMock.Setup(x => x.GetAverageVehiclePrice("jnksjnf")).ReturnsAsync("nice");
            //When
            var service = new VehicleSubmissionsService(databaseContext, marketMock.Object);
            var sub = new VehicleSubmissions() { TimeStamp = new DateTime(), UserId = "1" };
            Func<Task> action = async () => await service.AddVehicleSubmission(sub, 1461);
            //Then
            action.Should().Throw<ArgumentException>().WithMessage("User not found");
            databaseContext.Database.EnsureDeleted();
        }
        [Fact]
        public void AddVehicleSubmissionUnitTest_ShouldThrowArgumentException_WhenThereIsNoVehicle()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var user = new User { Id = "1", Email = "kevinsdf@yahoo.com", UserName = "jnkjkn" };
            databaseContext.UserTable.Add(user);
            databaseContext.SaveChanges();
            marketMock.Setup(x => x.GetAverageVehiclePrice("jnksjnf")).ReturnsAsync("nice");
            //When
            var service = new VehicleSubmissionsService(databaseContext, marketMock.Object);
            var sub = new VehicleSubmissions { TimeStamp = new DateTime(), UserId = "1" };
            Func<Task> action = async () => await service.AddVehicleSubmission(sub, 4984982);
            //Then
            action.Should().Throw<ArgumentException>().WithMessage("Vehicle not found");
            databaseContext.Database.EnsureDeleted();
        }
    }
}