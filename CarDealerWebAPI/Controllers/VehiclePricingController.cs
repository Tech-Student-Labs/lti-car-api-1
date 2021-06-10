using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerWebAPI.services;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclePricingController : ControllerBase
    {   
        private readonly IVehicleMarketValueService _marketValueService;
        public VehiclePricingController(IVehicleMarketValueService marketValueService)
        {
            _marketValueService = marketValueService;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> GetAMarketValue()
        {
            return Ok(await _marketValueService.GetAverageVehiclePrice("KL79MMS22MB176461"));
        }
    }
}