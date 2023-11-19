using System.ComponentModel.DataAnnotations;
 
namespace MusicSearchApp.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        public string UserName { get; set; } = null!;
 
        [Required]
        public string Password { get; set; } = null!;
 
        [Required]
        [Compare("Password", ErrorMessage = "Passwords are not same")]
        public string PasswordConfirm { get; set; } = null!;
    }
}