using System.Linq;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerWebAPI.services;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleService _service;

        public VehicleController(VehicleService service)
        {
            _service = service;
        }

        // GET
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAllVehicles());
        }

        [HttpGet("{id}")]
        public IActionResult GetVehicleById(int id)
        {
            return Ok(_service.GetVehicle(id));
        }
    }
}