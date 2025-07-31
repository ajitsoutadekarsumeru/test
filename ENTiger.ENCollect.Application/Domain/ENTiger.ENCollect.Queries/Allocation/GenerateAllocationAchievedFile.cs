using ENTiger.ENCollect.DomainModels.Enum;
using ENTiger.ENCollect.FeedbackModule;

namespace ENTiger.ENCollect.AllocationModule;

/// <summary>
///  API to generate file of allocation vs achieved insight based on filters
/// </summary>
public class GenerateAllocationAchievedFile : FlexiQueryBridge<GenerateAllocationAchievedFileDto>
{
    protected readonly ILogger<GenerateAllocationAchievedFile> _logger;
    protected GenerateAllocationAchievedFileParams _params;
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
    /// Constructor to initialize dependencies
    /// </summary>
    /// <param name="logger"></param>
    public GenerateAllocationAchievedFile(ILogger<GenerateAllocationAchievedFile> logger,
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
    ///  Assigns parameters received from FE
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    public virtual GenerateAllocationAchievedFile AssignParameters(GenerateAllocationAchievedFileParams @params)
    {
        _params = @params;
        return this;
    }

    /// <summary>
    /// Method that fetches data from elastic search based on input filters and creates csv file with data
    /// </summary>
    /// <returns></returns>
    public override GenerateAllocationAchievedFileDto Fetch()
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

        string path = config.Allocation_AchievedInsightIndex;

        string fileName = "Allocation_AchievedInsightReport_" + customid + ".csv";
        string filePath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.InsightReportFilePath, fileName);

        FetchDataIterative(path, CurrentCursor, RecordsFetchedSoFar, loopNo, filePath).GetAwaiter().GetResult();


        GenerateAllocationAchievedFileDto result = new GenerateAllocationAchievedFileDto()
        {
            WorkRequestId = customid
        };

        SaveDownloadFileAndloggedInUserDetails(fileName, customid);

        return result;


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
            string dslQuery = BuildDslQueryForAllocationVsAchieved(_params, CurrentCursor);

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
    /// Build dsl query for allocation vs achieved details insight with filters
    /// </summary>
    /// <param name="_params"></param>
    /// <returns></returns>
    private string BuildDslQueryForAllocationVsAchieved(GenerateAllocationAchievedFileParams _params, long CurrentCursor)
    {
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;

        long RecordPerLoopSize = _elasticSearchSettings.RecordPerLoopSize;


        string allocation_achieved_productgroupfilter = _elasticSearchService.FormatArrayFilter(_params.ProductGroup);
        string allocation_achieved_productfilter = _elasticSearchService.FormatArrayFilter(_params.Product);
        string allocation_achieved_subproductfilter = _elasticSearchService.FormatArrayFilter(_params.SubProduct);
        string allocation_achieved_regionfilter = _elasticSearchService.FormatArrayFilter(_params.Region);
        string allocation_achieved_statefilter = _elasticSearchService.FormatArrayFilter(_params.State);
        string allocation_achieved_cityfilter = _elasticSearchService.FormatArrayFilter(_params.City);
        string allocation_achieved_branchfilter = _elasticSearchService.FormatArrayFilter(_params.Branch);
        string allocation_achieved_agencyidfilter = _elasticSearchService.FormatArrayFilter(_params.FieldAgencyId);
        string allocation_achieved_telecallingidfilter = _elasticSearchService.FormatArrayFilter(_params.TelecallingAgencyId);
        string allocation_achieved_agentfilter = _elasticSearchService.FormatArrayFilter(_params.FieldCollectorId);
        string allocation_achieved_telecallerfilter = _elasticSearchService.FormatArrayFilter(_params.TelecallerId);
        string allocation_achieved_currentbucketfilter = _elasticSearchService.FormatArrayFilter(_params.Current_Bucket);
        string allocation_achieved_bombucketfilter = _elasticSearchService.FormatArrayFilter(_params.Bucket);

        string allocation_achieved_statusfilter = _elasticSearchService.GetFilterTextForElasticSearch(_params.Status);
        string allocation_achieved_loan_bucketfilter = _elasticSearchService.GetFilterTextForElasticSearch(_params.Loan_Bucket);



        string dslquerywithFilters = $@"
{{
  ""from"": {CurrentCursor},
  ""size"": {RecordPerLoopSize},
  ""track_total_hits"": true,
  ""query"": {{
    ""bool"": {{
      ""must"": [
";

        if (!string.IsNullOrEmpty(allocation_achieved_productgroupfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""productgroup_loanaccounts"": [{allocation_achieved_productgroupfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(allocation_achieved_productfilter))
            dslquerywithFilters += $@"
                                     {{
                                    ""terms"": {{
                                      ""product_loanaccounts"": [{allocation_achieved_productfilter}]
                                    }}
                                  }},
                            ";

        if (!string.IsNullOrEmpty(allocation_achieved_subproductfilter))
            dslquerywithFilters += $@"
                                  {{
                                    ""terms"": {{
                                      ""subproduct_loanaccounts"": [{allocation_achieved_subproductfilter}]
                                    }}
                                  }},
                            ";

        if (!string.IsNullOrEmpty(allocation_achieved_currentbucketfilter))
            dslquerywithFilters += $@"
                                  {{
                                    ""terms"": {{
                                      ""current_bucket_loanaccounts"": [{allocation_achieved_currentbucketfilter}]
                                    }}
                                  }},
                            ";

        if (!string.IsNullOrEmpty(allocation_achieved_bombucketfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""bucket_loanaccounts"": [{allocation_achieved_bombucketfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(allocation_achieved_regionfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""region_loanaccounts"": [{allocation_achieved_regionfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(allocation_achieved_statefilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""state_loanaccounts"": [{allocation_achieved_statefilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(allocation_achieved_cityfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""city_loanaccounts"": [{allocation_achieved_cityfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(allocation_achieved_branchfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""branch_loanaccounts"": [{allocation_achieved_branchfilter}]
                                        }}
                                      }},
                                ";


        if (!string.IsNullOrEmpty(allocation_achieved_agencyidfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""agency_applicationorg_id"": [{allocation_achieved_agencyidfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(allocation_achieved_telecallingidfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""telecallingagency_applicationorg_id"": [{allocation_achieved_telecallingidfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(allocation_achieved_agentfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""collector_applicationuser_id"": [{allocation_achieved_agentfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(allocation_achieved_telecallerfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""tellecaller_applicationuser_id"": [{allocation_achieved_telecallerfilter}]
                                        }}
                                      }},
                                ";

        if (allocation_achieved_statusfilter != ReportsEnum.NoFilterPresent.Value)
            dslquerywithFilters += $@"
                                      {{
                                        ""term"": {{
                                          ""collection_status"": {{
                                            ""value"": ""{allocation_achieved_statusfilter}""
                                          }}
                                        }}
                                      }},
                                ";

        if (allocation_achieved_loan_bucketfilter != ReportsEnum.NoFilterPresent.Value)
            dslquerywithFilters += $@"
                                    {{
                                    ""term"": {{
                                      ""loan_bucket"": {{
                                        ""value"": ""{allocation_achieved_loan_bucketfilter}""
                                      }}
                                    }}
                                  }},
                            ";


        dslquerywithFilters += $@"
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
                              }}
                                ]
                                }}
                              }},
                              ""sort"": [
                                {{""lastmodifieddate_loanaccounts"": ""desc""}}
                              ]  
                            }}
                        ";


        dslquerywithFilters = _elasticSearchService.RemoveCommaFromDslQuery(dslquerywithFilters);
        return dslquerywithFilters;

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
        ENTiger.ENCollect.AllocationModule.AllocationAchievedInsightDownloadRoot.Root myDeserializedClass = JsonSerializer.Deserialize<ENTiger.ENCollect.AllocationModule.AllocationAchievedInsightDownloadRoot.Root>(elasticresponse);

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

            csv.Context.RegisterClassMap<Allocation_AchievedInsightElkResponseHitsMap>();
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



    /// <summary>
    /// Saves the details of the file and the user who downloaded it.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="customid"></param>
    private async void SaveDownloadFileAndloggedInUserDetails(string fileName, string customid)
    {
        InsightSaveDownloadDetailsDto dto = new InsightSaveDownloadDetailsDto();
        dto.CustomId = customid;
        dto.FilePath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.InsightReportFilePath);
        dto.FileName = fileName;
        dto.Description = "Allocation Achieved Gap Insight";
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


}

/// <summary>
/// 
/// </summary>
public class GenerateAllocationAchievedFileParams : DtoBridge
{
    public List<string>? FieldAgencyId { get; set; }

    public List<string>? TelecallingAgencyId { get; set; }

    public List<string>? FieldCollectorId { get; set; }

    public List<string>? TelecallerId { get; set; }

    public List<string>? ProductGroup { get; set; }

    public List<string>? Product { get; set; }

    public List<string>? SubProduct { get; set; }

    public List<string>? Bucket { get; set; }

    public List<string>? Current_Bucket { get; set; }

    public List<string>? Region { get; set; }

    public List<string>? State { get; set; }

    public List<string>? City { get; set; }

    public List<string>? Branch { get; set; }

    public string? Loan_Bucket { get; set; }
    public string? Status { get; set; }

}
