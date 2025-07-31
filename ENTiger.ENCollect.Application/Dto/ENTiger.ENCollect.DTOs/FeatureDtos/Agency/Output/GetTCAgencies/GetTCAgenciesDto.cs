namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTCAgenciesDto : DtoBridge
    {
        public string? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? AgencyCode { get; set; }
    }
}