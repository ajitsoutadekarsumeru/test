namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAgencyTypesDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? AgencyType { get; set; }
        public string? AgencySubType { get; set; }
    }
}