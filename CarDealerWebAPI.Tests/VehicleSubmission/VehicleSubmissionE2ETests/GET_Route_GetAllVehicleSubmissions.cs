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
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleSubmissionE2ETests
{
    public class GET_Route_GetAllVehicleSubmissions
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
        public async Task GET_ShouldReturnZeroVehicleSubmission_WhenCalled()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var carDealerContext = testServer.Services.GetRequiredService<CarDealerContext>();
            //setup
            await client.PostAsJsonAsync("/Roles/Create", "");
            //setup user
            var response = await client.GetAsync("/VehicleSubmissions");
            //When
            var jsonObj = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<VehicleSubmissionsDTO>>(jsonObj);
            //Then
            result.Count().Should().Be(0);
            carDealerContext.Database.EnsureDeleted();
        }
        [Fact]
        public async Task GET_ShouldReturnOneVehicleSubmission_WhenCalled()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var carDealerContext = testServer.Services.GetRequiredService<CarDealerContext>();
            //setup
            await client.PostAsJsonAsync("/Roles/Create", "");
            //setup user
            var user = new User
            { Email = "KevinHuynh@yahoo.com", UserName = "hahahaha", FirstName = "ha", LastName = "Ha" };
            carDealerContext.UserTable.Add(user);
            //setup Vehicles
            carDealerContext.SaveChanges();
            var id = carDealerContext.UserTable.FirstOrDefault(x => true)?.Id;
            //1GCCT19X738198141
            var vehicles = new Vehicle
            { Make = "toyota", MarketValue = 12313, Model = "camry", VinNumber = "1GCCT19X738198141", Year = 1997 };
            //Call VehicleSubmissionController            
            var postResponse1 = await client.PostAsJsonAsync("/VehicleSubmissions", new VehicleSubmissions { UserId = id, Vehicle = vehicles });
            //When
            var response = await client.GetAsync("/VehicleSubmissions");
            //When
            var jsonObj = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<VehicleSubmissionsDTO>>(jsonObj);

            //Then
            result.Count().Should().Be(1);
            carDealerContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GET_ShouldReturnTwoVehicleSubmission_WhenCalled()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var carDealerContext = testServer.Services.GetRequiredService<CarDealerContext>();
            //setup
            await client.PostAsJsonAsync("/Roles/Create", "");
            //setup user
            var user = new User
            { Email = "KevinHuynh@yahoo.com", UserName = "hahahaha", FirstName = "ha", LastName = "Ha" };
            carDealerContext.UserTable.Add(user);
            //setup Vehicles
            carDealerContext.SaveChanges();
            var id = carDealerContext.UserTable.FirstOrDefault(x => true)?.Id;
            //1GCCT19X738198141
            var vehicles = new Vehicle
            { Make = "toyota", MarketValue = 12313, Model = "camry", VinNumber = "1GCCT19X738198141", Year = 1997 };
            var vehicles2 = new Vehicle()
            { Make = "toyota", MarketValue = 12313, Model = "camry", VinNumber = "2C4RDGCG5DR520736", Year = 1997 };
            //Call VehicleSubmissionController            
            await client.PostAsJsonAsync("/VehicleSubmissions", new VehicleSubmissions { UserId = id, Vehicle = vehicles });
            await client.PostAsJsonAsync("/VehicleSubmissions", new VehicleSubmissions { UserId = id, Vehicle = vehicles2 });
            //When
            var response = await client.GetAsync("/VehicleSubmissions");
            //When
            var jsonObj = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<VehicleSubmissionsDTO>>(jsonObj);

            //Then
            result.Count().Should().Be(2);
            carDealerContext.Database.EnsureDeleted();
        }

    }
}