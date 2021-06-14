using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace CarDealerWebAPI.Tests.VehicleE2ETests
{
    public class GET_Route_Vehicle_Id_MarketValue
    {
        private IWebHostBuilder HostBuilder => new WebHostBuilder()
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
            await todoService.Database.EnsureDeletedAsync();
            await todoService.Database.EnsureCreatedAsync();

            var vehicle = new Vehicle()
                {Make = "Toyota", Model = "Camry", Year = 2016, VinNumber = "abc123", MarketValue = 23000};
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
            await todoService.Database.EnsureDeletedAsync();
            await todoService.Database.EnsureCreatedAsync();

            var vehicle = new Vehicle()
                {Make = "Toyota", Model = "Camry", Year = 2016, VinNumber = "abcd123", MarketValue = 23001};
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