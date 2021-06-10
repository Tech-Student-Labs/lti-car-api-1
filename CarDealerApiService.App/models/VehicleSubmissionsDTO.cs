using System;

namespace CarDealerAPIService.App.models
{
    public class VehicleSubmissionsDTO
    {
        public DateTime? TimeStamp { get; set; }
        public Vehicle Vehicle { get; set; }

    }
}