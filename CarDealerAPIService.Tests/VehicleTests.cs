using CarDealerAPIService.App.models;
using FluentAssertions;
using Xunit;

namespace CarDealerAPIService.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Initialization_ShouldBeDefined_WhenVehicleIsCreated()
        {
            var vehicle = new Vehicle();

            vehicle.Should().NotBeNull();
        }

        [Fact]
        public void Initialization_ShouldBeDefined_WhenVehiclePriceModelIsCreated()
        {
            //Given
            var vehiclePrice = new VehiclePrice();
            //When
            vehiclePrice.Should().NotBeNull();
            //Then
        }
    }
}