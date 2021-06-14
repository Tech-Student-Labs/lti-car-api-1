using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using CarDealerAPIService.services;
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

        // test vin: KL79MMS22MB176461

        // GET
        [HttpGet("{vin}")]
        public async Task<IActionResult> GetMarketValue(string vin)
        {
            return Ok(await _marketValueService.GetAverageVehiclePrice(vin));
        }
    }
}