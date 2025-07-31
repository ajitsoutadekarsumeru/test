using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class SqlNsbConfiguration : NsbBaseConfiguration
    {
        public SqlNsbConfiguration(string endpointName, IEnumerable<BusRouteConfig> routes, IConfiguration configuration, IHostEnvironment env, string errorQueueName = "error") : base(endpointName, configuration, env, errorQueueName)
        {
            Guard.AgainstNullAndEmpty("SqlPersistenceConnectionString for connection string cannot be empty", configuration.GetSection("FlexBase")["SqlPersistenceConnectionString"]);
            string sqlPersistenceConnectionString = configuration.GetSection("FlexBase")["SqlPersistenceConnectionString"];

            Guard.AgainstNullAndEmpty("SqlTransportConnectionString for connection string cannot be empty", configuration.GetSection("FlexBase")["SqlTransportConnectionString"]);
            string sqlTransportConnectionString = configuration.GetSection("FlexBase")["SqlTransportConnectionString"];

            //Configure persistence
            var persistence = this.UsePersistence<SqlPersistence>();
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(sqlPersistenceConnectionString);
                });

            //Configure transport
            var transport = new SqlServerTransport(sqlTransportConnectionString);
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