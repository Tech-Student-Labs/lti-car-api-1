using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Language.Flow;
using Xunit;

namespace CarDealerWebAPI.Tests.UserServiceIntegrationTests
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

        private static readonly Mock<SignInManager<User>> SignInManagerMock = new Mock<SignInManager<User>>(
            UserManagerMock.Object,
            /* IHttpContextAccessor contextAccessor */Mock.Of<IHttpContextAccessor>(),
            /* IUserClaimsPrincipalFactory<TUser> claimsFactory */Mock.Of<IUserClaimsPrincipalFactory<User>>(),
            /* IOptions<IdentityOptions> optionsAccessor */null,
            /* ILogger<SignInManager<TUser>> logger */null,
            /* IAuthenticationSchemeProvider schemes */null,
            /* IUserConfirmation<TUser> confirmation */null);


        private static readonly Mock<RoleManager<User>> RoleManagerMock = new Mock<RoleManager<User>>();

        private static readonly Mock<IConfiguration> ConfigMock = new Mock<IConfiguration>();

        [Fact]
        public async Task Login_ShouldCallFindByEmailAsync_WhenTheValidSignInFunctionIsCalledAsync()
        {
            //Given That the UserManager And SigninManager are Setup
            var user = new User();

            UserLogin cred = new UserLogin {Email = "email", Password = "password"};

            UserManagerMock.Setup(x => x.FindByEmailAsync(cred.Email))
                .ReturnsAsync(user);
            SignInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, cred.Password, false))
                .ReturnsAsync(SignInResult.Success);
            UserManagerMock.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(new List<string>{"RegularUser"});

            var service = new UserService(UserManagerMock.Object, SignInManagerMock.Object);
            await service.Authenticate(cred);
            //Then Test the Behavior of the dependencies to see if they were called and what functions got called. 
            UserManagerMock.Verify(mock => mock.FindByEmailAsync(cred.Email), Times.Once);
            SignInManagerMock.Verify(mock => mock.CheckPasswordSignInAsync(user, cred.Password, false), Times.Once);
            UserManagerMock.Verify(mock => mock.GetRolesAsync(user),Times.Once);
        }
    }
}