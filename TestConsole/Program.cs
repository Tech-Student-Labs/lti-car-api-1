using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Formatting;

namespace TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            // client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            string vin = "KL79MMS22MB176461";
            // var stringTask = await client.GetStringAsync($"http://marketvalue.vinaudit.com/getmarketvalue.php?key=VA_DEMO_KEY&vin={vin}&format=json&period=90&mileage=average");

            // var msg = stringTask;
            // Console.WriteLine(msg);

            HttpResponseMessage message = await client.GetAsync($"http://marketvalue.vinaudit.com/getmarketvalue.php?key=VA_DEMO_KEY&vin={vin}&format=json&period=90&mileage=average");

            VehiclePriceRequest request = await message.Content.ReadAsAsync<VehiclePriceRequest>();
            Console.WriteLine(request.Prices.Average.ToString("$0.00"));
        }
    }
}
