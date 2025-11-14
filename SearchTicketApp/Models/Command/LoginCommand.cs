using System.ComponentModel.DataAnnotations;

namespace SearchTicketApp.Models.Command
{
    public class LoginCommand
    {
        [EmailAddress, Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
