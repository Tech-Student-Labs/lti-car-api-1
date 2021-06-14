using System;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace CarDealerWebAPI.Tests.VehicleSubmissionE2ETests
{
    public class POST_Route_VehicleSubmission
    {
        private IWebHostBuilder HostBuilder => new WebHostBuilder()
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>()
            .ConfigureServices(services =>
            {
                services.Remove(
                    services.SingleOrDefault(
                        s => s.ServiceType == typeof(DbContextOptions<CarDealerContext>))
                );
                services.AddDbContext<CarDealerContext>(options =>
                    options.UseInMemoryDatabase("ToDoGetSubmilttedVehicles"));
            });

        [Fact]
        //finish this up
        public async Task Action_Should_When()
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
                {Id = "1", Email = "KevinHuynh@yahoo.com", UserName = "hahahaha", FirstName = "ha", LastName = "Ha"};
            dbContext.UserTable.Add(user);
            //setup Vehicles
            var vehicles = new Vehicle
                {Id =2 , Make = "toyoya", MarketValue = 12313, Model = "camry", VinNumber = "1GCCT19X738198141", Year = 1997};
            dbContext.VehicleInventory.Add(vehicles);
            dbContext.SaveChanges();

            //When
            //Call VehicleSubmissionController            
            var response = await client.PostAsJsonAsync("/VehicleSubmissions", new {UserId = "1", VehicleId = 2, Vehicle = vehicles});
            var jsonObj = await response.Content.ReadAsStringAsync();

            //Then
            dbContext.UserTable.ToList().Count.Should().Be(1);
            jsonObj.Should().Be("jk");
            dbContext.VehicleSubmissions.ToList().Count.Should().Be(1);
        }
    }
}