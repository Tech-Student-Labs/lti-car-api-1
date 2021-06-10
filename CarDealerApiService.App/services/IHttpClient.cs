using System.Net.Http;
using System.Threading.Tasks;

namespace CarDealerApiService.services
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}