namespace ENTiger.ENCollect.AllocationModule;

/// <summary>
/// Retrieves a summary of secondary allocations summary Insights from Elasticsearch.
/// This class builds and sends composite aggregation queries to Elasticsearch,
/// parses paginated results using after_key, and returns mapped DTOs.
/// </summary>>
public class GetSecondaryAllocationSummary : FlexiQueryEnumerableBridgeAsync<GetSecondaryAllocationSummaryDto>
{
    protected readonly ILogger<GetSecondaryAllocationSummary> _logger;
    protected GetSecondaryAllocationSummaryParams _params;
    protected readonly RepoFactory _repoFactory;
    protected FlexAppContextBridge? _flexAppContext;
    public IElasticSearchService _elasticSearchService { get; set; }
    private readonly ElasticSearchIndexSettings _elasticIndexConfig;

    List<GetSecondaryAllocationSummaryDto> secondaryAllocationInsightOutput = new List<GetSecondaryAllocationSummaryDto>();
    long bucketInterval;

    /// <summary>
    /// Constructor for GetSecondaryAllocationSummary.
    /// Initializes dependencies via DI.
    /// </summary>
    public GetSecondaryAllocationSummary(ILogger<GetSecondaryAllocationSummary> logger, RepoFactory repoFactory, IElasticSearchService elasticSearchService, IOptions<ElasticSearchIndexSettings> ElasticSearchIndexSettings)
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
    /// Assigns query parameters and returns instance for fluent usage.
    /// </summary>
    public virtual GetSecondaryAllocationSummary AssignParameters(GetSecondaryAllocationSummaryParams @params)
    {
        _params = @params;
        return this;
    }

    /// <summary>
    /// Fetches and returns the secondary allocation insights summary by executing recursive queries to Elasticsearch.
    /// </summary>
    public override async Task<IEnumerable<GetSecondaryAllocationSummaryDto>> Fetch()
    {
        _flexAppContext = _params.GetAppContext();
        Guard.AgainstNullAndEmpty("_flexAppContext?.TenantId", _flexAppContext?.TenantId);
        _repoFactory.Init(_params);

        var config = _elasticIndexConfig.IndexNames.Where(a => a.TenantId == _flexAppContext?.TenantId).FirstOrDefault();
        bucketInterval = config?.BucketInterval ?? 10000;
        Guard.AgainstNull(nameof(config), config);

        string path = config.SecondaryAllocationInsightIndex;
        var afterKey = new GetSecondaryAllocationSummaryDto();
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;

        // Process secondary allocation start recursive fetching of data
        await FetchDataRecursive(year, month, afterKey, path);

        return secondaryAllocationInsightOutput;
    }

    /// <summary>
    /// Parse response of elastic search and construct afterkey object if present in response
    /// </summary>
    /// <param name="afterKey"></param>
    /// <param name="response"></param>
    /// <returns></returns> 
    private bool ParseResponse(GetSecondaryAllocationSummaryDto afterKey, string response)
    {
        using var jsonDoc = JsonDocument.Parse(response);
        var root = jsonDoc.RootElement;

        if (!root.TryGetProperty("aggregations", out var aggregations) ||
            !aggregations.TryGetProperty("secondary_allocation_insight", out var secondaryAllocationInsight))
        {
            _logger.LogWarning("No 'aggregations' or 'secondary_allocation_insight' found in Elasticsearch response.");
            return false;
        }

        // Update afterKey if exists for pagination
        if (secondaryAllocationInsight.TryGetProperty("after_key", out var afterKeyElement))
        {
            UpdateAfterKey(afterKey, afterKeyElement);
        }
        else
        {
            return false; // No more data to fetch
        }

        // Parse and map each bucket result
        if (secondaryAllocationInsight.TryGetProperty("buckets", out var buckets))
        {
            foreach (var bucket in buckets.EnumerateArray())
            {
                secondaryAllocationInsightOutput.Add(ParseBucketData(bucket));
            }
        }

        return true;
    }

    /// <summary>
    /// Builds a DSL query string for composite aggregation to fetch secondary allocation summary
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="afterKey"></param>
    /// <returns></returns>
    private string BuildDSLQueryForSecondaryAllocationInsightSummary(int year, int month, GetSecondaryAllocationSummaryDto afterKey)
    {
        Guard.AgainstNegativeAndZero(nameof(year), year);
        Guard.AgainstNegativeAndZero(nameof(month), month);
        Guard.AgainstNull(nameof(afterKey), afterKey);
        // Constructing after_key clause for paginated queries
        string afterkeyjson = string.Empty;

        if (afterKey != null && afterKey.Account_Allocation_Status != null)
        {
            afterkeyjson = $@"
                               ,""after"": {{
                                ""secondaryallocationstatus"": ""{afterKey.Account_Allocation_Status}"",
                                ""secondaryentity"": ""{afterKey.Entity}"",
                                ""allocationowner_applicationuser_id"":  ""{afterKey.Allocation_Owner_Id}"",
                                ""allocationowner_applicationuser_firstname"":  ""{afterKey.Allocation_Owner_Name}"",
                                ""loan_bucket"":  ""{afterKey.Loan_Bucket}"",
                                ""current_bucket_loanaccounts"": ""{afterKey.Current_Bucket}"",
                                ""bucket_loanaccounts"": ""{afterKey.Bom_Bucket}"",                                                             
                                ""region_loanaccounts"":  ""{afterKey.Region}"",
                                ""state_loanaccounts"": ""{afterKey.State}"",
                                ""city_loanaccounts"": ""{afterKey.City}"",
                                ""branch_loanaccounts"": ""{afterKey.Branch}"",
                                ""productgroup_loanaccounts"": ""{afterKey.ProductGroup}"",
                                ""product_loanaccounts"":  ""{afterKey.Product}"",                                
                                ""subproduct_loanaccounts"": ""{afterKey.SubProduct}"",
                                ""agency_applicationorg_id"":  ""{afterKey.Field_Agency_Id}"",
                                ""agency_applicationorg_firstname"":  ""{afterKey.Field_Agency_Name}"",
                                ""telecallingagency_applicationorg_firstname"":  ""{afterKey.Telecalling_Agency_Name}"",
                                ""telecallingagency_applicationorg_id"":  ""{afterKey.Telecalling_Agency_Id}"",
                                ""collector_applicationuser_id"":""{afterKey.Collector_Id}"",
                                ""collector_applicationuser_firstname"":""{afterKey.Collector_Name}"",
                                ""tellecaller_applicationuser_id"":""{afterKey.Telecaller_Id}"",
                                ""tellecaller_applicationuser_firstname"":""{afterKey.Telecaller_Name}"",
                                ""application_org_discriminator"":  ""{afterKey.Field_Discriminator}""

                              }}

                               ";

        }

        string dslstring = $@"
            {{
              ""size"":0,
              ""query"": {{
                ""bool"": {{
                  ""filter"": [
                    {{ ""term"": {{ ""year_loanaccounts"": {{ ""value"": ""{year}"" }} }} }},
                    {{ ""term"": {{ ""month_loanaccounts"": {{ ""value"": ""{month}"" }} }} }},
                    {{ ""term"": {{ ""primaryallocationstatus"": {{ ""value"": ""ACCOUNT ALLOCATED"" }} }} }}

                  ]
                }}
              }},
              ""aggs"": {{
                   ""secondary_allocation_insight"": {{          
                       ""composite"": 
                        {{                        
                               ""sources"": [
                               {{  ""secondaryallocationstatus"": {{  ""terms"": {{  ""field"": ""secondaryallocationstatus""  }} }} }} ,
                               {{  ""secondaryentity"": {{  ""terms"": {{  ""field"": ""secondaryentity"" }} }} }} ,
                               {{  ""allocationowner_applicationuser_id"": {{  ""terms"": {{  ""field"": ""allocationowner_applicationuser_id"" }} }} }} ,
                               {{  ""allocationowner_applicationuser_firstname"": {{  ""terms"": {{  ""field"": ""allocationowner_applicationuser_firstname"" }} }} }},
                               {{  ""loan_bucket"": {{  ""terms"": {{  ""field"": ""loan_bucket"" }} }} }} ,
                               {{  ""current_bucket_loanaccounts"": {{  ""terms"": {{  ""field"": ""current_bucket_loanaccounts"" }} }} }} ,                            
                               {{  ""region_loanaccounts"": {{  ""terms"": {{  ""field"": ""region_loanaccounts"" }} }} }},
                               {{  ""state_loanaccounts"": {{   ""terms"": {{  ""field"": ""state_loanaccounts"" }} }} }},
                               {{  ""city_loanaccounts"": {{    ""terms"": {{  ""field"": ""city_loanaccounts"" }} }} }},
                               {{  ""branch_loanaccounts"": {{  ""terms"": {{  ""field"": ""branch_loanaccounts"" }} }} }},
                               {{  ""product_loanaccounts"": {{ ""terms"": {{  ""field"": ""product_loanaccounts"" }} }} }},
                               {{  ""productgroup_loanaccounts"": {{ ""terms"": {{ ""field"": ""productgroup_loanaccounts"" }} }} }},
                               {{  ""subproduct_loanaccounts"": {{   ""terms"": {{ ""field"": ""subproduct_loanaccounts"" }} }} }},
                               {{  ""agency_applicationorg_id"": {{  ""terms"": {{ ""field"": ""agency_applicationorg_id"" }} }} }},
                               {{  ""agency_applicationorg_firstname"": {{ ""terms"": {{ ""field"": ""agency_applicationorg_firstname"" }} }} }},
                               {{  ""telecallingagency_applicationorg_firstname"": {{  ""terms"": {{ ""field"": ""telecallingagency_applicationorg_firstname"" }} }} }},
                               {{  ""telecallingagency_applicationorg_id"": {{ ""terms"": {{ ""field"": ""telecallingagency_applicationorg_id"" }} }} }},
                               {{  ""tellecaller_applicationuser_id"": {{ ""terms"": {{ ""field"": ""tellecaller_applicationuser_id"" }} }} }},
                               {{  ""tellecaller_applicationuser_firstname"": {{ ""terms"": {{ ""field"": ""tellecaller_applicationuser_firstname"" }} }} }},
                               {{  ""collector_applicationuser_id"": {{ ""terms"": {{ ""field"": ""collector_applicationuser_id"" }} }} }},
                               {{  ""collector_applicationuser_firstname"": {{ ""terms"": {{ ""field"": ""collector_applicationuser_firstname"" }} }} }},
                               {{  ""bucket_loanaccounts"": {{ ""terms"": {{ ""field"": ""bucket_loanaccounts"" }} }} }},
                               {{  ""application_org_discriminator"": {{ ""terms"": {{ ""field"": ""application_org_discriminator"" }} }} }}

                               ],
                    ""size"": 1000
                     {afterkeyjson}   
                    }},
                    ""aggregations"": {{ 
                                            ""total_prinicpal_overdue"": {{   ""sum"": {{  ""field"": ""principal_od""  }} }},
                                            ""total_outstanding_amt"":   {{   ""sum"": {{  ""field"": ""total_overdue_amt"" }} }} ,
                                            ""total_accounts"":          {{   ""value_count"": {{  ""field"": ""id_loanaccounts"" }} }}
                                      }}
                                   }}                    
                              }}                            
                          }}
                        ";
        // Full DSL query
        return dslstring;

    }


    /// <summary>
    /// Populates the afterKey object from Elasticsearch after_key JSON.
    /// Used to continue pagination.
    /// </summary>
    /// <param name="afterKey"></param>
    /// <param name="afterKeyData"></param>
    private void UpdateAfterKey(GetSecondaryAllocationSummaryDto afterKey, JsonElement afterKeyData)
    {

        afterKey.Account_Allocation_Status = afterKeyData.GetProperty("secondaryallocationstatus").GetString();
        afterKey.Entity = afterKeyData.GetProperty("secondaryentity").GetString();
        afterKey.Allocation_Owner_Id = afterKeyData.GetProperty("allocationowner_applicationuser_id").GetString();
        afterKey.Allocation_Owner_Name = afterKeyData.GetProperty("allocationowner_applicationuser_firstname").GetString();
        afterKey.Loan_Bucket = afterKeyData.GetProperty("loan_bucket").GetString();
        afterKey.Current_Bucket = afterKeyData.GetProperty("current_bucket_loanaccounts").GetString();            
        afterKey.Region = afterKeyData.GetProperty("region_loanaccounts").GetString();
        afterKey.State = afterKeyData.GetProperty("state_loanaccounts").GetString();
        afterKey.City = afterKeyData.GetProperty("city_loanaccounts").GetString();
        afterKey.Branch = afterKeyData.GetProperty("branch_loanaccounts").GetString();
        afterKey.Product = afterKeyData.GetProperty("product_loanaccounts").GetString();
        afterKey.ProductGroup = afterKeyData.GetProperty("productgroup_loanaccounts").GetString();
        afterKey.SubProduct = afterKeyData.GetProperty("subproduct_loanaccounts").GetString();
        afterKey.Field_Agency_Id = afterKeyData.GetProperty("agency_applicationorg_id").GetString();
        afterKey.Field_Agency_Name = afterKeyData.GetProperty("agency_applicationorg_firstname").GetString();
        afterKey.Telecalling_Agency_Name = afterKeyData.GetProperty("telecallingagency_applicationorg_firstname").GetString();
        afterKey.Telecalling_Agency_Id = afterKeyData.GetProperty("telecallingagency_applicationorg_id").GetString();
        afterKey.Telecaller_Id= afterKeyData.GetProperty("tellecaller_applicationuser_id").GetString();
        afterKey.Telecaller_Name = afterKeyData.GetProperty("tellecaller_applicationuser_firstname").GetString();
        afterKey.Collector_Id = afterKeyData.GetProperty("collector_applicationuser_id").GetString();
        afterKey.Collector_Name = afterKeyData.GetProperty("collector_applicationuser_firstname").GetString();            
        afterKey.Bom_Bucket = afterKeyData.GetProperty("bucket_loanaccounts").GetString();
        afterKey.Field_Discriminator = afterKeyData.GetProperty("application_org_discriminator").GetString();

    }

    /// <summary>
    /// Parses a single bucket element from Elasticsearch response and maps to DTO.
    /// </summary>
    /// <param name="bucket"></param>
    /// <returns></returns>
    private GetSecondaryAllocationSummaryDto ParseBucketData(JsonElement bucket)
    {

        return new GetSecondaryAllocationSummaryDto
        {
            Account_Allocation_Status = bucket.GetProperty("key").GetProperty("secondaryallocationstatus").GetString(),
            Entity = bucket.GetProperty("key").GetProperty("secondaryentity").GetString(),
            Allocation_Owner_Id = bucket.GetProperty("key").GetProperty("allocationowner_applicationuser_id").GetString(),

            Loan_Bucket = bucket.GetProperty("key").GetProperty("loan_bucket").ToString(),
            Current_Bucket = bucket.GetProperty("key").GetProperty("current_bucket_loanaccounts").ToString(),               
            Region = bucket.GetProperty("key").GetProperty("region_loanaccounts").ToString(),
            State = bucket.GetProperty("key").GetProperty("state_loanaccounts").ToString(),
            City = bucket.GetProperty("key").GetProperty("city_loanaccounts").ToString(),
            Branch = bucket.GetProperty("key").GetProperty("branch_loanaccounts").ToString(),
            Product = bucket.GetProperty("key").GetProperty("product_loanaccounts").ToString(),
            ProductGroup = bucket.GetProperty("key").GetProperty("productgroup_loanaccounts").ToString(),
            SubProduct = bucket.GetProperty("key").GetProperty("subproduct_loanaccounts").ToString(),
            Field_Agency_Id = bucket.GetProperty("key").GetProperty("agency_applicationorg_id").ToString(),
            Telecalling_Agency_Id = bucket.GetProperty("key").GetProperty("telecallingagency_applicationorg_id").ToString(),
            Telecalling_Agency_Name = bucket.GetProperty("key").GetProperty("telecallingagency_applicationorg_firstname").ToString(),
            Field_Agency_Name = bucket.GetProperty("key").GetProperty("agency_applicationorg_firstname").ToString(),
            Telecaller_Id = bucket.GetProperty("key").GetProperty("tellecaller_applicationuser_id").GetString(),
            Telecaller_Name = bucket.GetProperty("key").GetProperty("tellecaller_applicationuser_firstname").GetString(),
            Collector_Id = bucket.GetProperty("key").GetProperty("collector_applicationuser_id").GetString(),
            Collector_Name = bucket.GetProperty("key").GetProperty("collector_applicationuser_firstname").GetString(),
            Allocation_Owner_Name = bucket.GetProperty("key").GetProperty("allocationowner_applicationuser_firstname").ToString(),
            Bom_Bucket = bucket.GetProperty("key").GetProperty("bucket_loanaccounts").ToString(),
            Field_Discriminator = bucket.GetProperty("key").GetProperty("application_org_discriminator").ToString(),


            Total_Accounts = bucket.GetProperty("total_accounts").GetProperty("value").ToString(),
            Total_Prinicpal_Overdue = bucket.GetProperty("total_prinicpal_overdue").GetProperty("value").ToString(),
            Total_Overdue_Amount = bucket.GetProperty("total_outstanding_amt").GetProperty("value").ToString()


        };
    }

    /// <summary>
    /// Recursively fetches paginated data from Elasticsearch using after_key
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="afterKey"></param>
    /// <param name="path"></param>
    private async Task FetchDataRecursive(int year, int month, GetSecondaryAllocationSummaryDto afterKey, string path)
    {
        try
        {
            // Build DSL Query
            string dslQuery = BuildDSLQueryForSecondaryAllocationInsightSummary(year, month, afterKey);

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
                await FetchDataRecursive(year, month, afterKey, path);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching data from Elasticsearch.");
            throw;
        }
    }
}

/// <summary>
/// Parameter DTO for GetSecondaryAllocationSummary query
/// </summary>
public class GetSecondaryAllocationSummaryParams : DtoBridge
{

}
