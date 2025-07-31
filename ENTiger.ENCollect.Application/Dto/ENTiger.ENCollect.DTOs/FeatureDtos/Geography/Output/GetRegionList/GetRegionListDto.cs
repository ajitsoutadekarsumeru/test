namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetRegionListDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
    }
}