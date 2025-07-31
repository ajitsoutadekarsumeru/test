using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public partial class ConfigureFlexDb
    {
        public static void ForRepositoryForPostgreSql(IServiceCollection services)
        {
            //services.AddTransient<FlexEFDbContext, ApplicationEFPostgreSqlDbContext>();

            services.AddTransient<IFlexRepositoryBridge, FlexWriteDbRepositoryEFPostgreSql>();
            services.AddTransient<IFlexQueryRepositoryBridge, FlexReadDbRepositoryEFPostgreSql>();

            services.AddTransient<IFlexQueryableExtensionInclude, FlexQueryableExtensionEFCore>();
        }

        public static void ForMultitenantMasterDbForPostgreSql(IServiceCollection services)
        {
            services.AddTransient<IFlexNativeHostTenantProviderBridge, FlexNativeHostTenantProviderBridge>();
            services.AddTransient(typeof(IFlexTenantRepository<FlexTenantBridge>), typeof(FlexTenantMasterRepositoryEFPostgreSql));
            services.AddTransient(typeof(FlexEFTenantContext<FlexTenantBridge>), typeof(FlexEFTenantMasterPostgreSqlContext));
            services.AddTransient(typeof(IFlexTenantProvider<FlexTenantBridge, FlexAppContextBridge>), typeof(FlexNativeHostTenantProviderBridge));
        }
    }
}