using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Elasticsearch connection settings 
    /// </summary>
    public class ElasticSearchSettings
    {
        /// <summary>
        /// Gets or sets the connection details for Elasticsearch.
        /// </summary>
        public ElasticSearchConnection Connection { get; set; } = new ElasticSearchConnection();

        /// <summary>
        ///  Sets the batchsize that Elasticsearch processes in each iteration of download file operation
        /// </summary>
        public long RecordPerLoopSize { get; set; } = 10000;

        /// <summary>
        /// Setting defines the maximum allowable window size (i.e., the number of documents that can be retrieved in one scroll request)
        /// </summary>
        public long MaxWindowSize { get; set; } =  100000;


    }
    /// <summary>
    /// There is only one Elasticsearch connection configured for all SaaS clients, and we are using the same ES connectivity details for all tenants.
    /// For on-premises clients, there will be separate ES connectivity details for each environment. 
    /// Hence elasticsearch settings are configured once not tenant wise.
    /// </summary>
    public class ElasticSearchConnection
    {
        /// <summary>
        /// To set and get ElasticSearch url
        /// </summary>
        [Required]
        public string Url { get; set; } = "";

        /// <summary>
        /// To set and get ElasticSearch username
        /// </summary>
        [Required]
        public string Username { get; set; } = "";

        /// <summary>
        /// To set and get ElasticSearch password
        /// </summary>
        [Required]
        public string Password { get; set; } = "";

    }
}