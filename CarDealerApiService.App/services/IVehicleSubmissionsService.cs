using CarDealerAPIService.App.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDealerAPIService.services
{
    public interface IVehicleSubmissionsService
    {
        List<VehicleSubmissionsDTO> GetAllVehicleSubmissionsByUser(string Id);
        Task AddVehicleSubmission(VehicleSubmissions submission, int price);
        void UpdateVehicleSubmission(VehicleSubmissions submission);
        void DeleteVehicleSubmission(VehicleSubmissions submission);
        void DeleteVehicleSubmissionById(string Id);
        void DeleteVehicleSubmissionByVIN(string vin);
        VehicleSubmissions GetVehicleSubmissionsByVIN(string vin);
        List<VehicleSubmissionsDTO> GetAllVehicleSubmissions();

    }
}