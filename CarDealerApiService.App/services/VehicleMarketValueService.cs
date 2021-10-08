using CarDealerAPIService.App.models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CarDealerAPIService.services
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

            var stringTask =
                await _client.GetAsync(
                    $"http://marketvalue.vinaudit.com/getmarketvalue.php?key=VA_DEMO_KEY&vin={vin}&format=json&period=90&mileage=average");
            string result = await stringTask.Content.ReadAsStringAsync();
            if (result.Contains("error"))
            {
                throw new Exception("Cannot Find Vin Number");
            }
            var obj = JsonConvert.DeserializeObject<VehiclePriceRequest>(result);

            return obj.Prices.Average.ToString();
        }
    }
}