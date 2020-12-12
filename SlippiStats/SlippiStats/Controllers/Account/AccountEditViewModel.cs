using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SlippiStats.Controllers
{
    public class AccountEditViewModel : IValidatableObject
    {
        public string Message { get; set; }

        [Required(ErrorMessage = "Old password is required.")]
        [MaxLength(50, ErrorMessage = "Old password cannot exceed 50 characters.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        [MaxLength(50, ErrorMessage = "New password cannot exceed 50 characters.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm new password is required.")]
        [MaxLength(50, ErrorMessage = "Confirm new password cannot exceed 50 characters.")]
        public string ConfirmNewPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ConfirmNewPassword != NewPassword)
            {
                yield return new ValidationResult(
                    "Confirm new password does not match new password.",
                    new[] { nameof(ConfirmNewPassword) });
            }
        }
    }
}