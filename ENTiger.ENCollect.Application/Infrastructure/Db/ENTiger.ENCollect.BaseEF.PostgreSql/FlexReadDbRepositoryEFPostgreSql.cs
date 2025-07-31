using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public class FlexReadDbRepositoryEFPostgreSql : FlexEFQueryRepositoryBridge
    {
        public FlexReadDbRepositoryEFPostgreSql(ILogger<FlexReadDbRepositoryEFPostgreSql> logger) : base(logger)
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