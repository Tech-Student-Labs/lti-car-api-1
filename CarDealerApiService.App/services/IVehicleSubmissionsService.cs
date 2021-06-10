using System.Collections.Generic;
using CarDealerApiService.App.models;

namespace CarDealerApiService.services
{
    public interface IVehicleSubmissionsService
    {
        public List<VehicleSubmissionsDTO> GetAllVehicleSubmissionsByUser(string Id);
    }
}