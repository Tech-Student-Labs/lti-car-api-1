using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleListingE2ETests
{
    public class DELETE_Route_DeleteAVehicleListing
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
                    options.UseInMemoryDatabase("DeleteSubmittedVehicleListing"));

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                });
            });

        [Fact]
        public async Task DELETE_ShouldDeleteZeroListing_WhenThereIsNotMatchingVIN()
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
            await client.PostAsJsonAsync("/VehicleListing", new VehicleListing { Vehicle = vehicles, Price = 12000});
            dbContext.VehicleListings.Count().Should().Be(1);
            var vin = "JH4KA4540JC050162";
            //when
            await client.DeleteAsync($"VehicleListing/{vin}");
            dbContext.VehicleListings.Count().Should().Be(1);
            dbContext.Database.EnsureDeleted();
        }
        [Fact]
        public async Task DELETE_ShouldDeleteOneListing_WhenThereIsAMatchingVIN()
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
            await client.PostAsJsonAsync("/VehicleListing", new VehicleListing { Vehicle = vehicles, Price = 12000});
            dbContext.VehicleListings.Count().Should().Be(1);
            var vin = "1GCCT19X738198141";
            //when
            await client.DeleteAsync($"VehicleListing/{vin}");
            dbContext.VehicleListings.Count().Should().Be(0);
            dbContext.Database.EnsureDeleted();
        }
        
        [Fact]
        public async Task DELETE_ShouldDeleteOneListing_WhenThereIsAMatchingVINAndMultipleListings()
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
                { Make = "toyoya", MarketValue = 12313, Model = "camry", VinNumber = "JH4DA3340HS032394", Year = 1997 };
            await client.PostAsJsonAsync("/VehicleListing", new VehicleListing { Vehicle = vehicles, Price = 12000});
            await client.PostAsJsonAsync("/VehicleListing", new VehicleListing { Vehicle = vehicles1, Price = 20000});
            dbContext.VehicleListings.Count().Should().Be(2);
            var vin = "1GCCT19X738198141";
            //when
            await client.DeleteAsync($"VehicleListing/{vin}");
            //then
            dbContext.VehicleListings.Count().Should().Be(1);
            dbContext.Database.EnsureDeleted();
        }
        
    }
}