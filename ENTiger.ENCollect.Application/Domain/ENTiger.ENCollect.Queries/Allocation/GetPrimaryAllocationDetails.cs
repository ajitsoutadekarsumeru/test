
using ENTiger.ENCollect.DomainModels.Enum;

namespace ENTiger.ENCollect.AllocationModule;

/// <summary>
/// Gets primary allocation insights detailed data based on filters
/// </summary>
public class GetPrimaryAllocationDetails : FlexiQueryPagedListBridgeAsync<GetPrimaryAllocationDetailsParams, GetPrimaryAllocationDetailsDto, FlexAppContextBridge>
{
    protected readonly ILogger<GetPrimaryAllocationDetails> _logger;
    protected readonly RepoFactory _repoFactory;
    protected GetPrimaryAllocationDetailsParams _params;
    protected FlexAppContextBridge? _flexAppContext;
    public IElasticSearchService _elasticSearchService { get; set; }
    private readonly ElasticSearchIndexSettings _elasticIndexConfig;

    IEnumerable<GetPrimaryAllocationDetailsDto> queryResult = new List<GetPrimaryAllocationDetailsDto>();

    /// <summary>
    /// Constructor of GetPrimaryAllocationDetails
    /// </summary>
    /// <param name="logger"></param>
    public GetPrimaryAllocationDetails(ILogger<GetPrimaryAllocationDetails> logger, IElasticSearchService elasticSearchService, RepoFactory repoFactory, IOptions<ElasticSearchIndexSettings> ElasticSearchIndexSettings)
    {
        _logger = logger;
        _elasticSearchService = elasticSearchService;
        _repoFactory = repoFactory;
        _elasticIndexConfig = ElasticSearchIndexSettings.Value;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    public virtual GetPrimaryAllocationDetails AssignParameters(GetPrimaryAllocationDetailsParams @params)
    {
        _params = @params;
        return this;
    }

    /// <summary>
    ///  Get primary allocation insight data based on filters
    /// </summary>
    /// <returns></returns>
    public override async Task<FlexiPagedList<GetPrimaryAllocationDetailsDto>> Fetch()
    {


        _flexAppContext = _params.GetAppContext();

        Guard.AgainstNullAndEmpty("_flexAppContext?.TenantId", _flexAppContext?.TenantId);
        _repoFactory.Init(_params);

        var config = _elasticIndexConfig.IndexNames.Where(a => a.TenantId == _flexAppContext?.TenantId).FirstOrDefault();
        Guard.AgainstNull(nameof(config), config);

        string path = config.PrimaryAllocationInsightIndex;

        //Build DSL Query
        string dslquery = BuildDslQueryForPrimaryAllocationInsighDetail(_params);

        //Send request to elastic search
        var response = await _elasticSearchService.SendPostRequestToElasticSearchAsync(path, dslquery);
        Guard.AgainstNull(nameof(response), response);

        // Parse response
        var output =  ParseResponse(response);

        return output;
    }

    /// <summary>
    /// Parse Response of elastic search
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private FlexiPagedList<GetPrimaryAllocationDetailsDto> ParseResponse(object response)
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
    private FlexiPagedList<GetPrimaryAllocationDetailsDto> ParseResponseWithJsonDocument(string response)
    {
        using (JsonDocument doc = JsonDocument.Parse(response))
        {
            // Extract total count from the parsed JSON
            int TotalCount = doc.RootElement.GetProperty("hits")
                                            .GetProperty("total")
                                            .GetProperty("value")
                                            .GetInt32();

            // List to hold the final output
            List<GetPrimaryAllocationDetailsDto> detailoutput = new List<GetPrimaryAllocationDetailsDto>();

            // Iterate over each hit in the "hits" array
            foreach (JsonElement jsonproperties in doc.RootElement.GetProperty("hits").GetProperty("hits").EnumerateArray())
            {
                // Deserialize the "_source" field into GetPrimaryAllocationDetailsELKDto
                GetPrimaryAllocationDetailsELKDto outputModelForELK = JsonSerializer.Deserialize<GetPrimaryAllocationDetailsELKDto>(jsonproperties.GetProperty("_source").GetRawText());

                // Create and populate the GetPrimaryAllocationDetailsDto object
                GetPrimaryAllocationDetailsDto obj = new GetPrimaryAllocationDetailsDto
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
                    Current_POS = outputModelForELK.bom_current_pos_loanaccounts,
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
                    LA_Lastmodified = outputModelForELK.lastmodifieddate_loanaccounts != null
                        ? Convert.ToDateTime(outputModelForELK.lastmodifieddate_loanaccounts).ToString("yyyy-MM-dd")
                        : string.Empty,
                    LA_Lastmodified_Date_And_Time = outputModelForELK.lastmodifieddate_loanaccounts
                };

                // Add the object to the detailoutput list
                detailoutput.Add(obj);
            }

            queryResult = detailoutput;

            this.FlexiPagedList = new FlexiPagedList<GetPrimaryAllocationDetailsDto>(TotalCount, 0, _params.PageSize);
            this.FlexiPagedList.AddRange(queryResult);

            return FlexiPagedList;

        }


    }


    /// <summary>
    /// Build dsl query for primary allocation insight with filters
    /// </summary>
    /// <param name="_params"></param>
    /// <returns></returns>
    private string BuildDslQueryForPrimaryAllocationInsighDetail(GetPrimaryAllocationDetailsParams _params)
    {
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;

        int? from = (_params.PageNumber) * _params.PageSize;


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



        string dslquerywithFilters = $@"
{{
  ""from"": {from},
  ""size"": {_params.PageSize},
  ""track_total_hits"": true,
  ""query"": {{
    ""bool"": {{
      ""must"": [
";

        if (!string.IsNullOrEmpty(primaryallocationproductgroupfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""productgroup_loanaccounts"": [{primaryallocationproductgroupfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(primaryallocationproductfilter))
            dslquerywithFilters += $@"
                                     {{
                                    ""terms"": {{
                                      ""product_loanaccounts"": [{primaryallocationproductfilter}]
                                    }}
                                  }},
                            ";

        if (!string.IsNullOrEmpty(primaryallocationsubproductfilter))
            dslquerywithFilters += $@"
                                  {{
                                    ""terms"": {{
                                      ""subproduct_loanaccounts"": [{primaryallocationsubproductfilter}]
                                    }}
                                  }},
                            ";

        if (!string.IsNullOrEmpty(primaryallocationcurrentbucketfilter))
            dslquerywithFilters += $@"
                                  {{
                                    ""terms"": {{
                                      ""current_bucket_loanaccounts"": [{primaryallocationcurrentbucketfilter}]
                                    }}
                                  }},
                            ";

        if (!string.IsNullOrEmpty(primaryallocationbombucketfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""bucket_loanaccounts"": [{primaryallocationbombucketfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(primaryallocationregionfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""region_loanaccounts"": [{primaryallocationregionfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(primaryallocationstatefilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""state_loanaccounts"": [{primaryallocationstatefilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(primaryallocationcityfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""city_loanaccounts"": [{primaryallocationcityfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(primaryallocationbranchfilter))
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""branch_loanaccounts"": [{primaryallocationbranchfilter}]
                                        }}
                                      }},
                                ";

        if (!string.IsNullOrEmpty(primaryallocationallocationownerfilter))
            dslquerywithFilters += $@"
                                    {{
                                    ""terms"": {{
                                      ""allocationowner_applicationuser_id"": [{primaryallocationallocationownerfilter}]
                                    }}
                                  }},
                            ";

        if (primaryallocationstatusfilter != ReportsEnum.NoFilterPresent.Value)
            dslquerywithFilters += $@"
                                      {{
                                        ""term"": {{
                                          ""primaryallocationstatus"": {{
                                            ""value"": ""{primaryallocationstatusfilter}""
                                          }}
                                        }}
                                      }},
                                ";

        if (primaryallocationloan_bucketfilter != ReportsEnum.NoFilterPresent.Value)
            dslquerywithFilters += $@"
                                    {{
                                    ""term"": {{
                                      ""loan_bucket"": {{
                                        ""value"": ""{primaryallocationloan_bucketfilter}""
                                      }}
                                    }}
                                  }},
                            ";

        if (primaryallocationentityfilter != ReportsEnum.NoFilterPresent.Value)
            dslquerywithFilters += $@"
                                  {{
                                    ""term"": {{
                                      ""entity"": {{
                                        ""value"": ""{primaryallocationentityfilter}""
                                      }}
                                    }}
                                  }},
                            ";

        

        if (!string.IsNullOrEmpty(primaryallocationagencyfilter))
        {
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""agency_applicationorg_id"": [{primaryallocationagencyfilter}]
                                        }}
                                      }},
                                ";
        }

        if (!string.IsNullOrEmpty(primaryallocationtelecallingagencyfilter))
        {
            dslquerywithFilters += $@"
                                      {{
                                        ""terms"": {{
                                          ""telecallingagency_applicationorg_id"": [{primaryallocationtelecallingagencyfilter}]
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
/// 
/// </summary>
public class GetPrimaryAllocationDetailsParams : PagedQueryParamsDtoBridge
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
    public List<string>? AllocationOwner { get; set; }
    public string? Loan_Bucket { get; set; }
    public string? Status { get; set; }
    public string? Entity { get; set; }

}
