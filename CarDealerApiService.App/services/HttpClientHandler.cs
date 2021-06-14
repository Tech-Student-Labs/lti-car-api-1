using System.Net.Http;
using System.Threading.Tasks;

namespace CarDealerAPIService.services
{
    public class HttpClientHandler : IHttpClient
    {
        private HttpClient _client;

        public HttpClientHandler()
        {
            _client = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync(url);
        }
    }
}