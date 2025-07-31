namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchSegmentsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? SegmentationName { get; set; }

        public string? ExecutionType { get; set; }

        public DateTimeOffset? LastExecutionDate { get; set; }

        public bool? IsDisabled { get; set; }
    }
}