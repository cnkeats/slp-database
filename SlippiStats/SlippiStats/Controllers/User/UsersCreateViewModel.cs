using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SlippiStats.Controllers
{
    public class UsersCreateViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        [MaxLength(50, ErrorMessage = "Confirm password cannot exceed 50 characters.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ConfirmPassword != Password)
            {
                yield return new ValidationResult(
                    "Confirm password does not match password.",
                    new[] { nameof(ConfirmPassword) });
            }
        }
    }
}