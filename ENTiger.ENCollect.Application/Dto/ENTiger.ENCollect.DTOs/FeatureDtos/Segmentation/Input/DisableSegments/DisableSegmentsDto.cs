using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class DisableSegmentsDto : DtoBridge
    {
        [StringLength(32)]
        public string? Id { get; set; }

        public List<string> SegmentIds { get; set; }
    }
}