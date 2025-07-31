using ENTiger.ENCollect.DomainModels.Enum;

namespace ENTiger.ENCollect.AllocationModule;

/// <summary>
/// Retrieves allocation achieved summary insights details from Elasticsearch with pagination.
/// Builds DSL query with filters, sends it to Elasticsearch, parses response, and returns the DTO list.
/// </summary>
public class GetAllocationAchievedDetails : FlexiQueryPagedListBridgeAsync<GetAllocationAchievedDetailsParams, GetAllocationAchievedDetailsDto, FlexAppContextBridge>
{
    protected readonly ILogger<GetAllocationAchievedDetails> _logger;
    protected GetAllocationAchievedDetailsParams _params;
    protected readonly RepoFactory _repoFactory;
    protected FlexAppContextBridge? _flexAppContext;
    public IElasticSearchService _elasticSearchService { get; set; }
    private readonly ElasticSearchIndexSettings _elasticIndexConfig;

    IEnumerable<GetAllocationAchievedDetailsDto> queryResult = new List<GetAllocationAchievedDetailsDto>();

    /// <summary>
    /// Constructor for GetSecondaryAllocationDetails.
    /// Initializes dependencies via DI.
    /// </summary>
    /// <param name="logger"></param>
    public GetAllocationAchievedDetails(ILogger<GetAllocationAchievedDetails> logger, IElasticSearchService elasticSearchService, RepoFactory repoFactory, IOptions<ElasticSearchIndexSettings> ElasticSearchIndexSettings)
    {
        _logger = logger;
        _elasticSearchService = elasticSearchService;
        _repoFactory = repoFactory;
        _elasticIndexConfig = ElasticSearchIndexSettings.Value;
    }
    /// <summary>
    /// Assigns parameters passed from the request to the instance. 
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    public virtual GetAllocationAchievedDetails AssignParameters(GetAllocationAchievedDetailsParams @params)
    {
        _params = @params;
        return this;
    }

    /// <summary>
    /// Executes the Elasticsearch query using constructed DSL and returns paged data.
    /// </summary>
    /// <returns></returns>
    public override async Task<FlexiPagedList<GetAllocationAchievedDetailsDto>> Fetch()
    {
        IEnumerable<GetAllocationAchievedDetailsDto> queryResult = null;

        _flexAppContext = _params.GetAppContext();

        Guard.AgainstNullAndEmpty("_flexAppContext?.TenantId", _flexAppContext?.TenantId);
        _repoFactory.Init(_params);

        var config = _elasticIndexConfig.IndexNames.Where(a => a.TenantId == _flexAppContext?.TenantId).FirstOrDefault();
        Guard.AgainstNull(nameof(config), config);

        string path = config.Allocation_AchievedInsightIndex;

        //Build DSL Query
        string dslquery = BuildDslQueryForAllocationVsAchieved(_params);

        //Send request to elastic search
        var response = await _elasticSearchService.SendPostRequestToElasticSearchAsync(path, dslquery);
        Guard.AgainstNull(nameof(response), response);

        // Parse response
        var output = ParseResponse(response);

        return output;


    }


    /// <summary>
    /// Build dsl query for allocation vs achieved details insight with filters
    /// </summary>
    /// <param name="_params"></param>
    /// <returns></returns>
    private string BuildDslQueryForAllocationVsAchieved(GetAllocationAchievedDetailsParams _params)
    {
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;

        int? from = (_params.PageNumber) * _params.PageSize;


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
  ""from"": {from},
  ""size"": {_params.PageSize},
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
    /// Parses the raw object response into a paged list of DTOs.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private FlexiPagedList<GetAllocationAchievedDetailsDto> ParseResponse(object response)
    {
        string elasticresponse = response.ToString();

        Guard.AgainstNullAndEmpty(nameof(elasticresponse), elasticresponse);
        //Parse the response
        var result = ParseResponseWithJsonDocument(elasticresponse);

        return result;

    }

    /// <summary>
    /// Parses JSON string using JsonDocument and maps _source to output DTOs.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private FlexiPagedList<GetAllocationAchievedDetailsDto> ParseResponseWithJsonDocument(string response)
    {
        using (JsonDocument doc = JsonDocument.Parse(response))
        {
            //Extract total hit count for pagination metadata
            int TotalCount = doc.RootElement.GetProperty("hits")
                                            .GetProperty("total")
                                            .GetProperty("value")
                                            .GetInt32();

            // List to hold the final output
            List<GetAllocationAchievedDetailsDto> detailoutput = new List<GetAllocationAchievedDetailsDto>();

            // Iterate over each hit in the "hits" array
            foreach (JsonElement jsonproperties in doc.RootElement.GetProperty("hits").GetProperty("hits").EnumerateArray())
            {
                // Deserialize the "_source" field into GetPrimaryAllocationDetailsELKDto
                GetAllocationAchievedDetailsELKDto outputModelForELK = JsonSerializer.Deserialize<GetAllocationAchievedDetailsELKDto>(jsonproperties.GetProperty("_source").GetRawText());

                // Create and populate the GetPrimaryAllocationDetailsDto object
                GetAllocationAchievedDetailsDto obj = new GetAllocationAchievedDetailsDto
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
                    Field_Agent_Name = outputModelForELK.collector_applicationuser_firstname,
                    Field_Agent_Custom_ID = outputModelForELK.collector_applicationuser_customid,
                    Telecaller_Agent_Name = outputModelForELK.tellecaller_applicationuser_firstname,
                    Telecaller_Agent_Custom_ID = outputModelForELK.tellecaller_applicationuser_customid,

                    Primary_Alloc_Status_For_Telecalling_Agency = outputModelForELK.primaryallocationstatusfortelecallingagency,
                    Primary_Alloc_Status_For_Field_Agency = outputModelForELK.primaryallocationstatusforfieldagency,
                    Primary_Allocation_Status = outputModelForELK.primaryallocationstatus,
                    Secondary_Allocation_Status = outputModelForELK.secondaryallocationstatus,
                    LA_Lastmodified = outputModelForELK.lastmodifieddate_loanaccounts != null ? Convert.ToDateTime(outputModelForELK.lastmodifieddate_loanaccounts).Date.ToString("yyyy-MM-dd") : "",
                    LA_Lastmodified_Date_And_Time = outputModelForELK.lastmodifieddate_loanaccounts,
                    CollectionStatus = outputModelForELK.collection_status

                };


                // Add the object to the detailoutput list
                detailoutput.Add(obj);
            }

            queryResult = detailoutput;

            this.FlexiPagedList = new FlexiPagedList<GetAllocationAchievedDetailsDto>(TotalCount, 0, _params.PageSize);
            this.FlexiPagedList.AddRange(queryResult);

            return FlexiPagedList;

        }


    }




}

/// <summary>
/// 
/// </summary>
public class GetAllocationAchievedDetailsParams : PagedQueryParamsDtoBridge
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
