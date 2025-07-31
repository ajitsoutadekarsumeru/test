namespace ENTiger.ENCollect
{
    public partial class ExecuteSpRequestDto : DtoBridge
    {
        public string? SpName { get; set; }
       
        public string? WorkRequestId { get; set; }
        public string? UserId { get; set; }
        public string? UploadType { get; set; }
        public string? ActionType { get; set; }
        public string TenantId { get; set; }
        public Dictionary<string, string> Parameters { get; set; } = null;
    }
}