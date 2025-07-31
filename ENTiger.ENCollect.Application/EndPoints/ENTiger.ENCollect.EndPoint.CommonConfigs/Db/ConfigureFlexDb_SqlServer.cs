using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public partial class ConfigureFlexDb
    {
        public static void ForRepositoryForSqlServer(IServiceCollection services)
        {
            //services.AddTransient<FlexEFDbContext, ApplicationEFSqlServerDbContext>();

            services.AddTransient<IFlexRepositoryBridge, FlexWriteDbRepositoryEFSQLServer>();
            services.AddTransient<IFlexQueryRepositoryBridge, FlexReadDbRepositoryEFSQLServer>();

            services.AddTransient<IFlexQueryableExtensionInclude, FlexQueryableExtensionEFCore>();
        }

        public static void ForMultitenantMasterDbForSqlServer(IServiceCollection services)
        {
            services.AddTransient<IFlexNativeHostTenantProviderBridge, FlexNativeHostTenantProviderBridge>();
            services.AddTransient(typeof(IFlexTenantRepository<FlexTenantBridge>), typeof(FlexTenantMasterRepositoryEFSQLServer));
            services.AddTransient(typeof(FlexEFTenantContext<FlexTenantBridge>), typeof(FlexEFTenantMasterSqlServerContext));
            services.AddTransient(typeof(IFlexTenantProvider<FlexTenantBridge, FlexAppContextBridge>), typeof(FlexNativeHostTenantProviderBridge));
        }
    }
}