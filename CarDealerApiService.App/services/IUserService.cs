using CarDealerAPIService.App.models;
using System.Threading.Tasks;

namespace CarDealerAPIService.services
{
    public interface IUserService
    {
        Task<TokenDTO> Authenticate(UserLogin cred);
        Task<string> SignUpUser(UserSignUp cred);
        Task<string> SignUpAdmin(UserSignUp cred);
    }
}