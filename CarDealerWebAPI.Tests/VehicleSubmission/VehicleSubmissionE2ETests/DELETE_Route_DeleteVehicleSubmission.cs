using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.Exception.ExceptionModel;
using CarDealerAPIService.App.models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleSubmissionE2ETests
{
    public class Delete_Route_DeleteVehicleSubmission
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
                services.AddDbContext<CarDealerContext>(options =>
                    options.UseInMemoryDatabase("GetRouteSubmittedVehicle"));
            });

        [Fact]
        public async Task DeleteVehicleSubmission_ShouldThrowException_WhenVINDoesntMatchARecord()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var carDealerContext = testServer.Services.GetRequiredService<CarDealerContext>();
            //setup
            await client.PostAsJsonAsync("/Roles/Create", "");
            var vin = "JF1GH6B60BG810286";
            //When
            var response = await client.DeleteAsync($"/VehicleSubmissions/{vin}");
            var jsonObj = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ErrorDetails>(jsonObj);
            //Then
            result.Message.Should().Be("delete vehicle submission is null ");
        }

        [Fact]
        public async Task DeleteVehicleSubmission_ShouldDelete_WhenVINDoesMatchARecord()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var carDealerContext = testServer.Services.GetRequiredService<CarDealerContext>();
            //setup
            await client.PostAsJsonAsync("/Roles/Create", "");
            var vin = "JF1GH6B60BG810286";
            var user = new User { Id = "1", UserName = "tester", Email = "tester@yahoo.com" };
            var vehicle = new Vehicle
            { VinNumber = vin, Id = 1, Make = "toyoya", MarketValue = 7000, Model = "camry", Year = 2020 };
            var vehicleSub = new VehicleSubmissions { Vehicle = vehicle, UserId = "1" };
            carDealerContext.UserTable.Add(user);
            carDealerContext.VehicleInventory.Add(vehicle);
            carDealerContext.VehicleSubmissions.Add(vehicleSub);
            carDealerContext.SaveChanges();
            carDealerContext.VehicleSubmissions.Count().Should().Be(1);
            //When
            var response = await client.DeleteAsync($"/VehicleSubmissions/{vin}");
            //Then
            carDealerContext.VehicleSubmissions.Count().Should().Be(0);
        }
    }
}