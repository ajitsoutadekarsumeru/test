using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class DormantAgencyUserDto : DtoBridge
    {
        [Required]
        public List<string> AgentIds { get; set; }
    }
}