using System.Collections.Generic;
using CarDealerAPIService.App.models;

namespace CarDealerAPIService.services
{
    public interface IVehicleListingService
    {
        List<VehicleListing> GetAllVehicleListings();
        int AddToVehicleListing(VehicleListing vehicle);
        bool TrueForNonDuplicateVins(string vin);
        bool DeleteVehicleListings(string vin);

    }   
}