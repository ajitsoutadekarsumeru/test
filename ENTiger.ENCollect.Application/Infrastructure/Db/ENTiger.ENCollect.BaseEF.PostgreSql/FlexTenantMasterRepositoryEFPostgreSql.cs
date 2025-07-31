using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class FlexTenantMasterRepositoryEFPostgreSql : FlexEFTenantRepository<FlexTenantBridge>
    {
        public FlexTenantMasterRepositoryEFPostgreSql(ILogger<FlexEFTenantRepository<FlexTenantBridge>> logger) : base(logger)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FlexEFTenantMasterPostgreSqlContext>();

            var connectionString = "";//_configuration.GetSection("FlexBase")["TenantMasterDbConnection"];
            optionsBuilder.UseNpgsql(connectionString);

            _context = new FlexEFTenantMasterPostgreSqlContext(optionsBuilder.Options);
        }
    }
}