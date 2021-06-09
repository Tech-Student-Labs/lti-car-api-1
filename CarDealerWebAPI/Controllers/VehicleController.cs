using System;
using System.Linq;
using System.Runtime.InteropServices;
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
        private readonly IVehicleService _service;

        public VehicleController(IVehicleService service)
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

        [HttpGet("{id}/MarketValue")]
        public IActionResult GetVehicleMarketValue(int id)
        {
            return Ok(_service.GetMarketValue(id));
        }
    }
}