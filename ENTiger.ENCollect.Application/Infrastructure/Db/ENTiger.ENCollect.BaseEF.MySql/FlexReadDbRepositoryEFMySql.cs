using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public class FlexReadDbRepositoryEFMySql : FlexEFQueryRepositoryBridge
    {
        public FlexReadDbRepositoryEFMySql(ILogger<FlexReadDbRepositoryEFMySql> logger) : base(logger)
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