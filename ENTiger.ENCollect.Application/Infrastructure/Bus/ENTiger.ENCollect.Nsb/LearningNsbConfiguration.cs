using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class LearningNsbConfiguration : NsbBaseConfiguration
    {
        public LearningNsbConfiguration(string endpointName, IEnumerable<BusRouteConfig> routes, IConfiguration configuration, IHostEnvironment env, string errorQueueName = "error") : base(endpointName, configuration, env, errorQueueName)
        {
            string sqlPersistenceConnectionString = configuration.GetSection("FlexBase")["SqlPersistenceConnectionString"];

            //Configure persistence
            //var persistence = this.UsePersistence<LearningPersistence>();
            //Configure persistence
            var persistence = this.UsePersistence<SqlPersistence>();
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(sqlPersistenceConnectionString);
                });
            //Configure transport
            var transport = this.UseTransport<LearningTransport>();

            //The endpoint will traverse the folder hierarchy upwards in search for a .learningtransport directory or create one at the solution root folder if no matching folder has been found
            //transport.StorageDirectory("PathToStoreTransportFiles");

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