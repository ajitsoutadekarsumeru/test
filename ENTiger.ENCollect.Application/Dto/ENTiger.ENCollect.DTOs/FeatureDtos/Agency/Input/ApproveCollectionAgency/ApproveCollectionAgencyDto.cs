using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class ApproveCollectionAgencyDto : DtoBridge
    {
        [Required]
        public List<string> AgencyIds { get; set; }
    }
}