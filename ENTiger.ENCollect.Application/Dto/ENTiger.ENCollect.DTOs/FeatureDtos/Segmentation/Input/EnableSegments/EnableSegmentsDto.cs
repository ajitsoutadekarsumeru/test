using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class EnableSegmentsDto : DtoBridge
    {
        [StringLength(32)]
        public string? Id { get; set; }

        public List<string> SegmentIds { get; set; }
    }
}