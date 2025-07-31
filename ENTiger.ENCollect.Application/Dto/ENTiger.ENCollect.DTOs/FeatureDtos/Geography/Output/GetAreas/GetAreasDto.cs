namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAreaDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? NickName { get; set; }
        public string? CityId { get; set; }
        public string? CityName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}