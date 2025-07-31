using Amazon.S3;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AmazonSQSNsbConfiguration : NsbBaseConfiguration
    {
        public AmazonSQSNsbConfiguration(string endpointName, IEnumerable<BusRouteConfig> routes, IConfiguration configuration, IHostEnvironment env, string errorQueueName = "error") : base(endpointName, configuration, env, errorQueueName)
        {
            Guard.AgainstNullAndEmpty("SqlPersistenceConnectionString for connection string cannot be empty", configuration.GetSection("FlexBase")["SqlPersistenceConnectionString"]);
            string sqlPersistenceConnectionString = configuration.GetSection("FlexBase")["SqlPersistenceConnectionString"];

            Guard.AgainstNullAndEmpty("AmazonSQSTransportBucketName for connection string cannot be empty", configuration.GetSection("FlexBase")["AmazonSQSTransportBucketName"]);
            string s3BucketName = configuration.GetSection("FlexBase")["AmazonSQSTransportBucketName"];

            Guard.AgainstNullAndEmpty("AmazonSQSTransportKeyPrefix for connection string cannot be empty", configuration.GetSection("FlexBase")["AmazonSQSTransportKeyPrefix"]);
            string s3KeyPrefix = configuration.GetSection("FlexBase")["AmazonSQSTransportKeyPrefix"];

            //Configure persistence
            var persistence = this.UsePersistence<SqlPersistence>();
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(sqlPersistenceConnectionString);
                });

            var transport = new SqsTransport
            {
                S3 = new S3Settings(
                    bucketForLargeMessages: s3BucketName,
                    keyPrefix: s3KeyPrefix,
                    s3Client: new AmazonS3Client())
                {
                }
            };

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