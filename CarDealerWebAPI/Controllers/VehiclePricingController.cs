using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CarDealerApiService.App.Data;
using CarDealerApiService.App.models;
using CarDealerApiService.services;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerWebApi.Controllers
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