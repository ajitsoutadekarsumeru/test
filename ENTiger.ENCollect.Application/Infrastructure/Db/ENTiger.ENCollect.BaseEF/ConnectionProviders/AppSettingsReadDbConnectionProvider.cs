using Microsoft.Extensions.Configuration;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AppSettingsReadDbConnectionProvider : FlexAppSettingsReadDbConnectionProvider<FlexAppContextBridge>, IReadDbConnectionProviderBridge
    {
        public AppSettingsReadDbConnectionProvider(IConfiguration configuration) : base(configuration)
        {
        }
    }
}