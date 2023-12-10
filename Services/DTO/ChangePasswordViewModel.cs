using System.ComponentModel.DataAnnotations;
 
namespace MusicSearchApp.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; } = null!;
        
        [Required]
        [MaxLength(30)]
        public string CurrentPassword { get; set; } = null!;
 
 
        [Required]
        [MaxLength(30)]
        public string NewPassword { get; set; } = null!;
 
        [Required]
        [MaxLength(30)]
        [Compare("NewPassword", ErrorMessage = "Passwords are not same")]
        public string PasswordConfirm { get; set; } = null!;
    }
}