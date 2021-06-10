using System.Net.Http;
using System.Threading.Tasks;

namespace CarDealerAPIService.services
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}