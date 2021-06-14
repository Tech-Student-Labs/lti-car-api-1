using System;
using System.Linq;
using System.Runtime.InteropServices;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _service;

        public VehicleController(IVehicleService service)
        {
            _service = service;
        }

        // GET
        [Authorize]
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

        [HttpGet("MarketValue")]
        public IActionResult GetVehicleMarketValue()
        {
            return Ok(_service.GetMarketValues());
        }

        [HttpPost]
        public IActionResult AddVehicle([FromBody] Vehicle vehicle)
        {
            _service.AddVehicle(vehicle);
            return Ok("Created new vehicle");
        }

        [HttpPost("Range")]
        public IActionResult AddRangeOfVehicles([FromBody] Vehicle[] vehicles)
        {
            _service.AddRangeOfVehicles(vehicles);
            return Ok($"Created {vehicles.Length} new vehicles.");
        }

        [HttpPut]
        public IActionResult UpdateVehicle([FromBody] Vehicle vehicle)
        {
            _service.UpdateVehicle(vehicle);
            return Ok("Vehicle updated");
        }

        [HttpDelete]
        public IActionResult DeleteVehicle(Vehicle vehicle)
        {
            try
            {
                _service.DeleteVehicle(vehicle);
                return Ok($"Vehicle {vehicle.Id} deleted");
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVehicleById(int id)
        {
            try
            {
                _service.DeleteVehicleById(id);
                return Ok($"Vehicle {id} deleted");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}