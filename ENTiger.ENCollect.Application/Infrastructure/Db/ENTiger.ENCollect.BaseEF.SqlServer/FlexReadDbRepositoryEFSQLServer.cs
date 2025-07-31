using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public class FlexReadDbRepositoryEFSQLServer : FlexEFQueryRepositoryBridge
    {
        public FlexReadDbRepositoryEFSQLServer(ILogger<FlexReadDbRepositoryEFSQLServer> logger) : base(logger)
        {
        }

        public override void InitializeConnection<TFlexHostContextInfo>(IConnectionProvider<TFlexHostContextInfo> connectionProvider)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationEFSqlServerDbContext>();
            optionsBuilder.UseSqlServer(connectionProvider.ConnectionString);

            ApplicationEFSqlServerDbContext context = new ApplicationEFSqlServerDbContext(optionsBuilder.Options);
            InitializeDbContext(context);
        }
    }
}