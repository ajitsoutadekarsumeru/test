using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class EmailUtility : IEmailUtility
    {
        protected readonly ILogger<EmailUtility> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected readonly IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;
        private readonly EmailProviderFactory _emailProviderFactory;
        protected string? serviceProvider;

        public EmailUtility(ILogger<EmailUtility> logger, IRepoFactory repoFactory,
            EmailProviderFactory emailProviderFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();
            _emailProviderFactory = emailProviderFactory;
        }

        public async Task<bool> SendEmailAsync(string toAddress, string msg, string subject, string tenantId, List<string>? files = null, string? filePath = null, bool includedSignature = false)
        {
            _logger.LogInformation("EmailUtility : SendMail - Start | TenantId - " + tenantId);
            FlexAppContextBridge hostContextInfo = new FlexAppContextBridge()
            {
                TenantId = tenantId
            };
            _repoFactory.Init(hostContextInfo);
            bool result = false;

            serviceProvider = await _repoFactory.GetRepo().FindAll<FeatureMaster>()
                                        .Where(x => string.Equals(x.Parameter, "emailserviceprovider"))
                                        .Select(x => x.Value)
                                        .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(serviceProvider))
            {
                _logger.LogError("Please configure the <<EmailServiceProvider>> in DB | FeatureMaster Table | TenantId - " + tenantId);
            }
            else
            {
                TenantEmailConfiguration? model = await _repoTenantFactory.FindAll<TenantEmailConfiguration>()
                                                            .Where(x => x.TenantId == tenantId)
                                                            .FirstOrDefaultAsync();
                if (model == null)
                {
                    _logger.LogError("Please configure the Email Configuration in Tenant DB | TenantId - " + tenantId);
                }
                else
                {
                    string EmailLogPath = model.EmailLogPath;
                    if (string.IsNullOrEmpty(EmailLogPath))
                    {
                        _logger.LogError("EmailUtility : Please configure the <<EmailLogPath>> in Tenant DB | TenantEmailConfiguration Table");
                    }
                    else
                    {
                        string logFilePath = Path.Combine(EmailLogPath, tenantId);

                        //Get the Email provider
                        var emailProvider = _emailProviderFactory.GetEmailProvider(serviceProvider);

                        //Call Email provider
                        result = await emailProvider.SendEmailAsync(model, toAddress, msg, subject, logFilePath, files, filePath, includedSignature);
                    }
                }
            }
            _logger.LogInformation("EmailUtility : SendMail - End");
            return result;
        }
    }
}