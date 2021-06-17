using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleListingE2ETests
{
    public class GET_Route_GetAllVehicleListing
    {
        private readonly string adminToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiI5YjNiZGI4Ny1kZTQ3LTQxOGQtODg3ZS0zMzVkYTUzNTBmMWUiLCJyb2xlIjoiQWRtaW5Vc2VyIiwibmJmIjoxNjIzNzEwNDUzLCJleHAiOjE2MzIzNTA0NTMsImlhdCI6MTYyMzcxMDQ1MywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIn0.g11nmSnglviiN2H_zW5hOaNOnnMqwOVm_soOUcshlkM";
        //Generated this token with an unlimited lifetime.
        private IWebHostBuilder HostBuilder => new WebHostBuilder()
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>()
            .ConfigureServices(services =>
            {
                services.Remove(
                    services.SingleOrDefault(
                        s => s.ServiceType == typeof(DbContextOptions<CarDealerContext>))
                );
                services.AddDbContext<CarDealerContext>(options => options.UseInMemoryDatabase("VehicleListings"));
                
            });

        [Fact]
        public async Task Should_Return200Status_WhenNoVehiclesExist()
        {
            //GIVEN the service is running and there are no items in the Vehicles Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
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
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            //WHEN a GET request is submitted to Vehicle with no parameters
            var response = await client.GetAsync("/VehicleListing");
            var JsonObject = await response.Content.ReadAsStringAsync();
            //THEN the response should return a OK status
            JsonObject.Should().Be("[]");
        }

          [Fact]
        public async Task Should_Return1VehicleListings_When1VehicleListingsExist()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var dbContext = testServer.Services.GetRequiredService<CarDealerContext>();
            //setup roles
            dbContext.Database.EnsureDeleted();
            await client.PostAsJsonAsync("/Roles/Create", "");
            
            //setup Vehicles
            var vehicles = new Vehicle
                { Make = "toyoya", MarketValue = 12313, Model = "camry", VinNumber = "1GCCT19X738198141", Year = 1997 };
            dbContext.Add(vehicles);
            dbContext.SaveChanges();

            //When
            //Call VehicleListingsController            
            var response = await client.PostAsJsonAsync("/VehicleListing", new VehicleListing { VehicleId = 1, Price = 12000});
            //added a vehicle Listings 
            dbContext.VehicleListings.Count().Should().Be(1);
            var response1 = await client.GetAsync("/VehicleListing");
            var jsonObj = await response1.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<VehicleListing>>(jsonObj);
            result.Count().Should().Be(1);
        }
           [Fact]
        public async Task Should_Return2VehicleListings_When2VehicleListingsExist()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var dbContext = testServer.Services.GetRequiredService<CarDealerContext>();
            //setup roles
            dbContext.Database.EnsureDeleted();
            await client.PostAsJsonAsync("/Roles/Create", "");
            
            //setup Vehicles
            var vehicles = new Vehicle
                { Make = "toyoya", MarketValue = 12313, Model = "camry", VinNumber = "1GCCT19X738198141", Year = 1997 };
            var vehicles1 = new Vehicle
                { Make = "toyoya", MarketValue = 12313243, Model = "camry", VinNumber = "4T4BF3EK3AR045559", Year = 1997 };
            dbContext.Add(vehicles);
            dbContext.Add(vehicles1);
            dbContext.SaveChanges();

            //When
            //Call VehicleListingsController            
             await client.PostAsJsonAsync("/VehicleListing", new VehicleListing { VehicleId = 1, Price = 12000});
             await client.PostAsJsonAsync("/VehicleListing", new VehicleListing { VehicleId = 2, Price = 12000});
            //added a vehicle Listings 
            dbContext.VehicleListings.Count().Should().Be(2);
            var response1 = await client.GetAsync("/VehicleListing");
            var jsonObj = await response1.Content.ReadAsStringAsync();
            List<VehicleListing> result = JsonConvert.DeserializeObject<List<VehicleListing>>(jsonObj);
            result.Count.Should().Be(2);
            dbContext.Database.EnsureDeleted();
        }
    }
}