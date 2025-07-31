namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings required for database connections.
    /// </summary>
    public class ConnectionSettings
    {
        /// <summary>
        /// Gets or sets the connection string used to establish a database connection.
        /// </summary>
        public string ConnectionString { get; set; } = "";

        /// <summary>
        /// Gets or sets the name of the database provider.
        /// </summary>
        public string ProviderName { get; set; } = "";
    }
}