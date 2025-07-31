namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SimulateSegmentDto : DtoBridge
    {
        public string? SegmentId { get; set; }
        public string? SegmentName { get; set; }
        public decimal? Count { get; set; }
        public double? BOMPos { get; set; }
        public string? BOMPosPercentage { get; set; }
    }
}