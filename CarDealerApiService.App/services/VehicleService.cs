using System.Collections.Generic;
using System.Linq;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;

namespace CarDealerAPIService.services
{
    public class VehicleService : IVehicleService
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

        public Vehicle GetVehicle(int id)
        {
            var result = _db.VehicleInventory.FirstOrDefault(s => s.Id == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new System.ArgumentNullException(nameof(id),$"id:{id} not found in database.");
            }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null) throw new System.ArgumentNullException(nameof(vehicle),"The vehicle you are trying to add is null");
            _db.VehicleInventory.Add(vehicle);
            _db.SaveChanges();
        }

        public void AddRangeOfVehicles(Vehicle[] vehicles)
        {
            if (vehicles == null) throw new System.ArgumentNullException(nameof(vehicles),"The vehicle you are trying to add is null");
            _db.VehicleInventory.AddRange(vehicles);
            _db.SaveChanges();
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            if (vehicle == null) throw new System.ArgumentNullException(nameof(vehicle),"The vehicle you are trying to update is null");
            _db.VehicleInventory.Update(vehicle);
            _db.SaveChanges();
        }

        public void DeleteVehicle(Vehicle vehicle)
        {
            if (vehicle == null) throw new System.ArgumentNullException(nameof(vehicle),"The vehicle you are trying to delete is null");
            _db.VehicleInventory.Remove(vehicle);
            _db.SaveChanges();
        }

        public void DestroyDatabase()
        {
            var entities = _db.VehicleInventory.ToList();
            _db.VehicleInventory.RemoveRange(entities);
            _db.SaveChanges();
        }
    }
}