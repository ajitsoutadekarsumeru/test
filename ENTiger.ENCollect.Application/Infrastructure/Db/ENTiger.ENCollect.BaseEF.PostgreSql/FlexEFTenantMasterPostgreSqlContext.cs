using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect
{
    public class FlexEFTenantMasterPostgreSqlContext : ApplicationTenantEFDbContext
    {
        public FlexEFTenantMasterPostgreSqlContext() : base()
        {
        }

        public FlexEFTenantMasterPostgreSqlContext(DbContextOptions options) : base(options)
        {
        }
    }
}