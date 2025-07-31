using System.Data;
using Elastic.Transport;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Newtonsoft.Json.Linq;
using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ClearSegmentAndTreatmentPlugin : FlexiPluginBase, IFlexiPlugin<ClearSegmentAndTreatmentPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a147cc4dad9658c048e2746717a4914";
        public override string FriendlyName { get; set; } = "ClearSegmentAndTreatmentPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ClearSegmentAndTreatmentPlugin> _logger;
        protected readonly IFlexHost _flexHost;

        private readonly IELKUtility _elasticUtility;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IRepoFactory _repoFactory;

        private List<ElasticSearchClearSegmentAndTreatmentDto> elasticsearchagreeementids = new List<ElasticSearchClearSegmentAndTreatmentDto>();
        private string TenantId = string.Empty;
        private string? sqlConnectionString = string.Empty;
        private string hostName = string.Empty;
        private string dbType = string.Empty;
        private readonly DatabaseSettings _databaseSettings;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public ClearSegmentAndTreatmentPlugin(ILogger<ClearSegmentAndTreatmentPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IOptions<DatabaseSettings> databaseSettings, IELKUtility elasticUtility)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _elasticUtility = elasticUtility;
            _databaseSettings = databaseSettings.Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(ClearSegmentAndTreatmentPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();
            _repoFactory.Init(packet.Cmd.Dto);
            hostName = _flexAppContext.HostName;
            dbType = _databaseSettings.DBType;
            IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();

            sqlConnectionString = await _repoTenantFactory.FindAll<FlexTenantBridge>()
                                            .Where(x => x.Id == _flexAppContext.TenantId)
                                            .Select(x => x.DefaultWriteDbConnectionString)
                                            .FirstOrDefaultAsync();

            if (packet.Cmd.Dto.ClearSegment == true)
            {
                _logger.LogInformation("ClearSegmentAndTreatmentFFPlugin : ClearSegment | SegmentIds : " + string.Join(",", packet.Cmd.Dto.SegmentIds));
                foreach (var segmentId in packet.Cmd.Dto.SegmentIds)
                {
                    await GetElasticSearchLoanAccounts(segmentId);
                }
                await ClearSegmentTreatment();
            }
            else if (packet.Cmd.Dto.ClearTreatment == true)
            {
                _logger.LogInformation("ClearSegmentAndTreatmentFFPlugin : ClearTreatment | TreatmentIds : " + string.Join(",", packet.Cmd.Dto.TreatmentIds));
                ClearTreatment(packet.Cmd.Dto.TreatmentIds);
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GetElasticSearchLoanAccounts(string SegmentId)
        {
            TenantId = _flexAppContext.TenantId;
            var client = _elasticUtility.GetElasticConnection();

            var fetchindexname = await _repoFactory.GetRepo().FindAll<FeatureMaster>().Where(a => a.Parameter == "SegmentationIndexName").FirstOrDefaultAsync();

            string loanaccountsIndex = fetchindexname?.Value;//InitFlexEF.GetElasticSearchConnection(_TenantId).LoanAccountIndexName;

            string elasticsearchapipath = loanaccountsIndex + "/_search";
            string DSLQueryForAllDocs1 = @"

                {
                  ""track_total_hits"": true,
                  ""from"":0,
                  ""size"": 0,
                  ""query"": {
                  ""terms"": {
                        ""segmentationid"":[
                ";

            DSLQueryForAllDocs1 += $@"
                ""{SegmentId}""
                 ]
                }}
                }}
                }}
                ";

            var elkresp1 = await client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, elasticsearchapipath, PostData.String(DSLQueryForAllDocs1));

            string response1 = elkresp1.Body;
            dynamic RootObj1 = JObject.Parse(response1);
            int recordCount = RootObj1.hits.total.value;
            _logger.LogInformation("ClearSegAndTreatment Fetch Count log  " + DSLQueryForAllDocs1);
            _logger.LogInformation("ClearSegAndTreatment Segmentation record count " + recordCount);

            if (recordCount > 0)
            {
                var pages = (recordCount / 5000) + 1;
                _logger.LogInformation("Pages count " + pages);
                for (long page = 0; page < pages; page++)
                {
                    int fromRecord = (int)(page * 5000);
                    _logger.LogInformation("ClearSegAndTreatment fromRecord count " + fromRecord);
                    string DSLQueryForAllDocs2 = @"

                        {
                          ""track_total_hits"": true,
                          ""_source"": [""AGREEMENTID"",""id"",""bom_pos"",""segmentationid""],
                          ""from"":";

                    DSLQueryForAllDocs2 += $@"
                        ""{fromRecord}"",
                        ""size"": 5000,
                          ""query"": {{
                          ""terms"": {{
                                ""segmentationid"":[
                        ";

                    DSLQueryForAllDocs2 += $@"
                        ""{SegmentId}""
                         ]
                        }}
                        }}
                        }}
                        ";
                    _logger.LogInformation("ClearSegAndTreatment Fetch loanaccounts  " + DSLQueryForAllDocs2);
                    var elkresp2 = await client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, elasticsearchapipath, PostData.String(DSLQueryForAllDocs2));

                    if (elkresp2 != null && elkresp2.Body != null)
                    {
                        string response2 = elkresp2.Body;
                        dynamic RootObj = JObject.Parse(response2);

                        //var results = RootObj.hits._source;

                        if (RootObj != null && RootObj.hits != null && RootObj.hits.hits != null)
                        {
                            foreach (var res in RootObj.hits.hits)
                            {
                                ElasticSearchClearSegmentAndTreatmentDto loanaccount = new ElasticSearchClearSegmentAndTreatmentDto();
                                var obj = res._source;
                                if (obj != null)
                                {
                                    loanaccount.id = obj.id != null ? obj.id : "";
                                    loanaccount.segmentationid = obj.segmentationid != null ? obj.segmentationid : "";

                                    loanaccount.agreementid = obj.AGREEMENTID != null ? obj.AGREEMENTID : "";
                                    //logger.LogInformation("Agreementid " + loanaccount.agreementid);
                                    elasticsearchagreeementids.Add(loanaccount);
                                }
                            }
                        }
                        _logger.LogInformation("clear segandtreatment elasticloanaccountslist " + elasticsearchagreeementids.Count());
                    }
                }
            }

            string DSLQueryForAllDocs = @"
                    {
                                      ""query"": {
                                        ""bool"": {
                                                    ""must"": [
                                                      {
                                              ""terms"": {
                                                ""segmentationid"": [";

            DSLQueryForAllDocs += $@" ""{SegmentId}""
                                                ]}}
                                            }}
                                         ]
                                        }}
                                     }},
                ""script"": ""ctx._source.segmentationid = ''""
                }}";

            string updateelasticsearchapipath = loanaccountsIndex + "/_update_by_query";
            var elkresp = await client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, updateelasticsearchapipath, PostData.String(DSLQueryForAllDocs));

            _logger.LogInformation("Execute Segment Update query input " + DSLQueryForAllDocs);

            _logger.LogInformation("Execute Segment Update query input " + DSLQueryForAllDocs);

            string response = elkresp.Body;
            _logger.LogInformation("Execute Segment Update query response " + response);
        }

        private async Task ClearSegmentTreatment()
        {
            _logger.LogInformation("ClearSegmentTreatment " + elasticsearchagreeementids.Count());
            var Agreementids = elasticsearchagreeementids.Where(a => a.agreementid != null).Select(a => a.agreementid).Distinct().ToList();
            DataTable dt = new DataTable("TreatmentUpdateIntermediate");
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("AgreementId", typeof(string));
            dt.Columns.Add("WorkRequestId", typeof(string));
            dt.Columns.Add("IsDeleted", typeof(bool));
            dt.Columns.Add("CreatedDate", typeof(DateTime));
            dt.Columns.Add("LastModifiedDate", typeof(DateTime));

            string workrequestid = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            foreach (var Agreementid in Agreementids)
            {
                dt.Rows.Add(SequentialGuid.NewGuidString(), Agreementid, workrequestid, 0, DateTime.Now, DateTime.Now);
            }

            //fetch the factory instance
            var utility = _flexHost.GetUtilityService<DbUtilityFactory>(hostName);

            //fetch the correct enum w.r.t the dbType
            var dbTypeEnum = DBTypeEnum.FromValue<DBTypeEnum>(dbType.ToLower());
            var dbUtility = utility.GetUtility(dbTypeEnum);

            UpdateTreatmentRequestDto request = new UpdateTreatmentRequestDto();
            request.TenantId = _flexAppContext.TenantId;
            request.Data = dt;
            request.WorkRequestId = workrequestid;
            request.StoredProcedure = "ClearSegmentAndTreatment";
            await dbUtility.UpdateTreatmentLoanAccounts(request);
        }

        private void ClearTreatment(List<string> TreatmentIds)
        {
            _logger.LogInformation("Before DBtype value ");

            _logger.LogInformation("DBtype value " + dbType);
            if (string.Equals(dbType, "mysql", StringComparison.OrdinalIgnoreCase))
            {
                var treatmentids = TreatmentIds;//elasticsearchtreatmentids.Where(a=>a.treatmentid != null && a.treatmentid != "").Select(a=>a.treatmentid).Distinct().ToList();
                DataTable dt = new DataTable("TreatmentUpdateIntermediate");
                dt.Columns.Add("Id", typeof(string));
                dt.Columns.Add("TreatmentId", typeof(string));
                dt.Columns.Add("WorkRequestId", typeof(string));
                dt.Columns.Add("IsDeleted", typeof(bool));
                dt.Columns.Add("CreatedDate", typeof(DateTime));
                dt.Columns.Add("LastModifiedDate", typeof(DateTime));

                string workrequestid = DateTime.Now.ToString("yyyyMMddhhmmssfff");
                foreach (var treatmentid in treatmentids)
                {
                    _logger.LogInformation("Inside foreach loop");
                    dt.Rows.Add(SequentialGuid.NewGuidString(), treatmentid, workrequestid, 0, DateTime.Now, DateTime.Now);
                }

                string conn = sqlConnectionString;

                using (MySqlConnection connection = new MySqlConnection(conn))
                {
                    connection.Open();

                    MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM TreatmentUpdateIntermediate", connection);
                    MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
                    da.Fill(dt);

                    da.Update(dt);
                    using (MySqlCommand cmdSPTruncateIntermediateImport = new MySqlCommand("ClearTreatment", connection))
                    {
                        cmdSPTruncateIntermediateImport.CommandType = CommandType.StoredProcedure;
                        cmdSPTruncateIntermediateImport.CommandTimeout = 180;
                        cmdSPTruncateIntermediateImport.Parameters.AddWithValue("@WorkRequestId", workrequestid);
                        cmdSPTruncateIntermediateImport.ExecuteNonQuery();
                        _logger.LogInformation("Cleared Treatment");
                    }
                }
            }
            else
            {
                _logger.LogInformation("Inside mssql");
                var treatmentids = TreatmentIds;//elasticsearchtreatmentids.Where(a=>a.treatmentid != null && a.treatmentid != "").Select(a=>a.treatmentid).Distinct().ToList();
                DataTable dt = new DataTable("TreatmentUpdateIntermediate");
                dt.Columns.Add("Id", typeof(string));
                dt.Columns.Add("TreatmentId", typeof(string));
                dt.Columns.Add("WorkRequestId", typeof(string));
                dt.Columns.Add("IsDeleted", typeof(bool));
                dt.Columns.Add("CreatedDate", typeof(DateTimeOffset));
                dt.Columns.Add("LastModifiedDate", typeof(DateTimeOffset));

                string workrequestid = DateTime.Now.ToString("yyyyMMddhhmmssfff");
                foreach (var treatmentid in treatmentids)
                {
                    _logger.LogInformation("Inside foreach loop");
                    dt.Rows.Add(SequentialGuid.NewGuidString(), treatmentid, workrequestid, 0, DateTimeOffset.Now, DateTimeOffset.Now);
                }

                string ConnectionString = sqlConnectionString;

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TreatmentUpdateIntermediate", ConnectionString);
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    da.Fill(dt);

                    da.Update(dt);
                    using (SqlCommand cmdSPTruncateIntermediateImport = new SqlCommand("ClearTreatment", connection))
                    {
                        cmdSPTruncateIntermediateImport.CommandType = CommandType.StoredProcedure;
                        cmdSPTruncateIntermediateImport.CommandTimeout = 180;
                        cmdSPTruncateIntermediateImport.Parameters.AddWithValue("@WorkRequestId", workrequestid);
                        cmdSPTruncateIntermediateImport.ExecuteNonQuery();
                        _logger.LogInformation("Cleared Treatment");
                    }
                }
            }
        }
    }
}