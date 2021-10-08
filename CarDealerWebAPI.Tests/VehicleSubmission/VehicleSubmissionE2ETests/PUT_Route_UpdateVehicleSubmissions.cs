using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleSubmissionE2ETests
{
    public class PUT_Route_UpdateVehicleSubmissions
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
                    options.UseInMemoryDatabase("PUTSubmiltteddVehicles"));
            });

        [Fact]
        public async Task UpdateVehicleSubmission_ShouldUpdateTheYear_WhenPassedTheVehicleSubmission()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var dbContext = testServer.Services.GetRequiredService<CarDealerContext>();
            //setup roles
            dbContext.Database.EnsureDeleted();
            await client.PostAsJsonAsync("/Roles/Create", "");
            //setup user
            var user = new User
            { Email = "KevinHuynh@yahoo.com", UserName = "hahahaha", FirstName = "ha", LastName = "Ha" };
            dbContext.UserTable.Add(user);
            //setup Vehicles
            dbContext.SaveChanges();
            var id = dbContext.UserTable.FirstOrDefault(x => true)?.Id;
            //1GCCT19X738198141
            var vehicles = new Vehicle
            { Make = "toyota", MarketValue = 12313, Model = "camry", VinNumber = "1GCCT19X738198141", Year = 1997 };
            var vehicles1 = vehicles;
            vehicles1.Year = 2020;
            //When
            await client.PostAsJsonAsync("/VehicleSubmissions", new VehicleSubmissions { UserId = id, Vehicle = vehicles });
            dbContext.VehicleSubmissions.Count().Should().Be(1);
            var response = await client.PutAsJsonAsync("/VehicleSubmissions", new VehicleSubmissions { UserId = id, Vehicle = vehicles1 });
            var jsonObj = await response.Content.ReadAsStringAsync();

            //then
            jsonObj.Should().Contain("Vehicle submission updated");
            var vehicleSub = dbContext.VehicleSubmissions.Include(x => x.Vehicle).FirstOrDefault(x => x.Vehicle.Make == "toyota");
            vehicleSub.Vehicle.Year.Should().Be(2020);
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task UpdateVehicleSubmission_ShouldUpdateTheMake_WhenPassedTheVehicleSubmission()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var dbContext = testServer.Services.GetRequiredService<CarDealerContext>();
            //setup roles
            dbContext.Database.EnsureDeleted();
            await client.PostAsJsonAsync("/Roles/Create", "");
            //setup user
            var user = new User
            { Email = "KevinHuynh@yahoo.com", UserName = "hahahaha", FirstName = "ha", LastName = "Ha" };
            dbContext.UserTable.Add(user);
            //setup Vehicles
            dbContext.SaveChanges();
            var id = dbContext.UserTable.FirstOrDefault(x => true)?.Id;
            //1GCCT19X738198141
            var vehicles = new Vehicle
            { Make = "toyota", MarketValue = 12313, Model = "camry", VinNumber = "1GCCT19X738198141", Year = 1997 };
            var vehicles1 = vehicles;
            vehicles1.Make = "Tesla";
            //When
            await client.PostAsJsonAsync("/VehicleSubmissions", new VehicleSubmissions { UserId = id, Vehicle = vehicles });
            dbContext.VehicleSubmissions.Count().Should().Be(1);
            var response = await client.PutAsJsonAsync("/VehicleSubmissions", new VehicleSubmissions { UserId = id, Vehicle = vehicles1 });
            var jsonObj = await response.Content.ReadAsStringAsync();

            //then
            jsonObj.Should().Contain("Vehicle submission updated");
            var vehicleSub = dbContext.VehicleSubmissions.Include(x => x.Vehicle).FirstOrDefault(x => x.Vehicle.MarketValue == 12313);
            vehicleSub.Vehicle.Make.Should().Be("Tesla");
            dbContext.Database.EnsureDeleted();
        }
    }
}