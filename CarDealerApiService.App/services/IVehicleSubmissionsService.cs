using System.Collections.Generic;
using CarDealerAPIService.App.models;

namespace CarDealerAPIService.services
{
    public interface IVehicleSubmissionsService
    {
        public List<VehicleSubmissions> GetAllVehicleSubmissionsByUser(string Id);
    }
}