using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class SegmentationAdvanceFilterDtoWithId : SegmentationAdvanceFilterDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}