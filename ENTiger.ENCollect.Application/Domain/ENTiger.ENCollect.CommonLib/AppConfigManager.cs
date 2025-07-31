using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class AppConfigManager
    {
        public static IConfigurationSection GetSection(string key)
        {
            return FlexContainer.ServiceProvider.GetRequiredService<IConfiguration>().GetSection(key);
        }

        public static IConfigurationSection AppSettings
        {
            get
            {
                return FlexContainer.ServiceProvider.GetRequiredService<IConfiguration>().GetSection("AppSettings");
            }
        }

        public static IConfigurationSection ConnectionStrings
        {
            get
            {
                return FlexContainer.ServiceProvider.GetRequiredService<IConfiguration>().GetSection("ConnectionStrings");
            }
        }
    }
}