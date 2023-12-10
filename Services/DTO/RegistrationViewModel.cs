using System.ComponentModel.DataAnnotations;
 
namespace MusicSearchApp.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; } = null!;
 
        [Required]
        [MaxLength(30)]
        public string Password { get; set; } = null!;
 
        [Required]
        [Compare("Password", ErrorMessage = "Passwords are not same")]
        public string PasswordConfirm { get; set; } = null!;
    }
}