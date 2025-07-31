using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class LogoutDto : DtoBridge
    {
        [Required]
        public string Token { get; set; }
    }
}