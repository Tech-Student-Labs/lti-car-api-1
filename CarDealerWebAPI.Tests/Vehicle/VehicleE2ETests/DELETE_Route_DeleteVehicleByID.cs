using System;
using System.IO;
using System.Linq;
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
    public class DELETE_Route_DeleteVehicle
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
                services.AddDbContext<CarDealerContext>(options => options.UseInMemoryDatabase("DeleteVehicleByID"));
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                });
            });

        [Fact]
        public async Task DeleteVehicleByID_ShouldDeleteAVehicleWithID1_WhenAVehicleWithId1Exists()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetService<CarDealerContext>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var vehicle = new Vehicle
                {Id = 1, VinNumber = "jnjknf", Make = "Toyota", MarketValue = 12331, Model = "camry", Year = 1997};
            context.VehicleInventory.Add(vehicle);
            context.SaveChanges();
            context.VehicleInventory.Count().Should().Be(1);
            //When
            await client.DeleteAsync("/Vehicle/1");
            //Then
            context.VehicleInventory.Count().Should().Be(0);
            context.Database.EnsureDeleted();
        }
        [Fact]
        public async Task DeleteVehicleByID_ShouldNotDeleteVehicle_WhenAVehicleWithId1DoesNotExists()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetService<CarDealerContext>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var vehicle = new Vehicle
                {Id = 1, VinNumber = "jnjknf", Make = "Toyota", MarketValue = 12331, Model = "camry", Year = 1997};
            context.VehicleInventory.Add(vehicle);
            context.SaveChanges();
            context.VehicleInventory.Count().Should().Be(1);
            //When
            await client.DeleteAsync("/Vehicle/2");
            //Then
            context.VehicleInventory.Count().Should().Be(1);
            context.Database.EnsureDeleted();
        }
        
        [Fact]
        public async Task DeleteVehicleByID_ShouldThrowException_WhenAVehicleWithId1DoesNotExists()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetService<CarDealerContext>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var vehicle = new Vehicle
                {Id = 1, VinNumber = "jnjknf", Make = "Toyota", MarketValue = 12331, Model = "camry", Year = 1997};
            context.VehicleInventory.Add(vehicle);
            context.SaveChanges();
            context.VehicleInventory.Count().Should().Be(1);
            //When
            var resp =  await client.DeleteAsync("/Vehicle/2");
            var jsonRes = await resp.Content.ReadAsStringAsync();
            jsonRes.Should().Contain("Exception");
            //Then
            context.VehicleInventory.Count().Should().Be(1);
            context.Database.EnsureDeleted();
        }
    }
}