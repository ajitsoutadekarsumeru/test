using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AzureStorageQueueNsbConfiguration : NsbBaseConfiguration
    {
        public AzureStorageQueueNsbConfiguration(string endpointName, IEnumerable<BusRouteConfig> routes, IConfiguration configuration, IHostEnvironment env, string errorQueueName = "error") : base(endpointName, configuration, env, errorQueueName)
        {
            Guard.AgainstNullAndEmpty("AzureBusStorageConnectionString for connection string cannot be empty", configuration.GetSection("FlexBase")["AzureStorageConnectionString"]);
            string azureConnectionString = configuration.GetSection("FlexBase")["AzureStorageConnectionString"];

            //Configure persistence
            var persistence = this.UsePersistence<AzureTablePersistence>();
            persistence.ConnectionString(azureConnectionString);

            var queueServiceClient = new QueueServiceClient(azureConnectionString, new QueueClientOptions());
            var blobServiceClient = new BlobServiceClient(azureConnectionString, new BlobClientOptions());
            var tableServiceClient = new TableServiceClient(azureConnectionString, new TableClientOptions());

            var transport = new AzureStorageQueueTransport(queueServiceClient, blobServiceClient, tableServiceClient)
            {
                ReceiverBatchSize = 20,
                MaximumWaitTimeWhenIdle = TimeSpan.FromSeconds(1),
                DegreeOfReceiveParallelism = 16,
                PeekInterval = TimeSpan.FromMilliseconds(100),
                MessageInvisibleTime = TimeSpan.FromSeconds(30)
            };

            var routing = this.UseTransport(transport);
            //foreach (var route in routes)
            //{
            //    routing.RouteToEndpoint(route.Assembly, route.Destination);
            //    routing.RegisterPublisher(route.Assembly, route.Destination);
            //}
            foreach (var route in routes)
            {
                if (route.Assembly != null && route.Namespace != null)
                {
                    routing.RouteToEndpoint(route.Assembly, route.Namespace, route.Destination);
                    routing.RegisterPublisher(route.Assembly, route.Namespace, route.Destination);
                }
                else if (route.Type != null)
                {
                    routing.RouteToEndpoint(route.Type, route.Destination);
                    routing.RegisterPublisher(route.Type, route.Destination);
                }
                else if (route.Assembly != null)
                {
                    routing.RouteToEndpoint(route.Assembly, route.Destination);
                    routing.RegisterPublisher(route.Assembly, route.Destination);
                }
            }
        }
    }
}