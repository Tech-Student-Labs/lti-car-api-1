using System.Collections.Generic;
using System.Linq;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;

namespace CarDealerWebAPI.services
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

        public Vehicle GetVehicle(int id)
        {
            var result = _db.VehicleInventory.FirstOrDefault(s => s.Id == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new System.Exception();
            }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            try
            {
                _db.VehicleInventory.Add(vehicle);
                _db.SaveChanges();
            }
            catch
            {
                throw new System.Exception();
            }
        }

        public void AddRangeOfVehicles(Vehicle[] vehicles)
        {
            try
            {
                _db.VehicleInventory.AddRange(vehicles);
                _db.SaveChanges();
            }
            catch
            {
                throw new System.Exception();
            }
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                _db.VehicleInventory.Update(vehicle);
                _db.SaveChanges();
            }
            catch
            {
                throw new System.Exception();
            }
        }

        public void DeleteVehicle(Vehicle vehicle)
        {
            try
            {
                _db.VehicleInventory.Remove(vehicle);
                _db.SaveChanges();
            }
            catch
            {
                throw new System.Exception();
            }
        }

        public void DestroyDatabase()
        {
            var entities = _db.VehicleInventory.ToList();
            _db.VehicleInventory.RemoveRange(entities);
            _db.SaveChanges();
        }
    }
}