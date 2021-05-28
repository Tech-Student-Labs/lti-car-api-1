using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CarDealerAPIService.App.models
{
    public class VehiclePrice
    {
        [JsonProperty("vin")] public string Vin { get; set; }

        [JsonProperty("success")] public bool Success { get; set; }

        [JsonProperty("vehicle")] public static string Vehicle { get; set; }

        [JsonProperty("mileage")] public long Mileage { get; set; }

        [JsonProperty("count")] public long Count { get; set; }

        [JsonProperty("mean")] public long Mean { get; set; }

        [JsonProperty("stdev")] public long Stdev { get; set; }

        [JsonProperty("certainty")] public long Certainty { get; set; }

        [JsonProperty("period")] public List<DateTimeOffset> Period { get; set; }

        [JsonProperty("prices")] public Prices Prices { get; set; }
        
    }

    public class Prices
    {
        [JsonProperty("average")] public long Average { get; set; }

        [JsonProperty("below")] public long Below { get; set; }

        [JsonProperty("above")] public long Above { get; set; }
    }
}