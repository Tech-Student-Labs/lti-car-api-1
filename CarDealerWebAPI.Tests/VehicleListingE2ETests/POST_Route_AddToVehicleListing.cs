using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.Exception.ExceptionModel;
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
    public class POST_Route_AddToVehicleListing
    {
        private readonly string adminToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiI5YjNiZGI4Ny1kZTQ3LTQxOGQtODg3ZS0zMzVkYTUzNTBmMWUiLCJyb2xlIjoiQWRtaW5Vc2VyIiwibmJmIjoxNjIzNzEwNDUzLCJleHAiOjE2MzIzNTA0NTMsImlhdCI6MTYyMzcxMDQ1MywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIn0.g11nmSnglviiN2H_zW5hOaNOnnMqwOVm_soOUcshlkM";

        private IWebHostBuilder HostBuilder => new WebHostBuilder()
        .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>()
        .ConfigureServices(services =>
        {
            services.Remove(
                services.SingleOrDefault(
                    s => s.ServiceType == typeof(DbContextOptions<CarDealerContext>))
            );
            services.AddDbContext<CarDealerContext>(options =>
                options.UseInMemoryDatabase("AddSubmittedVehicleListing"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Test";
                options.DefaultChallengeScheme = "Test";
            });
        });


        [Fact]
        public async Task PostVehicleListing_ShouldAddVehicleToVehicleListings_WhenTheEndpointIsHit()
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
            var jsonObj = await response.Content.ReadAsStringAsync();
            //Then
            dbContext.VehicleListings.ToList().Count.Should().Be(1);
            dbContext.Database.EnsureDeleted();
        }
    }
}