using CarDealerAPIService.App.models;
using System.Collections.Generic;

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