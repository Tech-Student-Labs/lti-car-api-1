using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CarDealerAPIService.services
{
    public interface IRoleService
    {
        Task<IdentityResult> CreateRoleAsync();
    }
}