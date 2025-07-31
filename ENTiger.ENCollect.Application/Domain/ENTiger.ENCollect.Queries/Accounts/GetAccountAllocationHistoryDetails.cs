using System.ComponentModel.DataAnnotations;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetAccountAllocationHistoryDetails : FlexiQueryEnumerableBridgeAsync<GetAccountAllocationHistoryDetailsDto>
    {
        protected readonly ILogger<GetAccountAllocationHistoryDetails> _logger;
        protected GetAccountAllocationHistoryDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;
        private string? allocationHistoryIndexName;
        private readonly ElasticSearchSettings _elasticSettings;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetAccountAllocationHistoryDetails(ILogger<GetAccountAllocationHistoryDetails> logger, IRepoFactory repoFactory, IOptions<ElasticSearchSettings> elasticSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _elasticSettings = elasticSettings.Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAccountAllocationHistoryDetails AssignParameters(GetAccountAllocationHistoryDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetAccountAllocationHistoryDetailsDto>> Fetch()
        {
            string elasticSearchUrl = _elasticSettings.Connection.Url;
            string esusername = _elasticSettings.Connection.Username;
            string espassword = _elasticSettings.Connection.Password;

            _repoFactory.Init(_params);

            var indexdata = await _repoFactory.GetRepo().FindAll<FeatureMaster>().Where(a => a.Parameter == "loanaccountallocationhistoryindex").FirstOrDefaultAsync();

            if (indexdata != null)
            {
                allocationHistoryIndexName = indexdata.Value;
            }

            string elasticsearchapipath = allocationHistoryIndexName + "/_search";
            var settings = new ElasticsearchClientSettings(new Uri(elasticSearchUrl))
                        .Authentication(new BasicAuthentication(esusername, espassword));

            var client = new ElasticsearchClient(settings);

            List<GetAccountAllocationHistoryDetailsDto> result = new List<GetAccountAllocationHistoryDetailsDto>();

            string? DSLQueryForAllDocs = @"
                                            {
                                              ""from"": " + _params.skip + "," +
                                              "\"size\":" + _params.take + "," +
                                              "\"track_total_hits\":" + "true," +
                                              "\"query\":{" +
                                                "\"bool\": {" +
                                                  "\"must\": [" +
                                            "";

            string AccountNo = _params.AccountNumber;

            if (!string.IsNullOrEmpty(AccountNo))
            {
                DSLQueryForAllDocs += $@"
                                                            {{
                                                              ""query_string"": {{
                                                                ""default_field"": ""AccountNo"",
                                                                ""query"": ""{AccountNo}""
                                                              }}
                                                            }}
                                                            ";
            }

            DSLQueryForAllDocs += @"
                                                  ]
                                                }
                                              }
                                            ";

            DSLQueryForAllDocs += $@"
              , ""sort"": [ " + "{ \"CreatedDate\": {\"order\": \"desc\"}}]}";

            var ElkResp = client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, elasticsearchapipath, PostData.String(DSLQueryForAllDocs)).GetAwaiter().GetResult();

            string response = ElkResp.Body;
            dynamic RootObj = JObject.Parse(response);
            int count = RootObj.hits.total.value;

            foreach (var obj1 in RootObj.hits.hits)
            {
                GetAccountAllocationHistoryDetailsDto model = new GetAccountAllocationHistoryDetailsDto();
                var obj = obj1._source;
                model.AccountNo = obj.AccountNo;
                model.ExecutionTime = obj.CreatedDate;
                model.AllocatedByUserCode = obj.AllocatedByUserCode;
                model.AllocatedByUserName = obj.AllocatedByUserName;
                model.AllocationOwnerCode = obj.AllocationOwnerCode;
                model.AllocationOwnerName = obj.AllocationOwnerName;
                model.FieldAgencyAllocationExpiryDate = obj.FieldAgencyAllocationExpiryDate;
                model.FieldAgencyCode = obj.FieldAgencyCode;
                model.FieldAgencyName = obj.FieldAgencyName;
                model.FieldAgentOrStaffAllocationExpiryDate = obj.FieldAgentOrStaffAllocationExpiryDate;
                model.FieldAgentOrStaffCode = obj.FieldAgentOrStaffCode;
                model.FieldAgentOrStaffName = obj.FieldAgentOrStaffName;
                model.MethodOfAllocation = obj.MethodOfAllocation;
                model.TCAgencyCode = obj.TCAgencyCode;
                model.TCAgencyName = obj.TCAgencyName;
                model.TCAgentCode = obj.TCAgentCode;
                model.TCAgentName = obj.TCAgentName;
                model.TransactionIdOrName = obj.TransactionIdOrName;
                model.TypeOfAllocation = obj.TypeOfAllocation;
                model.CreatedDate = obj.CreatedDate;

                result.Add(model);
            }

            return result;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetAccountAllocationHistoryDetailsParams : DtoBridge
    {
        [Required]
        public string AccountNumber { get; set; }

        public int skip { get; set; }
        public int take { get; set; }
    }
}