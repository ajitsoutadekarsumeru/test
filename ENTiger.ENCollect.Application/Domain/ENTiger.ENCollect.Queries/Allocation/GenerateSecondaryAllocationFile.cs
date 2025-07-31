namespace ENTiger.ENCollect.AllocationModule;

/// <summary>
/// Generates a secondary allocation file by querying Elasticsearch based on filters
/// and saving the results in a CSV format for reporting.
/// </summary>
public class GenerateSecondaryAllocationFile : FlexiQueryBridge<GenerateSecondaryAllocationFileDto>
{
    protected readonly ILogger<GenerateSecondaryAllocationFile> _logger;
    protected GenerateSecondaryAllocationFileParams _params;
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
    public GenerateSecondaryAllocationFile(ILogger<GenerateSecondaryAllocationFile> logger,
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
    public virtual GenerateSecondaryAllocationFile AssignParameters(GenerateSecondaryAllocationFileParams @params)
    {
        _params = @params;
        return this;
    }

    /// <summary>
    /// Main execution method that builds the query, fetches data from Elasticsearch, writes to file, and returns a DTO.
    /// </summary>
    public override GenerateSecondaryAllocationFileDto Fetch()
    {
        long CurrentCursor = 0;
        long RecordsFetchedSoFar = 0;
        int loopNo = 0;
       // string FolderPathForReport = _elasticSearchSettings.FolderPath;
        string customid = DateTime.Now.ToString("yyyyMMddhhmmssfff");


        _flexAppContext = _params.GetAppContext();

        Guard.AgainstNullAndEmpty("_flexAppContext?.TenantId", _flexAppContext?.TenantId);
        _repoFactory.Init(_params);

        var config = _elasticIndexConfig.IndexNames.Where(a => a.TenantId == _flexAppContext?.TenantId).FirstOrDefault();
        Guard.AgainstNull(nameof(config), config);

        string path = config.SecondaryAllocationInsightIndex;


        string fileName = "SecondaryAllocationInsightReport_" + customid + ".csv";
        string filePath = _fileSystem.Path.Combine(_fileSettings.BasePath,_fileSettings.IncomingPath, _fileSettings.InsightReportFilePath, fileName);

        FetchDataIterative(path, CurrentCursor, RecordsFetchedSoFar, loopNo, filePath).GetAwaiter().GetResult();


        GenerateSecondaryAllocationFileDto result = new GenerateSecondaryAllocationFileDto()
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
        dto.FilePath = _fileSystem.Path.Combine(_fileSettings.BasePath,_fileSettings.IncomingPath, _fileSettings.InsightReportFilePath);
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
    private string BuildDslQueryForsecondaryAllocationInsighDownload(GenerateSecondaryAllocationFileParams _params, long CurrentCursor)
    {
        long RecordPerLoopSize = _elasticSearchSettings.RecordPerLoopSize;

        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;

        string secondaryallocationproductgroupfilter = _elasticSearchService.FormatArrayFilter(_params.ProductGroup);
        string secondaryallocationproductfilter = _elasticSearchService.FormatArrayFilter(_params.Product);
        string secondaryallocationsubproductfilter = _elasticSearchService.FormatArrayFilter(_params.SubProduct);
        string secondaryallocationcurrentbucketfilter = _elasticSearchService.FormatArrayFilter(_params.CurrentBucket);
        string secondaryallocationbombucketfilter = _elasticSearchService.FormatArrayFilter(_params.BOMBucket);
        string secondaryallocationregionfilter = _elasticSearchService.FormatArrayFilter(_params.Region);
        string secondaryallocationstatefilter = _elasticSearchService.FormatArrayFilter(_params.State);
        string secondaryallocationcityfilter = _elasticSearchService.FormatArrayFilter(_params.City);
        string secondaryallocationbranchfilter = _elasticSearchService.FormatArrayFilter(_params.Branch);
        string secondaryallocationagencyfilter = _elasticSearchService.FormatArrayFilter(_params.FieldAgencyId);
        string secondaryallocationtelecallingagencyfilter = _elasticSearchService.FormatArrayFilter(_params.TelecallingAgencyId);
        string secondaryallocation_agentfilter = _elasticSearchService.FormatArrayFilter(_params.FieldCollectorId);
        string secondaryallocation_telecallerfilter = _elasticSearchService.FormatArrayFilter(_params.TelecallerId);
        string secondaryallocationallocationownerfilter = _elasticSearchService.FormatArrayFilter(_params.AllocationOwner);

        string secondaryallocationstatusfilter = _elasticSearchService.GetFilterTextForElasticSearch(_params.Status);
        string secondaryallocationloan_bucketfilter = _elasticSearchService.GetFilterTextForElasticSearch(_params.Loan_Bucket);
        string secondaryallocationentityfilter = _elasticSearchService.GetFilterTextForElasticSearch(_params.Entity);



        string dslqueryWithFilters = $@"
{{
  ""from"": {CurrentCursor},
  ""size"": {RecordPerLoopSize},
  ""track_total_hits"": true,
  ""query"": {{
    ""bool"": {{
      ""must"": [
";

        if (!string.IsNullOrEmpty(secondaryallocationproductgroupfilter))
        {
            dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""productgroup_loanaccounts"": [{secondaryallocationproductgroupfilter}]
      }}
    }},
    ";
        }

        if (!string.IsNullOrEmpty(secondaryallocationproductfilter))
        {
            dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""product_loanaccounts"": [{secondaryallocationproductfilter}]
      }}
    }},
    ";
        }

        if (!string.IsNullOrEmpty(secondaryallocationsubproductfilter))
        {
            dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""subproduct_loanaccounts"": [{secondaryallocationsubproductfilter}]
      }}
    }},
    ";
        }

        if (!string.IsNullOrEmpty(secondaryallocationcurrentbucketfilter))
        {
            dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""current_bucket_loanaccounts"": [{secondaryallocationcurrentbucketfilter}]
      }}
    }},
    ";
        }

        if (!string.IsNullOrEmpty(secondaryallocationbombucketfilter))
        {
            dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""bucket_loanaccounts"": [{secondaryallocationbombucketfilter}]
      }}
    }},
    ";
        }

        if (!string.IsNullOrEmpty(secondaryallocationregionfilter))
        {
            dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""region_loanaccounts"": [{secondaryallocationregionfilter}]
      }}
    }},
    ";
        }

        if (!string.IsNullOrEmpty(secondaryallocationstatefilter))
        {
            dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""state_loanaccounts"": [{secondaryallocationstatefilter}]
      }}
    }},
    ";
        }

        if (!string.IsNullOrEmpty(secondaryallocationcityfilter))
        {
            dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""city_loanaccounts"": [{secondaryallocationcityfilter}]
      }}
    }},
    ";
        }

        if (!string.IsNullOrEmpty(secondaryallocationbranchfilter))
        {
            dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""branch_loanaccounts"": [{secondaryallocationbranchfilter}]
      }}
    }},
    ";
        }

        if (!string.IsNullOrEmpty(secondaryallocationallocationownerfilter))
        {
            dslqueryWithFilters += $@"
    {{
      ""terms"": {{
        ""allocationowner_applicationuser_id"": [{secondaryallocationallocationownerfilter}]
      }}
    }},
    ";
        }

        if (secondaryallocationstatusfilter != MagickString.NoFilterPresent)
        {
            dslqueryWithFilters += $@"
    {{
      ""term"": {{
        ""secondaryallocationstatus"": {{
        ""value"": ""{secondaryallocationstatusfilter}""
        }}
      }}
    }},
    ";
        }

        if (secondaryallocationloan_bucketfilter != MagickString.NoFilterPresent)
        {
            dslqueryWithFilters += $@"
    {{
      ""term"": {{
        ""loan_bucket"": {{
        ""value"": ""{secondaryallocationloan_bucketfilter}""
        }}
      }}
    }},
    ";
        }

        if (secondaryallocationentityfilter != MagickString.NoFilterPresent)
        {
            dslqueryWithFilters += $@"
    {{
      ""term"": {{
        ""entity"": {{
        ""value"": ""{secondaryallocationentityfilter}""
        }}
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
{{
     ""term"": {{
        ""primaryallocationstatus"": {{
                   ""value"": ""ACCOUNT ALLOCATED""
               }}
         }}
}},
";

        if (!string.IsNullOrEmpty(secondaryallocationagencyfilter))
        {
            dslqueryWithFilters += $@"
                              {{
                                ""terms"": {{
                                  ""agency_applicationorg_id"": [{secondaryallocationagencyfilter}]
                                }}
                              }},
                        ";
        }

        if (!string.IsNullOrEmpty(secondaryallocationtelecallingagencyfilter))
        {
            dslqueryWithFilters += $@"
                              {{
                                ""terms"": {{
                                  ""telecallingagency_applicationorg_id"": [{secondaryallocationtelecallingagencyfilter}]
                                }}
                              }},
                        ";

        }

        if (!string.IsNullOrEmpty(secondaryallocation_agentfilter))
        {
            dslqueryWithFilters += $@"
                              {{
                                ""terms"": {{
                                  ""collector_applicationuser_id"": [{secondaryallocation_agentfilter}]
                                }}
                              }},
                        ";
        }

        if (!string.IsNullOrEmpty(secondaryallocation_telecallerfilter))
        {
            dslqueryWithFilters += $@"
                              {{
                                ""terms"": {{
                                  ""tellecaller_applicationuser_id"": [{secondaryallocation_telecallerfilter}]
                                }}
                              }},
                        ";

        }




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
            string dslQuery = BuildDslQueryForsecondaryAllocationInsighDownload(_params, CurrentCursor);

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
        ENTiger.ENCollect.AllocationModule.SecondaryAllocationInsightDownloadRoot.Root myDeserializedClass = JsonSerializer.Deserialize<ENTiger.ENCollect.AllocationModule.SecondaryAllocationInsightDownloadRoot.Root>(elasticresponse);

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

            csv.Context.RegisterClassMap<SecondaryAllocationInsightElkResponseHitsMap>();
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
/// Defines the input parameters used for generating the secondary allocation file.
/// </summary>
public class GenerateSecondaryAllocationFileParams : DtoBridge
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
    public List<string>? FieldCollectorId { get; set; }
    public List<string>? TelecallerId { get; set; }
    public List<string>? AllocationOwner { get; set; }
    public string? Loan_Bucket { get; set; }
    public string? Status { get; set; }
    public string? Entity { get; set; }
}
