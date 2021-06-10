using System.Net.Http;
using System.Threading.Tasks;
using CarDealerAPIService.App.models;

namespace CarDealerWebAPI.services
{
    public interface IVehicleMarketValueService
    {
        long AveragePrice(string url);
        HttpResponseMessage Get(string url);
        Task<HttpResponseMessage> GetAsync(string url);
        Task<VehiclePriceRequest> PriceRequestAsync(string url);
    }
}