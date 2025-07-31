using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class RenewAgentDto : DtoBridge
    {
        [Required]
        public List<string> agentIds { get; set; }

        public DateTime NewExpiryDate { get; set; }
    }
}