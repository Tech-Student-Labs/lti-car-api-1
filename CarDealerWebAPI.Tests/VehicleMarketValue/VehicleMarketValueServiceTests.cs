using Moq;
using Xunit;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using Moq.Protected;
using System.Threading;
using System.Net;
using Microsoft.Extensions.Logging;
using System;
using CarDealerAPIService.services;

namespace CarDealerWebAPI.Tests.UnitTests
{
    public class VehicleMarketValueServiceTests
    {
        [Fact]
#pragma warning disable 4014
        public void GetAsync_ShouldBeCalledOnce_WhenGetAverageVehiclePriceIsCalled()
        {
            //Given
            var mock = new Mock<IHttpClient>();
            var response = new HttpResponseMessage();
            mock.Setup(s =>
                    s.GetAsync(
                        "http://marketvalue.vinaudit.com/getmarketvalue.php?key=VA_DEMO_KEY&vin=KL79MMS22MB176461&format=json&period=90&mileage=average"))
                .ReturnsAsync(response);
            var service = new VehicleMarketValueService(mock.Object);

            //When
            service.GetAverageVehiclePrice("KL79MMS22MB176461");

            //Then
            mock.Verify(
                mock => mock.GetAsync(
                    "http://marketvalue.vinaudit.com/getmarketvalue.php?key=VA_DEMO_KEY&vin=KL79MMS22MB176461&format=json&period=90&mileage=average"),
                Times.Once);
        }
    }
}