using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
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
    public class PUT_Route_UpdateVehicle
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
                services.AddDbContext<CarDealerContext>(options => options.UseInMemoryDatabase("PUTVehicle"));
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                });
            });

        [Fact]
        public void PUT_ShouldUpdateVehicle_WhenPassedTheUpdatedVehicle()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var service = testServer.Services.GetService<CarDealerContext>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var vehicle = new Vehicle { Id = 1,Make = "Toyota",Model = "Camry",MarketValue = 20000,VinNumber = "VinNumber",Year = 2016};
            service.VehicleInventory.Add(vehicle);
            service.SaveChanges();
            vehicle.Make = "nissan";
            vehicle.Model = "sentra";
            //When
            client.PutAsJsonAsync("/Vehicle",vehicle);
            //Then
            var foundVehicle = service.VehicleInventory.FirstOrDefault(x => true);
            foundVehicle.Make.Should().Be("nissan");
            foundVehicle.Model.Should().Be("sentra");
            foundVehicle.MarketValue.Should().Be(20000);
            foundVehicle.VinNumber.Should().Be("VinNumber");
        }
    }
}