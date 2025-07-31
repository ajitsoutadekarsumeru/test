namespace ENTiger.ENCollect
{
    public interface ITinyUrlProvider
    {
        Task<string> CreateTinyUrlAsync(string originalUrl, string tenantId);
    }
}