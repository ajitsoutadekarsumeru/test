namespace ENTiger.ENCollect
{
    public partial class GetDataRequestDto : DtoBridge
    {
        public string? SpName { get; set; }
      
        public string? WorkRequestId { get; set; } = null;
        public Dictionary<string, string> Parameters { get; set; } = null;
        public string TenantId { get; set; }
    }
}