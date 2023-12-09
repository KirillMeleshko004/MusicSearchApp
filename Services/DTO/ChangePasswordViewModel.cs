using System.ComponentModel.DataAnnotations;
 
namespace MusicSearchApp.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string UserName { get; set; } = null!;
        
        [Required]
        public string CurrentPassword { get; set; } = null!;
 
 
        [Required]
        public string NewPassword { get; set; } = null!;
 
        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords are not same")]
        public string PasswordConfirm { get; set; } = null!;
    }
}