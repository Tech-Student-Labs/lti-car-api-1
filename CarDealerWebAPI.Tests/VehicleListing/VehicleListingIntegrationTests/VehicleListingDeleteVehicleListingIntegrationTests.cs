using System;
using System.Linq;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleListingIntegrationTests
{
    public class VehicleListingDeleteVehicleListingIntegrationTests
    {
        
        [Fact]
        public void DeleteVehicleListings_ShouldDeleteNothing_WhenNoVehicleListingsExists()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new CarDealerContext(options);
            var vehicleListingService = new VehicleListingService(dbContext);
            //When
            Action action = () => vehicleListingService.DeleteVehicleListings("4T1BF3EK5BU638805");
            //Then
            action.Should().Throw<InvalidOperationException>().WithMessage("Cannot Delete Null Vehicle Listings");
            dbContext.VehicleListings.Count().Should().Be(0);
        }
        [Fact]
        public void DeleteVehicleListings_ShouldDeleteVehicleListings_WhenVehicleListingsExists()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new CarDealerContext(options);
            var vehicleListingService = new VehicleListingService(dbContext);
            var vehicle = new Vehicle {Id = 1, Make = "Toyota",MarketValue = 20000,Model = "Camry",VinNumber = "4T1BF3EK5BU638805",Year = 2020};
            var vehicleListing = new VehicleListing { Price = 50000,Vehicle = vehicle};
            dbContext.VehicleListings.Add(vehicleListing);
            dbContext.SaveChanges();
            dbContext.VehicleListings.Count().Should().Be(1);
            //When
            vehicleListingService.DeleteVehicleListings("4T1BF3EK5BU638805");
            //Then
            dbContext.VehicleListings.Count().Should().Be(0);
        }
        
        [Fact]
        public void DeleteVehicleListings_ShouldDeleteOneVehicleListings_WhenVehicleMultipleListingsExists()
        {
            //Given
            var options = new DbContextOptionsBuilder<CarDealerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new CarDealerContext(options);
            var vehicleListingService = new VehicleListingService(dbContext);
            var vehicle = new Vehicle {Id = 1, Make = "Toyota",MarketValue = 20000,Model = "Camry",VinNumber = "4T1BF3EK5BU638805",Year = 2020};
            var vehicle1 = new Vehicle {Id = 2, Make = "Toyota",MarketValue = 20000,Model = "Camry",VinNumber = "1GNEK13Z93R293940",Year = 2020};
            var vehicleListing = new VehicleListing { Price = 50000,Vehicle = vehicle};
            var vehicleListing1 = new VehicleListing { Price = 50000,Vehicle = vehicle1};
            dbContext.VehicleListings.Add(vehicleListing);
            dbContext.VehicleListings.Add(vehicleListing1);
            dbContext.SaveChanges();
            dbContext.VehicleListings.Count().Should().Be(2);
            //When
            vehicleListingService.DeleteVehicleListings("4T1BF3EK5BU638805");
            //Then
            dbContext.VehicleListings.Count().Should().Be(1);
        }
        
        
    }
}