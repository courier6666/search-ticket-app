using System.ComponentModel.DataAnnotations;
using SearchTicketApp.Data.Models.Abstract;

namespace SearchTicketApp.Models.Query
{
    public class LocationQuery : Entity
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Settlement { get; set; } = null!;
    }
}
