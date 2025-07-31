namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class AddGeoTagDto : DtoBridge
    {
        public double? Latitude { get; set; }
        public string? GeoTagReason { get; set; }
        public string? LocationName { get; set; }
        public double? Longitude { get; set; }
    }
}