namespace ENTiger.ENCollect
{
    public class GenerateCsvFile
    {
        public string? DataSource { get; set; }
        public string? Database { get; set; }
        public string? UserId { get; set; }
        public string? Password { get; set; }
        public string? StoredProcedure { get; set; }
        public string? WorkRequestId { get; set; }
        public string? Destination { get; set; }
        public string? ConnectionString { get; set; }
        public string? TenantId { get; set; }
        public string? ActionType { get; set; }
    }
}