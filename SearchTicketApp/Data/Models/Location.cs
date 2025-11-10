using System.ComponentModel.DataAnnotations;

namespace SearchTicketApp.Data.Models
{
    public class Location
    {
        [Required, Key]
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [MaxLength(200)] public string Settlement { get; set; } = null!;

    }
}
