using CarDealerWebAPI.services;
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

namespace CarDealerWebApi.Tests.UnitTests
{
    public class VehicleMarketValueServiceTests
    {
        // [Fact]
        // public void TestName()
        // {
        //     //Given
        //     var mockHandler = new Mock<HttpMessageHandler>();
        //     var httpMagic = new HttpClient(mockHandler.Object);
        //     var serviceMock = new Mock<IVehicleMarketValueService>();

        //     mockHandler
        //         .Protected()
        //         .Setup<Task<HttpResponseMessage>>(
        //             "SendAsync",
        //             ItExpr.IsAny<HttpRequestMessage>(),
        //             ItExpr.IsAny<CancellationToken>()
        //         )
        //         .ReturnsAsync(new HttpResponseMessage
        //         {
        //             StatusCode = HttpStatusCode.OK,
        //             Content = new StringContent(@"{""vin"": ""KL79MMS22MB176461"", ""success"": true, ""id"": ""2021_chevrolet_trailblazer_ls"", ""vehicle"": ""2021 Chevrolet Trailblazer LS"", ""mean"": 22937.96, ""stdev"": 1776, ""count"": 2912, ""mileage"": 548, ""certainty"": 99, ""period"": [""2021-05-02"", ""2021-06-05""], ""prices"": {""average"": 22937.96, ""below"": 21161.34, ""above"": 24714.58, ""distribution"": [{""group"": {""min"": 16043, ""max"": 20919, ""count"": 292}}, {""group"": {""min"": 20919, ""max"": 21668, ""count"": 291}}, {""group"": {""min"": 21668, ""max"": 22126, ""count"": 291}}, {""group"": {""min"": 22126, ""max"": 22521, ""count"": 291}}, {""group"": {""min"": 22521, ""max"": 22990, ""count"": 292}}, {""group"": {""min"": 22990, ""max"": 23290, ""count"": 291}}, {""group"": {""min"": 23290, ""max"": 23342, ""count"": 291}}, {""group"": {""min"": 23342, ""max"": 23950, ""count"": 291}}, {""group"": {""min"": 23950, ""max"": 25090, ""count"": 291}}, {""group"": {""min"": 25090, ""max"": 32999, ""count"": 291}}]}, ""adjustments"": {""mileage"": {""average"": 548.9, ""input"": 548.9, ""adjustment"": 0}}}")
        //         })
        //         .Verifiable();

        //     var httpClient = new HttpClient(mockHandler.Object)
        //     {
        //         BaseAddress = new Uri("http://marketvalue.vinaudit.com/"),
        //     };

        //     var _SUT = new VehicleMarketValueService(httpClient);
        //     var mockSut = new Mock<IVehicleMarketValueService>();

        //     //When
        //     var result = _SUT
        //         .GetAsync("getmarketvalue.php?key=VA_DEMO_KEY&vin=KL79MMS22MB176461&format=json&period=90&mileage=average");

        //     result.Should().NotBeNull();
        //     result.Id.Should().Be(1);

        //     //Then
        //     var expectedUri = new Uri("http://marketvalue.vinaudit.com/getmarketvalue.php?key=VA_DEMO_KEY&vin=KL79MMS22MB176461&format=json&period=90&mileage=average");

        //     mockHandler.Protected().Verify(
        //        "SendAsync",
        //        Times.Exactly(1), // we expected a single external request
        //        ItExpr.Is<HttpRequestMessage>(req =>
        //           req.Method == HttpMethod.Get  // we expected a GET request
        //           && req.RequestUri == expectedUri // to this uri
        //        ),
        //        ItExpr.IsAny<CancellationToken>()
        //     );
        // }

        [Fact]
        public async Task TestName()
        {
            //Given
            var mock = new Mock<IHttpClient>();
            var response = new HttpResponseMessage();
            mock.Setup(s => s.GetAsync("hello")).ReturnsAsync(response);
            var service = new VehicleMarketValueService(mock.Object);

            //When
            var result = await service.GetAverageVehiclePrice("KL79MMS22MB176461");

            //Then
            mock.Verify(mock => mock.GetAsync("hello"), Times.Once);
        }
    }
}