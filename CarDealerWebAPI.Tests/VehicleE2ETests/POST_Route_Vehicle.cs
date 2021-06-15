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
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiJmOGFmN2RiNS1kODI5LTQzYzktYmYyYS1jNmIxZGYwZjgwNzkiLCJyb2xlIjoiUmVndWxhclVzZXIiLCJuYmYiOjE2MjM2NzU4MTQsImV4cCI6MTYyMzc2MjIxNCwiaWF0IjoxNjIzNjc1ODE0LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAifQ.lVO0GK80VdCTuMHO2qrUA98jmhQj12_hhU6Xqqm42cc";
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
           // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //When
            var response = await client.PostAsJsonAsync("/Vehicle", new Vehicle{Id = 1, VinNumber = "jnjknf",Make = "Toyoya",MarketValue = 12331,Model = "camry",Year = 1997});
            //Then
            service?.VehicleInventory.Count().Should().Be(1);
        }
    }
}