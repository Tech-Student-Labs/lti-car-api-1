using System.Threading.Tasks;
using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin cred)
        {
            var result = await _userService.Authenticate(cred);

            return Ok(result);
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp(UserSignUp cred)
        {
            var result = await _userService.SignUpUser(cred);
            return Ok(result);
        }
        
        [HttpPost("SignupAdmin")]
        public async Task<IActionResult> SignUpAdmin(UserSignUp cred)
        {
            var result = await _userService.SignUpAdmin(cred);
            return Ok(result);
        }
    }
}