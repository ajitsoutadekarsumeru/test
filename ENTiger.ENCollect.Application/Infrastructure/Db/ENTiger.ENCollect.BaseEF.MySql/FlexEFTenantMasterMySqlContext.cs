using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect
{
    public class FlexEFTenantMasterMySqlContext : ApplicationTenantEFDbContext
    {
        public FlexEFTenantMasterMySqlContext() : base()
        {
        }

        public FlexEFTenantMasterMySqlContext(DbContextOptions options) : base(options)
        {
        }
    }
}