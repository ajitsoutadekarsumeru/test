using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class SmsProviderFactory : IFlexUtilityService
    {
        private readonly IServiceProvider _serviceProvider;

        public SmsProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual ISmsProvider GetSmsProvider(string smsProvider)
        {
            return smsProvider switch
            {
                // FlexContainer.ServiceProvider.
                "24_7" => _serviceProvider.GetRequiredService<SmsProvider24X7>(),
                "infobip" => _serviceProvider.GetRequiredService<SmsProviderInfoBip>(),
                "karix" => _serviceProvider.GetRequiredService<SmsProviderKarix>(),
                _ => throw new InvalidOperationException("Invalid SMS provider type")
            };
        }
    }
}