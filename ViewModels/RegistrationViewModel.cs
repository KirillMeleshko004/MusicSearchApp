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
        public string PasswordConfirm { get; set; } = null!;
    }
}