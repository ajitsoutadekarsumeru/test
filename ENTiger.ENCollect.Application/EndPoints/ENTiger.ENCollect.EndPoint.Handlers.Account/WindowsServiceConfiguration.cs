namespace ENTiger.ENCollect
{
    public static class WindowsServiceConfiguration
    {
        public static IHostBuilder UseWindowsServiceIfConfigured(this IHostBuilder hostBuilder, IConfiguration configuration)
        {
            // Read the appsettings value
            bool useWindowsService = configuration.GetSection("EndPoint").GetValue<bool>("RunAsWindowsService");

            if (useWindowsService)
            {
                hostBuilder.UseWindowsService();
            }

            return hostBuilder;
        }
    }
}