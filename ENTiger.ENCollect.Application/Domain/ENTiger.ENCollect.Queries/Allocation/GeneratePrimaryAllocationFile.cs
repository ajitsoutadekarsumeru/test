using ENTiger.ENCollect.DomainModels.Reports;
using ENTiger.ENCollect.DomainModels.Utilities;

namespace ENTiger.ENCollect.AllocationModule;

/// <summary>
/// API to generate file of primary allocation insight based on filters
/// </summary>
public class GeneratePrimaryAllocationFile : FlexiQueryBridge<GeneratePrimaryAllocationFileDto>
{
    protected readonly ILogger<GeneratePrimaryAllocationFile> _logger;
    protected GeneratePrimaryAllocationFileParams _params;
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
    /// Constructor of GeneratePrimaryAllocationFile
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="elasticSearchService"></param>
    /// <param name="repoFactory"></param>
    /// <param name="ElasticSearchIndexSettings"></param>
    /// <param name="elasticSearchSettings"></param>
    public GeneratePrimaryAllocationFile(ILogger<GeneratePrimaryAllocationFile> logger, 
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
    /// Assigns parameters received from FE
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    public virtual GeneratePrimaryAllocationFile AssignParameters(GeneratePrimaryAllocationFileParams @params)
    {
        Guard.AgainstNull(nameof(@params), @params);
        _params = @params;
        return this;
    }

    /// <summary>
    ///  Method that fetches data from elastic search based on input filters and creates csv file with data
    /// </summary>
    /// <returns></returns>
    public override GeneratePrimaryAllocationFileDto Fetch()
    {


        long CurrentCursor = 0;
        long RecordsFetchedSoFar = 0;
        int loopNo = 0;
        //string FolderPathForReport = _elasticSearchSettings.FolderPath;
        string customid = DateTime.Now.ToString("yyyyMMddhhmmssfff");


        _flexAppContext = _params.GetAppContext();

        Guard.AgainstNullAndEmpty("_flexAppContext?.TenantId", _flexAppContext?.TenantId);
        _repoFactory.Init(_params);

        var config = _elasticIndexConfig.IndexNames.Where(a => a.TenantId == _flexAppContext?.TenantId).FirstOrDefault();
        Guard.AgainstNull(nameof(config), config);

        string path = config.PrimaryAllocationInsightIndex;

        string fileName = "PrimaryAllocationInsightReport_" + customid + ".csv";
        string filePath = _fileSystem.Path.Combine(_fileSettings.BasePath,_fileSettings.IncomingPath, _fileSettings.InsightReportFilePath, fileName);

        FetchDataIterative(path, CurrentCursor, RecordsFetchedSoFar, loopNo, filePath).GetAwaiter().GetResult();

        
        GeneratePrimaryAllocationFileDto result = new GeneratePrimaryAllocationFileDto()
        {
            WorkRequestId = customid
        };

        SaveDownloadFileAndloggedInUserDetails(fileName, customid);

        return result;
    }
    /// <summary>
    /// Saves the details of the file and the user who downloaded it.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="customid"></param>
    private async void SaveDownloadFileAndloggedInUserDetails(string fileName, string customid)
    {
        InsightSaveDownloadDetailsDto dto = new InsightSaveDownloadDetailsDto();
        dto.CustomId = customid;
        dto.FilePath = _fileSystem.Path.Combine(_fileSettings.BasePath,_fileSettings.IncomingPath, _fileSettings.InsightReportFilePath);
        dto.FileName = fileName;
        dto.Description = "Primary Allocation Insight";
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
    /// Build dsl query based on input parameters
    /// </summary>
    /// <param name="_params">Input data from FE</param>
    /// <param name="CurrentCursor">the current position/cursor for paginated data fetching</param>
    /// <returns></returns>
    private string BuildDslQueryForPrimaryAllocationInsighDownload(GeneratePrimaryAllocationFileParams _params,long CurrentCursor)
    {
        long RecordPerLoopSize = _elasticSearchSettings.RecordPerLoopSize;        
        
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;
        
        string primaryallocationproductgroupfilter = _elasticSearchService.FormatArrayFilter(_params.ProductGroup);
        string primaryallocationproductfilter = _elasticSearchService.FormatArrayFilter(_params.Product);
        string primaryallocationsubproductfilter = _elasticSearchService.FormatArrayFilter(_params.SubProduct);
        string primaryallocationcurrentbucketfilter = _elasticSearchService.FormatArrayFilter(_params.CurrentBucket);
        string primaryallocationbombucketfilter = _elasticSearchService.FormatArrayFilter(_params.BOMBucket);
        string primaryallocationregionfilter = _elasticSearchService.FormatArrayFilter(_params.Region);
        string primaryallocationstatefilter = _elasticSearchService.FormatArrayFilter(_params.State);
        string primaryallocationcityfilter = _elasticSearchService.FormatArrayFilter(_params.City);
        string primaryallocationbranchfilter = _elasticSearchService.FormatArrayFilter(_params.Branch);
        string primaryallocationagencyfilter = _elasticSearchService.FormatArrayFilter(_params.FieldAgencyId);
        string primaryallocationtelecallingagencyfilter = _elasticSearchService.FormatArrayFilter(_params.TelecallingAgencyId);
        string primaryallocationallocationownerfilter = _elasticSearchService.FormatArrayFilter(_params.AllocationOwner);

        string primaryallocationstatusfilter = _elasticSearchService.GetFilterTextForElasticSearch(_params.Status);
        string primaryallocationloan_bucketfilter = _elasticSearchService.GetFilterTextForElasticSearch(_params.Loan_Bucket);
        string primaryallocationentityfilter = _elasticSearchService.GetFilterTextForElasticSearch(_params.Entity);



            string dslqueryWithFilters = $@"
{{
  ""from"": {CurrentCursor},
  ""size"": {RecordPerLoopSize},
  ""track_total_hits"": true,
  ""query"": {{
    ""bool"": {{
      ""must"": [
";

            if (!string.IsNullOrEmpty(primaryallocationproductgroupfilter))
            {
                dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""productgroup_loanaccounts"": [{primaryallocationproductgroupfilter}]
      }}
    }},
    ";
            }

            if (!string.IsNullOrEmpty(primaryallocationproductfilter))
            {
                dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""product_loanaccounts"": [{primaryallocationproductfilter}]
      }}
    }},
    ";
            }

            if (!string.IsNullOrEmpty(primaryallocationsubproductfilter))
            {
                dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""subproduct_loanaccounts"": [{primaryallocationsubproductfilter}]
      }}
    }},
    ";
            }

            if (!string.IsNullOrEmpty(primaryallocationcurrentbucketfilter))
            {
                dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""current_bucket_loanaccounts"": [{primaryallocationcurrentbucketfilter}]
      }}
    }},
    ";
            }

            if (!string.IsNullOrEmpty(primaryallocationbombucketfilter))
            {
                dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""bucket_loanaccounts"": [{primaryallocationbombucketfilter}]
      }}
    }},
    ";
            }

            if (!string.IsNullOrEmpty(primaryallocationregionfilter))
            {
                dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""region_loanaccounts"": [{primaryallocationregionfilter}]
      }}
    }},
    ";
            }

            if (!string.IsNullOrEmpty(primaryallocationstatefilter))
            {
                dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""state_loanaccounts"": [{primaryallocationstatefilter}]
      }}
    }},
    ";
            }

            if (!string.IsNullOrEmpty(primaryallocationcityfilter))
            {
                dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""city_loanaccounts"": [{primaryallocationcityfilter}]
      }}
    }},
    ";
            }

            if (!string.IsNullOrEmpty(primaryallocationbranchfilter))
            {
                dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""branch_loanaccounts"": [{primaryallocationbranchfilter}]
      }}
    }},
    ";
            }

            if (!string.IsNullOrEmpty(primaryallocationallocationownerfilter))
            {
                dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""allocationowner_applicationuser_id"": [{primaryallocationallocationownerfilter}]
      }}
    }},
    ";
            }

            if (primaryallocationstatusfilter != MagickString.NoFilterPresent)
            {
                dslqueryWithFilters += $@"
    {{
      ""term"": {{
        ""primaryallocationstatus"": {{
        ""value"": ""{primaryallocationstatusfilter}""
        }}
      }}
    }},
    ";
            }

            if (primaryallocationloan_bucketfilter != MagickString.NoFilterPresent)
            {
                dslqueryWithFilters += $@"
    {{
      ""term"": {{
        ""loan_bucket"": {{
        ""value"": ""{primaryallocationloan_bucketfilter}""
        }}
      }}
    }},
    ";
            }

            if (primaryallocationentityfilter != MagickString.NoFilterPresent)
            {
                dslqueryWithFilters += $@"
    {{
      ""term"": {{
        ""entity"": {{
        ""value"": ""{primaryallocationentityfilter}""
        }}
      }}
    }},
    ";
            }


        if (!string.IsNullOrEmpty(primaryallocationagencyfilter))
        {
            dslqueryWithFilters += $@"
                                   {{
                                     ""terms"": {{
                                       ""agency_applicationorg_id"": [{primaryallocationagencyfilter}]
                                     }}
                                   }},
                             ";
        }

        if (!string.IsNullOrEmpty(primaryallocationtelecallingagencyfilter))
        {
            dslqueryWithFilters += $@"
                                   {{
                                     ""terms"": {{
                                       ""telecallingagency_applicationorg_id"": [{primaryallocationtelecallingagencyfilter}]
                                     }}
                                   }},
                             ";

        }




        dslqueryWithFilters += $@"
  {{
    ""term"": {{
      ""year_loanaccounts"": {{
        ""value"": ""{year}""
      }}
    }}
  }},
  {{
    ""term"": {{
      ""month_loanaccounts"": {{
        ""value"": ""{month}""
      }}
    }}
  }},
";

            

            dslqueryWithFilters += @"
      ]
    }
  },
  ""sort"": [
    {""lastmodifieddate_loanaccounts"": ""desc""}
  ]  
}
";

            dslqueryWithFilters = _elasticSearchService.RemoveCommaFromDslQuery(dslqueryWithFilters);



            return dslqueryWithFilters;

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
                    string dslQuery = BuildDslQueryForPrimaryAllocationInsighDownload(_params, CurrentCursor);

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



        // Deserialize response
        ENTiger.ENCollect.AllocationModule.PrimaryAllocationInsightDownloadRoot.Root myDeserializedClass = JsonSerializer.Deserialize<ENTiger.ENCollect.AllocationModule.PrimaryAllocationInsightDownloadRoot.Root>(elasticresponse);

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
                
            csv.Context.RegisterClassMap<PrimaryAllocationInsightElkResponseHitsMap>();
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
/// 
/// </summary>
public class GeneratePrimaryAllocationFileParams : DtoBridge
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
    public List<string>? FieldAgencyId { get; set; }
    public List<string>? TelecallingAgencyId { get; set; }
    public List<string>? AllocationOwner { get; set; }
    public string? Loan_Bucket { get; set; }
    public string? Status { get; set; }
    public string? Entity { get; set; }

}
