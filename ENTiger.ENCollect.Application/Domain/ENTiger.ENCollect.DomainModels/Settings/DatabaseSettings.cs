namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings required for database and messaging service connections.
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// Gets or sets the connection string for the application database.
        /// </summary>
        public string AppDbConnection { get; set; } = "";

        /// <summary>
        /// Gets or sets the connection string for the application's read-only database.
        /// </summary>
        public string AppReadDbConnection { get; set; } = "";

        /// <summary>
        /// Gets or sets the connection string for Azure Storage.
        /// </summary>
        public string AzureStorageConnectionString { get; set; } = "";

        /// <summary>
        /// Gets or sets the connection string for SQL Transport.
        /// </summary>
        public string SqlTransportConnectionString { get; set; } = "";

        /// <summary>
        /// Gets or sets the connection string for SQL Persistence.
        /// </summary>
        public string SqlPersistenceConnectionString { get; set; } = "";

        /// <summary>
        /// Gets or sets the connection string for RabbitMQ.
        /// </summary>
        public string RabbitMqConnectionString { get; set; } = "";

        /// <summary>
        /// Gets or sets the connection string for Azure Service Bus.
        /// </summary>
        public string AzureServiceBusConnectionString { get; set; } = "";

        /// <summary>
        /// Gets or sets the bucket name for Amazon SQS transport.
        /// </summary>
        public string AmazonSQSTransportBucketName { get; set; } = "";

        /// <summary>
        /// Gets or sets the key prefix for Amazon SQS transport.
        /// </summary>
        public string AmazonSQSTransportKeyPrefix { get; set; } = "";

        /// <summary>
        /// Gets or sets the connection string for the tenant master database.
        /// </summary>
        public string TenantMasterDbConnection { get; set; } = "";

        /// <summary>
        /// Gets or sets the type of database used (e.g., MySQL, SQL Server).
        /// Default value is "MySql".
        /// </summary>
        public string DBType { get; set; } = "MySql";

        /// <summary>
        /// Gets or sets the command timeout duration in seconds. Default is 60 seconds.
        /// </summary>
        public int CommandTimeout { get; set; } = 60;

        /// <summary>
        /// Gets or sets the default tenant value to load data from tenant DB
        /// Default value is "1".
        /// </summary>
        public string DefaultTenant { get; set; } = "1";
    }
}