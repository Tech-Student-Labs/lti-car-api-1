using System;
using System.Collections.Generic;
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


        public async Task<TokenDTO> Authenticate(UserLogin cred)
        {
            var user = await _userManager.FindByEmailAsync(cred.Email);
            if (user == null) throw new System.Exception("Could Not Authenticate User");
            var result = await _signInManager.CheckPasswordSignInAsync(user, cred.Password, false);
            if (!result.Succeeded) throw new System.Exception("Could Not Authenticate User");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "http://localhost:5000",
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(100),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("ThisIsTheKeyPleaseDoNotShareThisKeyOrWeWillBeHacked")),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            IList<string> roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                var claim = new Claim(ClaimTypes.Role, role);
                tokenDescriptor.Subject.AddClaim(claim);
            }
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return new TokenDTO() {Token = token};
        }

        public async Task<string> SignUpUser(UserSignUp cred)
        {
            User user = new User()
                {Email = cred.Email, FirstName = cred.FirstName, LastName = cred.LastName, UserName = cred.UserName};
            var result = await _userManager.CreateAsync(user, cred.Password);
            await _userManager.AddToRoleAsync(user, "RegularUser");
            //add to roles here
            return result.ToString();
        }
        
        public async Task<string> SignUpAdmin(UserSignUp cred)
        {
            User user = new User()
                {Email = cred.Email, FirstName = cred.FirstName, LastName = cred.LastName, UserName = cred.UserName};
            var result = await _userManager.CreateAsync(user, cred.Password);
            await _userManager.AddToRoleAsync(user, "AdminUser");
            //add to roles here
            return result.ToString();
        }
    }
}