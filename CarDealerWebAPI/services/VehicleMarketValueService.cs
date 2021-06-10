using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CarDealerAPIService.App.models;
using System.Net.Http.Formatting;

namespace CarDealerWebAPI.services
{
    public class VehicleMarketValueService : IVehicleMarketValueService
    {
        private HttpClient _client;
        public VehicleMarketValueService(HttpClient client)
        {
            _client = client;
        }
        public HttpResponseMessage Get(string url)
        {
            return GetAsync(url).Result;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync(url);
        }

        public async Task<VehiclePriceRequest> PriceRequestAsync(string url)
        {
            HttpResponseMessage message = Get(url);
            return await message.Content.ReadAsAsync<VehiclePriceRequest>();
        }

        public long AveragePrice(string url)
        {
            return PriceRequestAsync(url).Result.Prices.Average;
        }
    }
}