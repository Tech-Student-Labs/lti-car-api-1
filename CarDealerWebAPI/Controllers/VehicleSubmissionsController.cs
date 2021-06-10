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

        public VehicleSubmissionsController(IVehicleSubmissionsService service)
        {
            _service = service;
        }

        [HttpGet("{UserId}")]
        public IActionResult GetAllVehicleSubmissionsByUser(string UserId)
        {
            return Ok(_service.GetAllVehicleSubmissionsByUser(UserId));
        }

        [HttpPost]
        public IActionResult AddVehicleSubmission(VehicleSubmissions submission)
        {
            _service.AddVehicleSubmission(submission);
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
    }
}