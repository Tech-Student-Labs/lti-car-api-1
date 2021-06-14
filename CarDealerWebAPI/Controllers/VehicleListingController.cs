using CarDealerAPIService.services;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleListingController : ControllerBase
    {
        private readonly IVehicleListingService _service;

        public VehicleListingController(IVehicleListingService service)
        {
            _service = service;
        }

          // GET
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAllVehicleListings());
        }
       
    }
}