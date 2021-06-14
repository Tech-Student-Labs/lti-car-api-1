using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleListingE2ETests
{
    public class GET_Route_GetAllVehicleListing
    {
        //Generated this token with an unlimited lifetime.
        private IWebHostBuilder HostBuilder => new WebHostBuilder()
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>()
            .ConfigureServices(services =>
            {
                services.Remove(
                    services.SingleOrDefault(
                        s => s.ServiceType == typeof(DbContextOptions<CarDealerContext>))
                );
                services.AddDbContext<CarDealerContext>(options => options.UseInMemoryDatabase("VehicleListing"));
                
            });

        [Fact]
        public async Task Should_Return200Status_WhenNoVehiclesExist()
        {
            //GIVEN the service is running and there are no items in the Vehicles Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            
            //WHEN a GET request is submitted to Vehicle with no parameters
            var result = await client.GetAsync("/VehicleListing");

            //THEN the response should return a OK status
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

          [Fact]
        public async Task Should_ReturnNoVehicleListings_WhenNoVehicleListingsExist()
        {
            //GIVEN the service is running and there are no items in the Vehicles Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            
            //WHEN a GET request is submitted to Vehicle with no parameters
            var response = await client.GetAsync("/VehicleListing");
            var JsonObject = await response.Content.ReadAsStringAsync();
            //THEN the response should return a OK status
            JsonObject.Should().Be("[]");
        }

          [Fact]
        public async Task Should_Return1VehicleListings_When1VehicleListingsExist()
        {
            //GIVEN the service is running and there are is 1 items in the Vehicles Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetRequiredService<CarDealerContext>();
            context.VehicleListings.Add(new VehicleListing{Id = 1, VehicleId = 1, Price = 23000});
            context.SaveChanges();
            //WHEN a GET request is submitted to Vehicle with no parameters
            var response = await client.GetAsync("/VehicleListing");
            var JsonObject = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<VehicleListing>>(JsonObject);
            //THEN the response should return a OK status
            result.Count.Should().Be(1);
            context.Database.EnsureDeleted();
        }
           [Fact]
        public async Task Should_Return2VehicleListings_When2VehicleListingsExist()
        {
            //GIVEN the service is running and there are is 1 items in the Vehicles Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetRequiredService<CarDealerContext>();
            context.VehicleListings.Add(new VehicleListing{Id = 1, VehicleId = 1, Price = 23000});
            context.VehicleListings.Add(new VehicleListing{Id = 2, VehicleId = 2, Price = 23000});

            context.SaveChanges();
            //WHEN a GET request is submitted to Vehicle with no parameters
            var response = await client.GetAsync("/VehicleListing");
            var JsonObject = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<VehicleListing>>(JsonObject);
            //THEN the response should return a OK status
            result.Count.Should().Be(2);
            context.Database.EnsureDeleted();

        }
    }
}