using System;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;
using Xunit;

namespace CarDealerWebAPI.Tests.VehicleListingE2ETests
{
    public class POST_Route_AddToVehicleListing
    {
        private readonly string adminToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiJjYjlkZTNjYS1mNTU1LTQ1NzctOWM4My0wYmFiMGRlMmE4ZGEiLCJyb2xlIjoiQWRtaW5Vc2VyIiwibmJmIjoxNjIzNzAyMTYxLCJleHAiOjMzMTgwNjEwOTYxLCJpYXQiOjE2MjM3MDIxNjEsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCJ9.2mimAkdW2HxEuotUNdxVPtUxcMOfP6wCLIh3twM28lY";

        private IWebHostBuilder HostBuilder => new WebHostBuilder()
        .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>()
        .ConfigureServices(services =>
        {
            services.Remove(
                services.SingleOrDefault(
                    s => s.ServiceType == typeof(DbContextOptions<CarDealerContext>))
            );
            services.AddDbContext<CarDealerContext>(options =>
                options.UseInMemoryDatabase("AddSubmittedVehicleListing"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Test";
                options.DefaultChallengeScheme = "Test";
            });
        });


        [Fact]
        //finish this up
        public async Task Action_Should_When()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var dbContext = testServer.Services.GetRequiredService<CarDealerContext>();
            //setup roles
            dbContext.Database.EnsureDeleted();
            await client.PostAsJsonAsync("/Roles/Create", "");
            
            //setup Vehicles
            var vehicles = new Vehicle
            { Id = 1, Make = "toyoya", MarketValue = 12313, Model = "camry", VinNumber = "1GCCT19X738198141", Year = 1997 };
            dbContext.Add(vehicles);
            dbContext.SaveChanges();

            //When
            //Call VehicleListingsController            
            var response = await client.PostAsJsonAsync("/VehicleListing", new VehicleListing { VehicleId = 1, Price = 12000});
            var jsonObj = await response.Content.ReadAsStringAsync();
            jsonObj.Should().Be("jk");
            //Then
            dbContext.VehicleListings.ToList().Count.Should().Be(1);

        }
    }
}