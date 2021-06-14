using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CarDealerAPIService.services
{
    public interface IRoleService
    {
        Task<IdentityResult> CreateRoleAsync();
    }
}