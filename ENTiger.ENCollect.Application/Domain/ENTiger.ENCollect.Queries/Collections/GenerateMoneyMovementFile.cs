using AutoMapper;
using AutoMapper.QueryableExtensions;
using ENTiger.ENCollect.AllocationModule;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GenerateMoneyMovementFile : FlexiQueryBridge<GenerateMoneyMovementFileDto>
    {
        protected readonly ILogger<GenerateMoneyMovementFile> _logger;
        protected GenerateMoneyMovementFileParams _params;
        protected FlexAppContextBridge? _flexAppContext;
        public IElasticSearchService _elasticSearchService { get; set; }
        private readonly ElasticSearchIndexSettings _elasticIndexConfig;
        private readonly ElasticSearchSettings _elasticSearchSettings;
        protected readonly RepoFactory _repoFactory;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        protected InsightDownloadFile? _model;
        protected readonly IFlexHost _flexHost;
        protected readonly IFlexPrimaryKeyGeneratorBridge _pkGenerator;

        /// <summary>
        /// Utility instance for handling CSV and Excel operations.
        /// </summary>
        private readonly ICsvExcelUtility _csvExcelUtility;
        /// <summary>
        /// Constructor to initialize dependencies for file generation and Elasticsearch querying.
        /// </summary>
        /// <param name="logger"></param>
        public GenerateMoneyMovementFile(ILogger<GenerateMoneyMovementFile> logger,
            IElasticSearchService elasticSearchService,
            RepoFactory repoFactory, IOptions<ElasticSearchIndexSettings> ElasticSearchIndexSettings,
            IOptions<ElasticSearchSettings> elasticSearchSettings,
            IOptions<FilePathSettings> fileSettings,
            IFileSystem fileSystem,
            IFlexHost flexHost,
            IFlexPrimaryKeyGeneratorBridge pkGenerator)
        {
            _logger = logger;
            _elasticSearchService = elasticSearchService;
            _repoFactory = repoFactory;
            _elasticIndexConfig = ElasticSearchIndexSettings.Value;
            _elasticSearchSettings = elasticSearchSettings.Value;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
            _flexHost = flexHost;
            _pkGenerator = pkGenerator;
        }
        /// <summary>
        /// Assigns query parameters received from the front-end.
        /// </summary>
        /// <param name="params">User-specified filter and request parameters.</param>
        public virtual GenerateMoneyMovementFile AssignParameters(GenerateMoneyMovementFileParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// Main execution method that builds the query, fetches data from Elasticsearch, writes to file, and returns a DTO.
        /// </summary>
        public override GenerateMoneyMovementFileDto Fetch()
        {
            long CurrentCursor = 0;
            long RecordsFetchedSoFar = 0;
            int loopNo = 0;
            string customid = DateTime.Now.ToString("yyyyMMddhhmmssfff");


            _flexAppContext = _params.GetAppContext();

            Guard.AgainstNullAndEmpty("_flexAppContext?.TenantId", _flexAppContext?.TenantId);
            _repoFactory.Init(_params);

            var config = _elasticIndexConfig.IndexNames.Where(a => a.TenantId == _flexAppContext?.TenantId).FirstOrDefault();
            Guard.AgainstNull(nameof(config), config);

            string path = config.MoneyMovementInsightIndex;


            string fileName = "MoneyMovementInsightReport_" + customid + ".csv";
            string filePath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.InsightReportFilePath, fileName);

            FetchDataIterative(path, CurrentCursor, RecordsFetchedSoFar, loopNo, filePath).GetAwaiter().GetResult();


            GenerateMoneyMovementFileDto result = new GenerateMoneyMovementFileDto()
            {
                WorkRequestId = customid
            };

            SaveDownloadFileAndloggedInUserDetails(fileName, customid);

            return result;

           
        }

        /// <summary>
        /// Fetches data from Elasticsearch iteratively with pagination support and writes results to a CSV file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="customid"></param>
        private async void SaveDownloadFileAndloggedInUserDetails(string fileName, string customid)
        {
            InsightSaveDownloadDetailsDto dto = new InsightSaveDownloadDetailsDto();
            dto.CustomId = customid;
            dto.FilePath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.InsightReportFilePath);
            dto.FileName = fileName;
            dto.Description = "Secondary Allocation Insight";
            dto.SetGeneratedId(_pkGenerator.GenerateKey());
            dto.SetAppContext(_flexAppContext);

            _model = _flexHost.GetDomainModel<InsightDownloadFile>().InsightSaveDownloadDetails(dto);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(InsightDownloadFile).Name, _model.Id);
            }
        }


        /// <summary>
        /// Builds the dynamic DSL query for secondary allocation download by applying user-specified filters.
        /// </summary>
        /// <param name="_params">Input data from FE</param>
        /// <param name="CurrentCursor">the current position/cursor for paginated data fetching</param>
        /// <returns></returns>
        private string BuildDslQueryForMoneyMovementInsighDownload(GenerateMoneyMovementFileParams _params, long CurrentCursor)
        {
            long RecordPerLoopSize = _elasticSearchSettings.RecordPerLoopSize;

            string moneymovementproductgroupfilter = _elasticSearchService.FormatArrayFilter(_params.ProductGroup);
            string moneymovementproductfilter = _elasticSearchService.FormatArrayFilter(_params.Product);
            string moneymovementsubproductfilter = _elasticSearchService.FormatArrayFilter(_params.SubProduct);
            string moneymovementcurrentbucketfilter = _elasticSearchService.FormatArrayFilter(_params.CurrentBucket);
            string moneymovementbombucketfilter = _elasticSearchService.FormatArrayFilter(_params.BOMBucket);
            string moneymovementregionfilter = _elasticSearchService.FormatArrayFilter(_params.Region);
            string moneymovementstatefilter = _elasticSearchService.FormatArrayFilter(_params.State);
            string moneymovementcityfilter = _elasticSearchService.FormatArrayFilter(_params.City);
            string moneymovementbranchfilter = _elasticSearchService.FormatArrayFilter(_params.Branch);
            string moneymovementagencyfilter = _elasticSearchService.FormatArrayFilter(_params.Agency);
            string moneymovementagentfilter = _elasticSearchService.FormatArrayFilter(_params.Agent);
            string moneymovementpaymentmodefilter = _elasticSearchService.FormatArrayFilter(_params.paymentMode);

            string moneymovementpaymentstatusfilter = _elasticSearchService.FormatArrayFilter(_params.paymentStatus);
            string moneymovementmonth = _elasticSearchService.GetFilterTextForElasticSearch(_params.receiptMonth);
            string moneymovementyear = _elasticSearchService.GetFilterTextForElasticSearch(_params.receiptYear);
            // Get Current Year and Month if the variables are null or empty
            // Check if the year and month filters are provided
            bool isYearMonthProvided = (!string.IsNullOrEmpty(moneymovementyear) && moneymovementyear != "NoFilterPresent") && (!string.IsNullOrEmpty(moneymovementmonth) && moneymovementmonth != "NoFilterPresent");

            DateTime startDate, endDate;

            if (isYearMonthProvided)
            {
                // Use provided Year and Month
                int year = int.Parse(moneymovementyear);
                int month = int.Parse(moneymovementmonth);

                // First and last day of the selected month
                startDate = new DateTime(year, month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1); // Last day of the month
            }
            else
            {
                
                DateTime currentDate = DateTime.Now;
                DateTime fiveMonthsAgo = currentDate.AddMonths(-5);
                DateTime firstDayOfFiveMonthAgo = new DateTime(fiveMonthsAgo.Year, fiveMonthsAgo.Month, 1); // Go back 5 months to include current month


                // Default: Load last 6 months including the current month
                startDate = firstDayOfFiveMonthAgo;
                endDate = DateTime.Now; // Current date


            }

            string fromDate = startDate.ToString("yyyy-MM-dd");
            string toDate = endDate.ToString("yyyy-MM-dd");

            string dslquerywithFilters = $@"
{{
  ""from"": {CurrentCursor},
  ""size"": {RecordPerLoopSize},
  ""track_total_hits"": true,
  ""query"": {{
    ""bool"": {{
      ""must"": [
        {{
          ""range"": {{
            ""collections_collectiondate_collectiondate"": {{
               ""gte"": ""{fromDate}"",
              ""lte"": ""{toDate}""
            }}
          }}
        }},";

            // Dynamically adding filters only if values are present
            if (!string.IsNullOrEmpty(moneymovementproductgroupfilter))
                dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_productgroup"": [{moneymovementproductgroupfilter}] }}
        }},";

            if (!string.IsNullOrEmpty(moneymovementproductfilter))
                dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_product"": [{moneymovementproductfilter}] }}
        }},";

            if (!string.IsNullOrEmpty(moneymovementsubproductfilter))
                dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_subproduct"": [{moneymovementsubproductfilter}] }}
        }},";

            if (!string.IsNullOrEmpty(moneymovementcurrentbucketfilter))
                dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_current_bucket_currentbucket"": [{moneymovementcurrentbucketfilter}] }}
        }},";

            if (!string.IsNullOrEmpty(moneymovementbombucketfilter))
                dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_bucket_bombucket"": [{moneymovementbombucketfilter}] }}
        }},";

            if (!string.IsNullOrEmpty(moneymovementregionfilter))
                dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_region_region"": [{moneymovementregionfilter}] }}
        }},";

            if (!string.IsNullOrEmpty(moneymovementstatefilter))
                dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_state_state"": [{moneymovementstatefilter}] }}
        }},";

            if (!string.IsNullOrEmpty(moneymovementcityfilter))
                dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_city_city"": [{moneymovementcityfilter}] }}
        }},";

            if (!string.IsNullOrEmpty(moneymovementbranchfilter))
                dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_branch_branchname"": [{moneymovementbranchfilter}] }}
        }},";

            if (!string.IsNullOrEmpty(moneymovementagentfilter))
                dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""applicationuser_agent_id"": [{moneymovementagentfilter}] }}
        }},";

            if (!string.IsNullOrEmpty(moneymovementpaymentstatusfilter))
                dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""paymentstatus"": [{moneymovementpaymentstatusfilter}] }}
        }},";

            if (!string.IsNullOrEmpty(moneymovementpaymentmodefilter))
                dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""collections_collectionmode_paymentmode"": [{moneymovementpaymentmodefilter}]  }}
        }},";

            if (!string.IsNullOrEmpty(moneymovementagencyfilter))
                dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""applicationorg_agency_id"": [{moneymovementagencyfilter}]  }}
        }},";



            // Closing JSON structure
            dslquerywithFilters += @"
      ]
    }
  }
}";




            dslquerywithFilters = _elasticSearchService.RemoveCommaFromDslQuery(dslquerywithFilters);
            return dslquerywithFilters;


        }




        /// <summary>
        /// Fetches data from ElasticSearch iteratively
        /// </summary>
        /// <param name="path"> Path of csv file where data is saved </param>
        /// <param name="CurrentCursor"> the current position/cursor for paginated data fetching </param>
        /// <param name="RecordsFetchedSoFar"> the count of records already fetched</param>
        /// <param name="loopNo">current iteration number </param>
        /// <param name="filePath"> Path of csv file where data is saved </param>
        /// <returns></returns>
        private async Task FetchDataIterative(string path, long CurrentCursor, long RecordsFetchedSoFar, int loopNo, string filePath)
        {

            bool hasMoreData = true;
            while (hasMoreData)
            {
                loopNo++;

                // Build DSL Query
                string dslQuery = BuildDslQueryForMoneyMovementInsighDownload(_params, CurrentCursor);

                // Send Request to Elasticsearch
                var response = await _elasticSearchService.SendPostRequestToElasticSearchAsync(path, dslQuery);

                Guard.AgainstNull(nameof(response), response);
                string elasticresponse = response.ToString() ?? string.Empty;

                Guard.AgainstNullAndEmpty(nameof(elasticresponse), elasticresponse);

                // Parse response
                hasMoreData = ParseResponseAndWriteToCsv(filePath, ref CurrentCursor, ref RecordsFetchedSoFar, loopNo, response);

                // Break if we've fetched all records
                if (RecordsFetchedSoFar >= _elasticSearchSettings.MaxWindowSize || !hasMoreData)
                    break;
            }
        }

        /// <summary>
        /// Parse the response of elastic search and saves data in csv file
        /// </summary>
        /// <param name="filePath">Path of csv file where data is saved</param>
        /// <param name="CurrentCursor">the current position/cursor for paginated data fetching</param>
        /// <param name="RecordsFetchedSoFar">the count of records already fetched</param>
        /// <param name="loopNo">current iteration number </param>
        /// <param name="response">Path of csv file where data is saved</param>
        /// <returns></returns>
        private bool ParseResponseAndWriteToCsv(string filePath, ref long CurrentCursor, ref long RecordsFetchedSoFar, int loopNo, object response)
        {
            string elasticresponse = response.ToString();
            long MaxWindowSize = _elasticSearchSettings.MaxWindowSize;
            long RecordPerLoopSize = _elasticSearchSettings.RecordPerLoopSize;

            bool hasMoreData = true;


            //try
            //{
                // Deserialize response
                ENTiger.ENCollect.CollectionsModule.MoneyMovementInsightDownloadRoot.Root myDeserializedClass = JsonSerializer.Deserialize<ENTiger.ENCollect.CollectionsModule.MoneyMovementInsightDownloadRoot.Root>(elasticresponse);
            ///}
            //catch (Exception ex)
            //{

            //}


            var writer = new StreamWriter(filePath, true, Encoding.UTF8);
            CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = ",",
                NewLine = Environment.NewLine,
                TrimOptions = TrimOptions.Trim,
                Encoding = Encoding.UTF8,
                HasHeaderRecord = (loopNo == 1) ? true : false
            };
            // Create CsvWriter instance and use it across loops
            using (var csv = new CsvWriter(writer, csvConfig))
            {

                csv.Context.RegisterClassMap<MoneyMovementInsightElkResponseHitsMap>();
                // Write data rows
               csv.WriteRecords(myDeserializedClass.hits.hits);

                // Update counters
               RecordsFetchedSoFar += myDeserializedClass.hits.hits.Count;
                CurrentCursor += RecordPerLoopSize;

                if (RecordsFetchedSoFar >= MaxWindowSize)
                {
                    // Write truncation message
                    csv.WriteComment(Environment.NewLine + "*** Records Truncated as limit of " + MaxWindowSize + " Reached.");
                    hasMoreData = false;
                }

                // Check if more data exists or we have reached the end
                if (RecordsFetchedSoFar >= myDeserializedClass.hits.total.value || myDeserializedClass.hits.hits.Count == 0)
                {
                    // Write report footer with timestamp
                    csv.WriteComment(Environment.NewLine + "* Report generated on " + DateTime.Now.ToLongDateString().Replace(",", " ") + " " + DateTime.Now.ToShortTimeString().Replace(",", " "));
                    hasMoreData = false;
                }
            }
            return hasMoreData;
        }


    }

    /// <summary>
    /// Defines the input parameters used for generating the Money Movement file.
    /// </summary>
    public class GenerateMoneyMovementFileParams : DtoBridge
    {
        public List<string>? ProductGroup { get; set; }
        public List<string>? Product { get; set; }
        public List<string>? SubProduct { get; set; }
        public List<string>? BOMBucket { get; set; }
        public List<string>? CurrentBucket { get; set; }
        public List<string>? Zone { get; set; }
        public List<string>? Region { get; set; }
        public List<string>? State { get; set; }
        public List<string>? City { get; set; }
        public List<string>? Branch { get; set; }
        public List<string>? Agency { get; set; }
        public List<string>? Agent { get; set; }
        public List<string>? paymentMode { get; set; }
        public List<string>? paymentStatus { get; set; }
        public string? receiptMonth { get; set; }
        public string? receiptYear { get; set; }
    }
}
