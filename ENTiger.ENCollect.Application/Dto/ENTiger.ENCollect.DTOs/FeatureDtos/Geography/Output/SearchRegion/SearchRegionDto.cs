namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchRegionDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}