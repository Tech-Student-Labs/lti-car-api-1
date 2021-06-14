using System.Collections.Generic;
using CarDealerAPIService.App.models;

namespace CarDealerAPIService.services
{
    public interface IVehicleService
    {
        List<Vehicle> GetAllVehicles();
        Vehicle GetVehicle(int id);
        int AddVehicle(Vehicle vehicle);
        void AddRangeOfVehicles(Vehicle[] vehicles);
        void UpdateVehicle(Vehicle vehicle);
        void DeleteVehicle(Vehicle vehicle);
        void DeleteVehicleById(int id);
        void DestroyDatabase();

        List<int> GetMarketValues();
    }
}