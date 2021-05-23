using System;
using Xunit;
using CarDealerAPIService.App;
using FluentAssertions;

namespace CarDealerAPIService.Tests
{
    public class CarInventoryTest
    {
        // [Fact]
        // public void ShouldHaveAThumbnail()
        // {
        //     var car1 = new CarInventory();

        //     car1.carThumbnail = "prettyPicture.png";

        //     car1.carThumbnail.Should().Be("prettyPicture.png");
        // }

        [Fact]
        public void ShouldHaveAName()
        {
            var car1 = new CarInventory();

            car1.name = "howdy";

            car1.name.Should().Be("howdy");
        }

        [Fact]
        public void ShouldHaveAnExterior()
        {
            var car1 = new CarInventory();

            car1.exterior = "perly white";

            car1.exterior.Should().Be("perly white");
        }
        
        [Fact]
        public void ShouldHaveAnInterior()
        {
            var car1 = new CarInventory();

            car1.interior = "Black with leather seating to warm your butt";

            car1.interior.Should().Be("Black with leather seating to warm your butt");
        }
        
        // [Fact]
        // public void ShouldHaveADealerLocation()
        // {
        //     var car1 = new CarInventory();

        //     car1.dealerLocation = "dealer near me";

        //     car1.dealerLocation.Should().Be("dealer near me");
        // }
    }
}