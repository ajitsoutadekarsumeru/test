using ENTiger.ENCollect.DomainModels.Enum;

namespace ENTiger.ENCollect.AllocationModule;

/// <summary>
/// Retrieves  secondary allocation summary insights details from Elasticsearch with pagination.
/// Builds DSL query with filters, sends it to Elasticsearch, parses response, and returns the DTO list.
/// </summary>
public class GetSecondaryAllocationDetails : FlexiQueryPagedListBridgeAsync<GetSecondaryAllocationDetailsParams, GetSecondaryAllocationDetailsDto, FlexAppContextBridge>
{
    protected readonly ILogger<GetSecondaryAllocationDetails> _logger;
    protected GetSecondaryAllocationDetailsParams _params;
    protected readonly RepoFactory _repoFactory; 
    protected FlexAppContextBridge? _flexAppContext;
    public IElasticSearchService _elasticSearchService { get; set; }
    private readonly ElasticSearchIndexSettings _elasticIndexConfig;
    public string NoFilterPresent = "NoFilterPresent";

    IEnumerable<GetSecondaryAllocationDetailsDto> queryResult = new List<GetSecondaryAllocationDetailsDto>();
    /// <summary>
    /// Constructor for GetSecondaryAllocationDetails.
    /// Initializes dependencies via DI.
    /// </summary>
    /// <param name="logger"></param>
    public GetSecondaryAllocationDetails(ILogger<GetSecondaryAllocationDetails> logger, IElasticSearchService elasticSearchService, RepoFactory repoFactory, IOptions<ElasticSearchIndexSettings> ElasticSearchIndexSettings)
    {
        Guard.AgainstNull(nameof(logger), logger);
        Guard.AgainstNull(nameof(repoFactory), repoFactory);
        Guard.AgainstNull(nameof(elasticSearchService), elasticSearchService);

        _logger = logger;
        _repoFactory = repoFactory;
        _elasticSearchService = elasticSearchService;
        _elasticIndexConfig = ElasticSearchIndexSettings.Value;
    }
    /// <summary>
    /// Assigns parameters passed from the request to the instance.
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    public virtual GetSecondaryAllocationDetails AssignParameters(GetSecondaryAllocationDetailsParams @params)
    {
        _params = @params;
        return this;
    }

    /// <summary>
    /// Executes the Elasticsearch query using constructed DSL and returns paged data.
    /// </summary>
    /// <returns></returns>
    public override async Task<FlexiPagedList<GetSecondaryAllocationDetailsDto>> Fetch()
    {
        _flexAppContext = _params.GetAppContext();

        Guard.AgainstNullAndEmpty("_flexAppContext?.TenantId", _flexAppContext?.TenantId);
        _repoFactory.Init(_params);

        var config = _elasticIndexConfig.IndexNames.Where(a => a.TenantId == _flexAppContext?.TenantId).FirstOrDefault();
        Guard.AgainstNull(nameof(config), config);

        string path = config.SecondaryAllocationInsightIndex;

        //Build DSL Query
        string dslquery = BuildDslQueryForSecondaryAllocationInsighDetail(_params);

        //Send request to elastic search
        var response = await _elasticSearchService.SendPostRequestToElasticSearchAsync(path, dslquery);
        Guard.AgainstNull(nameof(response), response);

        // Parse response
        var output = ParseResponse(response);

        return output;
    }

    /// <summary>
    /// Parses the raw object response into a paged list of DTOs.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private FlexiPagedList<GetSecondaryAllocationDetailsDto> ParseResponse(object response)
    {
        string elasticresponse = response.ToString();

        Guard.AgainstNullAndEmpty(nameof(elasticresponse), elasticresponse);
        // Parse JSON string into strongly typed objects
        var result = ParseResponseWithJsonDocument(elasticresponse);

        return result;

    }

    /// <summary>
    /// Parses JSON string using JsonDocument and maps _source to output DTOs.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private FlexiPagedList<GetSecondaryAllocationDetailsDto> ParseResponseWithJsonDocument(string response)
    {
        using (JsonDocument doc = JsonDocument.Parse(response))
        {
            //Extract total hit count for pagination metadata
            int TotalCount = doc.RootElement.GetProperty("hits")
                                            .GetProperty("total")
                                            .GetProperty("value")
                                            .GetInt32();

            // List to hold the final output
            List<GetSecondaryAllocationDetailsDto> detailoutput = new List<GetSecondaryAllocationDetailsDto>();

            // Iterate over each hit in the "hits" array
            foreach (JsonElement jsonproperties in doc.RootElement.GetProperty("hits").GetProperty("hits").EnumerateArray())
            {
                // Deserialize the "_source" field into GetPrimaryAllocationDetailsELKDto
                GetSecondaryAllocationDetailsELKDto outputModelForELK = JsonSerializer.Deserialize<GetSecondaryAllocationDetailsELKDto>(jsonproperties.GetProperty("_source").GetRawText());

                // Create and populate the GetPrimaryAllocationDetailsDto object
                GetSecondaryAllocationDetailsDto obj = new GetSecondaryAllocationDetailsDto
                {
                    AccountNo = outputModelForELK.agreementid_loanacounts,
                    ProductGroup = outputModelForELK.productgroup_loanaccounts,
                    Product = outputModelForELK.product_loanaccounts,
                    SubProduct = outputModelForELK.subproduct_loanaccounts,
                    CurrentBucket = outputModelForELK.current_bucket_loanaccounts,
                    BOMBucket = outputModelForELK.bucket_loanaccounts,
                    Zone = outputModelForELK.zone_loanaccounts,
                    Region = outputModelForELK.region_loanaccounts,
                    State = outputModelForELK.state_loanaccounts,
                    City = outputModelForELK.city_loanaccounts,
                    Branch = outputModelForELK.branch_loanaccounts,
                    BOM_POS = outputModelForELK.bom_pos_loanaccounts,
                    Current_POS = outputModelForELK.bom_pos_loanaccounts,
                    Principal_Overdue = outputModelForELK.principal_od,
                    Interest_Overdue = outputModelForELK.interest_od,
                    Charge_Overdue = outputModelForELK.charge_overdue,
                    Total_Overdue = outputModelForELK.total_overdue,
                    NPA_Flag = outputModelForELK.npa_stageid_loanaccounts,
                    Amount_Outstanding = outputModelForELK.amountoutstanding,
                    Allocation_Owner_Name = outputModelForELK.allocationowner_applicationuser_firstname,
                    Allocation_Owner_Role = outputModelForELK.allocation_owner_code_allocationowner_applicationuser_customid,
                    Allocation_Owner_Custom_ID = outputModelForELK.allocation_owner_code_allocationowner_applicationuser_customid,
                    Telecalling_Agency_Name = outputModelForELK.telecallingagency_applicationorg_firstname,
                    Telecalling_Agency_Custom_ID = outputModelForELK.telecallingagency_applicationorg_customid,
                    Field_Agency_Name = outputModelForELK.agency_applicationorg_firstname,
                    Field_Agency_Custom_ID = outputModelForELK.agency_applicationorg_customid,
                    Primary_Alloc_Status_For_Telecalling_Agency = outputModelForELK.primaryallocationstatusfortelecallingagency,
                    Primary_Alloc_Status_For_Field_Agency = outputModelForELK.primaryallocationstatusforfieldagency,
                    Primary_Allocation_Status = outputModelForELK.primaryallocationstatus,
                    Secondary_Allocation_Status = outputModelForELK.secondaryallocationstatus,
                    LA_Lastmodified = outputModelForELK.lastmodifieddate_loanaccounts != null ? Convert.ToDateTime(outputModelForELK.lastmodifieddate_loanaccounts).Date.ToString("yyyy-MM-dd") : "",
                    LA_Lastmodified_Date_And_Time = outputModelForELK.lastmodifieddate_loanaccounts


                };

                // Add the object to the detailoutput list
                detailoutput.Add(obj);
            }

            queryResult = detailoutput;

            this.FlexiPagedList = new FlexiPagedList<GetSecondaryAllocationDetailsDto>(TotalCount, 0 , _params.PageSize);
            this.FlexiPagedList.AddRange(queryResult);

            return FlexiPagedList;

        }


    }


    /// <summary>
    /// Dynamically builds the Elasticsearch query with filters based on user-supplied parameters.
    /// </summary>
    /// <param name="_params"></param>
    /// <returns></returns>
    private string BuildDslQueryForSecondaryAllocationInsighDetail(GetSecondaryAllocationDetailsParams _params)
    {
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;

        int? from = (_params.PageNumber) * _params.PageSize;

        // Uses helper methods on _elasticSearchService to format filters.
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



        string dslquerywithFilters = $@"
{{
  ""from"": {from},
  ""size"": {_params.PageSize},
  ""track_total_hits"": true,
  ""query"": {{
    ""bool"": {{
      ""must"": [
";

        if (!string.IsNullOrEmpty(secondaryallocationproductgroupfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""productgroup_loanaccounts"": [{secondaryallocationproductgroupfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(secondaryallocationproductfilter))
            dslquerywithFilters += $@"
                                     {{
                                    ""terms"": {{
                                      ""product_loanaccounts"": [{secondaryallocationproductfilter}]
                                    }}
                                  }},
                            ";

        if (!string.IsNullOrEmpty(secondaryallocationsubproductfilter))
            dslquerywithFilters += $@"
                                  {{
                                    ""terms"": {{
                                      ""subproduct_loanaccounts"": [{secondaryallocationsubproductfilter}]
                                    }}
                                  }},
                            ";

        if (!string.IsNullOrEmpty(secondaryallocationcurrentbucketfilter))
            dslquerywithFilters += $@"
                                  {{
                                    ""terms"": {{
                                      ""current_bucket_loanaccounts"": [{secondaryallocationcurrentbucketfilter}]
                                    }}
                                  }},
                            ";

        if (!string.IsNullOrEmpty(secondaryallocationbombucketfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""bucket_loanaccounts"": [{secondaryallocationbombucketfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(secondaryallocationregionfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""region_loanaccounts"": [{secondaryallocationregionfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(secondaryallocationstatefilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""state_loanaccounts"": [{secondaryallocationstatefilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(secondaryallocationcityfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""city_loanaccounts"": [{secondaryallocationcityfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(secondaryallocationbranchfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""branch_loanaccounts"": [{secondaryallocationbranchfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(secondaryallocationallocationownerfilter))
            dslquerywithFilters += $@"
                                    {{
                                    ""terms"": {{
                                      ""allocationowner_applicationuser_id"": [{secondaryallocationallocationownerfilter}]
                                    }}
                                  }},
                            ";

        if (secondaryallocationstatusfilter != ReportsEnum.NoFilterPresent.Value)
            dslquerywithFilters += $@"
                                      {{
                                        ""term"": {{
                                          ""secondaryallocationstatus"": {{
                                            ""value"": ""{secondaryallocationstatusfilter}""
                                          }}
                                        }}
                                      }},
                                ";

        if (secondaryallocationloan_bucketfilter != ReportsEnum.NoFilterPresent.Value)
            dslquerywithFilters += $@"
                                    {{
                                    ""term"": {{
                                      ""loan_bucket"": {{
                                        ""value"": ""{secondaryallocationloan_bucketfilter}""
                                      }}
                                    }}
                                  }},
                            ";

        if (secondaryallocationentityfilter != ReportsEnum.NoFilterPresent.Value)
            dslquerywithFilters += $@"
                                  {{
                                    ""term"": {{
                                      ""entity"": {{
                                        ""value"": ""{secondaryallocationentityfilter}""
                                      }}
                                    }}
                                  }},
                            ";

        

        if (!string.IsNullOrEmpty(secondaryallocationagencyfilter))
        {
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""agency_applicationorg_id"": [{secondaryallocationagencyfilter}]
                                        }}
                                      }},
                                ";
        }

        if (!string.IsNullOrEmpty(secondaryallocationtelecallingagencyfilter))
        {
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""telecallingagency_applicationorg_id"": [{secondaryallocationtelecallingagencyfilter}]
                                        }}
                                      }},
                                ";

        }

        if (!string.IsNullOrEmpty(secondaryallocation_agentfilter))
        {
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""collector_applicationuser_id"": [{secondaryallocation_agentfilter}]
                                        }}
                                      }},
                                ";
        }

        if (!string.IsNullOrEmpty(secondaryallocation_telecallerfilter))
        {
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""tellecaller_applicationuser_id"": [{secondaryallocation_telecallerfilter}]
                                        }}
                                      }},
                                ";

        }

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

}

/// <summary>
/// Parameter class holding all filter options for fetching secondary allocation details.
/// Inherits pagination capability from PagedQueryParamsDtoBridge.
/// </summary>
public class GetSecondaryAllocationDetailsParams : PagedQueryParamsDtoBridge
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
    //public List<string>? Agency { get; set; }
    public List<string>? FieldAgencyId { get; set; }
    public List<string>? TelecallingAgencyId { get; set; }

    public List<string>? FieldCollectorId { get; set; }

    public List<string>? TelecallerId { get; set; }

    public List<string>? AllocationOwner { get; set; }
    public string? Loan_Bucket { get; set; }
    public string? Status { get; set; }
    public string? Entity { get; set; }

}
