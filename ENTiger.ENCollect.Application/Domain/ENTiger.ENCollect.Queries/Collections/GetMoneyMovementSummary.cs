using System.Linq;

namespace ENTiger.ENCollect.CollectionsModule;

/// <summary>
/// Retrieves and summarizes money movement summary insight data from Elasticsearch using composite aggregations.
/// Supports recursive paging via after_key and parses aggregated buckets into output DTOs.
/// </summary>
public class GetMoneyMovementSummary : FlexiQueryEnumerableBridgeAsync<GetMoneyMovementSummaryDto>
{

    protected readonly ILogger<GetMoneyMovementSummary> _logger;
    protected GetMoneyMovementSummaryParams _params;
    protected readonly RepoFactory _repoFactory;
    protected FlexAppContextBridge? _flexAppContext;
    public IElasticSearchService _elasticSearchService { get; set; }
    private readonly ElasticSearchIndexSettings _elasticIndexConfig;
    // Output collection of aggregated money movement summaries
    List<GetMoneyMovementSummaryDto> moneyMovementSummaryOutput = new List<GetMoneyMovementSummaryDto>();
    long bucketInterval;



    /// <summary>
    /// Constructor to initialize dependencies.
    /// </summary>
    public GetMoneyMovementSummary(ILogger<GetMoneyMovementSummary> logger, RepoFactory repoFactory, IElasticSearchService elasticSearchService, IOptions<ElasticSearchIndexSettings> ElasticSearchIndexSettings)
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
    /// Assigns user-supplied parameters to the class instance.
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    public virtual GetMoneyMovementSummary AssignParameters(GetMoneyMovementSummaryParams @params)
    {
        _params = @params;
        return this;
    }

    /// <summary>
    /// Main entry point to fetch data. Determines tenant config, builds query, and triggers recursive data fetch.
    /// </summary>
    /// <returns></returns>
    public override async Task<IEnumerable<GetMoneyMovementSummaryDto>> Fetch()
    {
        _flexAppContext = _params.GetAppContext();
        Guard.AgainstNullAndEmpty("_flexAppContext?.TenantId", _flexAppContext?.TenantId);
        _repoFactory.Init(_params);

        var config = _elasticIndexConfig.IndexNames.Where(a => a.TenantId == _flexAppContext?.TenantId).FirstOrDefault();
        bucketInterval = config?.BucketInterval ?? 10000;
        Guard.AgainstNull(nameof(config), config);
        // From: Jan 1st of last year
        
        DateTime currentDate = DateTime.Now;
        DateTime fiveMonthsAgo = currentDate.AddMonths(-5);
        DateTime firstDayOfMonth = new DateTime(fiveMonthsAgo.Year, fiveMonthsAgo.Month, 1);


        // To: Today
        string toDate = DateTime.Now.ToString("yyyy-MM-dd");
        string fromDate = firstDayOfMonth.ToString("yyyy-MM-dd");

        string path = config.MoneyMovementInsightIndex;
        var afterKey = new GetMoneyMovementSummaryDto();

        
        // Process primary allocation insight data
        await FetchDataRecursive(fromDate,toDate, afterKey, path);

        return moneyMovementSummaryOutput;
    }
    /// <summary>
    /// Parses the Elasticsearch JSON response and updates the after_key if more data exists.
    /// Adds parsed bucket data into output list.
    /// </summary>
    /// <param name="afterKey"></param>
    /// <param name="response"></param>
    /// <returns></returns> 
    private bool ParseResponse(GetMoneyMovementSummaryDto afterKey, string response)
    {
        using var jsonDoc = JsonDocument.Parse(response);
        var root = jsonDoc.RootElement;

        if (!root.TryGetProperty("aggregations", out var aggregations) ||
            !aggregations.TryGetProperty("moneymovementinsight", out var moneyMovementInsight))
        {
            _logger.LogWarning("No 'aggregations' or 'moneymovementinsight' found in Elasticsearch response.");
            return false;
        }

        // Handle Pagination
        if (moneyMovementInsight.TryGetProperty("after_key", out var afterKeyElement))
        {
            UpdateAfterKey(afterKey, afterKeyElement);
        }
        else
        {
            return false; // No more data to fetch
        }

        // Parse bucket data into output list
        if (moneyMovementInsight.TryGetProperty("buckets", out var buckets))
        {
            foreach (var bucket in buckets.EnumerateArray())
            {
                GetMoneyMovementSummaryDto getMoneyMovementSummaryDto = new GetMoneyMovementSummaryDto();
                getMoneyMovementSummaryDto = ParseBucketData(bucket);

                AssignCategory(getMoneyMovementSummaryDto);

                moneyMovementSummaryOutput.Add(getMoneyMovementSummaryDto);
            }
        }

        return true;
    }

    private void AssignCategory(GetMoneyMovementSummaryDto getMoneyMovementSummaryDto)
    {
        if (_params.staffOrAgent == "AGENT")
        {
            if (AgencyStatuses.Contains(getMoneyMovementSummaryDto.paymentStatus))
            {
                getMoneyMovementSummaryDto.category = "Agency";

            }
            else if (BankStatuses.Contains(getMoneyMovementSummaryDto.paymentStatus))
            {
                getMoneyMovementSummaryDto.category = "Bank";

            }
            else
            {
                getMoneyMovementSummaryDto.category = "FOS";

            }
        }
        else
        {
            if (BankStatuses.Contains(getMoneyMovementSummaryDto.paymentStatus))
            {
                getMoneyMovementSummaryDto.category = "Bank";

            }
            else
            {
                getMoneyMovementSummaryDto.category = "Staff";
            }

        }
    }

    /// <summary>
    /// Builds the DSL query for money movement summary with optional after_key for pagination.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="afterKey"></param>
    /// <returns></returns>
    private string BuildDSLQueryForMoneyMovementInsightSummary(string fromDate,string toDate, GetMoneyMovementSummaryDto afterKey)
    {
    
        Guard.AgainstNull(nameof(afterKey), afterKey);

        string afterkeyjson = string.Empty;

        if (afterKey != null && afterKey.agencyName != null)
        {
            afterkeyjson = $@"
                               ,""after"": {{
                                ""applicationorg_agency_agencyname"": ""{afterKey.agencyName}"",
                                ""applicationuser_agent_agentname"": ""{afterKey.agentName}"",
                                ""applicationorg_agency_id"":""{afterKey.agencyId}"",
                                ""applicationuser_agent_id"":""{afterKey.agentId}"",
                                ""loanaccounts_productgroup"":  ""{afterKey.productGroup}"",
                                ""loanaccounts_product"":  ""{afterKey.product}"",
                                ""loanaccounts_subproduct"":  ""{afterKey.subProduct}"",
                                ""loanaccounts_current_bucket_currentbucket"": ""{afterKey.currentBucket}"",
                                ""loanaccounts_bucket_bombucket"": ""{afterKey.bomBucket}"",                                                             
                                ""loanaccounts_region_region"":  ""{afterKey.region}"",
                                ""loanaccounts_state_state"": ""{afterKey.state}"",
                                ""loanaccounts_city_city"": ""{afterKey.city}"",
                                ""loanaccounts_branch_branchname"": ""{afterKey.branchName}"",
                                ""collectionsmonth"": ""{afterKey.collectionDate}"",
                                ""depositmonth"":  ""{afterKey.depositDate}"",                                
                                ""collections_collectionmode_paymentmode"": ""{afterKey.paymentMode}"",
                                ""paymentstatus"":  ""{afterKey.paymentStatus}"",
                                ""hold_days"":  ""{afterKey.holdDays}"",
                                ""days_bucket"":  ""{afterKey.daysBucket}"",                 
                                ""loan_amount_bucket"":""{afterKey.loanAmountBucket}""
                              }}

                               ";

        }

        string dslstring = $@"
            {{
                 ""size"": 0,
                 ""query"": {{
                 ""bool"": {{
                 ""filter"": [
                            {{
                                ""range"": {{
                                ""collections_collectiondate_collectiondate"": {{
                                ""gte"": ""{fromDate}"",
                                ""lte"": ""{toDate}""
                                                                                 }}
                                            }}
                             }},
,
                            {{
                                ""term"": {{
                                  ""applicationuser_stafforagent"": ""{_params.staffOrAgent}""
                                }}
                            }}


                                ]
                            }}
                        }},
              ""aggs"": {{
                   ""moneymovementinsight"": {{          
                       ""composite"": 
                        {{                        
                               ""sources"": [
                                {{  
                                ""collections_collectionmode_paymentmode"": {{  ""terms"": {{ ""field"": ""collections_collectionmode_paymentmode"" }} }} 
                                }},                               
                                {{
                                    ""collectionsmonth"": {{
                                      ""terms"": {{
                                        ""field"": ""collections_month""
                                      }}
                                }}
                                }},
                                {{
                                    ""depositmonth"": {{
                                      ""terms"": {{
                                        ""field"": ""deposit_month""
                                      }}
                                }}
                                }},
                                {{ 
                                ""loanaccounts_productgroup"": {{  ""terms"": {{  ""field"": ""loanaccounts_productgroup"" }} }} 
                                }} ,

                               {{  
                                ""loanaccounts_product"": {{  ""terms"": {{  ""field"": ""loanaccounts_product"" }} }} 
                                }},

                               {{  
                                ""loanaccounts_subproduct"": {{  ""terms"": {{  ""field"": ""loanaccounts_subproduct"" }} }} 
                                }} ,
                               {{  ""loanaccounts_region_region"": {{   ""terms"": {{  ""field"": ""loanaccounts_region_region"" }} }} }},
                               {{  
                                ""loanaccounts_state_state"": {{    ""terms"": {{  ""field"": ""loanaccounts_state_state"" }} }} 
                               }},

                               {{ 
                                    ""loanaccounts_city_city"": {{  ""terms"": {{  ""field"": ""loanaccounts_city_city"" }} }} 
                               }},

                               {{  ""loanaccounts_branch_branchname"": {{ ""terms"": {{  ""field"": ""loanaccounts_branch_branchname"" }} }} 
                               }},
                               {{  
                                ""applicationorg_agency_agencyname"": {{  ""terms"": {{  ""field"": ""applicationorg_agency_agencyname""  }} }} 
                               }} ,

                               {{  
                                ""applicationuser_agent_agentname"": {{  ""terms"": {{  ""field"": ""applicationuser_agent_agentname"" }} }} 
                                }},
                                {{  
                                ""applicationorg_agency_id"": {{  ""terms"": {{  ""field"": ""applicationorg_agency_id""  }} }} 
                               }} ,

                               {{  
                                ""applicationuser_agent_id"": {{  ""terms"": {{  ""field"": ""applicationuser_agent_id"" }} }} 
                                }},
                                {{ 
                                ""loanaccounts_bucket_bombucket"": {{  ""terms"": {{  ""field"": ""loanaccounts_bucket_bombucket"" }} }} 
                                }},                           
                               {{  
                                ""loanaccounts_current_bucket_currentbucket"": {{  ""terms"": {{  ""field"": ""loanaccounts_current_bucket_currentbucket"" }} }} 
                                }} ,   
                                {{ 
                                ""loan_amount_bucket"": {{ ""terms"": {{ ""field"": ""loan_amount_bucket"" }} }}
                                }},
                               {{ 
                                ""paymentstatus"": {{ ""terms"": {{ ""field"": ""paymentstatus"" }} }}
                                }},
                                {{ 
                                ""hold_days"": {{ ""terms"": {{ ""field"": ""hold_days"" }} }}
                                }},
                                {{ 
                                ""days_bucket"": {{ ""terms"": {{ ""field"": ""daysbucket"" }} }}
                                }}                       
                                                         
                                
                                ],
                                    ""size"": 1000
                                        {afterkeyjson}   
                                        }},
                                        ""aggregations"": {{ 
                                            ""total_collected_amount"": {{   ""sum"": {{  ""field"": ""collections_amount_totalreceiptamount""  }} }},
                                           ""total_distinct_accounts"":          {{   ""cardinality"": {{  ""field"": ""loanaccounts_agreementid_agreementid"" }} }}
                                      }}
                                   }}                    
                              }}                            
                          }}
                        ";
        // Clean up any trailing commas using service method
        dslstring = _elasticSearchService.RemoveCommaFromDslQuery(dslstring);
       
        return dslstring;

    }

    /// <summary>
    /// Updates the pagination marker afterKey based on current response.
    /// </summary>
    /// <param name="afterKey"></param>
    /// <param name="afterKeyData"></param>
    private void UpdateAfterKey(GetMoneyMovementSummaryDto afterKey, JsonElement afterKeyData)
    {
        //Composite agreegation keys for pagination
        afterKey.agencyName = afterKeyData.GetProperty("applicationorg_agency_agencyname").GetString();
        afterKey.agentName = afterKeyData.GetProperty("applicationuser_agent_agentname").GetString();
        afterKey.agencyId = afterKeyData.GetProperty("applicationorg_agency_id").GetString();
        afterKey.agentId = afterKeyData.GetProperty("applicationuser_agent_id").GetString();
        afterKey.productGroup = afterKeyData.GetProperty("loanaccounts_productgroup").GetString();
        afterKey.product = afterKeyData.GetProperty("loanaccounts_product").GetString();
        afterKey.subProduct = afterKeyData.GetProperty("loanaccounts_subproduct").GetString();
        afterKey.currentBucket = afterKeyData.GetProperty("loanaccounts_current_bucket_currentbucket").GetInt32();
        afterKey.region = afterKeyData.GetProperty("loanaccounts_region_region").GetString();
        afterKey.state = afterKeyData.GetProperty("loanaccounts_state_state").GetString();
        afterKey.city = afterKeyData.GetProperty("loanaccounts_city_city").GetString();
        afterKey.branchName = afterKeyData.GetProperty("loanaccounts_branch_branchname").GetString();
        afterKey.bomBucket = afterKeyData.GetProperty("loanaccounts_bucket_bombucket").GetString();
        afterKey.collectionDate = afterKeyData.GetProperty("collectionsmonth").GetString();
        afterKey.depositDate = afterKeyData.GetProperty("depositmonth").GetString();
        afterKey.paymentMode = afterKeyData.GetProperty("collections_collectionmode_paymentmode").GetString();
        afterKey.paymentStatus = afterKeyData.GetProperty("paymentstatus").GetString();
        afterKey.holdDays = afterKeyData.GetProperty("hold_days").GetString();
        afterKey.daysBucket = afterKeyData.GetProperty("days_bucket").GetString();            
        //afterKey.staffOrAgent= afterKeyData.GetProperty("applicationuser_stafforagent").GetString(); 
        //afterKey.category= afterKeyData.GetProperty("category_terms").GetString();
        afterKey.loanAmountBucket= afterKeyData.GetProperty("loan_amount_bucket").GetString();
    }

    /// <summary>
    /// Parses a single bucket of aggregated data and maps to the output DTO.
    /// </summary>
    /// <param name="bucket"></param>
    /// <returns></returns>
    private GetMoneyMovementSummaryDto ParseBucketData(JsonElement bucket)
    {           

        return new GetMoneyMovementSummaryDto
        {
            //Composite Aggregated values
            agencyName = bucket.GetProperty("key").GetProperty("applicationorg_agency_agencyname").GetString(),
            agentName = bucket.GetProperty("key").GetProperty("applicationuser_agent_agentname").GetString(),
            agencyId = bucket.GetProperty("key").GetProperty("applicationorg_agency_id").GetString(),
            agentId = bucket.GetProperty("key").GetProperty("applicationuser_agent_id").GetString(),
            productGroup = bucket.GetProperty("key").GetProperty("loanaccounts_productgroup").GetString(),
            product = bucket.GetProperty("key").GetProperty("loanaccounts_product").GetString(),
            subProduct = bucket.GetProperty("key").GetProperty("loanaccounts_subproduct").GetString(),
            currentBucket = bucket.GetProperty("key").GetProperty("loanaccounts_current_bucket_currentbucket").GetInt32(),
            region = bucket.GetProperty("key").GetProperty("loanaccounts_region_region").GetString(),
            state = bucket.GetProperty("key").GetProperty("loanaccounts_state_state").GetString(),
            city = bucket.GetProperty("key").GetProperty("loanaccounts_city_city").GetString(),
            branchName = bucket.GetProperty("key").GetProperty("loanaccounts_branch_branchname").GetString(),
            bomBucket = bucket.GetProperty("key").GetProperty("loanaccounts_bucket_bombucket").GetString(),
            collectionDate = bucket.GetProperty("key").GetProperty("collectionsmonth").GetString(),
            depositDate = bucket.GetProperty("key").GetProperty("depositmonth").GetString(),
            paymentMode = bucket.GetProperty("key").GetProperty("collections_collectionmode_paymentmode").GetString(),
            paymentStatus = bucket.GetProperty("key").GetProperty("paymentstatus").GetString(),
            holdDays = bucket.GetProperty("key").GetProperty("hold_days").GetString(),
            daysBucket = bucket.GetProperty("key").GetProperty("days_bucket").GetString(),
           // staffOrAgent = bucket.GetProperty("key").GetProperty("applicationuser_stafforagent").GetString(),
           // category= bucket.GetProperty("key").GetProperty("category_terms").GetString(),
            loanAmountBucket = bucket.GetProperty("key").GetProperty("loan_amount_bucket").GetString(),

            //Aggregated Values
            countOfCollectedAccounts = bucket.GetProperty("total_distinct_accounts").GetProperty("value").GetInt32(),
            collectedAmount = bucket.GetProperty("total_collected_amount").GetProperty("value").GetDecimal()


        };
        


    }

    /// <summary>
    /// Recursively fetches data from Elasticsearch using composite after_key for pagination.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <param name="path"></param>
    private async Task FetchDataRecursive(string fromDate,string toDate,GetMoneyMovementSummaryDto afterKey, string path)
    {
        try
        {
            // Build DSL Query
            string dslQuery = BuildDSLQueryForMoneyMovementInsightSummary(fromDate,toDate, afterKey);

            // Send Request to Elasticsearch
            var response = await _elasticSearchService.SendPostRequestToElasticSearchAsync(path, dslQuery);

            Guard.AgainstNull(nameof(response), response);
            string elasticresponse = response.ToString() ?? string.Empty;

            Guard.AgainstNullAndEmpty(nameof(elasticresponse), elasticresponse);
            // Parse response
            bool hasMoreData = ParseResponse(afterKey, elasticresponse);

            if (hasMoreData)
            {
                // Recursive call with updated afterKey
                await FetchDataRecursive(fromDate,toDate, afterKey, path);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching data from Elasticsearch.");
            throw;
        }
    }

    private  readonly List<string> AgencyStatuses = new List<string>()
    {
        "ADDEDCOLLECTIONINBATCH",
        "CANCELLATIONREJECTED",
        "CANCELLATIONREQUESTED",
        "COLLECTIONACKNOWLEDGED",
        "COLLECTIONBATCHCREATED",
        "COLLECTIONBATCHCREATEDFORPARTNER",
        "READYFORBATCH",
        "COLLECTIONINQUEUE"
    };

    private readonly List<string> BankStatuses = new List<string>()
    {
        "COLLECTIONSUCCESS",
        "PAYINSLIPCREATED",
        "UPDATEDINCBS",
        "BOUNCEDINCBS",
        "COLLECTIONINITIATED",
        "CANCELLED",
        "PAYINSLIPACKNOWLEDGED",
        "COLLECTIONBATCHACKNOWLEDGED",
        "DISSOLVED",
        "ADDEDCOLLECTIONBATCHINPAYINSLIP"
    };



}

/// <summary>
/// Parameter placeholder for money movement summary query.
/// Currently inherits common context from DtoBridge.
/// </summary>
public class GetMoneyMovementSummaryParams : DtoBridge
{
    public string? staffOrAgent { get; set; }
}
