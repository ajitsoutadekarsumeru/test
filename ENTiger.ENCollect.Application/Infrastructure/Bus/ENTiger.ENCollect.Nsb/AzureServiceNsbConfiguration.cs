using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AzureServiceNsbConfiguration : NsbBaseConfiguration
    {
        public AzureServiceNsbConfiguration(string endpointName, IEnumerable<BusRouteConfig> routes, IConfiguration configuration, IHostEnvironment env, string errorQueueName = "error") : base(endpointName, configuration, env, errorQueueName)
        {
            Guard.AgainstNullAndEmpty("SqlPersistenceConnectionString for connection string cannot be empty", configuration.GetSection("FlexBase")["SqlPersistenceConnectionString"]);
            string sqlPersistenceConnectionString = configuration.GetSection("FlexBase")["SqlPersistenceConnectionString"];

            Guard.AgainstNullAndEmpty("AzureServiceBusConnectionString for connection string cannot be empty", configuration.GetSection("FlexBase")["AzureServiceBusConnectionString"]);
            string azureServiceBusConnectionString = configuration.GetSection("FlexBase")["AzureServiceBusConnectionString"];

            //Configure persistence
            var persistence = this.UsePersistence<SqlPersistence>();
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(sqlPersistenceConnectionString);
                });

            var transport = new AzureServiceBusTransport(azureServiceBusConnectionString);
            this.UseTransport(transport);

            //Configure route
            var routing = this.UseTransport(transport);
            foreach (var route in routes)
            {
                if (route.Assembly != null && route.Namespace != null)
                {
                    routing.RouteToEndpoint(route.Assembly, route.Namespace, route.Destination);
                }
                else if (route.Type != null)
                {
                    routing.RouteToEndpoint(route.Type, route.Destination);
                }
                else if (route.Assembly != null)
                {
                    routing.RouteToEndpoint(route.Assembly, route.Destination);
                }
            }
        }
    }
}