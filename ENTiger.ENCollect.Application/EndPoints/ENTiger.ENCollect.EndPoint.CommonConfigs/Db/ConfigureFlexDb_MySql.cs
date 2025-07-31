using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public partial class ConfigureFlexDb
    {
        public static void ForRepositoryForMySql(IServiceCollection services)
        {
            //services.AddTransient<FlexEFDbContext, ApplicationEFMySqlDbContext>();

            services.AddTransient<IFlexRepositoryBridge, FlexWriteDbRepositoryEFMySql>();
            services.AddTransient<IFlexQueryRepositoryBridge, FlexReadDbRepositoryEFMySql>();

            services.AddTransient<IFlexQueryableExtensionInclude, FlexQueryableExtensionEFCore>();
        }

        public static void ForMultitenantMasterDbForMySql(IServiceCollection services)
        {
            services.AddTransient<IFlexNativeHostTenantProviderBridge, FlexNativeHostTenantProviderBridge>();
            services.AddTransient(typeof(IFlexTenantRepository<FlexTenantBridge>), typeof(FlexTenantMasterRepositoryEFMySql));
            services.AddTransient(typeof(FlexEFTenantContext<FlexTenantBridge>), typeof(FlexEFTenantMasterMySqlContext));
            services.AddTransient(typeof(IFlexTenantProvider<FlexTenantBridge, FlexAppContextBridge>), typeof(FlexNativeHostTenantProviderBridge));
        }
    }
}