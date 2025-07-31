using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class SegmentationAdvanceFilterMastersDtoWithId : SegmentationAdvanceFilterMastersDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}