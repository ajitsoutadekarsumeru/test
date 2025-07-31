using Elastic.Clients.Elasticsearch;

namespace ENTiger.ENCollect
{
    public interface IELKUtility
    {
        ElasticsearchClient GetElasticConnection();

        string GetFilterTextForElasticSearch(string input);
    }
}