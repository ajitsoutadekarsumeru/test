using ENTiger.ENCollect;
using Sumeru.Flex;

Console.Title = "ENTiger.ENCollect.EndPoint.CronJob.CannedReports";

var host = Host.CreateDefaultBuilder(args)
    .ConfigureCommonHost(new ConfigureEndPointHostParams
    {
        HostName = "ENTiger.ENCollect.EndPoint.CronJob.CannedReports",
        Routes = NsbRouteConfig.GetRoute(),
        SearchPattern = "ENTiger.ENCollect*.dll"
    })
    .ConfigureServices((hostingContext, services) =>
    {
        // Register Hosted Services
        services.AddHostedService<Worker>();
        services.AddExtensions();
        // Bind Configuration
        services.Configure<CannedReportSetting>(
            hostingContext.Configuration.GetSection("CannedReportSetting")
        );
    })
    .Build();

// Use 'using' to ensure proper disposal of the scope
using var serviceScope = host.Services.CreateScope();
var serviceProvider = serviceScope.ServiceProvider;

// Get IFlexHost Service and Initialize It
serviceProvider.GetRequiredService<IFlexHost>().SetServiceProvider(serviceProvider);

await host.RunAsync();
