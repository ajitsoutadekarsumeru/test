namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class SearchUsersByGeoTagDto : DtoBridge
    {
        public string? UserId { get; set; }
        public string? MobileNumber { get; set; }
        public string? IMEI { get; set; }
        public bool? IsSearchByLocation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}