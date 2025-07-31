using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class LoginDto : DtoBridge
    {
        [Required]
        public string ReferenceId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? Token { get; set; }
    }
}