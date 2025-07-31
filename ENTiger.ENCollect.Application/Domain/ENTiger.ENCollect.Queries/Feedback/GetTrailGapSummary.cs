namespace ENTiger.ENCollect.FeedbackModule;

/// <summary>
/// Fetches Trail gap summary data from Elasticsearch
/// </summary>
public class GetTrailGapSummary : FlexiQueryEnumerableBridgeAsync<GetTrailGapSummaryDto>
{
    
    protected readonly ILogger<GetTrailGapSummary> _logger;
    protected GetTrailGapSummaryParams _params;
    protected readonly RepoFactory _repoFactory;
    protected FlexAppContextBridge? _flexAppContext;
    public IElasticSearchService _elasticSearchService { get; set; }
    private readonly ElasticSearchIndexSettings _elasticIndexConfig;

    List<GetTrailGapSummaryDto> trailgapinsightoutput = new List<GetTrailGapSummaryDto>();
    long bucketInterval;

    /// <summary>
    /// Constructor to initialize dependencies
    /// </summary>
    /// <param name="logger"></param>
    public GetTrailGapSummary(ILogger<GetTrailGapSummary> logger, RepoFactory repoFactory, IElasticSearchService elasticSearchService, IOptions<ElasticSearchIndexSettings> ElasticSearchIndexSettings)
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
    /// Assigns input parameters, for this api there is no input parameters
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    public virtual GetTrailGapSummary AssignParameters(GetTrailGapSummaryParams @params)
    {
        _params = @params;
        return this;
    }

    /// <summary>
    /// Fetches 
    /// </summary>
    /// <returns></returns>
    public override async Task<IEnumerable<GetTrailGapSummaryDto>> Fetch()
    {
        _flexAppContext = _params.GetAppContext();
        Guard.AgainstNullAndEmpty("_flexAppContext?.TenantId", _flexAppContext?.TenantId);
        _repoFactory.Init(_params);

        var config = _elasticIndexConfig.IndexNames.Where(a => a.TenantId == _flexAppContext?.TenantId).FirstOrDefault();
        bucketInterval = config?.BucketInterval ?? 10000;
        Guard.AgainstNull(nameof(config), config);

        string path = config.TrailGapInsightIndex;
        var afterKey = new GetTrailGapSummaryDto();
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;

        // Process trail gap allocation insight data
        await FetchDataRecursive(year, month, afterKey, path);


        return trailgapinsightoutput;
    }


    /// <summary>
    /// Recursive method to fetch data 
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="afterKey"></param>
    /// <param name="path"></param>
    private async Task FetchDataRecursive(int year, int month, GetTrailGapSummaryDto afterKey, string path)
    {
        try
        {
            // Build DSL Query
            string dslQuery = BuildDSLQueryForTrailGapInsightSummary(year, month, afterKey);

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
    /// Constructs dsl query of trail gap summary
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="afterKey"></param>
    /// <returns></returns>
    private string BuildDSLQueryForTrailGapInsightSummary(int year, int month, GetTrailGapSummaryDto afterKey)
    {

        Guard.AgainstNegativeAndZero(nameof(year), year);
        Guard.AgainstNegativeAndZero(nameof(month), month);
        Guard.AgainstNull(nameof(afterKey), afterKey);

        string afterkeyjson = string.Empty;

        if (afterKey != null && afterKey.ProductGroup != null)
        {
            afterkeyjson = $@"
                               ,""after"": {{
                                ""productgroup_loanaccounts"": ""{afterKey.ProductGroup}"",
                                ""product_loanaccounts"": ""{afterKey.Product}"",
                                ""subproduct_loanaccounts"":  ""{afterKey.SubProduct}"",
                                ""current_bucket_loanaccounts"":  ""{afterKey.Current_Bucket}"",
                                ""bucket_loanaccounts"":  ""{afterKey.Bom_Bucket}"",
                                ""region_loanaccounts"": ""{afterKey.Region}"",
                                ""state_loanaccounts"":  ""{afterKey.State}"",
                                ""city_loanaccounts"":  ""{afterKey.City}"",
                                ""branch_loanaccounts"": ""{afterKey.Branch}"",
                                ""agency_applicationorg_firstname"": ""{afterKey.Field_Agency_Name}"",
                                ""agency_applicationorg_id"": ""{afterKey.Field_Agency_Id}"",
                                ""telecallingagency_applicationorg_firstname"":  ""{afterKey.Telecalling_Agency_Name}"",
                                ""telecallingagency_applicationorg_id"": ""{afterKey.Telecalling_Agency_Id}"",
                                ""collector_applicationuser_firstname"": ""{afterKey.Collector_Name}"",
                                ""collector_applicationuser_id"":  ""{afterKey.Collector_Id}"",
                                ""tellecaller_applicationuser_firstname"":  ""{afterKey.Telecaller_Name}"",
                                ""tellecaller_applicationuser_id"":  ""{afterKey.Telecaller_Id}"",
                                ""application_org_discriminator"":  ""{afterKey.Field_Discriminator}"",
                                ""fbak_dispositiongroup"":  ""{afterKey.Current_Trail_Group}"",
                                ""fbak_dispositioncode"":  ""{afterKey.Current_Trail_Code}"",
                                ""loan_bucket"":  ""{afterKey.Loan_Bucket}"",
                                ""feedback_status"":  ""{afterKey.Status}"",
                                ""fbak_totalfeedbacksthismonth"":  ""{afterKey.TrailCount}""
                                

                              }}

                               ";

        }

        string dslstring = $@"
            
            {{
              ""size"": 0,
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
                ""trail_gap_insight"": {{
                  ""composite"": {{
                    ""sources"": [
                      {{ ""productgroup_loanaccounts"": {{ ""terms"": {{ ""field"": ""productgroup_loanaccounts"" }} }} }},
                      {{ ""product_loanaccounts"": {{ ""terms"": {{ ""field"": ""product_loanaccounts"" }} }} }},
                      {{ ""subproduct_loanaccounts"": {{ ""terms"": {{ ""field"": ""subproduct_loanaccounts"" }} }} }},
                      {{ ""current_bucket_loanaccounts"": {{ ""terms"": {{ ""field"": ""current_bucket_loanaccounts"" }} }} }},
                      {{ ""bucket_loanaccounts"": {{ ""terms"": {{ ""field"": ""bucket_loanaccounts"" }} }} }},
                      {{ ""region_loanaccounts"": {{ ""terms"": {{ ""field"": ""region_loanaccounts"" }} }} }},
                      {{ ""state_loanaccounts"": {{ ""terms"": {{ ""field"": ""state_loanaccounts"" }} }} }},
                      {{ ""city_loanaccounts"": {{ ""terms"": {{ ""field"": ""city_loanaccounts"" }} }} }},
                      {{ ""branch_loanaccounts"": {{ ""terms"": {{ ""field"": ""branch_loanaccounts"" }} }} }},
                      {{ ""agency_applicationorg_firstname"": {{ ""terms"": {{ ""field"": ""agency_applicationorg_firstname"" }} }} }},
                      {{ ""agency_applicationorg_id"": {{ ""terms"": {{ ""field"": ""agency_applicationorg_id"" }} }} }},
                      {{ ""telecallingagency_applicationorg_firstname"": {{ ""terms"": {{ ""field"": ""telecallingagency_applicationorg_firstname"" }} }} }},
                      {{ ""telecallingagency_applicationorg_id"": {{ ""terms"": {{ ""field"": ""telecallingagency_applicationorg_id"" }} }} }},
                      {{ ""collector_applicationuser_firstname"": {{ ""terms"": {{ ""field"": ""collector_applicationuser_firstname"" }} }} }},
                      {{ ""collector_applicationuser_id"": {{ ""terms"": {{ ""field"": ""collector_applicationuser_id"" }} }} }},
                      {{ ""tellecaller_applicationuser_firstname"": {{ ""terms"": {{ ""field"": ""tellecaller_applicationuser_firstname"" }} }} }},
                      {{ ""tellecaller_applicationuser_id"": {{ ""terms"": {{ ""field"": ""tellecaller_applicationuser_id"" }} }} }},
                      {{ ""application_org_discriminator"": {{ ""terms"": {{ ""field"": ""application_org_discriminator"" }} }} }},
                      {{ ""fbak_dispositiongroup"": {{ ""terms"": {{ ""field"": ""fbak_dispositiongroup"" }} }} }},
                      {{ ""fbak_dispositioncode"": {{ ""terms"": {{ ""field"": ""fbak_dispositioncode"" }} }} }},
                      {{  ""loan_bucket"": {{  ""histogram"": {{  ""field"": ""total_overdue_amt"",""interval"": ""{bucketInterval}"" }} }} }},
                      {{ ""feedback_status"": {{ ""terms"": {{  ""field"": ""feedback_status"" }} }} }},
                      {{ ""fbak_totalfeedbacksthismonth"": {{ ""terms"": {{ ""field"": ""fbak_totalfeedbacksthismonth"" }} }} }}
                    ],
                    ""size"": 1000
                    {afterkeyjson}
                  }},
                  ""aggregations"":{{
                    ""unique_accounts_count"":{{""value_count"":{{""field"":""id_loanaccounts""}}}},
                    ""sum_of_total_overdue_amt"":{{""sum"":{{""field"":""total_overdue_amt""}}}}
                  }}
                }}
              }}
            }}
            ";

        return dslstring;


    }


    /// <summary>
    /// Parse response of elastic search and construct afterkey object if present in response
    /// </summary>
    /// <param name="afterKey"></param>
    /// <param name="response"></param>
    /// <returns></returns> 
    private bool ParseResponse(GetTrailGapSummaryDto afterKey, string response)
    {
        using var jsonDoc = JsonDocument.Parse(response);
        var root = jsonDoc.RootElement;

        if (!root.TryGetProperty("aggregations", out var aggregations) ||
            !aggregations.TryGetProperty("trail_gap_insight", out var trailgapInsight))
        {
            _logger.LogWarning("No 'aggregations' or 'primary_allocation_insight' found in Elasticsearch response.");
            return false;
        }

        // Handle Pagination
        if (trailgapInsight.TryGetProperty("after_key", out var afterKeyElement))
        {
            UpdateAfterKey(afterKey, afterKeyElement);
        }
        else
        {
            return false; // No more data to fetch
        }

        // Parse Buckets
        if (trailgapInsight.TryGetProperty("buckets", out var buckets))
        {
            foreach (var bucket in buckets.EnumerateArray())
            {
                trailgapinsightoutput.Add(ParseBucketData(bucket));
            }
        }

        return true;
    }


    /// <summary>
    /// Construct after object for next request
    /// </summary>
    /// <param name="afterKey"></param>
    /// <param name="afterKeyData"></param>
    private void UpdateAfterKey(GetTrailGapSummaryDto afterKey, JsonElement afterKeyData)
    {
        afterKey.ProductGroup = afterKeyData.GetProperty("productgroup_loanaccounts").GetString();
        afterKey.Product = afterKeyData.GetProperty("product_loanaccounts").ToString();
        afterKey.SubProduct = afterKeyData.GetProperty("subproduct_loanaccounts").ToString();
        afterKey.Bom_Bucket = afterKeyData.GetProperty("bucket_loanaccounts").ToString();
        afterKey.Current_Bucket = afterKeyData.GetProperty("current_bucket_loanaccounts").ToString();
        afterKey.Region = afterKeyData.GetProperty("region_loanaccounts").ToString();
        afterKey.State = afterKeyData.GetProperty("state_loanaccounts").ToString();
        afterKey.City = afterKeyData.GetProperty("city_loanaccounts").ToString();
        afterKey.Branch = afterKeyData.GetProperty("branch_loanaccounts").ToString();
        afterKey.Field_Agency_Name = afterKeyData.GetProperty("agency_applicationorg_firstname").ToString();
        afterKey.Field_Agency_Id = afterKeyData.GetProperty("agency_applicationorg_id").ToString();
        afterKey.Telecalling_Agency_Name = afterKeyData.GetProperty("telecallingagency_applicationorg_firstname").ToString();
        afterKey.Telecalling_Agency_Id = afterKeyData.GetProperty("telecallingagency_applicationorg_id").ToString();
        afterKey.Collector_Name = afterKeyData.GetProperty("collector_applicationuser_firstname").ToString();
        afterKey.Collector_Id = afterKeyData.GetProperty("collector_applicationuser_id").ToString();
        afterKey.Telecaller_Name = afterKeyData.GetProperty("tellecaller_applicationuser_firstname").ToString();
        afterKey.Telecaller_Id = afterKeyData.GetProperty("tellecaller_applicationuser_id").ToString();
        afterKey.Field_Discriminator = afterKeyData.GetProperty("application_org_discriminator").ToString();
        afterKey.Current_Trail_Group = afterKeyData.GetProperty("fbak_dispositiongroup").ToString();
        afterKey.Current_Trail_Code = afterKeyData.GetProperty("fbak_dispositioncode").ToString();
        afterKey.Loan_Bucket = afterKeyData.GetProperty("loan_bucket").ToString();
        afterKey.Status = afterKeyData.GetProperty("feedback_status").ToString();
        afterKey.TrailCount = afterKeyData.GetProperty("fbak_totalfeedbacksthismonth").ToString();

    }

    /// <summary>
    /// Method to parse response based on bucket
    /// </summary>
    /// <param name="bucket"></param>
    /// <returns></returns>
    private GetTrailGapSummaryDto ParseBucketData(JsonElement bucket)
    {

        return new GetTrailGapSummaryDto
        {
            ProductGroup = bucket.GetProperty("key").GetProperty("productgroup_loanaccounts").GetString(),
            Product = bucket.GetProperty("key").GetProperty("product_loanaccounts").ToString(),
            SubProduct = bucket.GetProperty("key").GetProperty("subproduct_loanaccounts").ToString(),
            Bom_Bucket = bucket.GetProperty("key").GetProperty("bucket_loanaccounts").ToString(),
            Current_Bucket = bucket.GetProperty("key").GetProperty("current_bucket_loanaccounts").ToString(),
            Region = bucket.GetProperty("key").GetProperty("region_loanaccounts").ToString(),
            State = bucket.GetProperty("key").GetProperty("state_loanaccounts").ToString(),
            City = bucket.GetProperty("key").GetProperty("city_loanaccounts").ToString(),
            Branch = bucket.GetProperty("key").GetProperty("branch_loanaccounts").ToString(),
            Field_Agency_Name = bucket.GetProperty("key").GetProperty("agency_applicationorg_firstname").ToString(),
            Field_Agency_Id = bucket.GetProperty("key").GetProperty("agency_applicationorg_id").ToString(),
            Telecalling_Agency_Name = bucket.GetProperty("key").GetProperty("telecallingagency_applicationorg_firstname").ToString(),
            Telecalling_Agency_Id = bucket.GetProperty("key").GetProperty("telecallingagency_applicationorg_id").ToString(),
            Collector_Name = bucket.GetProperty("key").GetProperty("collector_applicationuser_firstname").ToString(),
            Collector_Id = bucket.GetProperty("key").GetProperty("collector_applicationuser_id").ToString(),
            Telecaller_Name = bucket.GetProperty("key").GetProperty("tellecaller_applicationuser_firstname").ToString(),
            Telecaller_Id = bucket.GetProperty("key").GetProperty("tellecaller_applicationuser_id").ToString(),
            Field_Discriminator = bucket.GetProperty("key").GetProperty("application_org_discriminator").ToString(),
            Current_Trail_Group = bucket.GetProperty("key").GetProperty("fbak_dispositiongroup").ToString(),
            Current_Trail_Code = bucket.GetProperty("key").GetProperty("fbak_dispositioncode").ToString(),
            Loan_Bucket = bucket.GetProperty("key").GetProperty("loan_bucket").ToString(),
            Status = bucket.GetProperty("key").GetProperty("feedback_status").ToString(),
            TrailCount = bucket.GetProperty("key").GetProperty("fbak_totalfeedbacksthismonth").ToString(),

            Total_Accounts = bucket.GetProperty("unique_accounts_count").GetProperty("value").ToString(),
            Total_Overdue_Amount = bucket.GetProperty("sum_of_total_overdue_amt").GetProperty("value").ToString()


        };
    }



}

/// <summary>
/// 
/// </summary>
public class GetTrailGapSummaryParams : DtoBridge
{
    
}
