using System.Collections.Generic;
using CarDealerAPIService.App.models;

namespace CarDealerAPIService.services
{
    public interface IVehicleSubmissionsService
    {
        public List<VehicleSubmissionsDTO> GetAllVehicleSubmissionsByUser(string Id);
    }
}