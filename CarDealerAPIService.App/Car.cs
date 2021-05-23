using System.Text;

namespace CarDealerAPIService.App
{
    public class Car : ICar
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string VIN { get; set; }

        private decimal _value;

        public Car(string make, string model, string year, string vin, decimal value)
        {
            Make = make;
            Model = model;
            Year = year;
            VIN = vin;
            _value = value;
        }

        public string GetValue()
        {
            return _value.ToString("C0");
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append($"Make: {Make}\n")
                .Append($"Model: {Model}\n")
                .Append($"Year: {Year}\n")
                .Append($"VIN: {VIN}\n")
                .Append($"Value: {GetValue()}");

            return builder.ToString();
        }
    }
}