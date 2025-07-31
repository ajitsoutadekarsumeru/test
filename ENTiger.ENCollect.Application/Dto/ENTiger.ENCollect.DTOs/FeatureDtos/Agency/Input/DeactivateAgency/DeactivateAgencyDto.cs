using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class DeactivateAgencyDto : DtoBridge
    {
        [Required]
        public string DeactivationReason { get; set; }
        [Required]
        public List<string> AgencyIds { get; set; }
    }
}