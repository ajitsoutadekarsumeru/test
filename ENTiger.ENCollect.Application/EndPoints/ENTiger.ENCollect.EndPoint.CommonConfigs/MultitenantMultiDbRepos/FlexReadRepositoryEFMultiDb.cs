using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public class FlexReadRepositoryEFMultiDb : FlexEFQueryRepositoryBridge
    {
        public FlexReadRepositoryEFMultiDb(ILogger<FlexReadDbRepositoryEFSQLServer> logger) : base(logger)
        {
        }

        public override void InitializeConnection<TFlexHostContextInfo>(IConnectionProvider<TFlexHostContextInfo> connectionProvider)
        {
            if (connectionProvider.DbType == DbTypeConstants.SqlServer)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationEFSqlServerDbContext>();
                optionsBuilder.UseSqlServer(connectionProvider.ConnectionString);

                ApplicationEFSqlServerDbContext context = new ApplicationEFSqlServerDbContext(optionsBuilder.Options);
                InitializeDbContext(context);
            }
            else if (connectionProvider.DbType == DbTypeConstants.PostgreSql)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationEFSqlServerDbContext>();
                optionsBuilder.UseNpgsql(connectionProvider.ConnectionString);

                ApplicationEFSqlServerDbContext context = new ApplicationEFSqlServerDbContext(optionsBuilder.Options);
                InitializeDbContext(context);
            }
            else if (connectionProvider.DbType == DbTypeConstants.MySql)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationEFSqlServerDbContext>();
                optionsBuilder.UseMySql(connectionProvider.ConnectionString, ServerVersion.AutoDetect(connectionProvider.ConnectionString));

                ApplicationEFSqlServerDbContext context = new ApplicationEFSqlServerDbContext(optionsBuilder.Options);
                InitializeDbContext(context);
            }
        }
    }
}