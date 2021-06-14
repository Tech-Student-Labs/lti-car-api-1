using System.Text.Json.Serialization;

namespace CarDealerAPIService.App.models
{
    public class Vehicle
    {
        [JsonIgnore] public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string VinNumber { get; set; }
        [JsonIgnore] public int MarketValue { get; set; }
    }
}