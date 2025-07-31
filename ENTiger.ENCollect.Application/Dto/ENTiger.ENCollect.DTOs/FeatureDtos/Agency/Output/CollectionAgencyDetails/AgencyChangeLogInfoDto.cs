namespace ENTiger.ENCollect.AgencyModule
{
    public class AgencyChangeLogInfoDto : DtoBridge
    {
        public string? PartyId { get; set; }
        public string? ProfileName { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? ChangedByUserId { get; set; }
        public string? ChangedByUserName { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public short? PartyStatusId { get; set; }
    }
}