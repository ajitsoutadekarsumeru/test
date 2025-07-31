using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AgencyTypeDtoWithId : AgencyTypeDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}