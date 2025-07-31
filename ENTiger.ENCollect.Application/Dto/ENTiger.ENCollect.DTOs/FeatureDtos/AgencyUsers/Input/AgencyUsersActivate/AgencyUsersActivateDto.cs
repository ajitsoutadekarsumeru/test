using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersActivateDto : DtoBridge
    {
        [Required]
        public List<string> Ids { get; set; }
    }

}
