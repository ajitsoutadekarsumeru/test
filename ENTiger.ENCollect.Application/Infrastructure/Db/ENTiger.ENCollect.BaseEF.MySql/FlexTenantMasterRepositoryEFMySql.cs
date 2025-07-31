using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class FlexTenantMasterRepositoryEFMySql : FlexEFTenantRepository<FlexTenantBridge>
    {
        private readonly DatabaseSettings _databaseSettings;
        public FlexTenantMasterRepositoryEFMySql(ILogger<FlexEFTenantRepository<FlexTenantBridge>> logger, IOptions<DatabaseSettings> databaseSettings) : base(logger)
        {
            _databaseSettings = databaseSettings.Value;
            var optionsBuilder = new DbContextOptionsBuilder<FlexEFTenantMasterMySqlContext>();

            var connectionString = _databaseSettings.TenantMasterDbConnection;
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            _context = new FlexEFTenantMasterMySqlContext(optionsBuilder.Options);
            
        }
    }
}