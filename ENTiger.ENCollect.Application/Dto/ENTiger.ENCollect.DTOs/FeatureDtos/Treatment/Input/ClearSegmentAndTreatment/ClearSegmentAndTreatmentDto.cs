namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class ClearSegmentAndTreatmentDto : DtoBridge
    {
        public bool? ClearSegment { get; set; }

        public bool? ClearTreatment { get; set; }

        public List<string>? SegmentIds { get; set; }

        public List<string>? TreatmentIds { get; set; }
    }
}