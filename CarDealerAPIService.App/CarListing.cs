using System.Text;

namespace CarDealerAPIService.App
{
    public class CarListing
    {
        public string PreMessage { get; set; }
        public Car Car { get; set; }
        public string PostMessage { get; set; }

        public CarListing(string preMessage, Car car, string postMessage)
        {
            PreMessage = preMessage;
            Car = car;
            PostMessage = postMessage;
        }

        public string BuildListing()
        {
            var builder = new StringBuilder();

            if(PreMessage != null)
            {
                builder.Append($"{PreMessage} ");
            }

            builder.Append($"{Car.Year} {Car.Make} {Car.Model}");

            if(PostMessage != null)
            {
                builder.Append($" {PostMessage}");
            }

            return builder.ToString();
        }
    }
}