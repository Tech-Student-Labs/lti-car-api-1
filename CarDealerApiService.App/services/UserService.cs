using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CarDealerApiService.App.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CarDealerApiService.services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ValidSignIn(string email,string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var checkPass = await _userManager.CheckPasswordAsync(user, password);
            return checkPass;
        }
        
        
        // private string generateJwtToken(User user)
        // {
        //     // generate token that is valid for 7 days
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     //pass in key
        //     var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //     var tokenDescriptor = new SecurityTokenDescriptor
        //     {
        //         Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
        //         Expires = DateTime.UtcNow.AddDays(7),
        //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //     };
        //     var token = tokenHandler.CreateToken(tokenDescriptor);
        //     return tokenHandler.WriteToken(token);
        // }
        
    }
}