using ENTiger.ENCollect;
using ENTiger.ENCollect.CronJobs.FilesArchive;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sumeru.Flex;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.Title = "FilesArchive";
        IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureCommonHost
                    (
                        new ConfigureEndPointHostParams
                        {
                            HostName = "ENTiger.ENCollect.EndPoint.CronJob.FilesArchive",
                            Routes = NsbRouteConfig.GetRoute(),
                            SearchPattern = "ENTiger.ENCollect*.dll"
                        }
                    )
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddHostedService<FileArchiverJob>();
                    services.AddExtensions();
                })
            .Build();

        var serviceScope = host.Services.CreateScope();

        var serviceProvider = serviceScope.ServiceProvider;
        var flexHost = serviceProvider.GetRequiredService<IFlexHost>();
        flexHost.SetServiceProvider(serviceProvider);


        await host.RunAsync();
    }
}