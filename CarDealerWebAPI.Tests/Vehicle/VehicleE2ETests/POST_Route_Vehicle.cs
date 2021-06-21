using System.IO;
using System.Linq;
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
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleE2ETests
{
    public class POST_Route_Vehicle
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
                services.AddDbContext<CarDealerContext>(options => options.UseInMemoryDatabase("PostVehicle"));
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                });
            });

        [Fact]
        public async Task Post_ShouldAddVehicleToDB_WhenCalled()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var service = testServer.Services.GetService<CarDealerContext>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //When
            var response = await client.PostAsJsonAsync("/Vehicle", new Vehicle{Id = 1, VinNumber = "jnjknf",Make = "Toyoya",MarketValue = 12331,Model = "camry",Year = 1997});
            //Then
            service?.VehicleInventory.Count().Should().Be(1);
            service.Database.EnsureDeleted();
        }
        [Fact]
        public async Task Post_ShouldAddVehicleToDbTwice_WhenCalledTwice()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var service = testServer.Services.GetService<CarDealerContext>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //When
            var response = await client.PostAsJsonAsync("/Vehicle", new Vehicle{Id = 1, VinNumber = "jnjknf",Make = "Toyoya",MarketValue = 12331,Model = "camry",Year = 1997});
            var response1 = await client.PostAsJsonAsync("/Vehicle", new Vehicle{Id = 2, VinNumber = "jnjknf",Make = "Toyoya",MarketValue = 12331,Model = "camry",Year = 1997});
            //Then
            service?.VehicleInventory.Count().Should().Be(2);
            service.Database.EnsureDeleted();
        }
    }
}