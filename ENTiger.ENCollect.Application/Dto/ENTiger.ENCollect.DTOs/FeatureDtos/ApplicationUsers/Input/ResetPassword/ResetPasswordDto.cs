using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ResetPasswordDto : DtoBridge
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }

        [Required]
        public string ReferenceId { get; set; }
    }
}