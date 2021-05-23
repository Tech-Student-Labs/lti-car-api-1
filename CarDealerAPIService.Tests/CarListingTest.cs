using CarDealerAPIService.App;
using Xunit;
using FluentAssertions;

namespace CarDealerAPIService.Tests
{
    public class CarListingTest
    {
        [Fact]
        public void TestName()
        {
            var car1 = new Car("Lincoln", "MKZ", "2021", "vin", 50000);
            var carListing1 = new CarListing("Lease the all new", car1, $"starting at {car1.GetValue()}");

            carListing1.BuildListing().Should().Be("Lease the all new 2021 Lincoln MKZ starting at $50,000");
        }
    }
}