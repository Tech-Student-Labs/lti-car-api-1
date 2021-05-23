namespace CarDealerAPIService.App
{
    public interface ICar
    {
         public string Make { get; set; }
         public string Model { get; set; }
         public string Year { get; set; }
         public string VIN { get; set; }
    }
}