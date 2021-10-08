using CarDealerAPIService.App.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleMarketValue.VehicleMarketValueE2ETests
{
    public class GET_Route_VehicleMarketValue
    {
        private static readonly IConfigurationBuilder builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        private readonly IConfiguration config = builder.Build();
        private IWebHostBuilder HostBuilder => new WebHostBuilder()
            .UseConfiguration(config)
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>()
            .ConfigureServices(services =>
            {
                services.Remove(
                    services.SingleOrDefault(
                        s => s.ServiceType == typeof(DbContextOptions<CarDealerContext>))
                );
            });

        [Fact]
        public async Task VehiclePricingController_ShouldReturnValue_WhenCalled()
        {
            //Given
            var server = new TestServer(HostBuilder);
            var client = server.CreateClient();
            //When
            var response = await client.GetAsync("VehiclePricing/KL79MMS22MB176461");
            var jsonObj = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject(jsonObj);

            //Then
            result.Should().NotBeNull();
        }
    }
}