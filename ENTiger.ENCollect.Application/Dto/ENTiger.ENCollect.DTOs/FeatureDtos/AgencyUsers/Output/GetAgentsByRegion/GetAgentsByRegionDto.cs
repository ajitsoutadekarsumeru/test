namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAgentsByRegionDto : DtoBridge
    {
        public string? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string? MiddleName { get; set; }

        public string? AgentCode { get; set; }
    }
}