using Elastic.Transport;
using ENTiger.ENCollect.DTOs.FeatureDtos.Segmentation.Output.SimulateSegment;
using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public class SimulateSegment : FlexiQueryEnumerableBridgeAsync<SimulateSegmentDto>
    {
        protected readonly ILogger<SimulateSegment> _logger;
        protected SimulateSegmentParams _params;
        protected readonly IRepoFactory _repoFactory;
        private readonly IELKUtility _elasticUtility;
        private string SegmentIndexName;
        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public SimulateSegment(ILogger<SimulateSegment> logger, IRepoFactory repoFactory, IELKUtility elasticUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _elasticUtility = elasticUtility;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SimulateSegment AssignParameters(SimulateSegmentParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SimulateSegmentDto>> Fetch()
        {
            List<SimulateSegmentDto> outputModel = new List<SimulateSegmentDto>();
            double? TotalBOMPos = 0;
            _repoFactory.Init(_params);

            var fetchindexname = await _repoFactory.GetRepo().FindAll<FeatureMaster>().Where(a => a.Parameter == "SegmentationIndexName").FirstOrDefaultAsync();

            SegmentIndexName = fetchindexname?.Value;

            IEnumerable<SimulateSegmentDto> result = null;

            foreach (var segmentId in _params.SegmentIds)
            {
                var elasticsearch = FetchSegmentedAccounts(segmentId);

                SimulateSegmentDto model = new SimulateSegmentDto();
                model.BOMPos = elasticsearch != null ? elasticsearch.BOM_POS : 0;
                model.SegmentId = segmentId;
                model.Count = elasticsearch != null ? elasticsearch.Count : 0;
                outputModel.Add(model);
            }

            var elasticsearchtwo = GetUnSegmentedAccounts();

            if (elasticsearchtwo != null)
            {
                SimulateSegmentDto model = new SimulateSegmentDto();
                model.BOMPos = elasticsearchtwo != null ? elasticsearchtwo.BOM_POS : 0;
                model.SegmentId = "";
                model.Count = elasticsearchtwo != null ? elasticsearchtwo.Count : 0;
                outputModel.Add(model);
            }

            var segments = await _repoFactory.GetRepo().FindAll<Segmentation>().Where(a => _params.SegmentIds.Contains(a.Id)).ToListAsync();
            if (segments.Count() > 0)
            {
                outputModel.ForEach(async a =>
                {
                    var res = segments.Where(b => b.Id == a.SegmentId).FirstOrDefault();
                    a.SegmentName = res != null ? res.Name : "";
                });
            }

            var elasticsearchthree = GetFetchAllAccounts();

            if (elasticsearchthree != null)
            {
                TotalBOMPos = elasticsearchthree.BOM_POS;
            }
            if (outputModel != null)
            {
                outputModel.ForEach(a =>
                {
                    if (TotalBOMPos != 0)
                    {
                        double? bomposvalue = a.BOMPos != null ? a.BOMPos : 0;
                        a.BOMPosPercentage = ((bomposvalue / TotalBOMPos) * 100).Value.ToString("0.##"); ;
                    }
                    else
                    {
                        a.BOMPosPercentage = "0";
                    }
                });
            }
            //packet.Output = outputModel.ToListAsync();

            return outputModel;
        }

        public SimulateElasticSearchResult FetchSegmentedAccounts(string segmentationid)
        {
            SimulateElasticSearchResult result = new SimulateElasticSearchResult();
            var client = _elasticUtility.GetElasticConnection();
            string elasticsearchapipath = SegmentIndexName + "/_search";

            string DSLQueryForAllDocs = @"
                                          {
                                                  ""track_total_hits"": true,
                                                  ""from"": 0,
                                                  ""size"": 0,
                                                  ""query"": {
                                                                ""bool"": {
                                                                    ""filter"": [
                                                                      {
                                                          ""term"": {
                                                            ""segmentationid"": " + "\"" + segmentationid + "\"" +
                                                          "}}]}}," +
                                                  "\"aggs\":" + "{" + "\"bom_pos\":" + "{ " + "\"sum\":" + " { " + "\"field\":" + "\"bom_pos\"" + "} }" +
                                                 "}" +
                                           "}";

            var ElkResp = client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, elasticsearchapipath, PostData.String(DSLQueryForAllDocs)).GetAwaiter().GetResult();

            string response = ElkResp.Body;
            dynamic RootObj = JObject.Parse(response);
            int count = RootObj.hits.total.value;
            var bomposvalue = RootObj.aggregations.bom_pos.value;
            result.Count = count;
            result.BOM_POS = bomposvalue;
            return result;
        }

        public SimulateElasticSearchResult GetFetchAllAccounts()
        {
            SimulateElasticSearchResult result = new SimulateElasticSearchResult();
            var client = _elasticUtility.GetElasticConnection();
            string elasticsearchapipath = SegmentIndexName + "/_search";

            string DSLQueryForAllDocs = @"
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

            ";

            var ElkResp = client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, elasticsearchapipath, PostData.String(DSLQueryForAllDocs)).GetAwaiter().GetResult();

            string response = ElkResp.Body;
            dynamic RootObj = JObject.Parse(response);
            int count = RootObj.hits.total.value;
            var bomposvalue = RootObj.aggregations.bom_pos.value;
            result.Count = count;
            result.BOM_POS = bomposvalue;
            return result;
        }

        public SimulateElasticSearchResult GetUnSegmentedAccounts()
        {
            SimulateElasticSearchResult result = new SimulateElasticSearchResult();
            var client = _elasticUtility.GetElasticConnection();
            string elasticsearchapipath = SegmentIndexName + "/_search";
            string segmentationid = "";

            string DSLQueryForAllDocs = @"
                                          {
                                                  ""track_total_hits"": true,
                                                  ""from"": 0,
                                                  ""size"": 0,
                                                  ""query"": {
                                                                ""bool"": {
                                                                    ""filter"": [
                                                                      {
                                                          ""term"": {
                                                            ""segmentationid"": " + "\"" + segmentationid + "\"" +
                                          "}}]}}," +
                                  "\"aggs\":" + "{" + "\"bom_pos\":" + "{ " + "\"sum\":" + " { " + "\"field\":" + "\"bom_pos\"" + "} }" +
                                 "}" +
                           "}";

            var ElkResp = client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, elasticsearchapipath, PostData.String(DSLQueryForAllDocs)).GetAwaiter().GetResult();

            string response = ElkResp.Body;
            dynamic RootObj = JObject.Parse(response);
            int count = RootObj.hits.total.value;
            var bomposvalue = RootObj.aggregations.bom_pos.value;
            result.Count = count;
            result.BOM_POS = bomposvalue;
            return result;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SimulateSegmentParams : DtoBridge
    {
        public List<string> SegmentIds { get; set; }
    }
}