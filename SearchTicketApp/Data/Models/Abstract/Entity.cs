using System.ComponentModel.DataAnnotations;

namespace SearchTicketApp.Data.Models.Abstract
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
