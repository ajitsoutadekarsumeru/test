using ENTiger.ENCollect;
using ENTiger.ENCollect.CronJobs;
using Sumeru.Flex;

Console.Title = "DailySummary";
IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureCommonHost
            (
                new ConfigureEndPointHostParams
                {
                    HostName = "ENTiger.ENCollect.EndPoint.CronJob.DailySummary",
                    Routes = NsbRouteConfig.GetRoute(),
                    SearchPattern = "ENTiger.ENCollect*.dll"
                }
            )
        .ConfigureServices((hostingContext, services) =>
        {
            services.AddHostedService<DailySummaryCroneJob>();
            services.AddExtensions();
        })
    .Build();

var serviceScope = host.Services.CreateScope();

var serviceProvider = serviceScope.ServiceProvider;
var flexHost = serviceProvider.GetRequiredService<IFlexHost>();
flexHost.SetServiceProvider(serviceProvider);


await host.RunAsync();

