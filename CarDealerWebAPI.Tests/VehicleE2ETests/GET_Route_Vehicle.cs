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
    public class GET_Route_Vehicle
    {
        private IWebHostBuilder HostBuilder => new WebHostBuilder()
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>()
            .ConfigureServices(services =>
            {
                services.Remove(
                    services.SingleOrDefault(
                        s => s.ServiceType == typeof(DbContextOptions<CarDealerContext>))
                );
                services.AddDbContext<CarDealerContext>(options => options.UseInMemoryDatabase("ToDoGet"));
            });

        [Fact]
        public async Task Should_Return200Status_WhenNoVehiclesExist()
        {
            //GIVEN the service is running and there are no items in the Vehicles Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();

            //WHEN a GET request is submitted to Vehicle with no parameters
            var result = await client.GetAsync("/Vehicle");

            //THEN the response should return a OK status
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Return0Count_WhenNoVehiclesExist()
        {
            //GIVEN the service is running and there are no items in the Vehicles Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var todoService = testServer.Services.GetRequiredService<CarDealerContext>();
            await todoService.Database.EnsureDeletedAsync();
            await todoService.Database.EnsureCreatedAsync();
            //WHEN a GET request is submitted to Vehicle with no parameters
            var result = await client.GetAsync("/Vehicle");

            //THEN the response should return No Vehicles
            var jsonObj = await result.Content.ReadAsStringAsync();
            List<Vehicle> vehicleObjList = JsonConvert.DeserializeObject<List<Vehicle>>(jsonObj);
            vehicleObjList?.Count.Should().Be(0);
            await todoService.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_Return200Status_WhenOneVehiclesExist()
        {
            //GIVEN the service is running and there are no items in the Vehicles Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var todoService = testServer.Services.GetRequiredService<CarDealerContext>();
            await todoService.Database.EnsureDeletedAsync();
            await todoService.Database.EnsureCreatedAsync();
            var vehicle1 = new Vehicle();
            await todoService.VehicleInventory.AddAsync(vehicle1);
            await todoService.SaveChangesAsync();
            //WHEN a GET request is submitted to Vehicle with no parameters
            var result = await client.GetAsync("/Vehicle");

            //THEN the response should return a OK status
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            await todoService.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_ReturnCountOne_WhenOneVehiclesExist()
        {
            //GIVEN the service is running and there are no items in the Vehicles Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var todoService = testServer.Services.GetRequiredService<CarDealerContext>();
            await todoService.Database.EnsureDeletedAsync();
            await todoService.Database.EnsureCreatedAsync();

            var vehicle1 = new Vehicle();
            await todoService.VehicleInventory.AddAsync(vehicle1);
            await todoService.SaveChangesAsync();
            //WHEN a GET request is submitted to Vehicle with no parameters
            var result = await client.GetAsync("/Vehicle");

            //THEN the response should return a Vehicle Object
            var response = await result.Content.ReadAsStringAsync();
            List<Vehicle> vehicleJsonObj = JsonConvert.DeserializeObject<List<Vehicle>>(response);
            vehicleJsonObj?.Count.Should().Be(1);
            await todoService.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_Return200Status_WhenTwoVehiclesExist()
        {
            //GIVEN the service is running and there are Two Items in the Vehicles Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var todoService = testServer.Services.GetRequiredService<CarDealerContext>();
            await todoService.Database.EnsureDeletedAsync();
            await todoService.Database.EnsureCreatedAsync();
            var vehicle1 = new Vehicle();
            await todoService.VehicleInventory.AddAsync(vehicle1);
            await todoService.VehicleInventory.AddAsync(vehicle1);
            await todoService.SaveChangesAsync();
            //WHEN a GET request is submitted to Vehicle with no parameters
            var result = await client.GetAsync("/Vehicle");

            //THEN the response should return a OK status
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            await todoService.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_ReturnCountTwo_WhenTwoVehiclesExist()
        {
            //GIVEN the service is running and there are no items in the Vehicles Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var todoService = testServer.Services.GetRequiredService<CarDealerContext>();
            await todoService.Database.EnsureDeletedAsync();
            await todoService.Database.EnsureCreatedAsync();

            var vehicle1 = new Vehicle();
            var vehicle2 = new Vehicle();
            await todoService.VehicleInventory.AddAsync(vehicle1);
            await todoService.VehicleInventory.AddAsync(vehicle2);
            await todoService.SaveChangesAsync();
            //WHEN a GET request is submitted to Vehicle with no parameters
            var result = await client.GetAsync("/Vehicle");

            //THEN the response should return Two Objects
            var response = await result.Content.ReadAsStringAsync();
            List<Vehicle> vehicleJsonObj = JsonConvert.DeserializeObject<List<Vehicle>>(response);
            vehicleJsonObj?.Count.Should().Be(2);
            await todoService.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_ReturnTheVehicleWithId1_WhenTheGetVehicleByIdIsCalledWithId1()
        {
            //GIVEN the service is running and there are no items in the Vehicles Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var todoService = testServer.Services.GetRequiredService<CarDealerContext>();
            await todoService.Database.EnsureDeletedAsync();
            await todoService.Database.EnsureCreatedAsync();
            
            var vehicle1 = new Vehicle() {Id = 1, VinNumber = "123qwe"};
            await todoService.VehicleInventory.AddAsync(vehicle1);
            await todoService.SaveChangesAsync();
            //WHEN a GET request is submitted to Vehicle with id = 1 in the parameters
            var result = await client.GetAsync("/Vehicle/1");

            //THEN the response should return an Vehicle Object with id = 1
            var response = await result.Content.ReadAsStringAsync();
            Vehicle vehicleJsonObj = JsonConvert.DeserializeObject<Vehicle>(response);
            vehicleJsonObj.VinNumber.Should().BeEquivalentTo(vehicle1.VinNumber);
            await todoService.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_ReturnTheStatusCode200_WhenTheGetVehicleByIdCalledWithId1()
        {
            //GIVEN the service is running and there are no items in the Vehicles Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var todoService = testServer.Services.GetRequiredService<CarDealerContext>();
            await todoService.Database.EnsureDeletedAsync();
            await todoService.Database.EnsureCreatedAsync();

            var vehicle1 = new Vehicle() {VinNumber = "123qwe"};
            var vehicle2 = new Vehicle();
            await todoService.VehicleInventory.AddAsync(vehicle1);
            await todoService.SaveChangesAsync();
            //WHEN a GET request is submitted to Vehicle with no parameters
            var result = await client.GetAsync("/Vehicle/1");

            //THEN the response should return a OK status
            var resultStatusCode = result.StatusCode;
            resultStatusCode.Should().Be(HttpStatusCode.OK);
            await todoService.Database.EnsureDeletedAsync();
        }
    }
}