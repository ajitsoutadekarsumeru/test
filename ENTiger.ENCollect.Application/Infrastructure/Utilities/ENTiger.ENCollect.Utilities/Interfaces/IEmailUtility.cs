namespace ENTiger.ENCollect
{
    public interface IEmailUtility
    {
        Task<bool> SendEmailAsync(string toAddress, string msg, string subject, string tenantId, List<string>? files = null, string? filePath = null, bool includedSignature = false);
    }
}