using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class SegmentationDtoWithId : SegmentationDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}