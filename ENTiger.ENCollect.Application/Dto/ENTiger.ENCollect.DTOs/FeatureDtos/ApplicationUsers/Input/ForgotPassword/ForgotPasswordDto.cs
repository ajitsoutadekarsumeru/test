using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ForgotPasswordDto : DtoBridge
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string ReferenceId { get; set; }
    }
}