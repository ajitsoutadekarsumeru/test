using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public class FlexWriteDbRepositoryEFMySql : FlexEFRepositoryBridge
    {
        public FlexWriteDbRepositoryEFMySql(ILogger<FlexWriteDbRepositoryEFMySql> logger) : base(logger)
        {
        }

        public override void InitializeConnection<TFlexHostContextInfo>(IConnectionProvider<TFlexHostContextInfo> connectionProvider)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationEFMySqlDbContext>();
            optionsBuilder.UseMySql(connectionProvider.ConnectionString, ServerVersion.AutoDetect(connectionProvider.ConnectionString));

            ApplicationEFMySqlDbContext context = new ApplicationEFMySqlDbContext(optionsBuilder.Options);
            InitializeDbContext(context);
        }
    }
}