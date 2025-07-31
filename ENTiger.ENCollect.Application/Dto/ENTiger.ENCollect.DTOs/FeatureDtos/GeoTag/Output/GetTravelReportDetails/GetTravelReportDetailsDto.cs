namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTravelReportDetailsDto : DtoBridge
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? AgentName { get; set; }
        public string? Distance { get; set; }
        public string? TransactionType { get; set; }
    }
}