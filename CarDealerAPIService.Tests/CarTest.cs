using System;
using Xunit;
using CarDealerAPIService.App;
using FluentAssertions;

namespace CarDealerAPIService.Tests
{
    public class CarTest
    {

        [Fact]
        public void ShouldHaveAttributesMakeModelYearVINValue()
        {
            var car1 = new Car("Chevy", "Spark", "2020", "vin", 10000);

            car1.Make.Should().Be("Chevy");
            car1.Model.Should().Be("Spark");
            car1.Year.Should().Be("2020");
            car1.VIN.Should().Be("vin");
            car1.GetValue().Should().Be("$10,000");

            car1.ToString().Should().Be("Make: Chevy\nModel: Spark\nYear: 2020\nVIN: vin\nValue: $10,000");
        }
    }
}