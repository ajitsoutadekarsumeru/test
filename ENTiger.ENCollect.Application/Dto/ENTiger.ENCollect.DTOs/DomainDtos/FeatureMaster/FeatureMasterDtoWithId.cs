using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class FeatureMasterDtoWithId : FeatureMasterDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}