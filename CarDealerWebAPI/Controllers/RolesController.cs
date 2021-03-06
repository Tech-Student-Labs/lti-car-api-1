using System.Threading.Tasks;
using CarDealerAPIService.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET
        [HttpPost("Create")]
        public async Task<IActionResult> CreateRole()
        {
            var result = await _roleService.CreateRoleAsync();
            return Ok(result);
        }
    }
}