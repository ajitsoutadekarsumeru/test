namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SimulateTreatmentDto : DtoBridge
    {
        public string TreatmentId { get; set; }
        public string TreatmentName { get; set; }
        public decimal? Count { get; set; }
        public double? BOMPos { get; set; }
        public double? BOMPosPercentage { get; set; }
    }
}