using System.ComponentModel.DataAnnotations;

namespace MusicSearchApp.ViewModels
{
    public class AuthorizationViewModel
    {
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; } = null!;
 
        [Required]
        [MaxLength(30)]
        public string Password { get; set; } = null!;
    }
}