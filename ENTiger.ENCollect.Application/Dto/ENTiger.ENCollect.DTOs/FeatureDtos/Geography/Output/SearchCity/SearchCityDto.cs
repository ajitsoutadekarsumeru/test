namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchCityDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }

        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}