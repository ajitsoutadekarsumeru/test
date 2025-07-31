namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetGeoTagDetailsDto : DtoBridge
    {
        public double? Latitude { get; set; }
        public string? LocationName { get; set; }
        public double? Longitude { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public string? Distance { get; set; }
        public string? GeoTagReason { get; set; }
        public string? TransactionType { get; set; }
    }
}