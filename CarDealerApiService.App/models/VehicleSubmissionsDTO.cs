using System;

namespace CarDealerApiService.App.models
{
    public class VehicleSubmissionsDTO
    {
        public DateTime? TimeStamp { get; set; }
        public Vehicle Vehicle { get; set; }

    }
}