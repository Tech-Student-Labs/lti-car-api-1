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
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CarDealerWebAPI.Tests
{
    public class UserServiceSignUpTests
    {
        private static readonly IConfigurationBuilder builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        private readonly IConfiguration config = builder.Build();
        private IWebHostBuilder HostBuilder => new WebHostBuilder()
            .UseConfiguration(config)
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>()
            //.Configure(app => app.UseMiddleware<ExceptionMiddleware>())
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
            await client.PostAsJsonAsync("/Roles/Create", "");
            //WHEN a GET request is submitted to VehicleSubmissions with UserID
            var response = await client.PostAsJsonAsync("/User/Signup",
                new UserSignUp()
                {
                    Email = "kevinynh@yahoo.com",
                    UserName = "userName",
                    Password = "123qwe123_",
                    FirstName = "Kevin",
                    LastName = "Huynh"
                });
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            //THEN the response should return a OK status

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
            var result = await client.PostAsJsonAsync("/User/Signup",
                new UserSignUp()
                {
                    Email = "kevinhuynh@yahoo.com",
                    UserName = "userName",
                    Password = "123qwe123_",
                    FirstName = "Kevin",
                    LastName = "Huynh"
                });

            //THEN the response should return a OK status
            context.UserTable.ToList().Count.Should().Be(1);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task SignUP_ShouldAdd2Users_WhenCalledCalledTwice()
        {
            //GIVEN the service is running and there is 1 items in the VehicleSubmissions Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetRequiredService<CarDealerContext>();

            //WHEN a GET request is submitted to VehicleSubmissions with UserID
            await client.PostAsJsonAsync("/User/Signup",
                new UserSignUp()
                {
                    Email = "kevinhuynh@yahoo.com",
                    UserName = "userName",
                    Password = "123qwe123_",
                    FirstName = "Kevin",
                    LastName = "Huynh"
                });
            await client.PostAsJsonAsync("/User/Signup",
                new UserSignUp()
                {
                    Email = "kevinuynh@yahoo.com",
                    UserName = "usrName",
                    Password = "12qwe123_",
                    FirstName = "Kevin",
                    LastName = "Huynh"
                });

            //THEN the response should return a OK status
            context.UserTable.ToList().Count.Should().Be(2);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task SignUP_ShouldNotAddUser_WhenEmailIsAlreadyInDb()
        {
            //GIVEN the service is running and there is 1 items in the VehicleSubmissions Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetRequiredService<CarDealerContext>();

            //WHEN a GET request is submitted to VehicleSubmissions with UserID
            await client.PostAsJsonAsync("/User/Signup",
                new UserSignUp()
                {
                    Email = "kevinhuynh@yahoo.com",
                    UserName = "userName",
                    Password = "123qwe123_",
                    FirstName = "Kevin",
                    LastName = "Huynh"
                });
            await client.PostAsJsonAsync("/User/Signup",
                new UserSignUp()
                {
                    Email = "kevinhuynh@yahoo.com",
                    UserName = "userName",
                    Password = "123qwe123_",
                    FirstName = "Kevin",
                    LastName = "Huynh"
                });

            //THEN the response should return a OK status
            context.UserTable.ToList().Count.Should().Be(1);
            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task LoginThrowError_WhenNoUserCalled()
        {
            //GIVEN the service is running and there is 1 items in the VehicleSubmissions Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetRequiredService<CarDealerContext>();

            //WHEN a GET request is submitted to VehicleSubmissions with UserID
            await client.PostAsJsonAsync("/User/Signup",
                new UserSignUp()
                {
                    Email = "kevinhuynh@yahoo.com",
                    UserName = "userName",
                    Password = "123q51651_",
                    FirstName = "Kevin",
                    LastName = "Huynh"
                });

            var response = await client.PostAsJsonAsync("/User/Login",
                new UserSignUp()
                {
                    Email = "kevinhuynh@yahoo.com",
                    UserName = "userName",
                    Password = "123qwe123_",
                    FirstName = "Kevin",
                    LastName = "Huynh"
                });
            var str = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ErrorDetails>(str);
            result?.Message.Should().Be("Could Not Authenticate User");
        }

        [Fact]
        public async Task UserSignUp_ShouldReturnSucceeded_WhenAUserIsMade()
        {
            //GIVEN the service is running and there is 1 items in the VehicleSubmissions Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetRequiredService<CarDealerContext>();
            await client.PostAsJsonAsync("/Roles/Create", "");
            //WHEN a GET request is submitted to VehicleSubmissions with UserID
            var response = await client.PostAsJsonAsync("/User/Signup",
                new UserSignUp()
                {
                    Email = "kevinynh@yahoo.com",
                    UserName = "userName",
                    Password = "123qwe123_",
                    FirstName = "Kevin",
                    LastName = "Huynh"
                });
            var jsonObj = await response.Content.ReadAsStringAsync();
            context.UserTable.ToList().Count.Should().Be(1);
            //THEN the response should return a OK status

            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task UserSignUp_ShouldContainFailed_WhenAUserIsNotMade()
        {
            //GIVEN the service is running and there is 1 items in the VehicleSubmissions Table
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetRequiredService<CarDealerContext>();
            await client.PostAsJsonAsync("/Roles/Create", "");
            //WHEN a GET request is submitted to VehicleSubmissions with UserID
            await client.PostAsJsonAsync("/User/Signup",
                new UserSignUp()
                {
                    Email = "kevinynh@yahoo.com",
                    UserName = "userName",
                    Password = "123qwe123_",
                    FirstName = "Kevin",
                    LastName = "Huynh"
                });
            var response = await client.PostAsJsonAsync("/User/Signup",
                new UserSignUp()
                {
                    Email = "kevinynh@yahoo.com",
                    UserName = "userName",
                    Password = "123qwe123_",
                    FirstName = "Kevin",
                    LastName = "Huynh"
                });
            var jsonObj = await response.Content.ReadAsStringAsync();
            context.UserTable.ToList().Count.Should().Be(1);
            jsonObj.Should().Contain("Failed");
            //THEN the response should return a OK status

            await context.Database.EnsureDeletedAsync();
        }
    }
}