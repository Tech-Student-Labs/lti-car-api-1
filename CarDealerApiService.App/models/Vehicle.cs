using System.Collections.Generic;

namespace CarDealerAPIService.App.models
{
//https://specifications.vinaudit.com/v3/specifications?format=json&key=VA_DEMO_KEY&year=2005&make=toyota&model=corolla&trim=ce
    public partial class Vehicle
    {
        public Input Input { get; set; }
        public Selections Selections { get; set; }
        public Attributes Attributes { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }

    public partial class Attributes
    {
        public long? Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public string Style { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Category { get; set; }
        public string MadeIn { get; set; }
        public string MadeInCity { get; set; }
        public long? Doors { get; set; }
        public string FuelType { get; set; }
        public string FuelCapacity { get; set; }
        public string CityMileage { get; set; }
        public string HighwayMileage { get; set; }
        public string Engine { get; set; }
        public string EngineSize { get; set; }
        public string EngineCylinders { get; set; }
        public string Transmission { get; set; }
        public string TransmissionType { get; set; }
        public string TransmissionSpeeds { get; set; }
        public string Drivetrain { get; set; }
        public string AntiBrakeSystem { get; set; }
        public string SteeringType { get; set; }
        public string CurbWeight { get; set; }
        public string GrossVehicleWeightRating { get; set; }
        public string OverallHeight { get; set; }
        public string OverallLength { get; set; }
        public string OverallWidth { get; set; }
        public string WheelbaseLength { get; set; }
        public long? StandardSeating { get; set; }
        public string InvoicePrice { get; set; }
        public string DeliveryCharges { get; set; }
        public string ManufacturerSuggestedRetailPrice { get; set; }
    }

    public partial class Input
    {
        public string Key { get; set; }
        public long? Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public string Format { get; set; }
    }

    public partial class Selections
    {
        public List<Trim> Trims { get; set; }
    }

    public partial class Trim
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long? Selected { get; set; }
        public List<object> Styles { get; set; }
    }
}
