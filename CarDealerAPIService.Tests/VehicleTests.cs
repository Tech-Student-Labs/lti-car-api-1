using System;
using CarDealerAPIService.App;
using CarDealerAPIService.App.models;
using FluentAssertions;
using Xunit;

namespace CarDealerAPIService.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Initilization_ShouldBeDefined_WhenVehicleIsCreated()
        {
            Vehicle vehicle = new Vehicle();
            
            vehicle.Should().NotBeNull();
        }

        [Fact]
        public void Initilization_ShouldBeDefined_WhenVehiclePriceModelIsCreated()
        {
            //Given
            VehiclePrice vehiclePrice = new VehiclePrice();
            //When
            vehiclePrice.Should().NotBeNull();
            //Then

        }
    }
}