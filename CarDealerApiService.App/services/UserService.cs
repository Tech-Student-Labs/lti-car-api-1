using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CarDealerAPIService.App.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CarDealerAPIService.services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<string> Authenticate(UserLogin cred)
        {
            var user = await _userManager.FindByEmailAsync(cred.Email);
            var result = await _signInManager.CheckPasswordSignInAsync(user, cred.Password, false);
            if (user == null || !result.Succeeded) throw new Exception("Could Not Authenticate User");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsTheKeyPleaseDoNotShareThisKeyOrWeWillBeHacked")),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }

        public async Task<string> SignUpUser(UserSignUp cred)
        {
            User user = new User() {Email = cred.Email, FirstName = cred.FirstName, LastName = cred.LastName, UserName = cred.UserName};

            var result = await _userManager.CreateAsync(user, cred.Password);
            
            //add to roles here
            return result.ToString();
        }
        
    }
}