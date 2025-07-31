using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class DeactivateAgentDto : DtoBridge
    {
        [Required]
        public List<string> AgentIds { get; set; }
        [Required]
        public string? DeactivationReason { get; set; }
    }
}