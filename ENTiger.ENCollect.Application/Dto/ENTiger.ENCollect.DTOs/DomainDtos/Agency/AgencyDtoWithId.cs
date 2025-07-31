using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AgencyDtoWithId : AgencyDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}