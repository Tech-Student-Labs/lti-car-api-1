using System.Net.Http;
using System.Threading.Tasks;
using CarDealerApiService.App.models;

namespace CarDealerApiService.services
{
    public interface IVehicleMarketValueService
    {
        Task<string> GetAverageVehiclePrice(string vin);
    }
}