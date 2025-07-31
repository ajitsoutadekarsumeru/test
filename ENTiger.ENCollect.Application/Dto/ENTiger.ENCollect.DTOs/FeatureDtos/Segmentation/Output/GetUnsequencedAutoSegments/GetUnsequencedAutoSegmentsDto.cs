namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetUnsequencedAutoSegmentsDto : DtoBridge
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public int? Sequence { get; set; }
    }
}