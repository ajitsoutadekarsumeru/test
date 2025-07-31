namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class agencylistDto : DtoBridge
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AgencyCode { get; set; }
        public string AgencyName { get; set; }
        public string Status { get; set; }
        public string PAN { get; set; }
        public string ContactNumber { get; set; }
        public string CustomId { get; set; }
        public string AgencyWFState { get; set; }
        public DateTime? ContractExpireDate { get; set; }
        public string AgencyType { get; set; }
    }
}