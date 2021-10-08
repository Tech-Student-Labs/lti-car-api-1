using Microsoft.AspNetCore.Identity;

namespace CarDealerAPIService.App.models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}