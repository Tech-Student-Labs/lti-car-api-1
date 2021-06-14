using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CarDealerAPIService.services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateRoleAsync()
        {
            var roleExistsUser = await _roleManager.RoleExistsAsync("RegularUser");
            var roleExistsAdmin = await _roleManager.RoleExistsAsync("AdminUser");
            if (roleExistsAdmin || roleExistsUser) throw new Exception("You Already have those roles in your DB");
            var roleCreated = await _roleManager.CreateAsync(new IdentityRole {Name = "RegularUser"});
            await _roleManager.CreateAsync(new IdentityRole {Name = "AdminUser"});
            return roleCreated;
        }
        
    }
}