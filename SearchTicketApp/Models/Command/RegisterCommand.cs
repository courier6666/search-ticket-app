using System.ComponentModel.DataAnnotations;

namespace SearchTicketApp.Models.Command
{
    public class RegisterCommand
    {
        [EmailAddress, Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
