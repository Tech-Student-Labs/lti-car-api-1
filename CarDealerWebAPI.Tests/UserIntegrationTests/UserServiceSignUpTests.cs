using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerWebAPI;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CarDealerWebApi.Tests
{
    public class UserServiceSignUpTests
    {
        private IWebHostBuilder HostBuilder => new WebHostBuilder()
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>()
            .ConfigureServices(services =>
            {
                services.Remove(
                    services.SingleOrDefault(
                        s => s.ServiceType == typeof(DbContextOptions<CarDealerContext>))
                );
                services.AddDbContext<CarDealerContext>(options => options.UseInMemoryDatabase("ToDoSignUpUsers"));
            });

        [Fact]
        public async Task SignUP_ShouldReturnStatus200_WhenCalled()
        {
            //GIVEN the service is running and there is 1 items in the VehicleSubmissions Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetRequiredService<CarDealerContext>();

            //WHEN a GET request is submitted to VehicleSubmissions with UserID
            var result = await client.PostAsJsonAsync("/User/Signup", new UserSignUp(){Email = "kevinhuynh@yahoo.com",Password = "123qwe123_",FirstName = "Kevin",LastName = "Huynh"});

            //THEN the response should return a OK status
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            await context.Database.EnsureDeletedAsync();
            
        }
        
        [Fact]
        public async Task SignUP_ShouldAddUser_WhenCalled()
        {
            //GIVEN the service is running and there is 1 items in the VehicleSubmissions Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetRequiredService<CarDealerContext>();

            //WHEN a GET request is submitted to VehicleSubmissions with UserID
            var result = await client.PostAsJsonAsync("/User/Signup", new UserSignUp(){Email = "kevinhuynh@yahoo.com",UserName = "userName", Password = "123qwe123_",FirstName = "Kevin",LastName = "Huynh"});

            //THEN the response should return a OK status
            context.UserTable.ToList().Count.Should().Be(1);
            await context.Database.EnsureDeletedAsync();
            
        }
    }
}