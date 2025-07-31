using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect
{
    public class ApplicationEFPostgreSqlDbContext : ApplicationEFDbContext
    {
        public ApplicationEFPostgreSqlDbContext()
        {
            //this constructor goes only with the migrations
        }

        public ApplicationEFPostgreSqlDbContext(DbContextOptions<ApplicationEFPostgreSqlDbContext> options) : base(options)
        {
            //this constructor is being used by the repos to initialize the context with various options including multitenancy

            //Uncomment below code to enable audit trail for your CUD transactions
            //this.EnableAuditTrail();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("default");
            base.OnModelCreating(modelBuilder);
        }
    }
}