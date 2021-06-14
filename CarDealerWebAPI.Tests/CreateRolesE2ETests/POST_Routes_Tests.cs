using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.Exception.ExceptionModel;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace CarDealerWebAPI.Tests.CreateRolesE2ETests
{
    public class POST_Routes_Tests
    {
        private IWebHostBuilder HostBuilder => new WebHostBuilder()
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>()
            .ConfigureServices(services =>
            {
                services.Remove(
                    services.SingleOrDefault(
                        s => s.ServiceType == typeof(DbContextOptions<CarDealerContext>))
                );
                services.AddDbContext<CarDealerContext>(options => options.UseInMemoryDatabase("ToDoRoles"));
            });

        [Fact]
        public async Task CreateRoles_ShouldReturnRolesCountTwo_WhenCalledOnce()
        {
            //Given'
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var todoService = testServer.Services.GetRequiredService<CarDealerContext>();
            await todoService.Database.EnsureDeletedAsync();
            await todoService.Database.EnsureCreatedAsync();
            //When
            var response = await client.PostAsJsonAsync("/Roles/Create", "");
            todoService.Roles.ToList().Count.Should().Be(2);
            //Then
        }

        [Fact]
        public async Task CreateRoles_ShouldHaveRegularUser_WhenCalledOnce()
        {
            //Given'
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var todoService = testServer.Services.GetRequiredService<CarDealerContext>();
            await todoService.Database.EnsureDeletedAsync();
            await todoService.Database.EnsureCreatedAsync();
            //When
            var response = await client.PostAsJsonAsync("/Roles/Create", "");
            todoService.Roles.ToList()[0].Name.Should().Be("RegularUser");
            //Then
        }

        [Fact]
        public async Task CreateRoles_ShouldHaveAdminUser_WhenCalledOnce()
        {
            //Given'
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var todoService = testServer.Services.GetRequiredService<CarDealerContext>();
            await todoService.Database.EnsureDeletedAsync();
            await todoService.Database.EnsureCreatedAsync();
            //When
            var response = await client.PostAsJsonAsync("/Roles/Create", "");
            todoService.Roles.ToList()[1].Name.Should().Be("AdminUser");
            //Then
        }
        
        [Fact]
        public async Task CreateRoles_ShouldThrowExceptionWithMessage_WhenCalledAlreadyExists()
        {
            //Given'
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var todoService = testServer.Services.GetRequiredService<CarDealerContext>();
            await todoService.Database.EnsureDeletedAsync();
            await todoService.Database.EnsureCreatedAsync();
            //When
            await client.PostAsJsonAsync("/Roles/Create", "");
            var response = await client.PostAsJsonAsync("/Roles/Create", "");
            var jsonObj = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ErrorDetails>(jsonObj);
            results?.Message.Should().Be("You Already have those roles in your DB");

            //Then
        }
    }
}