namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetSegmentsDto : DtoBridge
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public int? Sequence { get; set; }
    }
}