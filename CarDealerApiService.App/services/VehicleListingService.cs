using System;
using System.Collections.Generic;
using System.Linq;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using Microsoft.EntityFrameworkCore;

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
        
        public bool TrueForNonDuplicateVins(string vin)
        {
            var foundListing = _db.VehicleListings.Include(x => x.Vehicle).FirstOrDefault(x => x.Vehicle.VinNumber == vin);
            if (foundListing != null)
            {
                throw new Exception("You Already Have An Vehicle Listing");
            }
            return true;
            
        }

        public List<VehicleListing> GetAllVehicleListings()
        {
            return _db.VehicleListings.Include(x => x.Vehicle).ToList();
        }
        
        public bool DeleteVehicleListings(string vin)
        {
            var vehicleListingToDelete = _db.VehicleListings.FirstOrDefault(x=>x.Vehicle.VinNumber == vin);
            _db.VehicleListings.Remove(vehicleListingToDelete ?? throw new InvalidOperationException("Cannot Delete Null"));
            
            return true;
        }
    }
}