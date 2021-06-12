using System.Threading.Tasks;
using CarDealerAPIService.services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CarDealerWebAPI.Tests.RolesUnitTest
{
    public class RoleServiceUnitTest
    {
        public static Mock<RoleManager<IdentityRole>> GetMockRoleManager()
        {
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            return new Mock<RoleManager<IdentityRole>>(
                roleStore.Object, null, null, null, null);
        }

        [Theory]
        [InlineData("RegularUser")]
        [InlineData("AdminUser")]
        public async Task RoleExistsAsync_ShouldBeCalledOnce_WhenRoleServiceGetsCalled(string role)
        {
            //Given
            var roleMock = GetMockRoleManager();
            roleMock.Setup(x => x.RoleExistsAsync(role)).ReturnsAsync(false);
            var roleService = new RoleService(roleMock.Object);
            //When
            await roleService.CreateRoleAsync();
            //Then
            roleMock.Verify(x => x.RoleExistsAsync(role), Times.Once);
        }
    }
}