using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AgencyUserIdentificationDocDtoWithId : AgencyUserIdentificationDocDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}