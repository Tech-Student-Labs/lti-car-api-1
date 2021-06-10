using Microsoft.AspNetCore.Mvc;
using CarDealerAPIService.services;


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

    }
}