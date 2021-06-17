using System.Collections.Generic;
using System.Linq;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;

namespace CarDealerAPIService.services
{
    public class VehicleListingService : IVehicleListingService
    {
        private readonly CarDealerContext _db;

        public VehicleListingService(CarDealerContext db)
        {
            _db = db;
        }

        public int AddToVehicleListing(VehicleListing vehicle)
        {
            if (vehicle == null) { throw new System.ArgumentNullException(nameof(vehicle), "The vehicle you are trying to add is null"); }

            _db.VehicleListings.Add(vehicle);
            _db.SaveChanges();
            return vehicle.Id;
            
        }

        public List<VehicleListing> GetAllVehicleListings()
        {
            return _db.VehicleListings.ToList();
        }
    }
}