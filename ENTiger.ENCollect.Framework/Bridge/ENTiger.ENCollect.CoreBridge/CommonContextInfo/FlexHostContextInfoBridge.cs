namespace ENTiger.ENCollect
{
    public class FlexAppContextBridge : IFlexAppContextBridge
    {
        public string HostName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string CorrelationId { get; set; }
        public string TenantId { get; set; }
        public string RequestSource { get; set; }
        public string ClientIP { get; set; }
    }
}