using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SlippiStats.Controllers
{
    public class UsersEditViewModel : IValidatableObject
    {
        public string Message { get; set; }

        public bool CanDelete { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string UserName { get; set; }

        [MaxLength(50, ErrorMessage = "New password cannot exceed 50 characters.")]
        public string NewPassword { get; set; }

        [MaxLength(50, ErrorMessage = "Confirm new password cannot exceed 50 characters.")]
        public string ConfirmNewPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(NewPassword))
            {
                if (string.IsNullOrWhiteSpace(ConfirmNewPassword))
                {
                    yield return new ValidationResult(
                        "Confirm new password is required.",
                        new[] { nameof(ConfirmNewPassword) });
                }
                else if (ConfirmNewPassword != NewPassword)
                {
                    yield return new ValidationResult(
                        "Confirm new password does not match new password.",
                        new[] { nameof(ConfirmNewPassword) });
                }
            }
        }
    }
}