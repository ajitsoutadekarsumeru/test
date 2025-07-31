namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetListsDto : DtoBridge
    {
        public string? Id { get; set; }

        public string? AgencyCode { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? MiddleName { get; set; }
    }
}