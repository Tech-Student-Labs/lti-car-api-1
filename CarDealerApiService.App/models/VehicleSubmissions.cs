using System;
using System.Text.Json.Serialization;

namespace CarDealerAPIService.App.models
{
    public class VehicleSubmissions
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int VehicleId { get; set; }
        [JsonIgnore]
        public Vehicle Vehicle { get; set; }
    }
}