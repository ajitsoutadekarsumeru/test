namespace ENTiger.ENCollect.AllocationModule;

/// <summary>
/// 
/// </summary>
public class GetAllocationAchievedSummary : FlexiQueryEnumerableBridgeAsync<GetAllocationAchievedSummaryDto>
{
    
    protected readonly ILogger<GetAllocationAchievedSummary> _logger;
    protected GetAllocationAchievedSummaryParams _params;
    protected readonly RepoFactory _repoFactory;
    protected FlexAppContextBridge? _flexAppContext;
    public IElasticSearchService _elasticSearchService { get; set; }
    private readonly ElasticSearchIndexSettings _elasticIndexConfig;

    List<GetAllocationAchievedSummaryDto> allocationAchievedInsightOutput = new List<GetAllocationAchievedSummaryDto>();
    long bucketInterval;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public GetAllocationAchievedSummary(ILogger<GetAllocationAchievedSummary> logger, RepoFactory repoFactory, IElasticSearchService elasticSearchService, IOptions<ElasticSearchIndexSettings> ElasticSearchIndexSettings)
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
    public virtual GetAllocationAchievedSummary AssignParameters(GetAllocationAchievedSummaryParams @params)
    {
        _params = @params;
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override async Task<IEnumerable<GetAllocationAchievedSummaryDto>> Fetch()
    {
        _flexAppContext = _params.GetAppContext();
        Guard.AgainstNullAndEmpty("_flexAppContext?.TenantId", _flexAppContext?.TenantId);
        _repoFactory.Init(_params);

        var config = _elasticIndexConfig.IndexNames.Where(a => a.TenantId == _flexAppContext?.TenantId).FirstOrDefault();
        bucketInterval = config?.BucketInterval ?? 10000;
        Guard.AgainstNull(nameof(config), config);

        string path = config.Allocation_AchievedInsightIndex;
        var afterKey = new GetAllocationAchievedSummaryDto();
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;

        // Process primary allocation insight data
        await FetchDataRecursive(year, month, afterKey, path);

        return allocationAchievedInsightOutput;

    }


    /// <summary>
    /// Recursive method to fetch data 
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="afterKey"></param>
    /// <param name="path"></param>
    private async Task FetchDataRecursive(int year, int month, GetAllocationAchievedSummaryDto afterKey, string path)
    {
        try
        {
            // Build DSL Query
            string dslQuery = BuildDSLQueryForAllocation_AchievedInsightSummary(year, month, afterKey);

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


    /// <summary>
    /// Constructs dsl query of primary allocation insight summary
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="afterKey"></param>
    /// <returns></returns>
    private string BuildDSLQueryForAllocation_AchievedInsightSummary(int year, int month, GetAllocationAchievedSummaryDto afterKey)
    {

        Guard.AgainstNegativeAndZero(nameof(year), year);
        Guard.AgainstNegativeAndZero(nameof(month), month);
        Guard.AgainstNull(nameof(afterKey), afterKey);

        string afterkeyjson = string.Empty;

        if (afterKey != null && afterKey.CollectionStatus != null)
        {
            afterkeyjson = $@"
                               ,""after"": {{
                                ""collection_status"": ""{afterKey.CollectionStatus}"",
                                ""loan_bucket"": ""{afterKey.Loan_Bucket}"",
                                ""productgroup_loanaccounts"":  ""{afterKey.ProductGroup}"",
                                ""product_loanaccounts"":  ""{afterKey.Product}"",
                                ""subproduct_loanaccounts"":  ""{afterKey.Subproduct}"",
                                ""current_bucket_loanaccounts"": ""{afterKey.Current_Bucket}"",
                                ""bucket_loanaccounts"":  ""{afterKey.Bucket}"",
                                ""region_loanaccounts"": ""{afterKey.Region}"",
                                ""state_loanaccounts"": ""{afterKey.State}"",
                                ""city_loanaccounts"": ""{afterKey.City}"",
                                ""branch_loanaccounts"":  ""{afterKey.Branch}"",
                                ""agency_applicationorg_id"": ""{afterKey.FieldAgencyId}"",
                                ""agency_applicationorg_firstname"": ""{afterKey.FieldAgencyName}"",
                                ""telecallingagency_applicationorg_firstname"":  ""{afterKey.TelecallingAgencyName}"",
                                ""telecallingagency_applicationorg_id"":  ""{afterKey.TelecallingAgencyId}"",
                                ""collector_applicationuser_firstname"":  ""{afterKey.FieldAgentName}"",
                                ""collector_applicationuser_id"":  ""{afterKey.FieldAgentId}"",
                                ""tellecaller_applicationuser_firstname"":  ""{afterKey.TelecallerName}"",
                                ""tellecaller_applicationuser_id"":  ""{afterKey.TelecallerId}"",
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
                    {{ ""term"": {{ ""year_loanaccounts"": {{ ""value"": {year} }} }} }},
                    {{ ""term"": {{ ""month_loanaccounts"": {{ ""value"": {month} }} }} }},
					{{ ""term"": {{ ""primaryallocationstatus"": {{ ""value"": ""ACCOUNT ALLOCATED"" }} }} }}
					
                  ]
                }}
              }},
              ""aggs"": {{
                   ""allocation_achieved_insight"": {{          
                       ""composite"": 
                        {{                        
                               ""sources"": [
                               {{  ""collection_status"": {{  ""terms"": {{  ""field"": ""collection_status""  }} }} }} ,
                               {{  ""loan_bucket"": {{  ""terms"": {{  ""field"": ""loan_bucket"" }} }} }} ,
							   {{  ""productgroup_loanaccounts"": {{ ""terms"": {{ ""field"": ""productgroup_loanaccounts"" }} }} }},                       
                               {{  ""product_loanaccounts"": {{ ""terms"": {{  ""field"": ""product_loanaccounts"" }} }} }},
							   {{  ""subproduct_loanaccounts"": {{   ""terms"": {{ ""field"": ""subproduct_loanaccounts"" }} }} }},
							   {{  ""current_bucket_loanaccounts"": {{  ""terms"": {{  ""field"": ""current_bucket_loanaccounts"" }} }} }} ,
							   {{  ""bucket_loanaccounts"": {{ ""terms"": {{ ""field"": ""bucket_loanaccounts"" }} }} }},
                               {{  ""region_loanaccounts"": {{  ""terms"": {{  ""field"": ""region_loanaccounts"" }} }} }},
                               {{  ""state_loanaccounts"": {{   ""terms"": {{  ""field"": ""state_loanaccounts"" }} }} }},
                               {{  ""city_loanaccounts"": {{    ""terms"": {{  ""field"": ""city_loanaccounts"" }} }} }},
                               {{  ""branch_loanaccounts"": {{  ""terms"": {{  ""field"": ""branch_loanaccounts"" }} }} }},
							   {{  ""agency_applicationorg_id"": {{  ""terms"": {{ ""field"": ""agency_applicationorg_id"" }} }} }},
                               {{  ""agency_applicationorg_firstname"": {{ ""terms"": {{ ""field"": ""agency_applicationorg_firstname"" }} }} }},
                               {{  ""telecallingagency_applicationorg_firstname"": {{  ""terms"": {{ ""field"": ""telecallingagency_applicationorg_firstname"" }} }} }},
                               {{  ""telecallingagency_applicationorg_id"": {{ ""terms"": {{ ""field"": ""telecallingagency_applicationorg_id"" }} }} }},
                               {{  ""collector_applicationuser_firstname"": {{ ""terms"": {{ ""field"": ""collector_applicationuser_firstname"" }} }} }},
                               {{  ""collector_applicationuser_id"": {{ ""terms"": {{ ""field"": ""collector_applicationuser_id"" }} }} }},
                               {{  ""tellecaller_applicationuser_firstname"": {{ ""terms"": {{ ""field"": ""tellecaller_applicationuser_firstname"" }} }} }},
                               {{  ""tellecaller_applicationuser_id"": {{ ""terms"": {{ ""field"": ""tellecaller_applicationuser_id"" }} }} }},
                               {{  ""application_org_discriminator"": {{ ""terms"": {{ ""field"": ""application_org_discriminator"" }} }} }}
                               
							   
                               ],
                    ""size"": 1000
                    {afterkeyjson}
                        
                    }},
                    ""aggregations"": {{ 
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
    private void UpdateAfterKey(GetAllocationAchievedSummaryDto afterKey, JsonElement afterKeyData)
    {

        afterKey.CollectionStatus = afterKeyData.GetProperty("collection_status").GetString();
        afterKey.Loan_Bucket = afterKeyData.GetProperty("loan_bucket").GetString();
        afterKey.ProductGroup = afterKeyData.GetProperty("productgroup_loanaccounts").GetString();
        afterKey.Product = afterKeyData.GetProperty("product_loanaccounts").GetString();
        afterKey.Subproduct = afterKeyData.GetProperty("subproduct_loanaccounts").GetString();
        afterKey.Current_Bucket = afterKeyData.GetProperty("current_bucket_loanaccounts").GetString();
        afterKey.Bucket = afterKeyData.GetProperty("bucket_loanaccounts").GetString();
        afterKey.Region = afterKeyData.GetProperty("region_loanaccounts").GetString();
        afterKey.State = afterKeyData.GetProperty("state_loanaccounts").GetString();
        afterKey.City = afterKeyData.GetProperty("city_loanaccounts").GetString();
        afterKey.Branch = afterKeyData.GetProperty("branch_loanaccounts").GetString();
        afterKey.FieldAgencyId = afterKeyData.GetProperty("agency_applicationorg_id").GetString();
        afterKey.FieldAgencyName = afterKeyData.GetProperty("agency_applicationorg_firstname").GetString();
        afterKey.TelecallingAgencyId = afterKeyData.GetProperty("telecallingagency_applicationorg_id").GetString();
        afterKey.TelecallingAgencyName = afterKeyData.GetProperty("telecallingagency_applicationorg_firstname").GetString();
        afterKey.FieldAgentId = afterKeyData.GetProperty("collector_applicationuser_id").GetString();
        afterKey.FieldAgentName = afterKeyData.GetProperty("collector_applicationuser_firstname").GetString();
        afterKey.TelecallerId = afterKeyData.GetProperty("tellecaller_applicationuser_id").GetString();
        afterKey.TelecallerName = afterKeyData.GetProperty("tellecaller_applicationuser_firstname").GetString();
        afterKey.Field_Discriminator = afterKeyData.GetProperty("application_org_discriminator").GetString();


    }

    /// <summary>
    /// Method to parse response based on bucket
    /// </summary>
    /// <param name="bucket"></param>
    /// <returns></returns>
    private GetAllocationAchievedSummaryDto ParseBucketData(JsonElement bucket)
    {

        return new GetAllocationAchievedSummaryDto
        {
            CollectionStatus = bucket.GetProperty("key").GetProperty("collection_status").GetString(),
            Loan_Bucket = bucket.GetProperty("key").GetProperty("loan_bucket").GetString(),
            ProductGroup = bucket.GetProperty("key").GetProperty("productgroup_loanaccounts").GetString(),

            Product = bucket.GetProperty("key").GetProperty("product_loanaccounts").ToString(),
            Subproduct = bucket.GetProperty("key").GetProperty("subproduct_loanaccounts").ToString(),
            Current_Bucket = bucket.GetProperty("key").GetProperty("current_bucket_loanaccounts").ToString(),
            Bucket = bucket.GetProperty("key").GetProperty("bucket_loanaccounts").ToString(),
            Region = bucket.GetProperty("key").GetProperty("region_loanaccounts").ToString(),
            State = bucket.GetProperty("key").GetProperty("state_loanaccounts").ToString(),
            City = bucket.GetProperty("key").GetProperty("city_loanaccounts").ToString(),
            Branch = bucket.GetProperty("key").GetProperty("branch_loanaccounts").ToString(),
            FieldAgencyId = bucket.GetProperty("key").GetProperty("agency_applicationorg_id").ToString(),
            FieldAgencyName = bucket.GetProperty("key").GetProperty("agency_applicationorg_firstname").ToString(),
            TelecallingAgencyId = bucket.GetProperty("key").GetProperty("telecallingagency_applicationorg_id").ToString(),
            TelecallingAgencyName = bucket.GetProperty("key").GetProperty("telecallingagency_applicationorg_firstname").ToString(),
            FieldAgentName = bucket.GetProperty("key").GetProperty("collector_applicationuser_firstname").ToString(),
            FieldAgentId = bucket.GetProperty("key").GetProperty("collector_applicationuser_id").ToString(),
            TelecallerName = bucket.GetProperty("key").GetProperty("tellecaller_applicationuser_firstname").ToString(),
            TelecallerId = bucket.GetProperty("key").GetProperty("tellecaller_applicationuser_id").ToString(),

            Field_Discriminator = bucket.GetProperty("key").GetProperty("application_org_discriminator").ToString(),


            Total_Accounts = bucket.GetProperty("total_accounts").GetProperty("value").ToString(),
            Total_Overdue_Amount = bucket.GetProperty("total_outstanding_amt").GetProperty("value").ToString()


        };
    }


    /// <summary>
    /// Parse response of elastic search and construct afterkey object if present in response
    /// </summary>
    /// <param name="afterKey"></param>
    /// <param name="response"></param>
    /// <returns></returns> 
    private bool ParseResponse(GetAllocationAchievedSummaryDto afterKey, string response)
    {
        using var jsonDoc = JsonDocument.Parse(response);
        var root = jsonDoc.RootElement;

        if (!root.TryGetProperty("aggregations", out var aggregations) ||
            !aggregations.TryGetProperty("allocation_achieved_insight", out var allocation_achievedInsight))
        {
            _logger.LogWarning("No 'aggregations' or 'allocation_achieved_insight' found in Elasticsearch response.");
            return false;
        }

        // Handle Pagination
        if (allocation_achievedInsight.TryGetProperty("after_key", out var afterKeyElement))
        {
            UpdateAfterKey(afterKey, afterKeyElement);
        }
        else
        {
            return false; // No more data to fetch
        }

        // Parse Buckets
        if (allocation_achievedInsight.TryGetProperty("buckets", out var buckets))
        {
            foreach (var bucket in buckets.EnumerateArray())
            {
                allocationAchievedInsightOutput.Add(ParseBucketData(bucket));
            }
        }

        return true;
    }




}

/// <summary>
/// 
/// </summary>
public class GetAllocationAchievedSummaryParams : DtoBridge
{

}
