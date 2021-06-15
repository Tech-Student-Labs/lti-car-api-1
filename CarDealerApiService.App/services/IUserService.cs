using System.Threading.Tasks;
using CarDealerAPIService.App.models;

namespace CarDealerAPIService.services
{
    public interface IUserService
    {
        Task<TokenDTO> Authenticate(UserLogin cred);
        Task<string> SignUpUser(UserSignUp cred);
        Task<string> SignUpAdmin(UserSignUp cred);
    }
}