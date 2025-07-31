using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ENTiger.ENCollect
{
    public static class DbEndPointConfig
    {
        public static void AddFlexBaseDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuration for Single Tenant Database
            //EnableSingleTenant(services);

            EnableMultitenant(services, configuration);
        }

        private static void EnableSingleTenant(IServiceCollection services)
        {
            ConfigureSingleTenantDb(services);
            ConfigureFlexDb.ForRepositoryForMySql(services);
        }

        private static void EnableMultitenant(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureMultiTenantMasterDb(services);

            //Either
            //If you want to use the same sql server for all instances for multitenant application
            var dbType = configuration.GetSection("FlexBase")["DBType"];
            if (dbType == DbTypeConstants.MySql)
            {
                ConfigureFlexDb.ForMultitenantMasterDbForMySql(services);
                ConfigureFlexDb.ForRepositoryForMySql(services);
            }
            else
            {
                ConfigureFlexDb.ForMultitenantMasterDbForSqlServer(services);
                ConfigureFlexDb.ForRepositoryForSqlServer(services);
            }

            //Or
            //ConfigureFlexDb.ForRepositoryForMultiDb(services); //If you want to use different database type for each tenant
        }

        private static void ConfigureSingleTenantDb(IServiceCollection services)
        {
            //Default Settings for connection provider which gives you a simple single tenant application
            services.AddTransient<IWriteDbConnectionProviderBridge, AppSettingsWriteDbConnectionProvider>();
            services.AddTransient<IReadDbConnectionProviderBridge, AppSettingsReadDbConnectionProvider>();
        }

        private static void ConfigureMultiTenantMasterDb(this IServiceCollection services)
        {
            //Default Settings for connection provider which gives you a simple multi tenant application
            services.AddTransient<IWriteDbConnectionProviderBridge, NativeWriteDbTenantConnectionProvider>();
            services.AddTransient<IReadDbConnectionProviderBridge, NativeReadDbTenantConnectionProvider>();
        }
    }
}