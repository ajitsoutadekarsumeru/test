using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class UpdateSegmentsSequenceDto : DtoBridge
    {
        public List<UpdateInputSegmentsSequenceDto> input { get; set; }
    }

    public class UpdateInputSegmentsSequenceDto
    {
        [StringLength(32)]
        public string? Id { get; set; }

        public int? SequenceNumber { get; set; }
    }
}