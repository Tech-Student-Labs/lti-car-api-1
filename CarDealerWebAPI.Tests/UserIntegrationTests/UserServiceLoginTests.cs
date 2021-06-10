using System;
using System.IO;
using System.Linq;
using System.Reflection;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.services;
using CarDealerWebAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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
                       services.AddDbContext<CarDealerContext>(options => options.UseInMemoryDatabase("UserTest"));
                   });

        [Fact]
        public void test()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            //When

            //Then

        }


    }


}