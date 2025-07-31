using ENTiger.ENCollect;
using ENTiger.ENCollect.CronJobs.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sumeru.Flex;

Console.Title = "DormantUsers";
IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureCommonHost
            (
                new ConfigureEndPointHostParams
                {
                    HostName = "ENTiger.ENCollect.EndPoint.CronJob.DormantUsers",
                    Routes = NsbRouteConfig.GetRoute(),
                    SearchPattern = "ENTiger.ENCollect*.dll"
                }
            )
        .ConfigureServices((hostingContext, services) =>
        {
            services.AddHostedService<UserDormantStatusCheckCronJob>();
            services.AddExtensions();
        })
    .Build();

var serviceScope = host.Services.CreateScope();

var serviceProvider = serviceScope.ServiceProvider;
var flexHost = serviceProvider.GetRequiredService<IFlexHost>();
flexHost.SetServiceProvider(serviceProvider);


await host.RunAsync();

