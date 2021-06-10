using System.Net.Http;
using System.Threading.Tasks;

namespace CarDealerWebAPI.services
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}