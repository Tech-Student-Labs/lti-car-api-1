using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarDealerAPIService.services;
using CarDealerAPIService.App.models;


namespace CarDealerWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleSubmissionsController : ControllerBase
    {
        private readonly IVehicleSubmissionsService _service;
        private readonly IVehicleService _vehicleService;

        public VehicleSubmissionsController(IVehicleSubmissionsService service, IVehicleService vehicleService)
        {
            _service = service;
            _vehicleService = vehicleService;
        }

        [HttpGet("{UserId}")]
        public IActionResult GetAllVehicleSubmissionsByUser(string UserId)
        {
            return Ok(_service.GetAllVehicleSubmissionsByUser(UserId));
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicleSubmission(VehicleSubmissions submission)
        {
            submission.VehicleId = _vehicleService.AddVehicle(submission.Vehicle);
            await _service.AddVehicleSubmission(submission);
            return Ok("Vehicle submission added");
        }

        [HttpPut]
        public IActionResult UpdateVehicleSubmission(VehicleSubmissions submission)
        {
            _service.UpdateVehicleSubmission(submission);
            return Ok("Vehicle submission updated");
        }

        [HttpDelete]
        public IActionResult DeleteVehicleSubmission(VehicleSubmissions submission)
        {
            _service.DeleteVehicleSubmission(submission);
            return Ok("Vehicle submission deleted");
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteVehicleSubmissionById(string Id)
        {
            _service.DeleteVehicleSubmissionById(Id);
            return Ok("Vehicle submission deleted");
        }
    }
}