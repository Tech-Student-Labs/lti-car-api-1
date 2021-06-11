using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
using CarDealerWebAPI;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace CarDealerWebApi.Tests
{
    public class UserServiceLoginTests
    {
        private IWebHostBuilder HostBuilder => new WebHostBuilder()
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
        public async Task test()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var service = testServer.Services.GetRequiredService<CarDealerContext>();
            //When
            var hash = new PasswordHasher<User>();
            
            User user = new User();
            var k = service.UserTable.ToList();
            await client.PostAsJsonAsync("/User/Signup", new UserSignUp(){Email = "kevinhuynh@yahoo.com",UserName = "userName", Password = "123qwe123_",FirstName = "Kevin",LastName = "Huynh"});
            var response = await client.PostAsJsonAsync("User/Login",new UserLogin{Email = "kevinhuynh@yahoo.com",Password = "123qwe123_"});
            //Then
            var result = await response.Content.ReadAsStringAsync();
            result.Should().NotBeEmpty();
        }


    }


}