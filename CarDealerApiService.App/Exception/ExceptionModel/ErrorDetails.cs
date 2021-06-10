namespace CarDealerApiService.App.Exception.ExceptionModel
{
    public class ErrorDetails
    {
        public string Type { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}