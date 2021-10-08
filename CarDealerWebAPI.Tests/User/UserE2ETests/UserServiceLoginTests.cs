using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.Exception.ExceptionModel;
using CarDealerAPIService.App.models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CarDealerWebAPI.Tests.Tests
{
    public class UserServiceLoginTests
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
                services.AddDbContext<CarDealerContext>(options => options.UseInMemoryDatabase("UserLoginTests"));
            });

        [Fact]
        public async Task Login_ShouldReturnAnTokenDTO_WhenItCouldAuthenticateUser()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var service = testServer.Services.GetRequiredService<CarDealerContext>();
            //When

            User user = new User();
            var k = service.UserTable.ToList();
            await client.PostAsJsonAsync("/User/Signup",
                new UserSignUp()
                {
                    Email = "kevinhuynh@yahoo.com",
                    UserName = "userName",
                    Password = "123qwe123_",
                    FirstName = "Kevin",
                    LastName = "Huynh"
                });
            var response = await client.PostAsJsonAsync("User/Login",
                new UserLogin { Email = "kevinhuynh@yahoo.com", Password = "123qwe123_" });
            //Then
            var jsonObj = await response.Content.ReadAsStringAsync();
            jsonObj.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Login_ShouldReturnException_WhenItCouldNotFindUser()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var service = testServer.Services.GetRequiredService<CarDealerContext>();
            //When
            var hash = new PasswordHasher<User>();

            User user = new User();
            var k = service.UserTable.ToList();
            await client.PostAsJsonAsync("/User/Signup",
                new UserSignUp()
                {
                    Email = "kevinhuynh@yahoo.com",
                    UserName = "userName",
                    Password = "123qwe123_",
                    FirstName = "Kevin",
                    LastName = "Huynh"
                });
            var response = await client.PostAsJsonAsync("User/Login",
                new UserLogin { Email = "kevinuynh@yahoo.com", Password = "123qwe123_" });
            //Then
            var jsonObj = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ErrorDetails>(jsonObj);
            result.Message.Should().Be("Could Not Authenticate User");
        }


        [Fact]
        public async Task Login_ShouldReturnException_WhenItMatchPassword()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var service = testServer.Services.GetRequiredService<CarDealerContext>();
            //When
            var hash = new PasswordHasher<User>();

            User user = new User();
            var k = service.UserTable.ToList();
            await client.PostAsJsonAsync("/User/Signup",
                new UserSignUp()
                {
                    Email = "kevinhuynh@yahoo.com",
                    UserName = "userName",
                    Password = "123qwe123_",
                    FirstName = "Kevin",
                    LastName = "Huynh"
                });
            var response = await client.PostAsJsonAsync("User/Login",
                new UserLogin { Email = "kevinhuynh@yahoo.com", Password = "123qwe123" });
            //Then
            var jsonObj = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ErrorDetails>(jsonObj);
            result.Message.Should().Be("Could Not Authenticate User");
        }
    }
}