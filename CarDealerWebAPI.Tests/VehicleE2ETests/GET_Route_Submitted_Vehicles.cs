using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CarDealerApiService.App.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;
using FluentAssertions;
using System.Net;
using CarDealerApiService.App.models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CarDealerWebApi.Tests.VehicleE2ETests
{
    public class GET_Route_Submitted_Vehicles
    {
        private IWebHostBuilder HostBuilder => new WebHostBuilder()
        .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>()
        .ConfigureServices(services =>
        {
            services.Remove(
                services.SingleOrDefault(
                    s => s.ServiceType == typeof(DbContextOptions<CarDealerContext>))
            );
            services.AddDbContext<CarDealerContext>(options => options.UseInMemoryDatabase("ToDoGetSubmittedVehicles"));
        });

        [Fact]
        public async Task Should_Return200Status_WhenNoVehicleSubmissionsExist()
        {
            //GIVEN the service is running and there are no items in the VehicleSubmissions Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();

            //WHEN a GET request is submitted to VehicleSubmissions with UserID
            var result = await client.GetAsync("/VehicleSubmissions/1");

            //THEN the response should return a OK status
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Return200Status_WhenOneVehicleSubmissionsExists()
        {
            //GIVEN the service is running and there is 1 items in the VehicleSubmissions Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetRequiredService<CarDealerContext>();
            User newUser = new User() { Id = "1" };
            Vehicle newVehicle = new Vehicle() { Id = 1 };
            await context.Users.AddAsync(newUser);
            await context.VehicleInventory.AddAsync(newVehicle);
            await context.VehicleSubmissions.AddAsync(new VehicleSubmissions() { User = newUser, Vehicle = newVehicle });
            await context.SaveChangesAsync();


            //WHEN a GET request is submitted to VehicleSubmissions with UserID
            var result = await client.GetAsync("/VehicleSubmissions/1");

            //THEN the response should return a OK status
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_ReturnOneObject_WhenOneVehicleSubmissionsExists()
        {
            //GIVEN the service is running and there is 1 items in the VehicleSubmissions Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetRequiredService<CarDealerContext>();
            User newUser = new User() { Id = "1" };
            Vehicle newVehicle = new Vehicle() { Id = 1 };
            await context.Users.AddAsync(newUser);
            await context.VehicleInventory.AddAsync(newVehicle);
            await context.VehicleSubmissions.AddAsync(new VehicleSubmissions() { User = newUser, Vehicle = newVehicle });
            await context.SaveChangesAsync();


            //WHEN a GET request is submitted to VehicleSubmissions with UserID
            var result = await client.GetAsync("/VehicleSubmissions/1");

            //THEN the response should return a OK status
            var response = await result.Content.ReadAsStringAsync();
            List<VehicleSubmissionsDTO> vehicleJsonObj = JsonConvert.DeserializeObject<List<VehicleSubmissionsDTO>>(response);
            vehicleJsonObj?.Count.Should().Be(1);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_Return200Status_WhenManyVehicleSubmissionsExists()
        {
            //GIVEN the service is running and there is 1 items in the VehicleSubmissions Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetRequiredService<CarDealerContext>();
            User newUser = new User() { Id = "1" };
            Vehicle newVehicle1 = new Vehicle() { Make = "Toyota" };
            Vehicle newVehicle2 = new Vehicle() { Make = "Toyota" };
            Vehicle newVehicle3 = new Vehicle() { Make = "Toyota" };
            await context.Users.AddAsync(newUser);
            await context.VehicleInventory.AddAsync(newVehicle1);
            await context.VehicleInventory.AddAsync(newVehicle2);
            await context.VehicleInventory.AddAsync(newVehicle3);
            await context.VehicleSubmissions.AddAsync(new VehicleSubmissions() { User = newUser, Vehicle = newVehicle1 });
            await context.VehicleSubmissions.AddAsync(new VehicleSubmissions() { User = newUser, Vehicle = newVehicle2 });
            await context.VehicleSubmissions.AddAsync(new VehicleSubmissions() { User = newUser, Vehicle = newVehicle3 });

            await context.SaveChangesAsync();


            //WHEN a GET request is submitted to VehicleSubmissions with UserID
            var result = await client.GetAsync("/VehicleSubmissions/1");

            //THEN the response should return a OK status
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_ReturnOneObject_WhenManyVehicleSubmissionsExists()
        {
            //GIVEN the service is running and there is 1 items in the VehicleSubmissions Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetRequiredService<CarDealerContext>();
            User newUser = new User() { Id = "1" };
            Vehicle newVehicle1 = new Vehicle() { Make = "Toyota" };
            Vehicle newVehicle2 = new Vehicle() { Make = "Toyota" };
            Vehicle newVehicle3 = new Vehicle() { Make = "Toyota" };
            await context.Users.AddAsync(newUser);
            await context.VehicleInventory.AddAsync(newVehicle1);
            await context.VehicleInventory.AddAsync(newVehicle2);
            await context.VehicleInventory.AddAsync(newVehicle3);
            await context.VehicleSubmissions.AddAsync(new VehicleSubmissions() { User = newUser, Vehicle = newVehicle1 });
            await context.VehicleSubmissions.AddAsync(new VehicleSubmissions() { User = newUser, Vehicle = newVehicle2 });
            await context.VehicleSubmissions.AddAsync(new VehicleSubmissions() { User = newUser, Vehicle = newVehicle3 });
            await context.SaveChangesAsync();


            //WHEN a GET request is submitted to VehicleSubmissions with UserID
            var result = await client.GetAsync("/VehicleSubmissions/1");

            //THEN the response should return a OK status
            var response = await result.Content.ReadAsStringAsync();
            List<VehicleSubmissionsDTO> vehicleJsonObj = JsonConvert.DeserializeObject<List<VehicleSubmissionsDTO>>(response);
            vehicleJsonObj?.Count.Should().Be(3);
            await context.Database.EnsureDeletedAsync();
        }
    }
}