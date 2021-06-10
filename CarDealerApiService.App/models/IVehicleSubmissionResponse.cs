using System;

namespace CarDealerApiService.App.models
{
    public interface IVehicleSubmissionResponse
    {
        public DateTime? TimeStamp { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}