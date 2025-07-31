using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class TenantMigrationContextFactory : IDesignTimeDbContextFactory<TenantMigrationContext>
    {
        public TenantMigrationContext CreateDbContext(string[] args)
        {
            System.Diagnostics.Trace.WriteLine("Hello, ENTiger.ENCollect.Migrations.Tenant.SqlServer Factory!");
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json",
                           optional: true,
                           reloadOnChange: true)
            .AddEnvironmentVariables().Build();

            // Setting up services
            var services = new ServiceCollection();

            // Add the configuration to the services
            services.AddSingleton<IConfiguration>(configuration);

            System.Diagnostics.Trace.WriteLine("Configuring Serilog");

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.AddSerilog(dispose: true);
            });

            System.Diagnostics.Trace.WriteLine("Configuring Flexbase");

            services.AddFlexBase(new List<AssembliesToLoad> { new AssembliesToLoad("EBusiness*.dll") });

            System.Diagnostics.Trace.WriteLine("Build Services");

            var serviceProvider = services.BuildServiceProvider();
            IFlexHost flexHost = serviceProvider.GetRequiredService<IFlexHost>();
            IConfiguration config = serviceProvider.GetRequiredService<IConfiguration>();

            flexHost.SetServiceProvider(serviceProvider);

            return new TenantMigrationContext(serviceProvider, config, flexHost);
        }
    }
}