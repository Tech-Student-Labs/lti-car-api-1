using System.Net.Http;
using System.Threading.Tasks;
using CarDealerAPIService.App.models;

namespace CarDealerWebAPI.services
{
    public interface IVehicleMarketValueService
    {
        Task<string> GetAverageVehiclePrice(string vin);
    }
}