namespace CarDealerAPIService.App.models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string VinNumber { get; set; }
        public int MarketValue { get; set; }
    }
}