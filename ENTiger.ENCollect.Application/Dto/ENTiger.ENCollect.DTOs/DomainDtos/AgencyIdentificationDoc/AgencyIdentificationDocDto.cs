namespace ENTiger.ENCollect
{
    public partial class AgencyIdentificationDocDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? Path { get; set; }
        public string? FileName { get; set; }
        public long? FileSize { get; set; }
        public string? TFlexIdentificationId { get; set; }
    }
}