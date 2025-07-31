namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetNonSegmentedAndTreatmentedDto : DtoBridge
    {
        public decimal? SegmentBOM_POS { get; set; }
        public decimal? SegmentBOM_POSCount { get; set; }
        public decimal? TreatmentBOM_POS { get; set; }
        public decimal? TreatmentBOM_POSCount { get; set; }
        public string SegmentBOM_POSPercentage { get; set; }
        public string TreatmentBOM_POSPercentage { get; set; }
    }
}