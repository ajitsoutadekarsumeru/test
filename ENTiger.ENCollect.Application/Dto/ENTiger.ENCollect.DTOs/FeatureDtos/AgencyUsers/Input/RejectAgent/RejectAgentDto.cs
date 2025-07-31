using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class RejectAgentDto : DtoBridge
    {
        [Required]
        public List<string> AgentIds { get; set; }

        public string? Description { get; set; }
    }
}