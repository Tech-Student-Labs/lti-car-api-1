using System.Collections.Generic;
using Newtonsoft.Json;

namespace CarDealerAPIService.App.models
{
  public class VehicleDTO
  {
    [JsonProperty("input")] public Input Input { get; set; }

    [JsonProperty("selections")] public Selections Selections { get; set; }

    [JsonProperty("attributes")] public Attributes Attributes { get; set; }

    [JsonProperty("success")] public bool? Success { get; set; }

    [JsonProperty("error")] public string Error { get; set; }
  }

  public class Attributes
  {
    [JsonProperty("year")] public string Year { get; set; }

    [JsonProperty("make")] public string Make { get; set; }

    [JsonProperty("model")] public string Model { get; set; }

    [JsonProperty("trim")] public string Trim { get; set; }

    [JsonProperty("style")] public string Style { get; set; }

    [JsonProperty("type")] public string Type { get; set; }

    [JsonProperty("size")] public string Size { get; set; }

    [JsonProperty("category")] public string Category { get; set; }

    [JsonProperty("made_in")] public string MadeIn { get; set; }

    [JsonProperty("made_in_city")] public string MadeInCity { get; set; }

    [JsonProperty("doors")] public string Doors { get; set; }

    [JsonProperty("fuel_type")] public string FuelType { get; set; }

    [JsonProperty("fuel_capacity")] public string FuelCapacity { get; set; }

    [JsonProperty("city_mileage")] public string CityMileage { get; set; }

    [JsonProperty("highway_mileage")] public string HighwayMileage { get; set; }

    [JsonProperty("engine")] public string Engine { get; set; }

    [JsonProperty("engine_size")] public string EngineSize { get; set; }

    [JsonProperty("engine_cylinders")] public string EngineCylinders { get; set; }

    [JsonProperty("transmission")] public string Transmission { get; set; }

    [JsonProperty("transmission_type")] public string TransmissionType { get; set; }

    [JsonProperty("transmission_speeds")] public string TransmissionSpeeds { get; set; }

    [JsonProperty("drivetrain")] public string Drivetrain { get; set; }

    [JsonProperty("anti_brake_system")] public string AntiBrakeSystem { get; set; }

    [JsonProperty("steering_type")] public string SteeringType { get; set; }

    [JsonProperty("curb_weight")] public string CurbWeight { get; set; }

    [JsonProperty("gross_vehicle_weight_rating")]
    public string GrossVehicleWeightRating { get; set; }

    [JsonProperty("overall_height")] public string OverallHeight { get; set; }

    [JsonProperty("overall_length")] public string OverallLength { get; set; }

    [JsonProperty("overall_width")] public string OverallWidth { get; set; }

    [JsonProperty("wheelbase_length")] public string WheelbaseLength { get; set; }

    [JsonProperty("standard_seating")] public string StandardSeating { get; set; }

    [JsonProperty("invoice_price")] public string InvoicePrice { get; set; }

    [JsonProperty("delivery_charges")] public string DeliveryCharges { get; set; }

    [JsonProperty("manufacturer_suggested_retail_price")]
    public string ManufacturerSuggestedRetailPrice { get; set; }
  }

  public class Input
  {
    [JsonProperty("key")] public string Key { get; set; }

    [JsonProperty("year")] public string Year { get; set; }

    [JsonProperty("make")] public string Make { get; set; }

    [JsonProperty("model")] public string Model { get; set; }

    [JsonProperty("trim")] public string Trim { get; set; }

    [JsonProperty("format")] public string Format { get; set; }
  }

  public class Selections
  {
    [JsonProperty("trims")] public List<Trim> Trims { get; set; }
  }

  public class Trim
  {
    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("selected")] public long? Selected { get; set; }

    [JsonProperty("styles")] public List<Style> Styles { get; set; }
  }

  public class Style
  {
    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("name")] public string Name { get; set; }
  }
}