using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AgencyIdentificationDocDtoWithId : AgencyIdentificationDocDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}