namespace ENTiger.ENCollect.AgencyUsersModule
{
    public class ProfileIdentificationDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? IdentificationTypeId { get; set; }
        public string? IdentificationDocTypeId { get; set; }
        public ICollection<ProfileIdentificationDocDto>? IdentificationDocId { get; set; }
        public DateTime? DeferredTillDate { get; set; }
        public bool? IsDeferred { get; set; }
        public bool? IsWavedOff { get; set; }
        public string? TflexId { get; set; }
        public bool? IsDelete { get; set; }
        public string? FileName { get; set; }
    }
}