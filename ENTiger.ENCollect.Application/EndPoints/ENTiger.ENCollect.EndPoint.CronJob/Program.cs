using ENTiger.ENCollect;
using Sumeru.Flex;

Console.Title = "ENTiger.ENCollect.EndPoint.CronJob";
IHost host = Host.CreateDefaultBuilder(args)
.ConfigureCommonHost
    (
        new ConfigureEndPointHostParams
        {
            HostName = "ENTiger.ENCollect.EndPoint.CronJob",
            Routes = NsbRouteConfig.GetRoute(),
            SearchPattern = "ENTiger.ENCollect*.dll"
        }
    )
.ConfigureServices((hostingContext, services) =>
{
    services.AddHostedService<Worker>();
    services.AddExtensions();
})
.Build();

var serviceScope = host.Services.CreateScope();

var serviceProvider = serviceScope.ServiceProvider;
var flexHost = serviceProvider.GetRequiredService<IFlexHost>();
flexHost.SetServiceProvider(serviceProvider);

await host.RunAsync();