using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CarDealerAPIService.App.models;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace CarDealerWebAPI.services
{
    public class VehicleMarketValueService : IVehicleMarketValueService
    {
        private IHttpClient _client;
        public VehicleMarketValueService(IHttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetAverageVehiclePrice(string vin)
        {
            // HttpResponseMessage message = await _client.GetAsync($"http://marketvalue.vinaudit.com/getmarketvalue.php?key=VA_DEMO_KEY&vin={vin}&format=json&period=90&mileage=average");

            // VehiclePriceRequest request = await message.Content.ReadAsAsync<VehiclePriceRequest>();
            // return request.Prices.Average.ToString("$0.00");

            var stringTask = await _client.GetAsync($"http://marketvalue.vinaudit.com/getmarketvalue.php?key=VA_DEMO_KEY&vin={vin}&format=json&period=90&mileage=average");
            string result = await stringTask.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<VehiclePriceRequest>(result);

            return obj.Prices.Average.ToString();
            // return Task.Run(() => (string?)obj.Prices.Average.ToString());
            // return Task.FromResult<string?>(obj.Prices.Average.ToString());


            // var msg = stringTask;
            // return msg.ToString();
        }
    }
}