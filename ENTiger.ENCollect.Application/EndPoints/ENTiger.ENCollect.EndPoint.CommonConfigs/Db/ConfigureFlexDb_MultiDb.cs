using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public partial class ConfigureFlexDb
    {
        public static void ForRepositoryForMultiDb(IServiceCollection services)
        {
            //services.AddTransient<FlexEFDbContext, ApplicationEFSqlServerDbContext>();

            services.AddTransient<IFlexRepositoryBridge, FlexWriteRepositoryEFMultiDb>();
            services.AddTransient<IFlexQueryRepositoryBridge, FlexReadRepositoryEFMultiDb>();

            services.AddTransient<IFlexQueryableExtensionInclude, FlexQueryableExtensionEFCore>();
        }
    }
}