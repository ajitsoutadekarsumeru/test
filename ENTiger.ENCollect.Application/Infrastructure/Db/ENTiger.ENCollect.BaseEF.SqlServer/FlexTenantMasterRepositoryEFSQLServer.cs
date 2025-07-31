using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class FlexTenantMasterRepositoryEFSQLServer : FlexEFTenantRepository<FlexTenantBridge>
    {
        private readonly DatabaseSettings _databaseSettings;
        public FlexTenantMasterRepositoryEFSQLServer(ILogger<FlexEFTenantRepository<FlexTenantBridge>> logger, IOptions<DatabaseSettings> databaseSettings) : base(logger)
        {
            _databaseSettings = databaseSettings.Value;
            var optionsBuilder = new DbContextOptionsBuilder<FlexEFTenantMasterSqlServerContext>();

            var connectionString = _databaseSettings.TenantMasterDbConnection;
            optionsBuilder.UseSqlServer(connectionString);

            _context = new FlexEFTenantMasterSqlServerContext(optionsBuilder.Options);
        }
    }
}