namespace ENTiger.ENCollect
{
    public interface ISmsUtility
    {
        public Task<bool> SendSMS(string numbers, string message, string tenantId, string? language = null);
    }
}