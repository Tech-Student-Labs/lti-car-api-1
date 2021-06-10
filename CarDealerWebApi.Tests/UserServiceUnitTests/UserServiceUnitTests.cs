using System.Threading.Tasks;
using CarDealerApiService.App.models;
using CarDealerApiService.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace CarDealerWebApi.Tests.UserServiceIntegrationTests
{
    public class UserServiceUnitTests
    {
        private static readonly Mock<UserManager<User>> UserManagerMock = new Mock<UserManager<User>>(
            /* IUserStore<TUser> store */Mock.Of<IUserStore<User>>(),
            /* IOptions<IdentityOptions> optionsAccessor */null,
            /* IPasswordHasher<TUser> passwordHasher */null,
            /* IEnumerable<IUserValidator<TUser>> userValidators */null,
            /* IEnumerable<IPasswordValidator<TUser>> passwordValidators */null,
            /* ILookupNormalizer keyNormalizer */null,
            /* IdentityErrorDescriber errors */null,
            /* IServiceProvider services */null,
            /* ILogger<UserManager<TUser>> logger */null);

        [Fact]
        public async Task Login_ShouldCallFindByEmailAsyncAndCheckPasswordAsync_WhenTheValidSignInFunctionIsCalledAsync()
        {
            //Given That the UserManager And SigninManager are Setup
            var user = new User();
            var email = "email";
            var password = "password";
            UserManagerMock.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(user);
            UserManagerMock.Setup(x => x.CheckPasswordAsync(user,password))
                .ReturnsAsync(true);
            //When the service calls a Login Function 
            
            var service = new UserService(UserManagerMock.Object);
            await service.ValidSignIn(email, password);
            //Then Test the Behavior of the dependencies to see if they were called and what functions got called. 
            UserManagerMock.Verify(mock => mock.FindByEmailAsync(email),Times.Once);
            UserManagerMock.Verify(mock => mock.CheckPasswordAsync(user,password),Times.Once);
            //We Can Ei
        }

    }
}