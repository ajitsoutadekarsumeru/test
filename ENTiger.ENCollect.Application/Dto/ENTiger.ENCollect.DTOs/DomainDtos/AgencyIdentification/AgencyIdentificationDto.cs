namespace ENTiger.ENCollect
{
    public partial class AgencyIdentificationDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? IdentificationTypeId { get; set; }
        public string? IdentificationDocTypeId { get; set; }
        public string? IdentificationDocId { get; set; }
        public string? Value { get; set; }
        public DateTime? DeferredTillDate { get; set; }
        public bool? IsDeferred { get; set; }
        public bool? IsWavedOff { get; set; }
        public ICollection<AgencyIdentificationDocDto>? ProfileIdentificationDoc { get; set; }
    }
}