using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class RabbitMqNsbConfiguration : NsbBaseConfiguration
    {
        public RabbitMqNsbConfiguration(string endpointName, IEnumerable<BusRouteConfig> routes, IConfiguration configuration, IHostEnvironment env, string errorQueueName = "error") : base(endpointName, configuration, env, errorQueueName)
        {
            Guard.AgainstNullAndEmpty("SqlPersistenceConnectionString for connection string cannot be empty", configuration.GetSection("FlexBase")["SqlPersistenceConnectionString"]);
            string sqlPersistenceConnectionString = configuration.GetSection("FlexBase")["SqlPersistenceConnectionString"];

            Guard.AgainstNullAndEmpty("RabbitMqConnectionString for connection string cannot be empty", configuration.GetSection("FlexBase")["RabbitMqConnectionString"]);
            string rabbitMqConnectionString = configuration.GetSection("FlexBase")["RabbitMqConnectionString"];

            //Configure persistence
            var persistence = this.UsePersistence<SqlPersistence>();
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(sqlPersistenceConnectionString);
                });

            //Configure transport
            var transport = this.UseTransport<RabbitMQTransport>();
            transport.ConnectionString(rabbitMqConnectionString);
            transport.UseConventionalRoutingTopology(QueueType.Classic);

            //Configure route
            var routing = transport.Routing();
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