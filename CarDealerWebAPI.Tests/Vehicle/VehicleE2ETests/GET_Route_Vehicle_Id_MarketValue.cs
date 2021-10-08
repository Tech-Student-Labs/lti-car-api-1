using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleE2ETests
{
    public class GET_Route_Vehicle_Id_MarketValue
    {
        private readonly string adminToken =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiI5YjNiZGI4Ny1kZTQ3LTQxOGQtODg3ZS0zMzVkYTUzNTBmMWUiLCJyb2xlIjoiQWRtaW5Vc2VyIiwibmJmIjoxNjIzNzEwNDUzLCJleHAiOjE2MzIzNTA0NTMsImlhdCI6MTYyMzcxMDQ1MywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIn0.g11nmSnglviiN2H_zW5hOaNOnnMqwOVm_soOUcshlkM";

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
                services.AddDbContext<CarDealerContext>(options => options.UseInMemoryDatabase("ToDoGets"));
            });

        [Fact]
        public async Task Should_ReturnMarketValue_WhenEndpointIsVisited()
        {
            //GIVEN
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var todoService = testServer.Services.GetRequiredService<CarDealerContext>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            await todoService.Database.EnsureDeletedAsync();
            await todoService.Database.EnsureCreatedAsync();

            var vehicle = new Vehicle()
            { Make = "Toyota", Model = "Camry", Year = 2016, VinNumber = "abc123", MarketValue = 23000 };
            await todoService.VehicleInventory.AddAsync(vehicle);
            await todoService.SaveChangesAsync();
            //WHEN
            var result = await client.GetAsync("/Vehicle/MarketValue");

            //THEN
            var response = await result.Content.ReadAsStringAsync();
            List<int> vehicleJsonObj = JsonConvert.DeserializeObject<List<int>>(response);
            vehicleJsonObj?.Count.Should().Be(1);
            await todoService.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_Return200Status_WhenMarketValueEndpointIsVisited()
        {
            //GIVEN
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var todoService = testServer.Services.GetRequiredService<CarDealerContext>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            await todoService.Database.EnsureDeletedAsync();
            await todoService.Database.EnsureCreatedAsync();

            var vehicle = new Vehicle()
            { Make = "Toyota", Model = "Camry", Year = 2016, VinNumber = "abcd123", MarketValue = 23001 };
            await todoService.VehicleInventory.AddAsync(vehicle);
            await todoService.SaveChangesAsync();

            //WHEN
            var result = await client.GetAsync("/Vehicle/MarketValue");

            //THEN
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            await todoService.Database.EnsureDeletedAsync();
        }
    }
}