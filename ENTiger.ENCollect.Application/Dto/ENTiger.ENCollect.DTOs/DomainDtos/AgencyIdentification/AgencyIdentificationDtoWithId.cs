using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AgencyIdentificationDtoWithId : AgencyIdentificationDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}