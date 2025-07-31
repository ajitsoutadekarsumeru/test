using Elastic.Transport;
using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetNonSegmentedAndTreatmented : FlexiQueryBridgeAsync<GetNonSegmentedAndTreatmentedDto>
    {
        protected readonly ILogger<GetNonSegmentedAndTreatmented> _logger;
        protected GetNonSegmentedAndTreatmentedParams _params;
        private readonly IELKUtility _elasticUtility;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetNonSegmentedAndTreatmented(ILogger<GetNonSegmentedAndTreatmented> logger, IRepoFactory repoFactory, IELKUtility elasticUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _elasticUtility = elasticUtility;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetNonSegmentedAndTreatmented AssignParameters(GetNonSegmentedAndTreatmentedParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetNonSegmentedAndTreatmentedDto> Fetch()
        {
            decimal? TotalBOM_POS = 0;
            _repoFactory.Init(_params);

            GetNonSegmentedAndTreatmentedDto output = new GetNonSegmentedAndTreatmentedDto();
            output.TreatmentBOM_POS = 0;
            output.TreatmentBOM_POSCount = 0;
            var client = _elasticUtility.GetElasticConnection();

            var fetchindexname =await _repoFactory.GetRepo().FindAll<FeatureMaster>().Where(a => a.Parameter == "SegmentationIndexName").FirstOrDefaultAsync();

            string loanaccountsIndex = fetchindexname?.Value;
            string elasticsearchapipath = loanaccountsIndex + "/_search";
            string DSLQueryForAllDocs1 = @"
                        {
                                      ""track_total_hits"": true,
                                      ""from"": 0,
                                      ""size"": 0,
                                      ""query"": {
                                                    ""match_all"": { }
                                                }
                                      ,
                                      ""aggs"": {
                                                    ""bom_pos"": { ""sum"": { ""field"": ""bom_pos"" } }
                                                }
                     }

            "
            ;

            var ElkResp1 = client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, elasticsearchapipath, PostData.String(DSLQueryForAllDocs1)).GetAwaiter().GetResult();

            string response1 = ElkResp1.Body;
            dynamic RootObj1 = JObject.Parse(response1);

            var bomposvalue1 = RootObj1.aggregations.bom_pos.value;

            TotalBOM_POS = bomposvalue1;

            if (_params.NotSegmented == true)
            {
                long BOM_POS = 0;
                int unstampedRecordCount = 0;

                string DSLQueryForAllDocs = @"{
  ""track_total_hits"": true,
  ""from"": 0,
  ""size"": 0,
  ""query"": {
                    ""bool"": {
                        ""should"": [
                          {
                            ""bool"": {
                                ""must_not"": [
                                 {
                                    ""exists"": {
                                        ""field"": ""segmentationid""
                                    }
                                }
            ]
          }
                        },
        {
                            ""match"": {
                                ""segmentationid"": """"
                            }
                        }
      ]
    }
                },
""aggs"": {
                    ""bom_pos"": {
                        ""sum"": {
                            ""field"": ""bom_pos""
                        }
                    }
                }
            }";

                var ElkResp = client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, elasticsearchapipath, PostData.String(DSLQueryForAllDocs)).GetAwaiter().GetResult();

                string response = ElkResp.Body;
                dynamic RootObj = JObject.Parse(response);
                int count = RootObj.hits.total.value;
                var bomposvalue = RootObj.aggregations.bom_pos.value;
                output.SegmentBOM_POSCount = count;
                output.SegmentBOM_POS = bomposvalue;

                if (TotalBOM_POS != 0)
                {
                    output.SegmentBOM_POSPercentage = ((output.SegmentBOM_POS / TotalBOM_POS) * 100).Value.ToString("0.##");
                }
            }
            else if (_params.NotTreated == true)
            {
                var res = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(a => a.TreatmentId == null || a.TreatmentId == "").Select(a => new { a.BOM_POS }).ToListAsync();
                if (res.Count() > 0)
                {
                    output.TreatmentBOM_POSCount = res.Count();
                    output.TreatmentBOM_POS = res.Sum(a => a.BOM_POS);
                }
                if (TotalBOM_POS != 0)
                {
                    output.TreatmentBOM_POSPercentage = ((output.TreatmentBOM_POS / TotalBOM_POS) * 100).Value.ToString("0.##");
                }
            }

            return output;
        }
    }

    public class GetNonSegmentedAndTreatmentedParams : DtoBridge
    {
        public bool? NotSegmented { get; set; }

        public bool? NotTreated { get; set; }
    }
}