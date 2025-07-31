namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAgenciesByTypeIdDto : DtoBridge
    {
        public string? AgencyId { get; set; }
        public string? AgencyName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? AgencyCode { get; set; }
    }
}