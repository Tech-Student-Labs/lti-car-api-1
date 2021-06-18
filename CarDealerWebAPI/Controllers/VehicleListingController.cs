using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
using Microsoft.AspNetCore.Authorization;
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
        //only admin can add to vehicle listing which is the list that any users can see 
        [Authorize(Roles = "AdminUser")]
        [HttpPost]
        public IActionResult AddToVehicleListing([FromBody] VehicleListing vehicle)
        {
            _service.TrueForNonDuplicateVins(vehicle.Vehicle.VinNumber);
            _service.AddToVehicleListing(vehicle);
            return Ok(new {message = "Created new vehicle"});
        }
        
        [Authorize(Roles = "AdminUser")]
        [HttpDelete("{vin}")]
        public IActionResult DeleteAVehicleListing(string vin)
        {
            _service.DeleteVehicleListings(vin);
            return Ok(new {message = $"Deleted the vehicle with vin {vin}"});
        }
    }
}