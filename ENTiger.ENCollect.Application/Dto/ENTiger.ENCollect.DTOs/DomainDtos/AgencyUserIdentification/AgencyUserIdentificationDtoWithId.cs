using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AgencyUserIdentificationDtoWithId : AgencyUserIdentificationDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}