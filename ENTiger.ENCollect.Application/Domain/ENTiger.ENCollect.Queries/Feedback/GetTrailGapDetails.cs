using ENTiger.ENCollect.DomainModels.Enum;

namespace ENTiger.ENCollect.FeedbackModule;

/// <summary>
/// Fetches data of trail gap insight from elastic search with filters
/// </summary>
public class GetTrailGapDetails : FlexiQueryPagedListBridgeAsync<GetTrailGapDetailsParams, GetTrailGapDetailsDto, FlexAppContextBridge>
{
    protected readonly ILogger<GetTrailGapDetails> _logger;
    protected GetTrailGapDetailsParams _params;
    protected readonly RepoFactory _repoFactory;
    protected FlexAppContextBridge? _flexAppContext;
    public IElasticSearchService _elasticSearchService { get; set; }
    private readonly ElasticSearchIndexSettings _elasticIndexConfig;

    IEnumerable<GetTrailGapDetailsDto> queryResult = new List<GetTrailGapDetailsDto>();
    /// <summary>
    /// Constructor to initialize dependencies
    /// </summary>
    /// <param name="logger"></param>
    public GetTrailGapDetails(ILogger<GetTrailGapDetails> logger, IElasticSearchService elasticSearchService, RepoFactory repoFactory, IOptions<ElasticSearchIndexSettings> ElasticSearchIndexSettings)
    {
        _logger = logger;
        _elasticSearchService = elasticSearchService;
        _repoFactory = repoFactory;
        _elasticIndexConfig = ElasticSearchIndexSettings.Value;
    }
    /// <summary>
    /// Assigns input parameters
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual GetTrailGapDetails AssignParameters(GetTrailGapDetailsParams @params)
    {
        _params = @params;
        return this;
    }

    /// <summary>
    /// Processes data based on filters from elastic search, parses response and returns output model
    /// </summary>
    /// <returns></returns>
    public override async Task<FlexiPagedList<GetTrailGapDetailsDto>> Fetch()
    {
        _flexAppContext = _params.GetAppContext();

        Guard.AgainstNullAndEmpty("_flexAppContext?.TenantId", _flexAppContext?.TenantId);
        _repoFactory.Init(_params);

        var config = _elasticIndexConfig.IndexNames.Where(a => a.TenantId == _flexAppContext?.TenantId).FirstOrDefault();
        Guard.AgainstNull(nameof(config), config);

        string path = config.TrailGapInsightIndex;

        //Build DSL Query
        string dslquery = BuildDslQueryForTrailGapDetail(_params);

        //Send request to elastic search
        var response = await _elasticSearchService.SendPostRequestToElasticSearchAsync(path, dslquery);
        Guard.AgainstNull(nameof(response), response);

        // Parse response
        var output = ParseResponse(response);

        return output;
    }

    /// <summary>
    /// Build dsl query for trail gap details insight with filters
    /// </summary>
    /// <param name="_params"></param>
    /// <returns></returns>
    private string BuildDslQueryForTrailGapDetail(GetTrailGapDetailsParams _params)
    {
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;

        int? from = (_params.PageNumber) * _params.PageSize;


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
  ""from"": {from},
  ""size"": {_params.PageSize},
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

        if (trailgap_statusfilter != ReportsEnum.NoFilterPresent.Value)
            dslquerywithFilters += $@"
                                      {{
                                        ""term"": {{
                                          ""feedback_status"": {{
                                            ""value"": ""{trailgap_statusfilter}""
                                          }}
                                        }}
                                      }},
                                ";

        if (trailgap_loan_bucketfilter != ReportsEnum.NoFilterPresent.Value)
            dslquerywithFilters += $@"
                                    {{
                                    ""term"": {{
                                      ""loan_bucket"": {{
                                        ""value"": ""{trailgap_loan_bucketfilter}""
                                      }}
                                    }}
                                  }},
                            ";

        if (trailgap_trailgroupfilter != ReportsEnum.NoFilterPresent.Value)
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
    /// Parse Response of elastic search
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private FlexiPagedList<GetTrailGapDetailsDto> ParseResponse(object response)
    {
        string elasticresponse = response.ToString();

        Guard.AgainstNullAndEmpty(nameof(elasticresponse), elasticresponse);
        //Parse the response
        var result = ParseResponseWithJsonDocument(elasticresponse);

        return result;

    }

    /// <summary>
    /// Parse response with JsonDocument
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private FlexiPagedList<GetTrailGapDetailsDto> ParseResponseWithJsonDocument(string response)
    {
        using (JsonDocument doc = JsonDocument.Parse(response))
        {
            // Extract total count from the parsed JSON
            int TotalCount = doc.RootElement.GetProperty("hits")
                                            .GetProperty("total")
                                            .GetProperty("value")
                                            .GetInt32();

            // List to hold the final output
            List<GetTrailGapDetailsDto> detailoutput = new List<GetTrailGapDetailsDto>();

            // Iterate over each hit in the "hits" array
            foreach (JsonElement jsonproperties in doc.RootElement.GetProperty("hits").GetProperty("hits").EnumerateArray())
            {
                // Deserialize the "_source" field into GetPrimaryAllocationDetailsELKDto
                GetTrailGapDetailsELKDto outputModelForELK = JsonSerializer.Deserialize<GetTrailGapDetailsELKDto>(jsonproperties.GetProperty("_source").GetRawText());

                // Create and populate the GetPrimaryAllocationDetailsDto object
                GetTrailGapDetailsDto obj = new GetTrailGapDetailsDto
                {
                    AccountNo = outputModelForELK.agreementid_loanacounts,
                    ProductGroup = outputModelForELK.productgroup_loanaccounts,
                    Product = outputModelForELK.product_loanaccounts,
                    SubProduct = outputModelForELK.subproduct_loanaccounts,
                    CurrentBucket = outputModelForELK.current_bucket_loanaccounts,
                    Bucket = outputModelForELK.bucket_loanaccounts,
                    Region = outputModelForELK.region_loanaccounts,
                    State = outputModelForELK.state_loanaccounts,
                    City = outputModelForELK.city_loanaccounts,
                    Branch = outputModelForELK.branch_loanaccounts,
                    FieldAgencyId = outputModelForELK.agency_applicationorg_id,
                    FieldAgencyName = outputModelForELK.agency_applicationorg_firstname,
                    FieldAgentId = outputModelForELK.collector_applicationuser_id,
                    FieldAgentName = outputModelForELK.collector_applicationuser_firstname,
                    Status = outputModelForELK.feedback_status,
                    TrailGroup = outputModelForELK.fbak_dispositiongroup,
                    TotalOutstandingAmount = outputModelForELK.total_overdue_amt
                };

                // Add the object to the detailoutput list
                detailoutput.Add(obj);
            }

            queryResult = detailoutput;

            this.FlexiPagedList = new FlexiPagedList<GetTrailGapDetailsDto>(TotalCount, 0 , _params.PageSize);
            this.FlexiPagedList.AddRange(queryResult);

            return FlexiPagedList;

        }


    }



}

public class GetTrailGapDetailsParams : PagedQueryParamsDtoBridge
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