using System.Collections.Generic;
using System.Linq;
using System;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CarDealerAPIService.services
{
    public class VehicleSubmissionsService : IVehicleSubmissionsService
    {
        private readonly CarDealerContext _db;

        private readonly IVehicleMarketValueService _vehicleMarketValueService;

        public VehicleSubmissionsService(CarDealerContext db, IVehicleMarketValueService vehicleMarketValueService)
        {
            _db = db;
            _vehicleMarketValueService = vehicleMarketValueService;
        }

        public List<VehicleSubmissionsDTO> GetAllVehicleSubmissionsByUser(string Id)
        {
            User MyUser = new User() {Id = Id};

            var ListOfSubmissions = _db.VehicleSubmissions
                .Where(v => v.UserId == Id)
                .Select(x => new VehicleSubmissionsDTO {TimeStamp = x.TimeStamp, Vehicle = x.Vehicle})
                .ToList();
 
            return ListOfSubmissions;
        }

        public async Task AddVehicleSubmission(VehicleSubmissions submission)
        {
            if (_db.UserTable.FirstOrDefault(e => e.Id == submission.UserId) == null)
                throw new ArgumentException("User not found");
            if (_db.VehicleInventory.FirstOrDefault(e => e.Id == submission.VehicleId) == null)
                throw new ArgumentException("Vehicle not found");
            if (_db.VehicleSubmissions.FirstOrDefault(e => e.VehicleId == submission.VehicleId) != null)
                throw new ArgumentException("Vehicle already used in previous submission");

            var vehicle = await _db.VehicleInventory.FirstOrDefaultAsync(x => x.Id == submission.VehicleId);

            submission.Vehicle.MarketValue =
                int.Parse(await _vehicleMarketValueService.GetAverageVehiclePrice(vehicle.VinNumber));
            await _db.VehicleSubmissions.AddAsync(submission);
            await _db.SaveChangesAsync();
        }

        public void UpdateVehicleSubmission(VehicleSubmissions submission)
        {
            if (submission == null) throw new System.ArgumentNullException();
            _db.VehicleSubmissions.Update(submission);
            _db.SaveChanges();
        }

        public void DeleteVehicleSubmission(VehicleSubmissions submission)
        {
            if (submission == null) throw new System.ArgumentNullException();
            _db.VehicleSubmissions.Remove(submission);
            _db.SaveChanges();
        }

        public void DeleteVehicleSubmissionById(string Id)
        {
            var submissionToDelete = _db.VehicleSubmissions.FirstOrDefault(e => e.Id == Id);
            if (submissionToDelete == null) throw new System.ArgumentOutOfRangeException();
            _db.VehicleSubmissions.Remove(submissionToDelete);
            _db.SaveChanges();
        }

        public VehicleSubmissions GetVehicleSubmissionsByVIN(string vin)
        {
            var submission =  _db.VehicleSubmissions.FirstOrDefault(x => x.Vehicle.VinNumber == vin);

            if (submission == null || submission.Vehicle == null || submission.User == null)
            {
                throw new System.NullReferenceException();
            }

            return submission;
        }
    }
}