using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetSegmentAdvanceFieldsDto : DtoBridge
    {
        [StringLength(200)]
        public string? FieldName { get; set; }

        [StringLength(200)]
        public string? FieldId { get; set; }

        [StringLength(100)]
        public string? Operator { get; set; }
    }
}