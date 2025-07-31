using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Options;

namespace ENTiger.ENCollect
{
    public class ElasticUtility : IELKUtility
    {
        private readonly ElasticSearchSettings _elasticSettings;
        public ElasticUtility(IOptions<ElasticSearchSettings> elasticSettings)
        {
            _elasticSettings = elasticSettings.Value;
        }
        public ElasticsearchClient GetElasticConnection()
        {
            string elasticsearchurl = _elasticSettings.Connection.Url;
            string esusername = _elasticSettings.Connection.Username;
            string espassword = _elasticSettings.Connection.Password;

            var settings = new ElasticsearchClientSettings(new Uri(elasticsearchurl))
                        .Authentication(new BasicAuthentication(esusername, espassword));

            var client = new ElasticsearchClient(settings);

            return client;
        }

        public string GetFilterTextForElasticSearch(string input)
        {
            if (input is null)
                return MagickString.NoFilterPresent;

            input = input.Trim().ToUpper();

            if (input.Equals("ALL") || input.Equals("") || input.ToUpper().Equals(MagickString.NoFilterPresent.ToUpper()))
                return MagickString.NoFilterPresent;
            else if (input.Contains("*"))
            {
                string s = "\\\"*\\\"";
                input = input.Replace("*", s);
                return input;
            }
            else return input;
        }
    }
}