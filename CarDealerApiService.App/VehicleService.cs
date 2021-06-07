using System.Collections.Generic;
using System.Linq;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;

namespace CarDealerAPIService.App
{
    public class VehicleService
    {
        private readonly CarDealerContext _db;

        public VehicleService(CarDealerContext db)
        {
            _db = db;
        }

        public List<Vehicle> GetAllVehicles()
        {
            return _db.VehicleInventory.ToList();
        }
        
    }
}