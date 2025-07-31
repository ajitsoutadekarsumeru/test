using Microsoft.Extensions.Configuration;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AppSettingsWriteDbConnectionProvider : FlexAppSettingsWriteDbConnectionProvider<FlexAppContextBridge>, IWriteDbConnectionProviderBridge
    {
        public AppSettingsWriteDbConnectionProvider(IConfiguration configuration) : base(configuration)
        {
        }
    }
}