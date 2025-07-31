using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class UpdateSegmentFlagDto : DtoBridge
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}