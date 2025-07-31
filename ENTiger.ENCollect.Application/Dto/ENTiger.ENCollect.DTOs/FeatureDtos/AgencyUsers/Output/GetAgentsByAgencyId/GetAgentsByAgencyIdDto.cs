namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAgentsByAgencyIdDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}