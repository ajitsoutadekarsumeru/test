using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect
{
    public class FlexEFTenantMasterSqlServerContext : ApplicationTenantEFDbContext
    {
        public FlexEFTenantMasterSqlServerContext() : base()
        {
        }

        public FlexEFTenantMasterSqlServerContext(DbContextOptions options) : base(options)
        {
        }
    }
}