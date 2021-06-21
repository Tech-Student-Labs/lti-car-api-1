using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleE2ETests
{
    public class POST_Route_AddRangeOfVehicles
    {
        private readonly string token =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiI5YjNiZGI4Ny1kZTQ3LTQxOGQtODg3ZS0zMzVkYTUzNTBmMWUiLCJyb2xlIjoiQWRtaW5Vc2VyIiwibmJmIjoxNjIzNzEwNDUzLCJleHAiOjE2MzIzNTA0NTMsImlhdCI6MTYyMzcxMDQ1MywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIn0.g11nmSnglviiN2H_zW5hOaNOnnMqwOVm_soOUcshlkM";

        private IWebHostBuilder HostBuilder => new WebHostBuilder()
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>()
            .ConfigureServices(services =>
            {
                services.Remove(
                    services.SingleOrDefault(
                        s => s.ServiceType == typeof(DbContextOptions<CarDealerContext>))
                );
                services.AddDbContext<CarDealerContext>(options => options.UseInMemoryDatabase("PostVehicleRange"));
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                });
            });

        [Fact]
        public async Task AddRangeOfVehicles_ShouldAddThree_WhenThereIs3VehiclesInTheArray()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetService<CarDealerContext>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var vehicleArr = new Vehicle[]
            {
                new Vehicle
                {
                    Id = 1, VinNumber = "jksdnfsdf", Make = "toyota", MarketValue = 50000, Model = "camry", Year = 2000
                },
                new Vehicle
                {
                    Id = 2, VinNumber = "jksdnfsdsaf", Make = "toyota", MarketValue = 50000, Model = "camry",
                    Year = 2000
                },
                new Vehicle
                {
                    Id = 3, VinNumber = "jksdsdf", Make = "toyota", MarketValue = 50000, Model = "camry", Year = 2000
                },
            };
            //When
            var response = await client.PostAsJsonAsync("/Vehicle/Range", vehicleArr);
            //Then
            context.VehicleInventory.Count().Should().Be(3);
            context.Database.EnsureDeleted();
        }
        [Fact]
        public async Task AddRangeOfVehicles_ShouldAddNone_WhenNoVehicles()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetService<CarDealerContext>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var vehicleArr = new Vehicle[]
            {
            };
            //When
            var response = await client.PostAsJsonAsync("/Vehicle/Range", vehicleArr);
            //Then
            context.VehicleInventory.Count().Should().Be(0);
            context.Database.EnsureDeleted();
        }
        [Fact]
        public async Task AddRangeOfVehicles_ShouldAdd1_WhenThereIs1VehicleInTheArray()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetService<CarDealerContext>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var vehicleArr = new Vehicle[]
            {
                new Vehicle
                {
                    Id = 1, VinNumber = "jksdnfsdf", Make = "toyota", MarketValue = 50000, Model = "camry", Year = 2000
                }
            };
            //When
            var response = await client.PostAsJsonAsync("/Vehicle/Range", vehicleArr);
            //Then
            context.VehicleInventory.Count().Should().Be(1);
            context.Database.EnsureDeleted();
        }
    }
}