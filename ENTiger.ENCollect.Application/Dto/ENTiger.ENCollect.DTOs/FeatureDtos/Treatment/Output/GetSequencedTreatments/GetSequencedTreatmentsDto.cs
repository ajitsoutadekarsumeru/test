namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetSequencedTreatmentsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int? Sequence { get; set; }
    }
}