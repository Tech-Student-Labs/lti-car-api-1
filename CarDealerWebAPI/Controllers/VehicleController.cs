using System.Linq;
using CarDealerAPIService.App.Data;
using CarDealerAPIService.App.models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly CarDealerContext _db;

        public VehicleController(CarDealerContext db)
        {
            _db = db;
        }

        // GET
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Hello Galvanize");
        }
    }
}