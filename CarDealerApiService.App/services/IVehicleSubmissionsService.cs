using System.Collections.Generic;
using CarDealerAPIService.App.models;

namespace CarDealerAPIService.services
{
    public interface IVehicleSubmissionsService
    {
        List<VehicleSubmissionsDTO> GetAllVehicleSubmissionsByUser(string Id);
        void AddVehicleSubmission(VehicleSubmissions submission);
        void UpdateVehicleSubmission(VehicleSubmissions submission);
        void DeleteVehicleSubmission(VehicleSubmissions submission);
    }
}