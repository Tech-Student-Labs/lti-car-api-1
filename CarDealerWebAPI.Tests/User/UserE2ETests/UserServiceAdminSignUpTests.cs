using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CarDealerWebAPI.Tests.UserE2ETests
{
    public class UserServiceAdminSignUpTests
    {
        private IWebHostBuilder HostBuilder => new WebHostBuilder()
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>()
            //.Configure(app => app.UseMiddleware<ExceptionMiddleware>())
            .ConfigureServices(services =>
            {
                services.Remove(
                    services.SingleOrDefault(
                        s => s.ServiceType == typeof(DbContextOptions<CarDealerContext>))
                );
                services.AddDbContext<CarDealerContext>(options => options.UseInMemoryDatabase("SignUpAdminUsers"));
            });

        [Fact]
        public async Task SignupAdminPost_ShouldCreateAnAdminUser_WhenCalledWithAnUser()
        {
            //Given
            var testServer = new TestServer(HostBuilder);
            var client = testServer.CreateClient();
            var context = testServer.Services.GetRequiredService<CarDealerContext>();
            await client.PostAsJsonAsync("/Roles/Create", "");
            //When
            var response = await client.PostAsJsonAsync("/User/SignupAdmin",
                new UserSignUp()
                {
                    Email = "kevinynh@yahoo.com",
                    UserName = "userName",
                    Password = "123qwe123_",
                    FirstName = "Kevin",
                    LastName = "Huynh"
                });

            //Then
            var adminId = context.Roles.ToList().FirstOrDefault(x => x.Name == "AdminUser").Id;
            var userId = context.UserTable.ToList().FirstOrDefault(x => x.Email == "kevinynh@yahoo.com").Id;
            var userRoleCorrelation = context.UserRoles.FirstOrDefault(x => true);
            userRoleCorrelation.RoleId.Should().Be(adminId);
            userRoleCorrelation.UserId.Should().Be(userId);
            context.Users.Count().Should().Be(1);
        }
    }
}