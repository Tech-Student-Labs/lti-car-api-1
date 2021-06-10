using System.Collections.Generic;
using CarDealerApiService.App.models;

namespace CarDealerApiService.services
{
    public interface IVehicleService
    {
        List<Vehicle> GetAllVehicles();
        Vehicle GetVehicle(int id);
        void AddVehicle(Vehicle vehicle);
        void AddRangeOfVehicles(Vehicle[] vehicles);
        void UpdateVehicle(Vehicle vehicle);
        void DeleteVehicle(Vehicle vehicle);
        void DestroyDatabase();

        List<int> GetMarketValues();
    }
}