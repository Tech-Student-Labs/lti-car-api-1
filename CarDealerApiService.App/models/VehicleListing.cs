
using System.Text.Json.Serialization;

namespace CarDealerAPIService.App.models
{
    public class VehicleListing
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        [JsonIgnore] public Vehicle Vehicle { get; set; }
        public int Price {get; set;}
    }
}