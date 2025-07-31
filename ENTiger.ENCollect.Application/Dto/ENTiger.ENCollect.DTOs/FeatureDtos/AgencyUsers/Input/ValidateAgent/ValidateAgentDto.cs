using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ValidateAgentDto : DtoBridge
    {
        [Required]
        public string EmailId { get; set; }

        [Required]
        public string MobileNo { get; set; }
    }
}