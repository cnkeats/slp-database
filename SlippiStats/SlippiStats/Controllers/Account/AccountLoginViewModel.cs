using System.ComponentModel.DataAnnotations;

namespace SlippiStats.Controllers
{
    public class AccountLoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
        public string Password { get; set; }

        public bool IsPersistent { get; set; }

        public string ReturnUrl { get; set; }
    }
}