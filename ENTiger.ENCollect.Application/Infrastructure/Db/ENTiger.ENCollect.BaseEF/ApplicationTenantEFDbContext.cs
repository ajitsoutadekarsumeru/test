using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ApplicationTenantEFDbContext : FlexEFTenantContext<FlexTenantBridge>
    {
        public ApplicationTenantEFDbContext() : base()
        {
        }

        public ApplicationTenantEFDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TenantEmailConfiguration> TenantEmailConfiguration { get; set; }
        public DbSet<TenantSMSConfiguration> TenantSMSConfiguration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ILogger _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<ApplicationEFDbContext>>();

            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .EnableSensitiveDataLogging()
                .LogTo(message => _logger.LogDebug(message)) //This is to log the EF queries to the logger
                .AddInterceptors(new DefaultCreationTimeInterceptor()); //This is to set the creation time for the entities
        }
    }
}