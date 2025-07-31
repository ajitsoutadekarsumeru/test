using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ValidateAgentEmailDto : DtoBridge
    {
        [Required]
        public string EmailId { get; set; }
    }
}