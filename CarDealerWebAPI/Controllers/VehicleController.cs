using System;
using System.Linq;
using System.Runtime.InteropServices;
using CarDealerApiService.App.Data;
using CarDealerApiService.App.models;
using CarDealerApiService.services;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerWebApi.Controllers
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

        [HttpGet("MarketValue")]
        public IActionResult GetVehicleMarketValue()
        {
            return Ok(_service.GetMarketValues());
        }
    }
}