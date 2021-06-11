using System;

namespace CarDealerAPIService.App.models
{
    public class VehicleSubmissions
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int VehicleId { get; set; }
    }
}