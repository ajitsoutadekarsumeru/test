using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public class ApplicationEFDbMigrationContext : ApplicationEFDbContext
    {
        protected readonly IConfiguration _configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connectionString = _configuration.GetSection("FlexBase")["AppDbConnection"];
            optionsBuilder.UseSqlServer(connectionString);
        }

        public ApplicationEFDbMigrationContext(IServiceProvider serviceProvider, IConfiguration configuration, IFlexHost host)
        {
            host.SetServiceProvider(serviceProvider);
            _configuration = configuration;
        }
    }
}