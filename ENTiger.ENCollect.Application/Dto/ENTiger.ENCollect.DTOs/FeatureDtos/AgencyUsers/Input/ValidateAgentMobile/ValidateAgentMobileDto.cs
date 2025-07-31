using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ValidateAgentMobileDto : DtoBridge
    {
        [Required]
        public string MobileNo { get; set; }
    }
}