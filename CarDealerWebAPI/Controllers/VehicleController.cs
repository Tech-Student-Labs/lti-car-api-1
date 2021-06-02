using System.Linq;
using CarDealerAPIService.App.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : Controller
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
            return Ok(_db.VehicleInventory.ToList());
        }
    }
}