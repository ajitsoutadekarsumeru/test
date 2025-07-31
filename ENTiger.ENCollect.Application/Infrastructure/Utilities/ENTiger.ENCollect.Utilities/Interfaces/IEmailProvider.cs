namespace ENTiger.ENCollect
{
    public interface IEmailProvider
    {
        Task<bool> SendEmailAsync(TenantEmailConfiguration model, string toAddress, string msg, string subject, string logFilePath, List<string>? files = null, string? filePath = null, bool includedSignature = false);
    }
}