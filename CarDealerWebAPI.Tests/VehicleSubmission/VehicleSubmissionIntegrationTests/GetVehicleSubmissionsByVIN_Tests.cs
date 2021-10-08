using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleSubmissionIntegrationTests
{
    public class GetVehicleSubmissionsByVIN_Tests
    {
        private readonly Mock<IVehicleMarketValueService> mockMarketValueService =
            new Mock<IVehicleMarketValueService>();

        [Fact]
        public void GetVehicleSubmissionByVin_ShouldReturnVehicleSubmission_WhenCalled()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleSubmissionsService =
                new VehicleSubmissionsService(databaseContext, mockMarketValueService.Object);
            var user = new User
            { Id = "1", Email = "KevinHuynh@yahoo.com", UserName = "hahahaha", FirstName = "ha", LastName = "Ha" };
            databaseContext.UserTable.Add(user);
            //setup Vehicles
            var vehicle = new Vehicle
            { Id = 2, Make = "toyoya", MarketValue = 12313, Model = "camry", VinNumber = "1GCCT19X738198141", Year = 1997 };
            databaseContext.VehicleInventory.Add(vehicle);
            databaseContext.VehicleSubmissions.Add(new VehicleSubmissions { UserId = "1", VehicleId = 2 });
            databaseContext.SaveChanges();
            //When
            var response = vehicleSubmissionsService.GetVehicleSubmissionsByVIN("1GCCT19X738198141");
            //Then
            response.Vehicle.Should().Be(vehicle);
            databaseContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetVehicleSubmissionByVin_ShouldReturnNull_WhenVehicleDoesntExist()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleSubmissionsService =
                new VehicleSubmissionsService(databaseContext, mockMarketValueService.Object);
            var user = new User
            { Id = "1", Email = "KevinHuynh@yahoo.com", UserName = "hahahaha", FirstName = "ha", LastName = "Ha" };
            databaseContext.UserTable.Add(user);
            databaseContext.SaveChanges();
            //When
            Action action = () => vehicleSubmissionsService.GetVehicleSubmissionsByVIN("1GCCT19X738198141");
            //Then
            action.Should().Throw<NullReferenceException>();
            databaseContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetVehicleSubmissionByVin_ShouldReturnNull_WhenUserDoesntExist()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CarDealerContext(options);
            var vehicleSubmissionsService =
                new VehicleSubmissionsService(databaseContext, mockMarketValueService.Object);
            var vehicle = new Vehicle
            {
                Id = 1,
                Make = "testMake",
                MarketValue = 10000,
                Model = "TestModel",
                VinNumber = "1GCCT19X738198141",
                Year = 2020
            };
            databaseContext.VehicleSubmissions.Add(new VehicleSubmissions { UserId = "12", VehicleId = 2, Vehicle = vehicle });
            databaseContext.SaveChanges();
            //When
            Action action = () => vehicleSubmissionsService.GetVehicleSubmissionsByVIN("1GCCT19X738198141");
            //Then
            action.Should().Throw<NullReferenceException>();
            databaseContext.Database.EnsureDeleted();
        }
    }
}