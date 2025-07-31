namespace ENTiger.ENCollect
{
    public interface ISmsProvider
    {
        public Task<bool> SendSmsAsync(List<TenantSMSConfiguration> model, string numbers, string message, string file);
    }
}