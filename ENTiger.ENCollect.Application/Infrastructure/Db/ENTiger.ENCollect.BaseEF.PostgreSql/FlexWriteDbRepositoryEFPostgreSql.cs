using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public class FlexWriteDbRepositoryEFPostgreSql : FlexEFRepositoryBridge
    {
        public FlexWriteDbRepositoryEFPostgreSql(ILogger<FlexWriteDbRepositoryEFPostgreSql> logger) : base(logger)
        {
        }

        public override void InitializeConnection<TFlexHostContextInfo>(IConnectionProvider<TFlexHostContextInfo> connectionProvider)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationEFPostgreSqlDbContext>();
            optionsBuilder.UseNpgsql(connectionProvider.ConnectionString);

            ApplicationEFPostgreSqlDbContext context = new ApplicationEFPostgreSqlDbContext(optionsBuilder.Options);
            InitializeDbContext(context);
        }
    }
}