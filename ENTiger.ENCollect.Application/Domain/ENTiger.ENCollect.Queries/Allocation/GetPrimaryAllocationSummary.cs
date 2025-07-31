
namespace ENTiger.ENCollect.AllocationModule;

/// <summary>
/// Gets primary allocation insights summary data
/// </summary>
public class GetPrimaryAllocationSummary : FlexiQueryEnumerableBridgeAsync<GetPrimaryAllocationSummaryDto>
{
    protected readonly ILogger<GetPrimaryAllocationSummary> _logger;
    protected GetPrimaryAllocationSummaryParams _params;
    protected readonly RepoFactory _repoFactory;
    protected FlexAppContextBridge? _flexAppContext;
    public IElasticSearchService _elasticSearchService { get; set; }
    private readonly ElasticSearchIndexSettings _elasticIndexConfig;
    long bucketInterval;

    List<GetPrimaryAllocationSummaryDto> primaryAllocationInsightOutput = new List<GetPrimaryAllocationSummaryDto>();

    /// <summary>
    /// Constructor of GetPrimaryAllocationSummary
    /// </summary>
    /// <param name="logger"></param>
    public GetPrimaryAllocationSummary(ILogger<GetPrimaryAllocationSummary> logger, RepoFactory repoFactory, IElasticSearchService elasticSearchService, IOptions<ElasticSearchIndexSettings> ElasticSearchIndexSettings)
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
    /// 
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    public virtual GetPrimaryAllocationSummary AssignParameters(GetPrimaryAllocationSummaryParams @params)
    {
        _params = @params;
        return this;
    }

    /// <summary>
    /// Get primary allocation insight summary data
    /// </summary>
    /// <returns></returns>
    public override async Task<IEnumerable<GetPrimaryAllocationSummaryDto>> Fetch()
    {
        _flexAppContext = _params.GetAppContext();
        Guard.AgainstNullAndEmpty("_flexAppContext?.TenantId", _flexAppContext?.TenantId);
        _repoFactory.Init(_params); 
    
        var config = _elasticIndexConfig.IndexNames.Where(a => a.TenantId == _flexAppContext?.TenantId).FirstOrDefault();
        bucketInterval = config?.BucketInterval ?? 10000;

        Guard.AgainstNull(nameof(config), config);
        
        string path = config.PrimaryAllocationInsightIndex;
        var afterKey = new GetPrimaryAllocationSummaryDto();
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;

        // Process primary allocation insight data
        await FetchDataRecursive(year, month, afterKey, path);

        return primaryAllocationInsightOutput;
    }


    /// <summary>
    /// Parse response of elastic search and construct afterkey object if present in response
    /// </summary>
    /// <param name="afterKey"></param>
    /// <param name="response"></param>
    /// <returns></returns> 
    private bool ParseResponse(GetPrimaryAllocationSummaryDto afterKey, string response)
    {
        using var jsonDoc = JsonDocument.Parse(response);
        var root = jsonDoc.RootElement;

        if (!root.TryGetProperty("aggregations", out var aggregations) ||
            !aggregations.TryGetProperty("primary_allocation_insight", out var primaryAllocationInsight))
        {
            _logger.LogWarning("No 'aggregations' or 'primary_allocation_insight' found in Elasticsearch response.");
            return false;
        }

        // Handle Pagination
        if (primaryAllocationInsight.TryGetProperty("after_key", out var afterKeyElement))
        {
            UpdateAfterKey(afterKey, afterKeyElement);
        }
        else
        {
            return false; // No more data to fetch
        }

        // Parse Buckets
        if (primaryAllocationInsight.TryGetProperty("buckets", out var buckets))
        {
            foreach (var bucket in buckets.EnumerateArray())
            {
                primaryAllocationInsightOutput.Add(ParseBucketData(bucket));
            }
        }

        return true;
    }

    /// <summary>
    /// Constructs dsl query of primary allocation insight summary
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="afterKey"></param>
    /// <returns></returns>
    private string BuildDSLQueryForPrimaryAllocationInsightSummary(int year, int month, GetPrimaryAllocationSummaryDto afterKey)
    {

        Guard.AgainstNegativeAndZero(nameof(year), year);
        Guard.AgainstNegativeAndZero(nameof(month), month);
        Guard.AgainstNull(nameof(afterKey), afterKey);

        string afterkeyjson = string.Empty;

        if (afterKey != null && afterKey.Account_Allocation_Status != null)
        {
            afterkeyjson = $@"
                               ,""after"": {{
                                ""primaryallocationstatus"": ""{afterKey.Account_Allocation_Status}"",
                                ""entity"": ""{afterKey.Entity}"",
                                ""allocationowner_applicationuser_id"":  ""{afterKey.Allocation_Owner_Id}"",
                                ""allocationowner_applicationuser_firstname"":  ""{afterKey.Allocation_Owner_Name}"",
                                ""loan_bucket"":  ""{afterKey.Loan_Bucket}"",
                                ""current_bucket_loanaccounts"": ""{afterKey.Current_Bucket}"",
                                ""region_loanaccounts"":  ""{afterKey.Region}"",
                                ""state_loanaccounts"": ""{afterKey.State}"",
                                ""city_loanaccounts"": ""{afterKey.City}"",
                                ""branch_loanaccounts"": ""{afterKey.Branch}"",
                                ""product_loanaccounts"":  ""{afterKey.Product}"",
                                ""productgroup_loanaccounts"": ""{afterKey.ProductGroup}"",
                                ""subproduct_loanaccounts"": ""{afterKey.SubProduct}"",
                                ""agency_applicationorg_id"":  ""{afterKey.Field_Agency_Id}"",
                                ""agency_applicationorg_firstname"":  ""{afterKey.Field_Agency_Name}"",
                                ""telecallingagency_applicationorg_firstname"":  ""{afterKey.Telecalling_Agency_Name}"",
                                ""telecallingagency_applicationorg_id"":  ""{afterKey.Telecalling_Agency_Id}"",
                                ""bucket_loanaccounts"":  ""{afterKey.Bom_Bucket}"",
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
                    {{ ""term"": {{ ""month_loanaccounts"": {{ ""value"": ""{month}"" }} }} }}
                  ]
                }}
              }},
              ""aggs"": {{
                   ""primary_allocation_insight"": {{          
                       ""composite"": 
                        {{                        
                               ""sources"": [
                               {{  ""primaryallocationstatus"": {{  ""terms"": {{  ""field"": ""primaryallocationstatus""  }} }} }} ,
                               {{  ""entity"": {{  ""terms"": {{  ""field"": ""entity"" }} }} }} ,
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
        return dslstring;

    }

    /// <summary>
    /// Construct after object for next request
    /// </summary>
    /// <param name="afterKey"></param>
    /// <param name="afterKeyData"></param>
    private void UpdateAfterKey(GetPrimaryAllocationSummaryDto afterKey, JsonElement afterKeyData)
    {

        afterKey.Account_Allocation_Status = afterKeyData.GetProperty("primaryallocationstatus").GetString();
        afterKey.Entity = afterKeyData.GetProperty("entity").GetString();
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
        afterKey.Bom_Bucket = afterKeyData.GetProperty("bucket_loanaccounts").GetString();
        afterKey.Field_Discriminator = afterKeyData.GetProperty("application_org_discriminator").GetString();

    }

    /// <summary>
    /// Method to parse response based on bucket
    /// </summary>
    /// <param name="bucket"></param>
    /// <returns></returns>
    private GetPrimaryAllocationSummaryDto ParseBucketData(JsonElement bucket)
    {

        return new GetPrimaryAllocationSummaryDto
        {          
            Account_Allocation_Status = bucket.GetProperty("key").GetProperty("primaryallocationstatus").GetString(),
            Entity = bucket.GetProperty("key").GetProperty("entity").GetString(),            
            Allocation_Owner_Id = bucket.GetProperty("key").GetProperty("allocationowner_applicationuser_id").GetString(),
            
            Loan_Bucket = bucket.GetProperty("key").GetProperty("loan_bucket").GetString(),
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

            Allocation_Owner_Name = bucket.GetProperty("key").GetProperty("allocationowner_applicationuser_firstname").ToString(),
            Bom_Bucket = bucket.GetProperty("key").GetProperty("bucket_loanaccounts").ToString(),
            Field_Discriminator = bucket.GetProperty("key").GetProperty("application_org_discriminator").ToString(),


            Total_Accounts = bucket.GetProperty("total_accounts").GetProperty("value").ToString(),
            Total_Prinicpal_Overdue = bucket.GetProperty("total_prinicpal_overdue").GetProperty("value").ToString(),
            Total_Overdue_Amount = bucket.GetProperty("total_outstanding_amt").GetProperty("value").ToString()


        };
    }

    /// <summary>
    /// Recursive method to fetch data 
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="afterKey"></param>
    /// <param name="path"></param>
    private async Task FetchDataRecursive(int year, int month, GetPrimaryAllocationSummaryDto afterKey, string path)
    {
        try
        {
            // Build DSL Query
            string dslQuery = BuildDSLQueryForPrimaryAllocationInsightSummary(year, month, afterKey);

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
/// 
/// </summary>
public class GetPrimaryAllocationSummaryParams : DtoBridge
{

}
