using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class CommonHostConfig
    {
        public static IHostBuilder ConfigureCommonHost(this IHostBuilder hostBuilder, ConfigureEndPointHostParams hostParams)
        {
            hostBuilder
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                    config.AddJsonFile("appsettings.json",
                                       optional: true,
                                       reloadOnChange: true);
                    config.AddJsonFile($"appsettings.EndPoint.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                   // config.AddJsonFile($"elasticindexnames.json", optional: false, reloadOnChange: true);

                    config.AddEnvironmentVariables();
                })
                .UseSerilog((context, loggerConfig) =>
                {
                    loggerConfig = ConfigureLogger.ForSerilog(context, loggerConfig, hostParams.HostName, context.Configuration);
                })
                .UseNServiceBus(ctx =>
                {
                    return BusEndPointConfig.GetEndpoint(ctx.Configuration, ctx.HostingEnvironment, hostParams.Routes);
                })
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddFlexBaseCoreServices(hostParams);
                    services.AddFlexBaseBusServices();
                    services.AddFlexBaseDbServices(hostingContext.Configuration);
                    services.AddHttpFactoryServices(hostingContext.Configuration);
                    services.AddOtherBusinessServices(hostingContext.Configuration);
                });

            return hostBuilder;
        }

        public static void AddFlexBaseCoreServices(this IServiceCollection services, ConfigureEndPointHostParams hostParams)
        {
            string jsonString = File.ReadAllText($"appsettings.EndPoint.json");
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonString);
            string[] tenants = ((IEnumerable<dynamic>)jsonObject.Tenants).Select(t => (string)t).ToArray();

            services.AddFlexBase(new List<AssembliesToLoad> { new AssembliesToLoad(hostParams.SearchPattern) });

            //Add PK Generator
            services.AddTransient<IFlexPrimaryKeyGeneratorBridge, SequentialGuidPrimaryKeyGeneratorBridge>();

            services.AddAutoMapper(typeof(CoreMapperConfiguration).Assembly);

            services.AddUtilityServices(tenants, new List<AssembliesToLoad> { new AssembliesToLoad("ENTiger.ENCollect*.dll") });
        }
    }

    public class ConfigureEndPointHostParams
    {
        public string HostName { get; set; }
        public List<BusRouteConfig> Routes { get; set; }
        public string SearchPattern { get; set; }
        public string DirectoryPath { get; set; } = "";
    }
}