using System.Collections.Generic;
using System.Linq;
using CarDealerApiService.App.Data;
using CarDealerApiService.App.models;
using Microsoft.EntityFrameworkCore;

namespace CarDealerApiService.services
{
    public class VehicleSubmissionsService : IVehicleSubmissionsService
    {
        private readonly CarDealerContext _db;
        public VehicleSubmissionsService(CarDealerContext db)
        {
            _db = db;
        }

        public List<VehicleSubmissionsDTO> GetAllVehicleSubmissionsByUser(string Id)
        {
            User MyUser = new User() {Id = Id};
            var ListOfSubmissions = _db.VehicleSubmissions
                .Where(v => v.User.Id == Id)
                .Select(x => new VehicleSubmissionsDTO { TimeStamp = x.TimeStamp, Vehicle = x.Vehicle })
                .ToList();
            return ListOfSubmissions;
        }

    }
}