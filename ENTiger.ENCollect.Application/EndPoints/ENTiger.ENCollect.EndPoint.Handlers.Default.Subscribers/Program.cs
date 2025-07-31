using ENTiger.ENCollect;
using Sumeru.Flex;

System.Diagnostics.Trace.WriteLine($"ENTiger.ENCollect.EndPoint.Handlers.Default.Subscribers");
IHost host = Host.CreateDefaultBuilder(args).UseWindowsService()
        .ConfigureCommonHost
            (
                new ConfigureEndPointHostParams
                {
                    HostName = "ENTiger.ENCollect.EndPoint.Handlers.Default.Subscribers",
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

var endpoint = FlexContainer.ServiceProvider.GetRequiredService<IConfiguration>().GetSection("EndPoint")["Name"];
System.Diagnostics.Trace.WriteLine(endpoint);

await host.RunAsync();