using ENTiger.ENCollect;
using ENTiger.ENCollect.CommunicationModule;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sumeru.Flex;

Console.Title = "RunTrigger";
IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureCommonHost
            (
                new ConfigureEndPointHostParams
                {
                    HostName = "ENTiger.ENCollect.EndPoint.CronJob.RunTrigger",
                    Routes = NsbRouteConfig.GetRoute(),
                    SearchPattern = "ENTiger.ENCollect*.dll"
                }
            )
        .ConfigureServices((hostingContext, services) =>
        {
            services.AddHostedService<RunTriggersCronJob>();
            services.AddExtensions();
        })
    .Build();

var serviceScope = host.Services.CreateScope();

var serviceProvider = serviceScope.ServiceProvider;
var flexHost = serviceProvider.GetRequiredService<IFlexHost>();
flexHost.SetServiceProvider(serviceProvider);


await host.RunAsync();

