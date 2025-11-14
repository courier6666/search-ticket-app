using System.ComponentModel.DataAnnotations;
using SearchTicketApp.Data.Models.Abstract;

namespace SearchTicketApp.Models.Dto
{
    public class LocationCommand : Entity
    {

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        [MaxLength(200)] public string Settlement { get; set; } = null!;

    }
}
