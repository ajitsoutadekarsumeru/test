using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ApplicationEFSqlServerDbContext : ApplicationEFDbContext
    {
        public ApplicationEFSqlServerDbContext()
        {
            //this constructor goes only with the migrations
        }

        public ApplicationEFSqlServerDbContext(DbContextOptions<ApplicationEFSqlServerDbContext> options) : base(options)
        {
            //this constructor is being used by the repos to initialize the context with various options including multitenancy

            //Uncomment below code to enable audit trail for your CUD transactions
            //this.EnableAuditTrail();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ILogger _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<ApplicationEFSqlServerDbContext>>();

            base.OnConfiguring(optionsBuilder);

            optionsBuilder.AddInterceptors(new DateTimeNowWithoutOffsetInterceptor());
        }
    }
}