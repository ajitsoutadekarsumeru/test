using ENTiger.ENCollect.AllocationModule;
using ENTiger.ENCollect.DomainModels.Reports;
using ENTiger.ENCollect.DomainModels.Utilities;

namespace ENTiger.ENCollect.FeedbackModule;

/// <summary>
/// API to generate file of trail gap insight based on filters
/// </summary>
public class GenerateTrailGapFile : FlexiQueryBridge<GenerateTrailGapFileDto>
{
    protected readonly ILogger<GenerateTrailGapFile> _logger;
    protected GenerateTrailGapFileParams _params;
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
    /// <param name="elasticSearchService"></param>
    /// <param name="repoFactory"></param>
    /// <param name="ElasticSearchIndexSettings"></param>
    /// <param name="elasticSearchSettings"></param>
    public GenerateTrailGapFile(ILogger<GenerateTrailGapFile> logger,
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
    public virtual GenerateTrailGapFile AssignParameters(GenerateTrailGapFileParams @params)
    {
        Guard.AgainstNull(nameof(@params), @params);
        _params = @params;
        return this;
    }

    /// <summary>
    ///  Method that fetches data from elastic search based on input filters and creates csv file with data
    /// </summary>
    /// <returns></returns>
    public override GenerateTrailGapFileDto Fetch()
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

        string path = config.TrailGapInsightIndex;

        string fileName = "TrailGapInsightReport_" + customid + ".csv";
        string filePath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.InsightReportFilePath, fileName);

        FetchDataIterative(path, CurrentCursor, RecordsFetchedSoFar, loopNo, filePath).GetAwaiter().GetResult();


        GenerateTrailGapFileDto result = new GenerateTrailGapFileDto()
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
            string dslQuery = BuildDslQueryForTrailGapInsighDownload(_params, CurrentCursor);

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
    /// Build dsl query based on input parameters
    /// </summary>
    /// <param name="_params">Input data from FE</param>
    /// <param name="CurrentCursor">the current position/cursor for paginated data fetching</param>
    /// <returns></returns>
    private string BuildDslQueryForTrailGapInsighDownload(GenerateTrailGapFileParams _params, long CurrentCursor)
    {
        long RecordPerLoopSize = _elasticSearchSettings.RecordPerLoopSize;

        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;

        string trailgap_productgroupfilter = _elasticSearchService.FormatArrayFilter(_params.ProductGroup);
        string trailgap_productfilter = _elasticSearchService.FormatArrayFilter(_params.Product);
        string trailgap_subproductfilter = _elasticSearchService.FormatArrayFilter(_params.SubProduct);
        string trailgap_regionfilter = _elasticSearchService.FormatArrayFilter(_params.Region);
        string trailgap_statefilter = _elasticSearchService.FormatArrayFilter(_params.State);
        string trailgap_cityfilter = _elasticSearchService.FormatArrayFilter(_params.City);
        string trailgap_branchfilter = _elasticSearchService.FormatArrayFilter(_params.Branch);
        string trailgap_agencyidfilter = _elasticSearchService.FormatArrayFilter(_params.FieldAgencyId);
        string trailgap_telecallingidfilter = _elasticSearchService.FormatArrayFilter(_params.TelecallingAgencyId);
        string trailgap_agentfilter = _elasticSearchService.FormatArrayFilter(_params.FieldCollectorId);
        string trailgap_telecallerfilter = _elasticSearchService.FormatArrayFilter(_params.TelecallerId);
        string trailgap_currentbucketfilter = _elasticSearchService.FormatArrayFilter(_params.Current_Bucket);
        string trailgap_bombucketfilter = _elasticSearchService.FormatArrayFilter(_params.Bucket);



        string trailgap_statusfilter = _elasticSearchService.GetFilterTextForElasticSearch(_params.Status);
        string trailgap_loan_bucketfilter = _elasticSearchService.GetFilterTextForElasticSearch(_params.Loan_Bucket);
        string trailgap_trailgroupfilter = _elasticSearchService.GetFilterTextForElasticSearch(_params.TrailGroup);


        string dslquerywithFilters = $@"
{{
  ""from"": {CurrentCursor},
  ""size"": {RecordPerLoopSize},
  ""track_total_hits"": true,
  ""query"": {{
    ""bool"": {{
      ""must"": [
";

        if (!string.IsNullOrEmpty(trailgap_productgroupfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""productgroup_loanaccounts"": [{trailgap_productgroupfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(trailgap_productfilter))
            dslquerywithFilters += $@"
                                     {{
                                    ""terms"": {{
                                      ""product_loanaccounts"": [{trailgap_productfilter}]
                                    }}
                                  }},
                            ";

        if (!string.IsNullOrEmpty(trailgap_subproductfilter))
            dslquerywithFilters += $@"
                                  {{
                                    ""terms"": {{
                                      ""subproduct_loanaccounts"": [{trailgap_subproductfilter}]
                                    }}
                                  }},
                            ";

        if (!string.IsNullOrEmpty(trailgap_currentbucketfilter))
            dslquerywithFilters += $@"
                                  {{
                                    ""terms"": {{
                                      ""current_bucket_loanaccounts"": [{trailgap_currentbucketfilter}]
                                    }}
                                  }},
                            ";

        if (!string.IsNullOrEmpty(trailgap_bombucketfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""bucket_loanaccounts"": [{trailgap_bombucketfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(trailgap_regionfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""region_loanaccounts"": [{trailgap_regionfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(trailgap_statefilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""state_loanaccounts"": [{trailgap_statefilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(trailgap_cityfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""city_loanaccounts"": [{trailgap_cityfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(trailgap_branchfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""branch_loanaccounts"": [{trailgap_branchfilter}]
                                        }}
                                      }},
                                ";


        if (!string.IsNullOrEmpty(trailgap_agencyidfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""agency_applicationorg_id"": [{trailgap_agencyidfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(trailgap_telecallingidfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""telecallingagency_applicationorg_id"": [{trailgap_telecallingidfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(trailgap_agentfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""collector_applicationuser_id"": [{trailgap_agentfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(trailgap_telecallerfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""tellecaller_applicationuser_id"": [{trailgap_telecallerfilter}]
                                        }}
                                      }},
                                ";

        if (trailgap_statusfilter != "NoFilterPresent")
            dslquerywithFilters += $@"
                                      {{
                                        ""term"": {{
                                          ""feedback_status"": {{
                                            ""value"": ""{trailgap_statusfilter}""
                                          }}
                                        }}
                                      }},
                                ";

        if (trailgap_loan_bucketfilter != "NoFilterPresent")
            dslquerywithFilters += $@"
                                    {{
                                    ""term"": {{
                                      ""loan_bucket"": {{
                                        ""value"": ""{trailgap_loan_bucketfilter}""
                                      }}
                                    }}
                                  }},
                            ";

        if (trailgap_trailgroupfilter != "NoFilterPresent")
            dslquerywithFilters += $@"
                                  {{
                                    ""term"": {{
                                      ""fbak_dispositiongroup"": {{
                                        ""value"": ""{trailgap_trailgroupfilter}""
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
        ENTiger.ENCollect.FeedbackModule.TrailGapInsightDownloadRoot.Root myDeserializedClass = JsonSerializer.Deserialize<ENTiger.ENCollect.FeedbackModule.TrailGapInsightDownloadRoot.Root>(elasticresponse);

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

            csv.Context.RegisterClassMap<TrailGapInsightElkResponseHitsMap>();
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
        dto.Description = "Trail Gap Insight";
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
public class GenerateTrailGapFileParams : DtoBridge
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
    public string? TrailGroup { get; set; }

}
