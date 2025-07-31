namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchTreatmentsDto : DtoBridge
    {
        public int? count { get; set; }
        public ICollection<SearchTreatmentOutputDto> outputmodel { get; set; }
    }
}