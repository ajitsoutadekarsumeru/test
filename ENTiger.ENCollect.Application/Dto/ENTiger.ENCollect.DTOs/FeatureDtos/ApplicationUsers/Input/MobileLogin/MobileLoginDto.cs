using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class MobileLoginDto : DtoBridge
    {
        [Required]
        public string ReferenceId { get; set; }

        [Required]
        public string EmailId { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string IMEI { get; set; }
    }
}