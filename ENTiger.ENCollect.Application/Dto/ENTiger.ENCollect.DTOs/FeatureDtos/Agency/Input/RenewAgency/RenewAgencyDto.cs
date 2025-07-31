using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class RenewAgencyDto : DtoBridge
    {
        [Required]
        public List<string> agencyIds { get; set; }

        public DateTime NewExpiryDate { get; set; }
    }
}