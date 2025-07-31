using ENTiger.ENCollect;
using ENTiger.ENCollect.AccountsModule;
using Sumeru.Flex;

Console.Title = "CustomerConsent";
IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureCommonHost
            (
                new ConfigureEndPointHostParams
                {
                    HostName = "ENTiger.ENCollect.EndPoint.CronJob.CustomerConsent",
                    Routes = NsbRouteConfig.GetRoute(),
                    SearchPattern = "ENTiger.ENCollect*.dll"
                }
            )
        .ConfigureServices((hostingContext, services) =>
            {
                services.AddHostedService<UpdateCustomerConsentExpiryCronJob>();
                services.AddExtensions();
            })
    .Build();

var serviceScope = host.Services.CreateScope();

var serviceProvider = serviceScope.ServiceProvider;
var flexHost = serviceProvider.GetRequiredService<IFlexHost>();
flexHost.SetServiceProvider(serviceProvider);


await host.RunAsync();

