using System.Threading.Tasks;

namespace CarDealerAPIService.services
{
    public interface IVehicleMarketValueService
    {
        Task<string> GetAverageVehiclePrice(string vin);
    }
}