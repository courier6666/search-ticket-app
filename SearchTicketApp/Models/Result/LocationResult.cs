using System.ComponentModel.DataAnnotations;
using SearchTicketApp.Data.Models.Abstract;

namespace SearchTicketApp.Models.Result
{
    public class LocationResult : Entity
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Settlement { get; set; } = null!;
    }
}
