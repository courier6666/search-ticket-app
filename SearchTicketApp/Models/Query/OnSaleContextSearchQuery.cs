using Microsoft.AspNetCore.Mvc;

namespace SearchTicketApp.Models.Query
{
    public class OnSaleContextSearchQuery : OnSaleSearchQuery
    {
        public bool MyTimeZone { get; set; }
        public string? ContextOption { get; set; }
        public bool ClosestToMe => ContextOption == nameof(ClosestToMe);

        public bool MostPopular => ContextOption == nameof(MostPopular);

        public bool MostRelevant => ContextOption == nameof(MostRelevant);
    }
}
