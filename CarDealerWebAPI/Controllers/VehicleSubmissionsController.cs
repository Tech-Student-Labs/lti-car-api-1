using System;
using System.Linq;
using System.Threading.Tasks;
using CarDealerAPIService.App.Data;
using Microsoft.AspNetCore.Mvc;
using CarDealerAPIService.services;
using CarDealerAPIService.App.models;


namespace CarDealerWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleSubmissionsController : ControllerBase
    {
        private readonly IVehicleSubmissionsService _service;
        private readonly IVehicleService _vehicleService;
        private readonly IVehicleMarketValueService _marketPrice;
        private readonly CarDealerContext _context;
        
        public VehicleSubmissionsController(IVehicleSubmissionsService service, IVehicleService vehicleService, IVehicleMarketValueService marketPrice,CarDealerContext context)
        {
            _service = service;
            _vehicleService = vehicleService;
            _marketPrice = marketPrice;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllVehicleSubmissions()
        {
            return Ok(_service.GetAllVehicleSubmissions());
        }
        [HttpGet("{UserId}")]
        public IActionResult GetAllVehicleSubmissionsByUser(string UserId)
        {
            return Ok(_service.GetAllVehicleSubmissionsByUser(UserId));
        }
        //this should add to both the vehicle table and the vehicle submission table 
        //and will need a admin to add it to the vehicle listing table to be seen by everyone.
        [HttpPost]
        public async Task<IActionResult> AddVehicleSubmission(VehicleSubmissions submission)
        {
            //assign and pass it into AddVehicleSubmission
            var foundVehicle = _context.VehicleInventory.ToList()
                .FirstOrDefault(x => x.VinNumber == submission.Vehicle.VinNumber);
            if (foundVehicle != null)
                throw new Exception("Already Submitted that Vehicle");
            var price = Int32.Parse(await _marketPrice.GetAverageVehiclePrice(submission.Vehicle.VinNumber));
            submission.VehicleId = _vehicleService.AddVehicle(submission.Vehicle);
            await _service.AddVehicleSubmission(submission,price);
            return Ok(new {message = "Vehicle submission added"});
        }

        [HttpPut]
        public IActionResult  UpdateVehicleSubmission(VehicleSubmissions submission)
        {
            _service.UpdateVehicleSubmission(submission);
            return Ok(new {message = "Vehicle submission updated"});
        }

        [HttpDelete]
        public IActionResult DeleteVehicleSubmission(VehicleSubmissions submission)
        {
            _service.DeleteVehicleSubmission(submission);
            return Ok(new {message = "Vehicle submission deleted"});
        }

        // [HttpDelete("{Id}")]
        // public IActionResult DeleteVehicleSubmissionById(string Id)
        // {
        //     _service.DeleteVehicleSubmissionById(Id);
        //     return Ok(new {message = "Vehicle submission deleted"});
        // }
        //
        
        [HttpDelete("{vin}")]
        public IActionResult DeleteVehicleSubmissionByVin(string vin)
        {
            _service.DeleteVehicleSubmissionByVIN(vin);
            return Ok(new {message = "Vehicle submission deleted"});
        }
    }
}