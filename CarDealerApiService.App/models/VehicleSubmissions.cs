using System;
using CarDealerApiService.App.models;

namespace CarDealerAPIService.App.models
{
    public class VehicleSubmissions
    {
        public string Id { get; set; }
        public User User { get; set; }
        public DateTime? TimeStamp { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}