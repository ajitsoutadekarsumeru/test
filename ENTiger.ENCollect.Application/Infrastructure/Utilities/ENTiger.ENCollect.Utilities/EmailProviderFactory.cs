using Microsoft.Extensions.DependencyInjection;

namespace ENTiger.ENCollect
{
    public class EmailProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public EmailProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEmailProvider GetEmailProvider(string emailProvider)
        {
            return emailProvider switch
            {
                // FlexContainer.ServiceProvider.
                "smtp" => _serviceProvider.GetRequiredService<EmailProviderSmtp>(),
                "netcorecloud" => _serviceProvider.GetRequiredService<EmailProviderNetCoreCloud>(),

                _ => throw new InvalidOperationException("Invalid Email provider type")
            };
        }
    }
}