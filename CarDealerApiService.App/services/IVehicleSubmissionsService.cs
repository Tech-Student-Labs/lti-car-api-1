using System.Collections.Generic;
using System.Threading.Tasks;
using CarDealerAPIService.App.models;

namespace CarDealerAPIService.services
{
    public interface IVehicleSubmissionsService
    {
        List<VehicleSubmissionsDTO> GetAllVehicleSubmissionsByUser(string Id);
        Task AddVehicleSubmission(VehicleSubmissions submission);
        void UpdateVehicleSubmission(VehicleSubmissions submission);
        void DeleteVehicleSubmission(VehicleSubmissions submission);
        void DeleteVehicleSubmissionById(string Id);
    }
}