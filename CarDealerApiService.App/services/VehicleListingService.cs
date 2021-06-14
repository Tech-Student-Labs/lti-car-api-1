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

        public List<VehicleListing> GetAllVehicleListings()
        {
            return _db.VehicleListings.ToList();
        }
    }
}