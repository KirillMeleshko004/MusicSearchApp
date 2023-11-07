using System.ComponentModel.DataAnnotations;

namespace MusicSearchApp.ViewModels
{
    public class AuthorizationViewModel
    {
        [Required]
        public string UserName { get; set; } = null!;
 
        [Required]
        public string Password { get; set; } = null!;
    }
}