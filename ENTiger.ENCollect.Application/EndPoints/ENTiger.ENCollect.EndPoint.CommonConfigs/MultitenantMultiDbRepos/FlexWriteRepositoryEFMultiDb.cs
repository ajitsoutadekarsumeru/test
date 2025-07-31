using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public class FlexWriteRepositoryEFMultiDb : FlexEFRepositoryBridge
    {
        public FlexWriteRepositoryEFMultiDb(ILogger<FlexWriteRepositoryEFMultiDb> logger) : base(logger)
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
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationEFPostgreSqlDbContext>();
                optionsBuilder.UseNpgsql(connectionProvider.ConnectionString);

                ApplicationEFPostgreSqlDbContext context = new ApplicationEFPostgreSqlDbContext(optionsBuilder.Options);
                InitializeDbContext(context);
            }
            else if (connectionProvider.DbType == DbTypeConstants.MySql)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationEFMySqlDbContext>();
                optionsBuilder.UseMySql(connectionProvider.ConnectionString, ServerVersion.AutoDetect(connectionProvider.ConnectionString));

                ApplicationEFMySqlDbContext context = new ApplicationEFMySqlDbContext(optionsBuilder.Options);
                InitializeDbContext(context);
            }
        }
    }
}