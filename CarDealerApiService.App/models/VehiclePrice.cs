using System;
using System.Collections.Generic;

namespace CarDealerAPIService.App.models
{
    //http://marketvalue.vinaudit.com/getmarketvalue.php?key=VA_DEMO_KEY&vin=1VXBR12EXCP901213&format=json&period=90&mileage=average
    public partial class VehiclePrice
    {
        public string Vin { get; set; }
        public bool Success { get; set; }
        public string Vehicle { get; set; }
        public long? Mileage { get; set; }
        public long? Count { get; set; }
        public long? Mean { get; set; }
        public long? Stdev { get; set; }
        public long? Certainty { get; set; }
        public List<DateTimeOffset> Period { get; set; }
        public Prices Prices { get; set; }
    }

    public partial class Prices
    {
        public long? Average { get; set; }
        public long? Below { get; set; }
        public long? Above { get; set; }
    }
}