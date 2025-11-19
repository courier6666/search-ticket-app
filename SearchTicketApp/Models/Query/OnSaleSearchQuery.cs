using SearchTicketApp.Data.Models;

namespace SearchTicketApp.Models.Query
{
    public class OnSaleSearchQuery
    {
        public string? Title { get; set; }

        public string? Settlement { get; set; }

        public float? PriceLower { get; set; }

        public float? PriceUpper { get; set; }

        public TravelTransportationType? TravelTransportationType { get; set; }
    }
}
